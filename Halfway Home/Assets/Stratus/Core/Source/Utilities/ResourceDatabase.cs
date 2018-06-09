using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;
using System.Linq;
using System.IO;

namespace Stratus
{
  /// <summary>
  /// An asset containing useful metadata for all assets placed in resource folders.
  /// (Original design by Bunny83)
  /// https://answers.unity.com/questions/1133078/recover-path-information-for-an-asset-but-at-runti.html
  /// </summary>
  [SingletonAsset("Assets", "Stratus Resource Database")]
  public class ResourceDatabase : SingletonAsset<ResourceDatabase>, ISerializationCallbackReceiver
  {
    public enum Type
    {
      Unknown = 0,
      Any = 0,
      Folder = 1,
      Asset = 2
    }

    /// <summary>
    /// Serialized metadata about an UnityObject embedded in the special Resources folder.
    /// </summary>
    public class ResourceMetaData
    {
      //--------------------------------------------------------------------/
      // Fields
      //--------------------------------------------------------------------/
      [SerializeField] private string _name;
      [SerializeField] private string _extension;
      [SerializeField] private string _path;
      [SerializeField] private Type _type = Type.Unknown;
      [SerializeField] private string _objectTypeName;
      private System.Type _objectType;
      private ResourceMetaData _parent;
      internal Dictionary<string, ResourceMetaData> children;

      //--------------------------------------------------------------------/
      // Properties
      //--------------------------------------------------------------------/
      public string name => _name;
      public string extension => _extension;
      public string path => _path;
      public string resourcesPath => string.IsNullOrEmpty(path) ? name : path + "/" + name;
      public Type type => _type;
      public ResourceMetaData parent => _parent;
      public string description => $"{name} ({_objectTypeName})";

      //--------------------------------------------------------------------/
      // CTOR
      //--------------------------------------------------------------------/
      public ResourceMetaData()
      {
        if (_type == Type.Folder)
          children = new Dictionary<string, ResourceMetaData>();
      }

      public ResourceMetaData(string fileName, string path, Type type, string objectType)
      {
        int index = fileName.LastIndexOf(".");
        if (index > 0)
        {
          _name = fileName.Substring(0, index);
          _extension = fileName.Substring(index + 1);
        }
        else
        {
          _name = fileName;
          _extension = string.Empty;
        }

        _path = path;
        _type = type;
        _objectTypeName = objectType;
        _objectType = System.Type.GetType(_objectTypeName);

        if (_type == Type.Folder)
          children = new Dictionary<string, ResourceMetaData>();
      }

      //--------------------------------------------------------------------/
      // Methods
      //--------------------------------------------------------------------/
      public ResourceMetaData GetChild(string path, Type resourceType = Type.Any)
      {
        if (type != Type.Folder)
          return null;

        string p = path;
        int index = path.IndexOf('/');
        if (index > 0)
        {
          p = path.Substring(0, index);
          path = path.Substring(index + 1);
        }
        else
          path = "";

        ResourceMetaData item = null;
        if (!children.TryGetValue(p, out item) || item == null)
          return null;
        else if (path.Length > 0)
          return item.GetChild(path, resourceType);
        else if (resourceType != Type.Unknown && item.type != resourceType)
          return null;

        return item;
      }

      public IEnumerable<ResourceMetaData> GetChildren(string name, Type resourceType, bool includeSubFolders = false, System.Type assetType = null)
      {
        // Assets don't have children
        if (type != Type.Asset)
          yield break;

        bool checkName = !string.IsNullOrEmpty(name);
        bool checkType = assetType != null;
        var items = children.Values;
        foreach(var item in items)
        {
          if (resourceType != Type.Any && item.type != resourceType)
            continue;
          if (checkName && name != item.name)
            continue;
          if (checkType && !assetType.IsAssignableFrom(item._objectType))
            continue;

          yield return item;
        }

        if (includeSubFolders)
        {
          foreach(var folder in items.Where(i => i.type == Type.Folder))
          {
            foreach (var item in folder.GetChildren(name, resourceType, includeSubFolders, assetType))
              yield return item;
          }
        }
      }

      public T Load<T>() where T : UnityEngine.Object
      {
        return Resources.Load<T>(resourcesPath);
      }

      internal void OnDeserialize()
      {
        if (string.IsNullOrEmpty(path))
          _parent = ResourceDatabase.get.root;
        else
          _parent = ResourceDatabase.GetFolder(path);
        if (_parent != null)
          parent.children.Add(name, this);
        if (type == Type.Folder)
        {
          children = new Dictionary<string, ResourceMetaData>();
        }
        _objectType = System.Type.GetType(_objectTypeName);
      }

    }

    //--------------------------------------------------------------------/
    // Fields
    //--------------------------------------------------------------------/
    public bool enabled = true;
    public bool automaticUpdate = false;
    [SerializeField, HideInInspector] internal List<ResourceMetaData> _items = new List<ResourceMetaData>();
    [SerializeField, HideInInspector] private int _fileCount = 0;
    [SerializeField, HideInInspector] private int _folderCount = 0;
    internal ResourceMetaData root = new ResourceMetaData("", "", Type.Folder, "");

