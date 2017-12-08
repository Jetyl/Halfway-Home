/******************************************************************************/
/*!
@file   WatcherWindow.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Stratus
{
  /// <summary>
  /// Allows the user to monitor a specific object's properties at runtime
  /// </summary>
  public class WatcherWindow : EditorWindow
  {
    public MonoBehaviour component;

    //[MenuItem("Stratus/Tools/Watcher")]
    private static void Open()
    {
      EditorWindow.GetWindow(typeof(WatcherWindow), false, "Watcher");
    }

    //------------------------------------------------------------------------/
    // Messages
    //------------------------------------------------------------------------/
    private void OnGUI()
    {
      component = (MonoBehaviour)EditorGUILayout.ObjectField(component, typeof(MonoBehaviour), true);
    }

    void DisplayProperties()
    {

    }

    void DisplayFields()
    {

    }
    


  }

}