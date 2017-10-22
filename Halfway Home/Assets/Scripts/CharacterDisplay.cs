using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{

    public CharacterList Character;

    public List<Poses> Poses;

    public List<Distances> Distances;

    public StagePosition Direction;

    public StageDistance Distance;

    public SpriteRenderer visual;
    public SpriteRenderer BackSprite;

    public float SpriteSwitchSpeed = 1;

    public bool FlipOnLeft;

	// Use this for initialization
	void Start ()
    {

        //visual = GetComponentInChildren<SpriteRenderer>();
        var awhite = Color.white;
        awhite.a = 0;
        visual.color = awhite;
        BackSprite.color = awhite;

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void EnterStage(string pose, StageDistance distance)
    {
        Start(); // just incase this gets called before start, somehow;
        visual.sprite = GetPose(pose);
        ChangeDistance(distance);
        //visual.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, 2));
        

    }
    public void ChangePose(string pose)
    {
        //visual.sprite = GetPose(pose);
        StartCoroutine(ChangeSprite(GetPose(pose)));
    }

    IEnumerator ChangeSprite(Sprite newSprite)
    {

        BackSprite.sprite = newSprite;
        var Awhite = Color.white;
        Awhite.a = 0;
        visual.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Awhite, SpriteSwitchSpeed));
        BackSprite.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, SpriteSwitchSpeed));

        yield return new WaitForSeconds(2);

        visual.sprite = newSprite;
        visual.color = Color.white;
        BackSprite.color = Awhite;

    }

    public void ChangePosition(StagePosition pos)
    {
        Direction = pos;

        if (FlipOnLeft)
        {
            if (Direction == StagePosition.Left)
                visual.flipX = true;
            else
                visual.flipX = false;
        }
        else
        {
            if (Direction == StagePosition.Right)
                visual.flipX = true;
            else
                visual.flipX = false;
        }


    }

    public void ChangeDistance(StageDistance distance)
    {
        Distance = distance;
        float scale = Distances[(int)distance].Scale;
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = new Vector3(transform.position.x, Distances[(int)distance].Offset, transform.position.z);
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

[System.Serializable]
public class Distances
{
    public float Scale;
    public float Offset;
}