    //--------------------------------------------------------------------/
    // Properties
    //--------------------------------------------------------------------/
    public int fileCount => _fileCount;
    public int folderCount => _folderCount;
    public List<ResourceMetaData> items => _items;

    //--------------------------------------------------------------------/
    // Methods: Static
    //--------------------------------------------------------------------/
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Stratus/Update Resource Database")]
    internal static void TriggerUpdate()
    {
      get.UpdateDatabase();
    }
#endif

    //--------------------------------------------------------------------/
    // Methods: Public
    //--------------------------------------------------------------------/
    public static ResourceMetaData GetAsset(string name, System.Type assetType = null)
      => get.root.GetChildren(name, Type.Asset, true, assetType).FirstOrDefault();
    public static ResourceMetaData GetFolder(string path) => get.root.GetChild(path, Type.Folder);
    public static IEnumerable<ResourceMetaData> GetAllAssets(string name, System.Type assetType = null) =>
      get.root.GetChildren(name, Type.Asset, true, assetType);
    public static IEnumerable<ResourceMetaData> GetAllAssets<T>(string name) => GetAllAssets(name, typeof(T));
    public static string ConvertPath(string path) => path.Replace("\\", "/");

    //--------------------------------------------------------------------/
    // Methods: Editor
    //--------------------------------------------------------------------/
#if UNITY_EDITOR
    void ScanFolder(DirectoryInfo folder, List<DirectoryInfo> resourceList, bool onlyTopFolders)
    {      
      string folderName = folder.Name.ToLower();
      if (folderName == "editor")
        return;
      else if (folderName == "resources")
      {
        resourceList.Add(folder);
        if (onlyTopFolders)
          return;
      }
      foreach(var directory in folder.GetDirectories())
      {
        ScanFolder(directory, resourceList, onlyTopFolders);
      }        
    }

    List<DirectoryInfo> FindResourcesFolders(bool onlyTopFolders)
    {
      var assets = new DirectoryInfo(Application.dataPath);
      var list = new List<DirectoryInfo>();
      ScanFolder(assets, list, onlyTopFolders);
      return list;
    }

    private void AddFileList(DirectoryInfo resourceFolder, int prefix)
    {
      string resourceFolderPath = resourceFolder.FullName;
      if (resourceFolderPath.Length < prefix)
        resourceFolderPath = "";
      else
        resourceFolderPath = resourceFolderPath.Substring(prefix);
      resourceFolderPath = ConvertPath(resourceFolderPath);
      
      // Add directories, recurse
      foreach(var folder in resourceFolder.GetDirectories())
      {
        _items.Add(new ResourceMetaData(folder.Name, resourceFolderPath, Type.Folder, ""));
        AddFileList(folder, prefix);
      }

      // Add files
      foreach(var file in resourceFolder.GetFiles())
      {
        string extension = file.Extension.ToLower();
        if (extension == ".meta")
          continue;

        string assetPath = "assets/" + file.FullName.Substring(Application.dataPath.Length + 1);
        assetPath = ConvertPath(assetPath);

        UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));
        if (obj == null)
        {
          Trace.Error($"File at path {assetPath} could not be loaded and is ignored. Probably not an asset?");
          continue;
        }

        string type = obj.GetType().AssemblyQualifiedName;
        _items.Add(new ResourceMetaData(file.Name, resourceFolderPath, Type.Asset, type));
      }

      // Now unload
      Resources.UnloadUnusedAssets();
    }
    
    public void UpdateDatabase(bool setDirty = false)
    {
      _items.Clear();
      root.children.Clear();
      List<DirectoryInfo> topFolders = FindResourcesFolders(true);
      foreach(var folder in topFolders)
      {
        string path = folder.FullName;
        int prefix = path.Length;
        if (!path.EndsWith("/"))
          prefix++;
        AddFileList(folder, prefix);
      }

      _folderCount = _fileCount = 0;
      foreach(var item in _items)
      {
        if (item.type == Type.Folder)
          _folderCount++;
        else if (item.type == Type.Asset)
          _fileCount++;
      }

      if (setDirty)
      {
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
      }

      Trace.Script($"Updated! Folders = {folderCount}, Items = {fileCount}");
    }
#endif

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
      if (_items == null || _items.Count == 0)
        UpdateDatabase(); 
#endif
    }

    public void OnAfterDeserialize()
    {
      root.children.Clear();
      foreach(var item in _items)
      {
        item?.OnDeserialize();
      }
    }


  }

#if UNITY_EDITOR
  public class ResourceDatabasePostProcessor : UnityEditor.AssetPostprocessor
  {
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
      if (!ResourceDatabase.instantiated)
        return;

      if (!ResourceDatabase.get.automaticUpdate)
        return;

      var files = importedAssets.Concat(deletedAssets).Concat(movedAssets).Concat(movedFromAssetPaths);
      bool update = false;
      foreach (var file in files)
      {
        var fn = file.ToLower();
        if (!fn.Contains("resourcedb.asset") && fn.Contains("/resources/"))
        {
          update = true;
          break;
        }
      }
      if (update)
      {
        ResourceDatabase.get.UpdateDatabase();
      }
    }
  } 
#endif



}