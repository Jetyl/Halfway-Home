using UnityEngine;
using System.Collections;
using LitJson;

public class GameStartUp : MonoBehaviour
{

    public TextAsset Timeline;
    

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

        

        Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));

    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void TestingAndDebugging()
    {
        if(Game.current == null)
        {
            
            Game.current = new Game();
            Game.current.Day = DebugDay;
            Game.current.Hour = DebugHour;
            StartCoroutine(DebugStart());
        }
    }


    IEnumerator DebugStart()
    {



        yield return new WaitForSeconds(Time.deltaTime);

        Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));
    }


}
