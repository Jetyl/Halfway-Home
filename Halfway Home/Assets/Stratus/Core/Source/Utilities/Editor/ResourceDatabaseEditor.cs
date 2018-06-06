using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Stratus
{
  [CustomEditor(typeof(ResourceDatabase))]
  public class ResourceDatabaseEditor : ScriptableEditor<ResourceDatabase>
  {
    private Vector2 scrollPosition;
    private int selectedIndex;

    protected override void OnStratusEditorEnable()
    {
      AddSection(ShowControls);
    }

    private void ShowControls(Rect rect)
    {
      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField($"Folders: {target.folderCount}");
        EditorGUILayout.LabelField($"Files: {target.fileCount}");
      }
      EditorGUILayout.EndHorizontal();

      if (GUILayout.Button("Update"))
        target.UpdateDatabase(true);

      scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
      for(int i = 0; i < target.items.Count; ++i)
      {
        ResourceDatabase.ResourceMetaData item = target.items[i];
        GUIContent content = new GUIContent($"{item.name}");
        if (EditorGUILayout.DropdownButton(content, FocusType.Passive))
        {
          selectedIndex = i;
        }

      }
      //foreach(var item in target.items)
      //{
      //}
      EditorGUILayout.EndScrollView();
    }

    //private void SelectItem()


  }

}