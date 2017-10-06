using UnityEngine;
using System.Collections;
using LitJson;

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
          StartCoroutine(DebugStart());
        }

    Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));

    }

    void TestingAndDebugging()
    {
        if(Game.current == null)
        {
            
            Game.current = new Game();
            Game.current.Day = DebugDay;
            Game.current.Hour = DebugHour;
            
        }
    }


    IEnumerator DebugStart()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));
    }


}
