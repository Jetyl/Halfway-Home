/******************************************************************************/
/*!
File:   GameStartUp.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
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
    void Awake ()
    {
        if(DebugMode && !Debug.isDebugBuild) DebugMode = false;
        if (DebugMode)
        {
            //SaveLoad.Delete();
            ReaderReference.Clear();
            
            TestingAndDebugging();
            return;
        }
        else
        {
            //delete debugged saves, if we are no longer in a debug mode
            if(Game.current != null && Game.current.Progress.GetBoolValue("Debug Mode"))
            {
                Game.current = null;
                SaveLoad.Delete();
            }
        }
        //print(Game.current);
        if (Game.current == null) //for new games
        {
            ReaderReference.Clear();
            Game.current = new Game();
            SetStartValues();
            Game.current.Progress.SetValue<bool>("Tutorial", true);
            StartCoroutine(DelayStart(2));
            //print("new");
        }
        else //for continuing games
        {
            //print("load");
            ReaderReference.LoadSave();
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
        Game.current.Self.SetWellbeingStat(Personality.Wellbeing.Depression, StartDelusion);
        Game.current.Self.SetWellbeingStat(Personality.Wellbeing.Fatigue, StartFatigue);
        Game.current.Self.SetWellbeingStat(Personality.Wellbeing.Stress, StartStress);
    }

    void SetDebugValues()
    {
        Game.current.Day = DebugDay;
        Game.current.Hour = DebugHour;
        Game.current.Progress.SetValue("week", DebugWeek);
        Game.current.Progress.SetValue("Debug Mode", true);
        Game.current.Self.SetSocialStar(Personality.Social.Awareness, DebugAwareness);
        Game.current.Self.SetSocialStar(Personality.Social.Grace, DebugGrace);
        Game.current.Self.SetSocialStar(Personality.Social.Expression, DebugExpression);
    }


    IEnumerator DelayStart(int frames)
    {
        yield return new WaitForSeconds(Time.deltaTime * frames);

        Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));

        if (DebugMode)
        {
            SetDebugValues();
            Space.DispatchEvent(Events.Debug);
        }
    }


}
