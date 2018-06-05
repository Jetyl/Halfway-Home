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
  public class ResourceDatabase : SingletonAsset<ResourceDatabase>
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
        //if (string.IsNullOrEmpty(path))
        //  parent = ResourceDatabase.instance.
      }

    }

    //--------------------------------------------------------------------/
    // Fields
    //--------------------------------------------------------------------/
    public bool automaticUpdate = false;
    [SerializeField] internal List<ResourceMetaData> metadata = new List<ResourceMetaData>();
    [SerializeField, HideInInspector] private int _fileCount = 0;
    private int _folderCount = 0;
    internal ResourceMetaData root = new ResourceMetaData("", "", Type.Folder, "");

    //--------------------------------------------------------------------/
    // Properties
    //--------------------------------------------------------------------/
    public int fileCount => _fileCount;
    public int folderCount => _folderCount;

    //--------------------------------------------------------------------/
    // Methods: Static
    //--------------------------------------------------------------------/
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Stratus/Update Resource Database")]
    internal static void TriggerUpdate()
    {

    }
#endif

    //--------------------------------------------------------------------/
    // Methods: Public
    //--------------------------------------------------------------------/
    public static ResourceMetaData GetAsset(string name, System.Type assetType = null)
      => instance.root.GetChildren(name, Type.Asset, true, assetType).FirstOrDefault();
    public static ResourceMetaData GetFolder(string path) => instance.root.GetChild(path, Type.Folder);
    public static IEnumerable<ResourceMetaData> GetAllAssets(string name, System.Type assetType = null) =>
      instance.root.GetChildren(name, Type.Asset, true, assetType);
    public static IEnumerable<ResourceMetaData> GetAllAssets<T>(string name) => GetAllAssets(name, typeof(T));
    public static string ConvertPath(string path) => path.Replace("\\", "/");

    //--------------------------------------------------------------------/
    // Methods
    //--------------------------------------------------------------------/
    void ScanFolder(DirectoryInfo folder, List<DirectoryInfo> resourceList, bool onlyTopFolders)
    {
      string folderName = folder.Name.ToLower();
      if (folderName == "Editor")
        return;
      else if (folderName == "Resources")
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

    //List<DirectoryInfo> FindResourcesFolders(bool onlyTopFolders)
    //{
    //  var assets = new Directory(Application.dataPath);
    //  var list = new List<DirectoryInfo>();
    //}

    //[UnityEditor.MenuItem("Stratus/Generate Resource Database")]
    public static void Generate()
    {

    }

    public void Process()
    {

    }



  }

}