using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStatusIcon : MonoBehaviour
{

    public Room Location;

    public Sprite SeenIcon;
    public Sprite CompleteIcon;

    Image manga;

    // Use this for initialization
    void Start ()
    {

        manga = GetComponent<Image>();
        var col = manga.color;
        col.a = 0;
        manga.color = col;

    }
	
	// Update is called once per frame
	void Update ()
    {


    }


    void OnMap(DefaultEvent eventdata)
    {
        var see = Game.current.FlagMap(Location);

        switch(see)
        {
            case SceneSeen.Unseen:
                var co = manga.color;
                co.a = 0;
                manga.color = co;
                break;
            case SceneSeen.Seen:
                manga.sprite = SeenIcon;
                var col = manga.color;
                col.a = 1;
                manga.color = col;
                break;
            case SceneSeen.Completed:
                manga.sprite = CompleteIcon;
                var coll = manga.color;
                coll.a = 1;
                manga.color = coll;
                break;
            default:
                break;
        }

        

    }

}
