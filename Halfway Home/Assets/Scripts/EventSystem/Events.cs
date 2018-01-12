/*******************************************************************************
filename    Events.cs
author      Jesse Lozano

Brief Description:
This is the string holder for the Events system. that way, for the user to input
what type of event they want when connecting or dispatching, they just type Events.
and they'll get the string the code will be looking for.

All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Events
{

    public static readonly String DefaultEvent = "DefaultEvent";
    public static readonly String KeyboardEvent = "KeyboardEvent";
    public static readonly String MouseUp = "MouseUp";
    public static readonly String MouseDown = "MouseDown";
    public static readonly String MouseEnter = "MouseEnter";
    public static readonly String MouseExit = "MouseExit";
    public static readonly String MouseDrag = "MouseDrag";
    public static readonly String MouseOver = "MouseOver";

    public static readonly String Create = "CreateEvent";
    public static readonly String Initialize = "InitializeEvent";
    public static readonly String LogicUpdate = "LogicUpdate";
    public static readonly String Destroy = "DestroyEvent";


    public static readonly String Fade = "FadeEvent";
    public static readonly String FinishedFade = "FinishedFadeEvent";
    public static readonly String Transition = "TransitionEvent";
    public static readonly String FinishedTransition = "FinishedTransitionEvent";
    public static readonly String Rotate = "RotateEvent";
    public static readonly String FinishedRotate = "FinishedRotateEvent";
    public static readonly String Scale = "ScaleEvent";
    public static readonly String FinishedScale = "FinishedScaleEvent";
    public static readonly String Translate = "TranslateEvent";
    public static readonly String FinishedTranslate = "FinishedTranslateEvent";

    public static readonly String CloseLoadMenu = "CloseLoadMenuEvent";
    public static readonly String CloseOptionsMenu = "CloseOptionsMenuEvent";
    public static readonly String Pause = "PauseEvent";
    public static readonly String UnPause = "UnPauseEvent";
    public static readonly String ReturnToMainMenu = "ReturnToMainMenuEvent";
    public static readonly String QuitGame = "QuitGameEvent";

    public static readonly String Save = "SaveEvent";
    public static readonly String Load = "LoadEvent";


    //Halfway-Home Custom Events

    public static readonly String StartGame = "StartGameEvent";
    public static readonly String EndGame = "EndGameEvent";
    public static readonly String MapChoiceMade = "MapChoiceMadeEvent";
    public static readonly String MapChoiceConfirmed = "MapChoiceConfirmedEvent";
    public static readonly String LeaveMap = "LeaveMapEvent";
    public static readonly String ReturnToMap = "ReturnToMapEvent";
    public static readonly String NewStory = "NewStoryEvent";
    public static readonly String ResumeStory = "ResumeStoryEvent";
    public static readonly String FinishedStory = "FinishedStoryEvent";
    public static readonly String MapIcon = "MapIconEvent";
    public static readonly String SleepIcon = "SleepIconEvent";
    public static readonly String AwakeIcon = "AwakeIconEvent";
    public static readonly String CharacterCall = "CharacterCallEvent";
    public static readonly String CharacterExit = "CharacterExitEvent";
    public static readonly String Backdrop = "BackdropEvent";
    public static readonly String MoveCharacter = "MoveCharacterEvent";
    public static readonly String NewDay = "NewDayEvent";
    public static readonly String GetPlayerInfo = "PlayerInfoEvent";
    public static readonly String GetPlayerInfoFinished = "FinishedPlayerInfoEvent";
    public static readonly String SetPlayerIdentity = "PlayerIdentityEvent";
    //description system
    public static readonly String AutoType = "AutoTypeEvent";
    public static readonly String FinishedAutoType = "FinishedAutoTypeEvent";
    public static readonly String PrintLine = "PrintLineEvent";
    public static readonly String SkipTyping = "SkipTypingEvent";
    public static readonly String StopSkipTyping = "StopSkipTypingEvent";
    public static readonly String Description = "DescriptionEvent";
    public static readonly String FinishedDescription = "FinishedDescriptionEvent";
    public static readonly String CloseDescription = "CloseDescriptionEvent";
    public static readonly String DescriptionClosed = "DescriptionClosedEvent";
    public static readonly String OpenHistory = "OpenHistoryEvent";
    public static readonly String CloseHistory = "CloseHistoryEvent";

    //choice system
    public static readonly String Choice = "ChoiceEvent";
    public static readonly String ChoiceMade = "ChoiceMadeEvent";

    
    
    //conversation system
    public static readonly String StartConversation = "StartConversationEvent";
    public static readonly String ConversationChoice = "ConversationChoiceEvent";
    public static readonly String NextConversationNode = "ConversationNodeEvent";
    public static readonly String EndConversation = "EndConversationEvent";
    public static readonly String ConversationCheckpoint = "ConversationCheckpointEvent";
    public static readonly String Counter = "PossibleCounterEvent";
    public static readonly String Interupt = "AttemptedInteruptionEvent";
    public static readonly String InteruptLost = "LostSequeEvent";

   

    //other ui events
    public static readonly String OpenUI = "OpenUIEvent";
    public static readonly String CloseUI = "CloseUIEvent";
    public static readonly String BlackScreenOn = "BlackScreenOnEvent";
    public static readonly String BlackScreenOff = "BlackScreenOffEvent";
    public static readonly String Investigate = "InvestigateEvent";
    public static readonly String OpenImage = "OpenImageEvent";
    public static readonly String CloseImage = "CloseImageEvent";
    public static readonly String ScreenTint = "ScreenTintEvent";
    public static readonly String Tooltip = "TooltipEvent";
    public static readonly String AddStat = "AddStatEvent";
    public static readonly String StatChange = "StatChangeEvent";
    public static readonly String TimeChange = "TimeChangeEvent";

    //a null event. no one should listen to this
    public static readonly String Null = "NullEvent";

    //Nonstatic
    public string EventName = DefaultEvent;
    
    public Events() { }
    public Events(string eventName)
    {
        EventName = eventName;
    }


    public static implicit operator string(Events value)
    {

        return value.EventName;
    }

    public static implicit operator Events(string value)
    {
        return new Events(value);
    }

    public static bool operator ==(Events x, Events y)
    {
        if (x.EventName == y.EventName) return true;
        return false;
    }

    public static bool operator !=(Events x, Events y)
    {
        if (x.EventName != y.EventName) return true;
        return false;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;
        Events c = obj as Events;
        if ((System.Object)c == null)
            return false;
        return EventName == c.EventName;
    }
    public bool Equals(Events c)
    {
        if ((object)c == null)
            return false;
        return c.EventName == EventName;
    }

    public override int GetHashCode()
    {
        return this.EventName.GetHashCode();
    }

}


#if UNITY_EDITOR
namespace CustomInspector
{
    using UnityEditor;

    public static class InspectorValues
    {
        public static readonly float LabelWidth = 120;
        public static readonly float MinRectWidth = 340;
        //How fast the property scales with the width of the window.
        public static readonly float WidthScaler = 2.21f;
    }


    [CustomPropertyDrawer(typeof(Events))]
    public class EventPropertyDrawer : PropertyDrawer
    {
        static string[] EventNames;
        static string[] EventValues;

        bool AsString = false;

        float ToggleWidth = 70;

        //this is adding all of our events to a list in a way the editor will be able to read
        static EventPropertyDrawer()
        {
            List<string> eventNames = new List<string>();
            List<string> eventValues = new List<string>();

            var list = typeof(Events).GetFields();

            foreach(var element in list)
            {
                if(element.IsStatic && element.FieldType == typeof(string))
                {
                    eventValues.Add(element.Name);
                    eventNames.Add(element.GetValue(element.FieldType) as string);
                }
            }

            EventNames = eventNames.ToArray();
            EventValues = eventValues.ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var refer = property.FindPropertyRelative("EventName");
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

            var prevAsString = AsString;
            AsString = EditorGUI.ToggleLeft(toggleRect, "AsString", AsString);

            //whether we are in toggle mode, or not
            if (!AsString && EventNames.Length != 0)
            {
                //getting positions in array for pop-up
                var index = Array.IndexOf(EventNames, refer.stringValue);

                if (index == -1)
                {
                    if (AsString == prevAsString)
                    {
                        refer.stringValue = EditorGUI.TextField(eventRect, refer.stringValue);
                        AsString = true;
                        return;
                    }
                    
                    index = 0;
                }

                refer.stringValue = EventNames[EditorGUI.Popup(eventRect, "", index, EventValues)];
            }
            else
            {
                refer.stringValue = EditorGUI.TextField(eventRect, refer.stringValue);
            }

        }
    }
}
#endif