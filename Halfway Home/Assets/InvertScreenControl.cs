using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertScreenControl : MonoBehaviour
{
    public string ActiveTag;

    public string[] Speakers;
    
    InvertColorEffect Inverter;

    bool Active;

    bool Inverted;

    public float InvertSpeed = 0.5f;

	// Use this for initialization
	void Start ()
    {

        Inverter = GetComponent<InvertColorEffect>();
        Inverter.SetInvertAmount(0);
        Inverter.enabled = false;

        if (Game.current != null)
            Active = Game.current.Progress.GetBoolValue(ActiveTag);

        Space.Connect<DescriptionEvent>(Events.Description, OnNewLine);
        Space.Connect<DefaultEvent>(Events.Progress, OnProgressUpdate);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void OnNewLine(DescriptionEvent eventdata)
    {
        if (!Active)
            return;

        bool On = InvertOn(eventdata.TrueSpeaker);
        
        if(On && !Inverter.enabled)
            StartCoroutine(InvertFade(InvertSpeed));
        else if(!On && Inverter.enabled)
            StartCoroutine(InvertFade(InvertSpeed));



    }

    bool InvertOn(string trueSpeaker)
    {
        foreach(var speaker in Speakers)
        {
            if (trueSpeaker.ToLower() == speaker.ToLower())
                return true;
        }

        return false;
    }


    void OnProgressUpdate(DefaultEvent eventdata)
    {
        Active = Game.current.Progress.GetBoolValue(ActiveTag);
    }


    IEnumerator InvertFade(float aTime)
    {
        if(Inverted)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Inverter.SetInvertAmount(1 - t);
                yield return null;
            }

            Inverter.SetInvertAmount(0);
            Inverter.enabled = false;
        }
        else
        {
            Inverter.enabled = true;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Inverter.SetInvertAmount(t);
                yield return null;
            }

            Inverter.SetInvertAmount(1);
        }

        Inverted = !Inverted;

    }


}
