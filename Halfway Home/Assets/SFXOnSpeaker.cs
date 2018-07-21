using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXOnSpeaker : MonoBehaviour
{

    public string Speaker;

    public string SoundEffectTag;
    
    bool SoundON;

    // Use this for initialization
    void Start ()
    {
        Space.Connect<DescriptionEvent>(Events.Description, OnNewLine);

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnNewLine(DescriptionEvent eventdata)
    {

        var Active = IsActive(eventdata.TrueSpeaker);

        if (Active && !SoundON)
        {
            SoundON = true;
            Stratus.Scene.Dispatch<AudioManager.AudioEvent>(new AudioManager.AudioEvent(AudioManager.AudioEvent.SoundType.ALayer, SoundEffectTag));
        }

        if (!Active && SoundON)
        {
            SoundON = false;
            //stop heart beat SFX event here later.
        }

    }

    bool IsActive(string trueSpeaker)
    {
        if (trueSpeaker.ToLower() == Speaker.ToLower())
            return true;
        else
            return false;
    }

}
