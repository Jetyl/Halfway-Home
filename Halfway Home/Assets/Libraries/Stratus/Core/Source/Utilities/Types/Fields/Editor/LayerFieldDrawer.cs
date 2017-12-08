/******************************************************************************/
/*!
@file   LayerFieldDrawer.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEditor;
using UnityEngine;

namespace Stratus
{
  namespace Types
  {
    [CustomPropertyDrawer(typeof(LayerField))]
    public class LayerFieldDrawer : PropertyDrawer
    {
      public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
      {
        SerializedProperty layer = property.FindPropertyRelative("layer");

        EditorGUI.BeginProperty(position, label, layer);
        layer.intValue = EditorGUI.LayerField(position, label, layer.intValue);
        EditorGUI.EndProperty();
      }

    }

  }
}
