/******************************************************************************/
/*!
File:   AutoType.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AutoType : MonoBehaviour
{
    // 1/(pause speed + player pref) = # of characters added to the screen per second.
    public float DefaultPauseSpeed = 0.2f; 
    //public AudioClip sound;
    public string ScrollEvent;

    public List<AutoDelays> ExtraDelays;

    public List<char> DelayAllowances;

    float letterPause;

    float PauseSpeedMultiplier = 1;

    string message;

    //AudioSource audios;

    private TextMeshProUGUI Text;

    private TMP_Animator Effects;

    Dictionary<int, float> UpdateSpeed;

    Coroutine typing;

    float DefaultVolume;
    
    bool Skipping;

    bool Paused;

    // Use this for initialization
    void Start()
    {
        Text = gameObject.GetComponent<TextMeshProUGUI>();
        Effects = gameObject.GetComponent<TMP_Animator>();
        //audios = GetComponent<AudioSource>();
        Text.useMaxVisibleDescender = true;
        EventSystem.ConnectEvent<AutoTypeEvent>(gameObject, Events.AutoType, TypingText);
        EventSystem.ConnectEvent<DefaultEvent>(gameObject, Events.PrintLine, SkipTyping);
        //DefaultVolume = audios.volume;

        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
        Space.Connect<DefaultEvent>(Events.UnPause, OnUnPause);

        Space.Connect<DefaultEvent>(Events.GetPlayerInfo, OnPause);
        Space.Connect<DefaultEvent>(Events.GetPlayerInfoFinished, OnUnPause);

    }

    public void SetSkipping(bool SetSkip)
    {
        Skipping = SetSkip;

        if(Skipping &&typing != null)
        {
            StopCoroutine(typing);
            Text.text = message;
            Text.maxVisibleCharacters = message.Length;
            Space.DispatchEvent(Events.FinishedAutoType);
        }
           
    }

    public void Clear()
    {
        if(typing != null)
            StopCoroutine(typing);

        Text.text = "";
    }
    
    void OnPause(DefaultEvent eventdata)
    {
        Paused = true;
        if (typing != null)
            StopCoroutine(typing);
    }

    void OnUnPause(DefaultEvent eventdata)
    {
        Paused = false;
        if(Text.IsActive())
            typing = StartCoroutine(TypeText(Text.maxVisibleCharacters));
    }

    public void TypingText(AutoTypeEvent eventdata)
    {
        if(typing != null)
        {
            StopCoroutine(typing);
            typing = null;
        }
        message = eventdata.text;
        Text.text = "";
        PauseSpeedMultiplier = Game.current.Progress.GetFloatValue("TextSpeed");
        letterPause = 1 / (DefaultPauseSpeed * PauseSpeedMultiplier);
        UpdateSpeed = TextParser.ExtractTextSpeed(ref message);

        if (!Paused)
            typing = StartCoroutine(TypeText(0));
        else
            Text.maxVisibleCharacters = 0;
    }

    public void SkipTyping(DefaultEvent eventdata)
    {
        StopCoroutine(typing);
        Effects.KillAllCustomTags(ref message);
        Text.text = message;
        Text.maxVisibleCharacters = message.Length;
        typing = StartCoroutine(EffectsActive());
        Space.DispatchEvent(Events.FinishedAutoType);
    }

    IEnumerator TypeText(int visible)
    {
        Text.maxVisibleCharacters = visible;
        Text.text = message;

        Effects.DynamicAnimator();

        if(!Skipping)
        {
            
            yield return new WaitForSeconds(Time.deltaTime);

            while (Text.maxVisibleCharacters < Text.GetParsedText().Length)
            {
                Stratus.Scene.Dispatch<AudioManager.AudioEvent>(new AudioManager.AudioEvent(AudioManager.AudioEvent.SoundType.SFX, ScrollEvent));
                /*
                if (!audios.isPlaying)
                {
                    if (Text.maxVisibleCharacters < Text.GetParsedText().Length)
                    {
                        print(Text.maxVisibleCharacters);
                        audios.volume = DefaultVolume * Game.current.Progress.GetFloatValue("SFXVolume")
                            * Game.current.Progress.GetFloatValue("MasterVolume");
                        audios.PlayOneShot(sound);
                    }


                }
                */
                if (UpdateSpeed.ContainsKey(Text.maxVisibleCharacters))
                {
                    if (UpdateSpeed.ContainsKey(Text.maxVisibleCharacters + 1))
                    {
                        letterPause = (UpdateSpeed[Text.maxVisibleCharacters]);

                        print("Delay " + letterPause);
                    }
                    else
                    {
                        letterPause = 1 / (UpdateSpeed[Text.maxVisibleCharacters] * PauseSpeedMultiplier);
                        print("on " + letterPause + "with Speed: " + UpdateSpeed[Text.maxVisibleCharacters]);
                    }
                }

                var vis = Text.maxVisibleCharacters;

                if (vis < 0)
                    vis = 0;
                if (vis >= Text.GetParsedText().Length)
                    vis = Text.GetParsedText().Length - 1;


                var charaPause = DelayContains(vis, letterPause);
                //print(Text.GetParsedText()[vis] + " " + charaPause);
                Text.maxVisibleCharacters++;

                Effects.AnimateText();

                yield return new WaitForSeconds(charaPause);
            }
        }
        else
        {
            Text.maxVisibleCharacters = message.Length;
            yield return new WaitForSeconds(letterPause);
        }

        

        Text.maxVisibleCharacters = message.Length;

        //audios.Stop();
        Space.DispatchEvent(Events.FinishedAutoType);

        while(true)
        {
            Effects.AnimateText();
            yield return new WaitForSeconds(Time.deltaTime);
        }

        
    }
    IEnumerator EffectsActive()
    {
        
        while (true)
        {
            Effects.AnimateText();
            yield return new WaitForSeconds(Time.deltaTime);
        }


    }

    float DelayContains(int index, float value)
    {

        if(Text.GetParsedText().Length <= index + 1)
            return value;

        //first we needs to see if we can even have a modifed delay for this character.
        bool Allow = false;
        char chara = Text.GetParsedText()[index + 1];

        foreach (var del in DelayAllowances)
        {
            if (chara == del)
                Allow = true;

        }

        if (!Allow)
            return value;
        
        //now that we know we can get a modified delay, search for a modified delay
        char character = Text.GetParsedText()[index];

        foreach (var del in ExtraDelays)
        {
            if(del.chracter == character)
            {
                //print(character);
                return value * del.DelayMultiplier;
            }

        }

        return value;
    }



}


/**
* CLASS NAME  : AutoTypeEvent
* DESCRIPTION : event data for the autotype function.
**/
public class AutoTypeEvent : EventData
{
    public string text;

    public AutoTypeEvent(string textToType)
    {
        text = textToType;
    }

}

[System.Serializable]
public struct AutoDelays
{
    public char chracter;
    public float DelayMultiplier;
}