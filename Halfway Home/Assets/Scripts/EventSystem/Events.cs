/*******************************************************************************
filename    Events.cs
author      Jesse Lozano

Brief Description:
This is the string holder for the Events system. that way, for the user to input
what type of event they want when connecting or dispatching, they just type Events.
and they'll get the string the code will be looking for.

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


    //chris's events 
    public static readonly String TrackingCameraMove_Event = "TrackingCameraMove_Event";
    public static readonly String ImportantObjectAdd_Event = "ImportantObjectAdd_Event";
    public static readonly String ImportantObjectRemove_Event = "ImportantObjectRemove_Event";

    public static readonly String DisableCamera = "DisableCameraEvent";
    public static readonly String EnableCamera = "EnableCameraEvent";

    public static readonly String ZoomCamera = "ZoomCameraEvent";
    public static readonly String ResetZoomCamera = "ResetZoomCameraEvent";

    //a-0 custom events 

    //ovement related
    public static readonly String Walk = "WalkEvent";
    public static readonly String DisableMove = "DisableMoveEvent";
    public static readonly String EnableMove = "EnableMoveEvent";
    public static readonly String StopWalking = "StopWalkingEvent";

    //notice/inspect related
    public static readonly String Notice = "NoticeEvent";
    public static readonly String UnNoticed = "UnNoticeEvent";
    public static readonly String Inspect = "InspectEvent";

    //description system
    public static readonly String AutoType = "AutoTypeEvent";
    public static readonly String FinishedAutoType = "FinishedAutoTypeEvent";
    public static readonly String SkipTyping = "SkipTypingEvent";
    public static readonly String Description = "DescriptionEvent";
    public static readonly String FinishedDescription = "FinishedDescriptionEvent";
    public static readonly String CloseDescription = "CloseDescriptionEvent";
    public static readonly String DescriptionClosed = "DescriptionClosedEvent";

    //choice system
    public static readonly String Choice = "ChoiceEvent";
    public static readonly String ChoiceMade = "ChoiceMadeEvent";

    //room relevant
    public static readonly String MoveRoom = "MoveRoomEvent";
    public static readonly String FinishedMoveRoom = "FinishedMoveRoomEvent";
    public static readonly String DetectRoom = "DetectRoomEvent";

    //inventory system
    public static readonly String OpenInventory = "OpenInventoryEvent";
    public static readonly String CloseInventory = "CloseInventoryEvent";
    public static readonly String SetItem = "SetItemEvent";
    public static readonly String SelectItem = "SelectItemEvent";
    public static readonly String UseItem = "UseItemEvent";
    public static readonly String ExamineItem = "ExamineItemEvent";
    public static readonly String NameItem = "NameItemEvent";
    public static readonly String NameCombine = "NameCombineEvent";
    public static readonly String NewItem = "NewItemEvent";
    public static readonly String PuzzleSolved = "PuzzleSolvedEvent";

    //shought system
    public static readonly String UseIdea = "UseIdeaEvent";
    public static readonly String AddIdea = "AddIdeaEvent";
    public static readonly String ExamineIdea = "ExamineIdeaEvent";
    public static readonly String BeCreative = "CreativeEvent";
    public static readonly String CreativityLost = "UnCreativeEvent";
    public static readonly String OpenThoughts = "OpenThoughtsEvent";
    public static readonly String CloseThoughts = "CloseThoughtsEvent";
    public static readonly String ThoughtsOn = "ThoughtsOnEvent";
    public static readonly String ThoughtsOff = "ThoughtsOffEvent";

    
    //conversation system
    public static readonly String StartConversation = "StartConversationEvent";
    public static readonly String ConversationChoice = "ConversationChoiceEvent";
    public static readonly String NextConversationNode = "ConversationNodeEvent";
    public static readonly String EndConversation = "EndConversationEvent";
    public static readonly String ConversationCheckpoint = "ConversationCheckpointEvent";
    public static readonly String Counter = "PossibleCounterEvent";
    public static readonly String Interupt = "AttemptedInteruptionEvent";
    public static readonly String InteruptLost = "LostSequeEvent";

    //phone system
    public static readonly String TakeOutPhone = "TakeOutPhoneEvent";
    public static readonly String PutAwayPhone = "PutAwayPhoneEvent";
    public static readonly String PhoneOn = "PhoneOnEvent";
    public static readonly String PhoneOff = "PhoneOffEvent";
    public static readonly String Notification = "NotificationEvent";

    //other ui events
    public static readonly String OpenUI = "OpenUIEvent";
    public static readonly String CloseUI = "CloseUIEvent";
    public static readonly String BlackScreenOn = "BlackScreenOnEvent";
    public static readonly String BlackScreenOff = "BlackScreenOffEvent";
    public static readonly String Investigate = "InvestigateEvent";
    public static readonly String OpenImage = "OpenImageEvent";
    public static readonly String CloseImage = "CloseImageEvent";
    public static readonly String ScreenTint = "ScreenTintEvent";

    //other general events
    public static readonly String SpriteChange = "SpriteChangeEvent";
    public static readonly String Dream = "DreamEvent";
    public static readonly String NewDay = "NewDayEvent";
    public static readonly String Naming = "NameEvent";
    public static readonly String Animate = "AnimateEvent";
    public static readonly String Jukebox = "JukeboxEvent";

    //linking events
    public static readonly String Link1 = "Link1Event";
    public static readonly String Link2 = "Link2Event";
    public static readonly String Link3 = "Link3Event";
    public static readonly String Link4 = "Link4Event";
    public static readonly String Link5 = "Link5Event";
    public static readonly String Link6 = "Link6Event";
    public static readonly String Link7 = "Link7Event";
    public static readonly String Link8 = "Link8Event";
    public static readonly String Link9 = "Link9Event";
    public static readonly String Link10 = "Link10Event";
    public static readonly String Link11 = "Link11Event";
    public static readonly String Link12 = "Link12Event";
    public static readonly String Link13 = "Link13Event";
    public static readonly String Link14 = "Link14Event";
    public static readonly String Link15 = "Link15Event";
    public static readonly String Link16 = "Link16Event";
    public static readonly String Link17 = "Link17Event";

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
    
    public static implicit operator Events (string value)
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