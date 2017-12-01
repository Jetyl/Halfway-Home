using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;

public class GameStartUp : MonoBehaviour
{

    public TextAsset Timeline;

    [Range(0, 7)]
    public int StartDay;
    [Range(0, 23)]
    public int StartHour;

    public bool DebugMode;

    public int StartDelusion;
    public int StartStress;
    public int StartFatigue;


    [Range(0,7)]
    public int DebugDay;
    [Range(0, 23)]
    public int DebugHour;
    [Range(1, 4)]
    public int DebugWeek;

    public int DebugExpression;
    public int DebugGrace;
    public int DebugAwareness;

    public List<ProgressPoint> DebugValues;

    public HalfwayHome.HalfwayHomeStoryReader ReaderReference;

    // Use this for initialization
    void Start ()
    {

        if (DebugMode)
        {
            SaveLoad.Delete();
            ReaderReference.Clear();
        }

        SaveLoad.Load(); //loads the game file

        if (SaveLoad.GetSave(0) != null) //if a save exists, set that to the main game
            Game.current = SaveLoad.GetSave(0);
       
        if (DebugMode)
        {
            TestingAndDebugging();
            return;
        }
        else
        {
            if(Game.current != null && Game.current.Progress.GetBoolValue("Debug Mode"))
            {
                Game.current = null;
                SaveLoad.Delete();
            }
        }

        if (Game.current == null) //for new games
        {
            ReaderReference.Clear();
            Game.current = new Game();
            SetStartValues();
            Game.current.Progress.SetValue<bool>("Tutorial", true);
            StartCoroutine(DelayStart(2));
        }
        else
        {
            StartCoroutine(DelayStart(2));
        }

    //Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));

    }

    void TestingAndDebugging()
    {
        if(Game.current == null)
        {
            
            Game.current = new Game();
            
            SetStartValues();
            SetDebugValues();
            foreach(var point in DebugValues)
            {
                Game.current.Progress.UpdateProgress(point.ProgressName, point);
            }
            StartCoroutine(DelayStart(3));

        }
    }

    void SetStartValues()
    {
        Game.current.Day = StartDay;
        Game.current.Hour = StartHour;
        Game.current.Self.SetWellbeingStat(Personality.Wellbeing.delusion, StartDelusion);
        Game.current.Self.SetWellbeingStat(Personality.Wellbeing.fatigue, StartFatigue);
        Game.current.Self.SetWellbeingStat(Personality.Wellbeing.stress, StartStress);
    }

    void SetDebugValues()
    {
        Game.current.Day = DebugDay;
        Game.current.Hour = DebugHour;
        Game.current.Progress.SetValue("Week", DebugWeek);
        Game.current.Progress.SetValue("Debug Mode", true);
        Game.current.Self.SetSocialStat(Personality.Social.awareness, DebugAwareness);
        Game.current.Self.SetSocialStat(Personality.Social.grace, DebugGrace);
        Game.current.Self.SetSocialStat(Personality.Social.expression, DebugExpression);
    }


    IEnumerator DelayStart(int frames)
    {
        yield return new WaitForSeconds(Time.deltaTime * frames);

        Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));
    }


}
