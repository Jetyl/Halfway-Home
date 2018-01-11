/******************************************************************************/
/*!
File:   CharacterDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
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

    string Pose;

	// Use this for initialization
	void Start ()
    {

        //visual = GetComponentInChildren<SpriteRenderer>();
        

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnSave()
    {
        var data = new CharacterIntermission();
        data.chara = Character.Character;
        data.Dis = Distance;
        data.PosX = transform.position.x;
        data.PosY = transform.position.y;
        data.Name = Pose;
        data.Dir = Direction;
        Game.current.CastCall.Add(data);
    }

    public void OnLoad(CharacterIntermission chara)
    {
        visual.sprite = GetPose(chara.Name);
        ChangeDistance(chara.Dis);
        transform.position = new Vector3(chara.PosX, chara.PosY, transform.position.z);
    }

    public void EnterStage(string pose, StageDistance distance, bool Skip)
    {
        Start(); // just incase this gets called before start, somehow;
        
        visual.sprite = GetPose(pose);
        ChangeDistance(distance);


        var awhite = Color.white;
        awhite.a = 0;

        if (Skip)
        {
            visual.color = Color.white;
            BackSprite.color = awhite;
        }
        else
        {

            visual.color = awhite;
            BackSprite.color = awhite;

            visual.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, SpriteSwitchSpeed));
        }
            


    }
    public void ChangePose(string pose, bool Skip)
    {
        //visual.sprite = GetPose(pose);
        if (!Skip)
            StartCoroutine(ChangeSprite(GetPose(pose)));
        else
            visual.sprite = GetPose(pose);
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
    public void ExitStage(bool Skip)
    {
        //visual.sprite = Poses[pose];
        var awhite = Color.white;
        awhite.a = 0;

        if (Skip)
        {
            visual.color = awhite;
            Destroy(gameObject, 0.5f);
            return;
        }

        
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
                Pose = name;
                return Poses[i].Visual;
            }
        }

        Debug.LogError("Character: " + Character.Character + " does not know pose " + name);
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

[System.Serializable]
public class CharacterIntermission
{
    //class for if player saved mid scene.
    public string chara;
    public StagePosition Dir;
    public StageDistance Dis;
    public float PosX;
    public float PosY;
    public string Name;
}