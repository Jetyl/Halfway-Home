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
    public StagePosition FacingDirection;

    public StageDistance Distance;

    public SpriteRenderer visual;
    public SpriteRenderer BackSprite;

    public float SpriteSwitchSpeed = 1;

    public bool FlipOnLeft;

    string Pose;

    bool Entering;

    Vector3 Destination;

    Coroutine Expressing;

    bool Scaled;

    string MCName;

    public float ScaleRatio = 1.2f;

    GameObject ChildTransform;

    // Use this for initialization
    void Start ()
    {
        ChildTransform = transform.GetChild(0).gameObject;
        //visual = GetComponentInChildren<SpriteRenderer>();

        MCName = Game.current.PlayerName;
        Space.Connect<DescriptionEvent>(Events.Description, OnScale);

        GetComponent<Scale>().Start();
        GetComponent<Translate>().Start();

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
        data.face = FacingDirection;
        Game.current.CastCall.Add(data);
    }

    public void OnLoad(CharacterIntermission chara)
    {
        Start();

        visual.sprite = GetPose(chara.Name);
        var awhite = Color.white;
        awhite.a = 0;
        visual.color = Color.white;
        BackSprite.color = awhite;

        ChangeDistance(chara.Dis, true);
        ChangeFacing(chara.face);
        transform.position = new Vector3(chara.PosX, chara.PosY, transform.position.z);
    }

    public void EnterStage(string pose, StageDistance distance, StagePosition facing,  bool Skip)
    {
        Start(); // just incase this gets called before start, somehow;
        


        if(distance == StageDistance.Same) ChangeDistance(StageDistance.Center, true);
        else ChangeDistance(distance, true);

        ChangeFacing(facing);

        if (pose.ToLower() == "none" || pose == "") 
            pose = Poses[0].Name;

        visual.sprite = GetPose(pose);

        var awhite = Color.white;
        awhite.a = 0;

        if (Skip)
        {
            visual.color = Color.white;
            BackSprite.color = awhite;
        }
        else
        {
            Entering = true;
            visual.color = awhite;
            BackSprite.color = awhite;
            //visual.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, SpriteSwitchSpeed));
        }
            


    }

    void OnScale(DescriptionEvent eventdata)
    {

        if (this == null)
            return;


        if (eventdata.TrueSpeaker == "")
            return;

        if (Scaled)
        {
            if (eventdata.TrueSpeaker != Character.Character && eventdata.TrueSpeaker != MCName)
            {
                Scaled = false;

                float scale = Distances[(int)Distance].Scale;
                ChildTransform.DispatchEvent(Events.Scale, new TransformEvent(new Vector3(scale, scale, scale), SpriteSwitchSpeed));

            }
        }
        else
        {

            if (eventdata.TrueSpeaker == Character.Character)
            {
                Scaled = true;
                float scale = Distances[(int)Distance].Scale;
                var scalevec = new Vector3(scale, scale, scale) * ScaleRatio;
                ChildTransform.DispatchEvent(Events.Scale, new TransformEvent(scalevec, SpriteSwitchSpeed));
            }
        }
    }

    public void MoveOnStage(Vector3 newPosition, float time)
    {

        Destination = newPosition;

        if(Entering)
        {

            Entering = false;

            var pos = newPosition;

            if (Direction == StagePosition.Left)
            {
                pos.x -= 2.5f;
            }
            else
            {
                pos.x += 2.5f;
            }

            transform.localPosition = pos;
            
        }

        gameObject.DispatchEvent(Events.Translate, new TransformEvent(Destination, time));
        
    }

    public void ChangePose(string pose, bool Skip)
    {
        if (pose.ToLower() == "none" || pose == "")
            return;

        //visual.sprite = GetPose(pose);
        if (!Skip)
        {
            if (Expressing != null)
            {

                StopCoroutine(Expressing);
                visual.sprite = BackSprite.sprite; 
                visual.color = Color.white;
                var Awhite = Color.white;
                Awhite.a = 0;
                BackSprite.color = Awhite;
            }

            Expressing = StartCoroutine(ChangeSprite(GetPose(pose)));
        }
            
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

        yield return new WaitForSeconds(SpriteSwitchSpeed + 0.25f);

        visual.sprite = newSprite;
        visual.color = Color.white;
        BackSprite.color = Awhite;
    }

    public void ChangeFacing(StagePosition pos)
    {
        if (pos == StagePosition.Same)
            return;

        FacingDirection = pos;

        print(Character.Character + ", " + FacingDirection);

        if (FlipOnLeft)
        {
            if (FacingDirection == StagePosition.Right)
            {
                visual.flipX = false;
                BackSprite.flipX = false;
            }
            else
            {
                visual.flipX = true;
                BackSprite.flipX = true;
            }
                
        }
        else
        {
            if (FacingDirection == StagePosition.Right)
            {
                visual.flipX = true;
                BackSprite.flipX = true;
            }
            else
            {
                visual.flipX = false;
                BackSprite.flipX = false;
            }
        }


    }
    

    public void ChangeDistance(StageDistance distance, bool skip)
    {
        Distance = distance;
        float scale = Distances[(int)distance].Scale;
        var newpos = new Vector3(transform.position.x, Distances[(int)distance].Offset, transform.position.z);

        if (skip)
        {
            ChildTransform.transform.localScale = new Vector3(scale, scale, scale);
            ChildTransform.transform.localPosition = newpos;
        }
        else
        {
            ChildTransform.DispatchEvent(Events.Scale, new TransformEvent(new Vector3(scale, scale, scale), SpriteSwitchSpeed));
            ChildTransform.DispatchEvent(Events.Translate, new TransformEvent(newpos, SpriteSwitchSpeed));
        }
        
        //

    }
    public void ExitStage(StagePosition direction, bool Skip)
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
        
        visual.gameObject.DispatchEvent(Events.Fade, new FadeEvent(awhite, SpriteSwitchSpeed));
        var pos = transform.position;

        if(direction == StagePosition.Left)
        {
            pos.x -= 2.5f;
        }
        else if ( direction ==  StagePosition.Right)
        {
            pos.x += 2.5f;
        }

        gameObject.DispatchEvent(Events.Translate, new TransformEvent(pos, 2));

        Destroy(gameObject, 5);
    }


    Sprite GetPose(string name)
    {
        for(int i = 0; i < Poses.Count; ++i)
        {
            if(Poses[i].Name.ToLower() == name.ToLower())
            {
                Pose = name;
                return Poses[i].Visual;
            }
        }

        Debug.LogError("Character: " + Character.Character + " does not know pose " + name);
        return null;
    }


    void OnDestroy()
    {
        //Space.DisConnect<DefaultEvent>(Events.FinishedDescription, EndTransitions);
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
    public StagePosition face;
    public StageDistance Dis;
    public float PosX;
    public float PosY;
    public string Name;

    public CharacterIntermission()
    {
        chara = "";
        Name = "";
    }

    public CharacterIntermission(CharacterIntermission copy_)
    {
        chara = copy_.chara;
        Dir = copy_.Dir;
        face = copy_.face;
        Dis = copy_.Dis;
        PosX = copy_.PosX;
        PosY = copy_.PosY;
        Name = copy_.Name;
    }

}