using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;
using TMPro;

public class DynamicMapDescriptor : MonoBehaviour
{
  
  public TextMeshProUGUI RoomText;
  public TextMeshProUGUI EffectText;
  public List<RoomDescriptor> Descriptors;
    
	void Start ()
  {
    Scene.Connect<HoverOverRoomEvent>(OnHoverOverRoomEvent);
	}

  void OnHoverOverRoomEvent(HoverOverRoomEvent e)
  {
        RoomText.text = GetRoomName(e.Place);
        if (e.Open)
            EffectText.text = GetDescriptor(e.Place);
        else
        {
            EffectText.text = e.AccessRules.GetToolTipInfo();
        }

  }
    

    string GetDescriptor(Room r)
    {
        foreach(var rooms in Descriptors)
        {
            if (rooms.Room == r)
                return rooms.GetText().DescriptorText;
        }

        return "";
    }

    string GetRoomName(Room r)
    {
        foreach (var rooms in Descriptors)
        {
            if (rooms.Room == r)
                return rooms.name;
        }

        return "";
    }


    // Update is called once per frame
    void Update ()
  {
		
	}
}

public class HoverOverRoomEvent : Stratus.Event
{
    public Room Place;
    public HalfwayHome.MapAccessTime AccessRules;
    public bool Open;

    public HoverOverRoomEvent(Room place, HalfwayHome.MapAccessTime accessRules, bool open)
    {
        Place = place;
        AccessRules = accessRules;
        Open = open;
    }
}

[System.Serializable]
public class Conditional
{
    public ProgressType Type;
    public ProgressPoint Condition;
    public Personality.Social Social;
    public Personality.Wellbeing Wellbeing;
    [Range(0, 5)]
    public int SocCompare;
    [Range(0, 100)]
    public int WellCompare;
}

[System.Serializable]
public class DescriptorCondition
{
    public List<Conditional> Conditions;
    public string DescriptorText;

    public bool ConditionsMet()
    {
        foreach(var condition in Conditions)
        {
            switch(condition.Type)
            {
                case ProgressType.ProgressPoint:
                    if (Game.current.Progress.CheckProgress(condition.Condition) == false)
                        return false;
                    break;
                case ProgressType.Socials:
                    if (Game.current.Self.GetTrueSocialStat(condition.Social) < condition.SocCompare)
                        return false;
                    break;
                case ProgressType.Wellbeing:
                    if (Game.current.Self.GetWellbingStat(condition.Wellbeing) < condition.WellCompare)
                        return false;
                    break;
            }
            
        }

        return true;
    }

}

[System.Serializable]
public class RoomDescriptor
{
    public string name;
    public Room Room;
    public DescriptorCondition DefaultCondition;
    public List<DescriptorCondition> Conditions;

    public DescriptorCondition GetText()
    {
        foreach (var point in Conditions)
        {
            if (point.ConditionsMet())
            {
                return point;
            }
        }

        return DefaultCondition;
    }

}


#if UNITY_EDITOR
namespace CustomInspector
{
    using UnityEditor;


    [CustomPropertyDrawer(typeof(Conditional))]
    public class ConditionalDrawer : PropertyDrawer
    {

        float ToggleWidth = 70;

        //this is adding all of our events to a list in a way the editor will be able to read
        static ConditionalDrawer()
        {

        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            
            var refer2 = property.FindPropertyRelative("Type");

            var labelRect = new Rect(position.x, position.y, InspectorValues.LabelWidth, position.height);
            EditorGUI.LabelField(labelRect, property.name);

            var rectIWant = new Rect(position.x, position.y, position.width, position.height);

            //this is defining the sizes of the rectangles for the editor
            var propStartPos = labelRect.position.x + labelRect.width;
            if (position.width > InspectorValues.MinRectWidth)
            {
                propStartPos += (position.width - InspectorValues.MinRectWidth) / InspectorValues.WidthScaler;
            }

            var toggleRect = new Rect(propStartPos, position.y, ToggleWidth, EditorGUIUtility.singleLineHeight);
            var eventRect = new Rect(toggleRect.position.x + toggleRect.width, position.y, position.width - (toggleRect.position.x + toggleRect.width) + 14, EditorGUIUtility.singleLineHeight);
            //var enumRect = new Rect(propStartPos, position.y, 40, EditorGUIUtility.singleLineHeight);
            
            ProgressType ty = (ProgressType)EditorGUI.EnumPopup(toggleRect, (ProgressType)refer2.enumValueIndex);
            refer2.enumValueIndex = (int)ty;
            //ref2.boolValue = EditorGUI.ToggleLeft(toggleRect, "Value", ref2.boolValue);
            //toggleRect.y += EditorGUIUtility.singleLineHeight + 2;

            switch (ty)
            {
                case ProgressType.ProgressPoint:
                    var refboo = property.FindPropertyRelative("Condition");
                    rectIWant.y += EditorGUIUtility.singleLineHeight + 2;
                    EditorGUI.PropertyField(rectIWant, refboo);
                    break;
                case ProgressType.Socials:
                    var refflo = property.FindPropertyRelative("Social");

                    rectIWant.y += EditorGUIUtility.singleLineHeight + 2;
                    EditorGUI.PropertyField(rectIWant, refflo);

                    var compare = property.FindPropertyRelative("SocCompare");
                    rectIWant.y += EditorGUIUtility.singleLineHeight + 2;
                    EditorGUI.PropertyField(rectIWant, compare);
                    break;
                case ProgressType.Wellbeing:
                    var reffwel = property.FindPropertyRelative("Wellbeing");

                    rectIWant.y += EditorGUIUtility.singleLineHeight + 2;
                    EditorGUI.PropertyField(rectIWant, reffwel);

                    var compare2 = property.FindPropertyRelative("WellCompare");
                    rectIWant.y += EditorGUIUtility.singleLineHeight + 2;
                    EditorGUI.PropertyField(rectIWant, compare2);
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