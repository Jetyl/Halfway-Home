/******************************************************************************/
/*!
File:   ProgressPoint.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System;
using System.Collections.Generic;
using LitJson;

[System.Serializable]
public class ProgressPoint
{

    // a class for interacting with the progress system
    
    
    public string ProgressName;
    public PointTypes TypeID;

    public bool BoolValue;
    public int IntValue;
    public float FloatValue;
    public string StringValue;

    public ValueCompare compare;

    public ProgressPoint()
    {
        ProgressName = "";
        StringValue = "";
    }
    public ProgressPoint(string eventName)
    {
        ProgressName = eventName;
        StringValue = "";
    }

    public ProgressPoint(string eventName, PointTypes type)
    {
        ProgressName = eventName;
        TypeID = type;
        StringValue = "";
    }

    public ProgressPoint(ProgressPoint copy_)
    {
        ProgressName = copy_.ProgressName;
        TypeID = copy_.TypeID;
        StringValue = copy_.StringValue;
        BoolValue = copy_.BoolValue;
        IntValue = copy_.IntValue;
        FloatValue = copy_.FloatValue;
        compare = copy_.compare;
    }

    /*
    public static implicit operator string(ProgressPoint value)
    {
        return value.ProgressName;
    }

    public static implicit operator ProgressPoint(string value)
    {
        return new ProgressPoint(value);
    }
    */

    public ProgressPoint(JsonData data)
    {
        ProgressName = (string)data["Name"];
        TypeID = (PointTypes)(int)data["Type"];


        switch (TypeID)
        {
            case PointTypes.Flag:
                BoolValue = (bool)data["MatchValue"];
                break;
            case PointTypes.Float:
                FloatValue = (float)(double)data["MatchValue"];
                compare = (ValueCompare)(int)data["Compare"];
                break;
            case PointTypes.Integer:
                IntValue = (int)data["MatchValue"];
                compare = (ValueCompare)(int)data["Compare"];
                break;
            case PointTypes.String:
                StringValue = (string)data["MatchValue"];
                break;
            default:
                break;
        }

    }



}

public enum PointTypes
{
    None,
    Flag,
    Integer,
    Float,
    String
}



#if UNITY_EDITOR
namespace CustomInspector
{
    using UnityEditor;


    [CustomPropertyDrawer(typeof(ProgressPoint))]
    public class ProgressPropertyDrawer : PropertyDrawer
    {
        
        float ToggleWidth = 70;

        //this is adding all of our events to a list in a way the editor will be able to read
        static ProgressPropertyDrawer()
        {
            
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            

            var refer = property.FindPropertyRelative("ProgressName");
            var refer2 = property.FindPropertyRelative("TypeID");

            var labelRect = new Rect(position.x, position.y, InspectorValues.LabelWidth, position.height);
            EditorGUI.LabelField(labelRect, property.name);
            

            //this is defining the sizes of the rectangles for the editor
            var propStartPos = labelRect.position.x + labelRect.width;
            if (position.width > InspectorValues.MinRectWidth)
            {
                propStartPos += (position.width - InspectorValues.MinRectWidth) / InspectorValues.WidthScaler;
            }

            var toggleRect = new Rect(propStartPos, position.y, ToggleWidth, EditorGUIUtility.singleLineHeight);
            var eventRect = new Rect(toggleRect.position.x + toggleRect.width, position.y, position.width - (toggleRect.position.x + toggleRect.width) + 14, EditorGUIUtility.singleLineHeight);
            //var enumRect = new Rect(propStartPos, position.y, 40, EditorGUIUtility.singleLineHeight);
            
            refer.stringValue = EditorGUI.TextField(eventRect, refer.stringValue);
            PointTypes ty = (PointTypes)EditorGUI.EnumPopup(toggleRect, (PointTypes)refer2.enumValueIndex);
            refer2.enumValueIndex = (int)ty;
            //ref2.boolValue = EditorGUI.ToggleLeft(toggleRect, "Value", ref2.boolValue);

            switch (ty)
            {
                case PointTypes.Flag:
                    var refboo = property.FindPropertyRelative("BoolValue");
                    toggleRect.y += EditorGUIUtility.singleLineHeight + 2;
                    refboo.boolValue = EditorGUI.ToggleLeft(toggleRect, "Value", refboo.boolValue);
                    break;
                case PointTypes.Float:
                    var refflo = property.FindPropertyRelative("FloatValue");

                    eventRect.y += EditorGUIUtility.singleLineHeight + 2;
                    refflo.floatValue = EditorGUI.FloatField(eventRect, refflo.floatValue);

                    var compare = property.FindPropertyRelative("compare");
                    toggleRect.y += EditorGUIUtility.singleLineHeight + 2;
                    ValueCompare com = (ValueCompare)EditorGUI.EnumPopup(toggleRect, (ValueCompare)compare.enumValueIndex);
                    compare.enumValueIndex = (int)com;
                    break;
                case PointTypes.Integer:
                    var refint = property.FindPropertyRelative("IntValue");

                    eventRect.y += EditorGUIUtility.singleLineHeight + 2;
                    refint.intValue = EditorGUI.IntField(eventRect, refint.intValue);

                    var compare2 = property.FindPropertyRelative("compare");
                    toggleRect.y += EditorGUIUtility.singleLineHeight + 2;
                    ValueCompare com2 = (ValueCompare)EditorGUI.EnumPopup(toggleRect, (ValueCompare)compare2.enumValueIndex);
                    compare2.enumValueIndex = (int)com2;
                    break;
                case PointTypes.String:
                    var refst = property.FindPropertyRelative("StringValue");
                    eventRect.y += EditorGUIUtility.singleLineHeight + 2;
                    refst.stringValue = EditorGUI.TextField(eventRect, refst.stringValue);
                    break;
                default:
                    break;
            }


            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 35.0f;
        }


    }
}
#endif