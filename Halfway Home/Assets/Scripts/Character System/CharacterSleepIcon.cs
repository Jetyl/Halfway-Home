/******************************************************************************/
/*!
File:   CharacterSleepIcon.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterSleepIcon : MonoBehaviour
{
    public CharacterList Character;

    Image manga;

	// Use this for initialization
	void Start ()
    {
        manga = GetComponent<Image>();
        var col = manga.color;
        col.a = 0;
        manga.color = col;

        Space.Connect<CharacterEvent>(Events.SleepIcon, OnSleep);
        Space.Connect<CharacterEvent>(Events.AwakeIcon, OnAwake);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnSleep(CharacterEvent eventdata)
    {
        if (eventdata.Person != Character.Character)
            return;

        var col = manga.color;
        col.a = 1;
        manga.color = col;

    }
    void OnAwake(CharacterEvent eventdata)
    {
        if (eventdata.Person != Character.Character)
            return;

        var col = manga.color;
        col.a = 0;
        manga.color = col;

    }

}

public class CharacterEvent:DefaultEvent
{
    public string Person;

    public CharacterEvent(string person)
    {
        Person = person;
    }
}


[Serializable]
public class CharacterList
{
    public string Character;
    public GameObject Actor;
}


#if UNITY_EDITOR
namespace CustomInspector
{
    using UnityEditor;
    using LitJson;

    [CustomPropertyDrawer(typeof(CharacterList))]
    public class CharacterListPropertyDrawer : PropertyDrawer
    {
        static string[] EventNames;
        
        float ToggleWidth = 70;

        //this is adding all of our events to a list in a way the editor will be able to read
        static CharacterListPropertyDrawer()
        {
            List<string> eventNames = new List<string>();

            var list = TextParser.ToJson("Characters");

            foreach (JsonData element in list)
            {

                eventNames.Add((string)element["Name"]);
                
            }

            EventNames = eventNames.ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var refer = property.FindPropertyRelative("Character");
            var labelRect = new Rect(position.x, position.y, InspectorValues.LabelWidth, position.height);
            EditorGUI.LabelField(labelRect, property.name);

            //this is defining the sizes of the rectangles for the editor
            var propStartPos = labelRect.position.x + labelRect.width;
            if (position.width > InspectorValues.MinRectWidth)
            {
                propStartPos += (position.width - InspectorValues.MinRectWidth) / InspectorValues.WidthScaler;
            }

            var toggleRect = new Rect(propStartPos, position.y, ToggleWidth, position.height);
            var eventRect = new Rect(toggleRect.position.x + toggleRect.width, position.y, position.width - (toggleRect.position.x + toggleRect.width) + 14, position.height);
            

            //whether we are in toggle mode, or not
            if (EventNames.Length != 0)
            {
                //getting positions in array for pop-up
                var index = Array.IndexOf(EventNames, refer.stringValue);

                if (index == -1)
                {
                    index = 0;
                }

                refer.stringValue = EventNames[EditorGUI.Popup(eventRect, "", index, EventNames)];
            }
            else
            {
                refer.stringValue = EditorGUI.TextField(eventRect, refer.stringValue);
            }

        }
    }
}
#endif