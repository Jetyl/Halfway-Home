/******************************************************************************/
/*!
@file   ExportWindow.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using UnityEditor;
using Stratus.Utilities;

namespace Stratus
{
  public class ExportWindow : EditorWindow
  {
    [MenuItem("Stratus/Export")]
    private static void Open()
    {
      Export();          
    }

    private void OnGUI()
    {
      
    }

    private static void Export()
    {
      var location = Assets.GetFolderPath("Stratus");
      AssetDatabase.ExportPackage(location, "Assets/StratusFramework.unitypackage", 
        ExportPackageOptions.Recurse | ExportPackageOptions.Default | 
        ExportPackageOptions.Interactive);
      Trace.Script("Exported");
    }
  }

}