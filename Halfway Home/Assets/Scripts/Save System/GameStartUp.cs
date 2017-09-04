using UnityEngine;
using System.Collections;

public class GameStartUp : MonoBehaviour
{

    public GameObject description;

    public GameObject inventory;

    public GameObject UIeventsytem;

    public GameObject ImageDisplay;

    public GameObject ThoughtDisplay;

    public GameObject PhoneDisplay;

    public GameObject CharacterDisplay;

    public GameObject CursorControl;

    public bool DebugMode;

    // Use this for initialization
    void Start ()
    {

        if (DebugMode)
            SaveLoad.Delete();

        SaveLoad.Load();
        
        if (GameObject.Find("DescriptionHUD") == null)
        {
            //generate the description prefab
            Instantiate(description);
        }

        if (GameObject.Find("Inventory") == null)
        {
            //generate the inventory prefab
            Instantiate(inventory);
        }

        if (GameObject.Find("UIEventSystem") == null && GameObject.Find("EventSystem") == null)
        {
            //generate the inventory prefab
            Instantiate(UIeventsytem);
        }

        if (GameObject.Find("ImageDisplay") == null)
        {
            //generate the inventory prefab
            Instantiate(ImageDisplay);
        }

        if (GameObject.Find("ThoughtDisplay") == null)
        {
            //generate the inventory prefab
            Instantiate(ThoughtDisplay);
        }

        if (GameObject.Find("Phone") == null)
        {
            //generate the inventory prefab
            Instantiate(PhoneDisplay);
        }

        if (GameObject.Find("CharacterDisplay") == null)
        {
            //generate the inventory prefab
            Instantiate(CharacterDisplay);
        }

        if (GameObject.Find("CursorControl") == null)
        {
            //generate the inventory prefab
            Instantiate(CursorControl);
        }

        if (DebugMode)
        {
            TestingAndDebugging();
        }

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
            DebugMindReader();
        }
    }


    void DebugMindReader()
    {
        print(Feelings.Hollow + " " + Game.current.Mind.getMood(Feelings.Hollow));
        print(Feelings.Melancholy + " " + Game.current.Mind.getMood(Feelings.Melancholy));
        print(Feelings.Frustration + " " + Game.current.Mind.getMood(Feelings.Frustration));
        print(Feelings.Anxiety + " " + Game.current.Mind.getMood(Feelings.Anxiety));
        print(Feelings.Desire + " " + Game.current.Mind.getMood(Feelings.Desire));
        print(Feelings.Comfort + " " + Game.current.Mind.getMood(Feelings.Comfort));
        print(Feelings.Affection + " " + Game.current.Mind.getMood(Feelings.Affection));
        print("Total Mood " + Game.current.Mind.MoodTotal());
    }

}
