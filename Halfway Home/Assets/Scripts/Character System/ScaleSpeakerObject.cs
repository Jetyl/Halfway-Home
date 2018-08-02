using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSpeakerObject : MonoBehaviour
{
    public CharacterList Speaker;

    public float ScaleRatio = 1.2f;

    public float TimeToScale = 0.5f;

    bool Scaled;

    string MCName;

    Vector3 BaseSize;

	// Use this for initialization
	void Start ()
    {
        MCName = Game.current.PlayerName;
        Space.Connect<DescriptionEvent>(Events.Description, OnScale);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnScale(DescriptionEvent eventdata)
    {

        if (this == null)
            return;
        

        if (eventdata.TrueSpeaker == "")
            return;

        if (Scaled)
        {
            if (eventdata.TrueSpeaker != Speaker.Character && eventdata.TrueSpeaker != MCName)
            {
                Scaled = false;
                
                //iTween.ScaleTo(gameObject, BaseSize, TimeToScale);
            }
        }
        else
        {
            
            if (eventdata.TrueSpeaker == Speaker.Character)
            {
                Scaled = true;
                BaseSize = transform.localScale;
                Vector3 newscale = transform.localScale * ScaleRatio;
                //iTween.ScaleTo(gameObject, newscale, TimeToScale);
            }
        }
    }
    

    void OnDestroy()
    {
        //Space.DisConnect<DescriptionEvent>(Events.Description, OnScale);
    }

}
