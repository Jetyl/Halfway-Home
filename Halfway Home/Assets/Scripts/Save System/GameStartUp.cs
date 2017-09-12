using UnityEngine;
using System.Collections;
using LitJson;

public class GameStartUp : MonoBehaviour
{

    public TextAsset Timeline;
    

    public bool DebugMode;

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
            Game.current.Day = 1;
            StartCoroutine(DebugStart());
        }
    }


    IEnumerator DebugStart()
    {



        yield return new WaitForSeconds(Time.deltaTime);

        Space.DispatchEvent(Events.StartGame, new ConversationEvent(Timeline));
    }


}
