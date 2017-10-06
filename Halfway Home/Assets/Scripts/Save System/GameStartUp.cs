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

    [Range(0,7)]
    public int DebugDay;
    [Range(0, 23)]
    public int DebugHour;
    [Range(1, 4)]
    public int DebugWeek;

    public List<ProgressPoint> DebugValues;

    // Use this for initialization
    void Start ()
    {

        if (DebugMode)
            SaveLoad.Delete();

        SaveLoad.Load();
        
       
        if (DebugMode)
        {
            TestingAndDebugging();
        }

        if (Game.current == null)
        {

          Game.current = new Game();
          Game.current.Day = StartDay;
          Game.current.Hour = StartHour;
            //Game.current.Progress.SetValue<bool>("Tutorial", true);
          StartCoroutine(DelayStart(1));
        }

    //Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));

    }

    void TestingAndDebugging()
    {
        if(Game.current == null)
        {
            
            Game.current = new Game();
            Game.current.Day = DebugDay;
            Game.current.Hour = DebugHour;
            Game.current.Progress.SetValue("Week", DebugWeek);
            Game.current.Progress.SetValue("Debug Mode", true);
            foreach(var point in DebugValues)
            {
                Game.current.Progress.UpdateProgress(point.ProgressName, point);
            }
            StartCoroutine(DelayStart(2));

        }
    }


    IEnumerator DelayStart(int frames)
    {
        yield return new WaitForSeconds(Time.deltaTime * frames);

        Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));
    }


}
