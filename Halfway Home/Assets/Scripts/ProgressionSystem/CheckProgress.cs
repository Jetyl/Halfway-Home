using UnityEngine;
using System.Collections;
using System;

public class CheckProgress : MonoBehaviour
{

    public string Notes; //a comment in editor for what this instance is checking

    public EventListener ListeningOn = EventListener.Owner;

    public Events CheckOn = Events.Inspect;

    public EventListener CallOnPass = EventListener.Owner;

    public Events SayOnPass = Events.Inspect;

    public EventListener CallOnFail = EventListener.Owner;

    public Events SayOnFail = Events.Inspect;

    public ProgressPoint CheckToMatch;

    public Sprite InventoryMatch;

    public bool CurrentlyInInventory;

    public ProgressType TypeOfProgress;
    

    public float MoodValueToMatch;

    public ValueCompare Compare;

    public uint LuckRange;

    public float LuckPercentToFail;

    public bool AffectLuck;
    

    public string NoteTitle;

    public Sprite Image;

    public int TaskNumber;

    public Task.TaskState NewTaskState;

    [Range(0, 100)]
    public int BatteryLife = 0;

    public float DrainSpeed;

    public string BeatName;
    public Beat.BeatState beatState;
    
    public bool PreviousScene;

    public DayOfWeek date;

    public string DreamName;

    // Use this for initialization
    void Start ()
    {

        if(TypeOfProgress == ProgressType.ProgressPoint)
        {
            if(Game.current.Progress.Contains(CheckToMatch.ProgressName) != true)
                print(CheckToMatch.ProgressName + " not set on Object " + gameObject.name);
        }


        if (ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, CheckOn, Check);
        else if (ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(CheckOn, Check);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Check(DefaultEvent eventdata)
    {

        switch (TypeOfProgress)
        {
            case ProgressType.None:
                break;
            case ProgressType.ProgressPoint:

                if (Game.current.Progress.CheckProgress(CheckToMatch))
                    Pass();
                else
                    Fail();

                break;
            
            case ProgressType.PlotBeat:
                if (Game.current.Progress.CheckBeatState(BeatName, beatState))
                    Pass();
                else
                    Fail();
                break;
            
            case ProgressType.Date:
                if (Game.current.Date == date)
                    Pass();
                else
                    Fail();
                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
        

    }


    void Pass()
    {
        if (CallOnPass == EventListener.Owner)
            gameObject.DispatchEvent(SayOnPass);
        else if (CallOnPass == EventListener.Space)
            Space.DispatchEvent(SayOnPass);
    }

    void Fail()
    {
        if (CallOnFail == EventListener.Owner)
            gameObject.DispatchEvent(SayOnFail);
        else if (CallOnFail == EventListener.Space)
            Space.DispatchEvent(SayOnFail);
    }

    

    public bool CompareValues(float a, float b)
    {

        switch(Compare)
        {
            case ValueCompare.EqualTo:
                return a == b;
            case ValueCompare.GreaterThan:
                return a > b;
            case ValueCompare.LessThan:
                return a < b;
            default:
                return false;
        }

    }


    void OnDestroy()
    {

        if (Space.Instance != null && ListeningOn == EventListener.Space)
        {
            EventSystem.DisconnectEvent(Space.Instance.gameObject, CheckOn, this);
        }
    }

}


public enum ProgressType
{
    None,
    ProgressPoint,
    PlotBeat,
    Date
}


public enum ValueCompare
{
    GreaterThan,
    LessThan,
    EqualTo

}

/*
#if UNITY_EDITOR
namespace CustomInspector
{
    using UnityEditor;


    [CustomEditor(typeof(CheckProgress))]
    public class CheckProgressEditor : Editor
    {


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("ListeningOn"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("CheckOn"));





            serializedObject.ApplyModifiedProperties();
        }


    }
}
#endif
*/