using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{

    public CharacterList Character;

    public List<Poses> Poses;

    public StagePosition Direction;

    SpriteRenderer visual;

	// Use this for initialization
	void Start ()
    {

        visual = GetComponentInChildren<SpriteRenderer>();
        var awhite = Color.white;
        awhite.a = 0;
        visual.color = awhite;


	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void EnterStage(string pose)
    {
        Start(); // just incase this gets called before start, somehow;
        visual.sprite = GetPose(pose);
        //visual.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, 2));
        

    }
    public void ChangePose(string pose)
    {
        visual.sprite = GetPose(pose);
    }
    public void ExitStage()
    {
        //visual.sprite = Poses[pose];
        var awhite = Color.white;
        awhite.a = 0;
        visual.gameObject.DispatchEvent(Events.Fade, new FadeEvent(awhite, 1));
        var pos = transform.position;

        if(Direction == StagePosition.Left)
        {
            pos.x -= 2.5f;
        }
        else
        {
            pos.x += 2.5f;
        }
        iTween.MoveTo(gameObject, pos, 2);
        Destroy(gameObject, 5);
    }


    Sprite GetPose(string name)
    {
        for(int i = 0; i < Poses.Count; ++i)
        {
            if(Poses[i].Name == name)
            {
                return Poses[i].Visual;
            }
        }

        Debug.LogError("Character: " + Character + "does not know pose " + name);
        return null;
    }

}

[System.Serializable]
public class Poses
{
    //because unity hates dictionaries
    public string Name;
    public Sprite Visual;
}