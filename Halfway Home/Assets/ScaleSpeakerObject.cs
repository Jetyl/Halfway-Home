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
        
        eventdata.Speaker = eventdata.Speaker.Replace("[", "");
        eventdata.Speaker = eventdata.Speaker.Replace("]", "");

        if (eventdata.Speaker == "")
            return;

        if (Scaled)
        {
            if (eventdata.Speaker != Speaker.Character && eventdata.Speaker != MCName)
            {
                Scaled = false;
                
                iTween.ScaleTo(gameObject, Vector3.one, TimeToScale);
            }
        }
        else
        {
            print(eventdata.Speaker);
            if (eventdata.Speaker == Speaker.Character)
            {
                Scaled = true;
                
                Vector3 newscale = transform.localScale * ScaleRatio;
                iTween.ScaleTo(gameObject, newscale, TimeToScale);
            }
        }
    }

}
