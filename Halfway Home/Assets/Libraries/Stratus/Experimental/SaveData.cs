using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Stratus.Utilities;
using System.IO;

namespace Stratus
{
  /// <summary>
  /// An abstract class for handling runtime save-data. Useful for player profiles, etc...
  /// </summary>
  [Serializable]
  public abstract class SaveData
  {
    //--------------------------------------------------------------------------------------------/
    // Classes
    //--------------------------------------------------------------------------------------------/
    /// <summary>
    /// Specifies what suffixes to add to the save file name
    /// </summary>
    public enum SuffixFormat
    {
      Incremental,
      SystemTime
    }

    //--------------------------------------------------------------------------------------------/
    // Attributes
    //--------------------------------------------------------------------------------------------/
    /// <summary>
    /// A required attribute that specifies the wanted folder path and name for a savedata asset
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class SaveDataAttribute : Attribute
    {
      /// <summary>
      /// The folder relative to the application's persistent data path where you want this data stored
      /// </summary>
      public string folder { get; set; }
      /// <summary>
      /// What naming convention to use for a save file of this type
      /// </summary>
      public string namingConvention { get; set; } // = typeof(SaveData).DeclaringType.Name;
      /// <summary>
      /// What suffix to use for a save file of this type
      /// </summary>
      public SuffixFormat suffix { get; set; } = SuffixFormat.Incremental;
      /// <summary>
      /// What extension format to use for a save file of this type
      /// </summary>
      public string extension { get; set; } = ".sav";

      public class MissingException : Exception
      {
        public MissingException(string className) : base("The class declaration for " + className + " is missing the [SaveData] attribute, which provides the path information needed in order to construct the asset.")
        {
          // Fill later?
          this.HelpLink = "http://msdn.microsoft.com";
          this.Source = "Exception_Class_Samples";
        }
      }
    }

    //--------------------------------------------------------------------------------------------/
    // Properties
    //--------------------------------------------------------------------------------------------/
    /// <summary>
    /// The JSON representation of this data
    /// </summary>
    public string json { get { return JsonUtility.ToJson(this, true); } }

    /// <summary>
    /// The character used to separate directories in Unity
    /// </summary>
    public static char DirectorySeparatorChar { get; } = '/';

    /// <summary>
    /// Whether this SaveData has been saved to disk
    /// </summary>
    public bool isSaved { get; protected set; } = false;

    //--------------------------------------------------------------------------------------------/
    // Fields
    //--------------------------------------------------------------------------------------------/
    /// <summary>
    /// The time at which this save was made
    /// </summary>
    [HideInInspector]
    public string time;
  }

  /// <summary>
  /// An abstract class for handling runtime save-data. Useful for player profiles, etc...
  /// </summary>
  public abstract class SaveData<T> : SaveData
  {
    //--------------------------------------------------------------------------------------------/
    // Properties
    //--------------------------------------------------------------------------------------------/
    /// <summary>
    /// The folder inside the relative path to this asset
    /// </summary>
    public static string namingConvention { get; } = attribute.GetProperty<string>("namingConvention");

    /// <summary>
    /// The folder inside the relative path to this asset
    /// </summary>
    public static string folder { get; } = attribute.GetProperty<string>("folder");

    /// <summary>
    /// The extension used by this save data
    /// </summary>
    public static string extension { get; } = attribute.GetProperty<string>("extension");

    /// <summary>
    /// The extension used by this save data
    /// </summary>
    public static SuffixFormat suffix { get; } = attribute.GetProperty<SuffixFormat>("suffix");

    /// <summary>
    /// The persistent data path that Unity is using
    /// </summary>
    public static string relativePath => Application.persistentDataPath;

    /// <summary>
    /// The path to the directory being used by this save data
    /// </summary>
    public static string path
    {
      get
      {
        var path = relativePath;
        if (folder != null)
          path += DirectorySeparatorChar + folder;
        path += DirectorySeparatorChar;
        return path;
      }
    }

    /// <summary>
    /// The attribute containing data about this class
    /// </summary>
    private static SaveDataAttribute attribute //; { get; } = AttributeUtility.FindAttribute<SaveDataAttribute>(typeof(T));
    {
      get
      {
        if (attribute_ == null)
        {
          var type = typeof(T);
          attribute_ = AttributeUtility.FindAttribute<SaveDataAttribute>(type);
          if (attribute_ == null)
            throw new SaveDataAttribute.MissingException(type.Name);
        }
        return attribute_;
      }
    }

    /// <summary>
    /// The number of save files present in the specified folder for save data
    /// </summary>
    public static int count => files != null ? files.Length : 0;

    /// <summary>
    /// Returns all instances of the save data from the path
    /// </summary>
    public static string[] files
    {
      get
      {
        // If the directory does not exist yet..
        if (!Directory.Exists(path))
          return null;

        // Look at the files matching the extension in the given folder
        var saves = new List<string>();
        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
          string fileExtension = Path.GetExtension(file);
          if (fileExtension == extension)
            saves.Add(file);
        }

        if (saves.Count > 0)
          return saves.ToArray();
        return null;
      }
    }

    private static SaveDataAttribute attribute_;

    //--------------------------------------------------------------------------------------------/
    // Methods
    //--------------------------------------------------------------------------------------------/
    /// <summary>
    /// Saves the data to the specified folder in the application's persistent path
    /// using the specified filename
    /// </summary>
    public void Save(string name)
    {
      // Update the time to save at
      this.time = DateTime.Now.ToString();
      // Now write to the file
      var fileName = name + extension;
      var fullPath = path + fileName;
      Trace.Script("Saving to " + fullPath);
      File.WriteAllText(fullPath, json);
      // Note that it has been saved
      isSaved = true;
    }

    /// <summary>
    /// Composes a default save data name
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    private static string ComposeName(SuffixFormat format)
    {
      string name = namingConvention;
      switch (format)
      {
        case SuffixFormat.Incremental:
          name += "_" + count;
          break;
        default:
          break;
      }
      return name;
    }

    /// <summary>
    /// Saves the data to the specified folder in the application's persistent path
    /// using the default naming convention
    /// </summary>
    public static void Save(SaveData<T> saveData)
    {
      Save(saveData, ComposeName(suffix));
    }


    /// <summary>
    /// Saves the data to the specified folder in the application's persistent path
    /// using the specified filename
    /// </summary>
    public static void Save(SaveData<T> saveData, string name)
    {
      // Update the time to save at
      saveData.time = DateTime.Now.ToString();
      // Now write to the file
      var fileName = name + extension;
      var fullPath = path + fileName;
      Trace.Script("Saving to " + fullPath);
      File.WriteAllText(fullPath, saveData.json);
      // Note that it has been saved
      saveData.isSaved = true;
    }

    /// <summary>
    /// Loads a save data file from the specified path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T Load(string name)
    {
      var fileName = name + extension;
      var fullPath = path + fileName;
      if (!File.Exists(fullPath))
        throw new FileNotFoundException("The file was not found!");

      string data = File.ReadAllText(fullPath);
      T saveData = JsonUtility.FromJson<T>(data);
      return saveData;
    }




  }

}