using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AutoType : MonoBehaviour
{

    public float DefaultPauseSpeed = 0.2f;
    public AudioClip sound;

    float letterPause;

    float PauseSpeedMultiplier = 1;

    string message;

    AudioSource audios;

    private TextMeshProUGUI Text;

    Dictionary<int, float> UpdateSpeed;

    Coroutine typing;

    float DefaultVolume;

    // Use this for initialization
    void Start()
    {
        Text = gameObject.GetComponent<TextMeshProUGUI>();
        audios = GetComponent<AudioSource>();
        Text.useMaxVisibleDescender = true;
        EventSystem.ConnectEvent<AutoTypeEvent>(gameObject, Events.AutoType, TypingText);
        EventSystem.ConnectEvent<DefaultEvent>(gameObject, Events.SkipTyping, SkipTyping);
        DefaultVolume = audios.volume;
           
    }


    public void Clear()
    {
        if(typing != null)
            StopCoroutine(typing);

        Text.text = "";
    }


    public void TypingText(AutoTypeEvent eventdata)
    {
        message = eventdata.text;
        Text.text = "";
        PauseSpeedMultiplier = Game.current.Progress.GetFloatValue("TextSpeed");
        letterPause = 1 / (DefaultPauseSpeed * PauseSpeedMultiplier);
        UpdateSpeed = TextParser.ExtractTextSpeed(ref message);
        typing = StartCoroutine(TypeText());
    }

    public void SkipTyping(DefaultEvent eventdata)
    {
        StopCoroutine(typing);
        Text.text = message;
        Text.maxVisibleCharacters = message.Length;
        Space.DispatchEvent(Events.FinishedAutoType);
    }

    IEnumerator TypeText()
    {
        Text.maxVisibleCharacters = 0;
        Text.text = message;
        bool AudioOff = false;
        int length = message.Length;
        while (Text.maxVisibleCharacters < message.Length)
        {

            if (Text.text[Text.maxVisibleCharacters] == '<')
            {
                int i = Text.maxVisibleCharacters;
                while (Text.text[i] != '>')
                {
                    i += 1;
                }
                i -= Text.maxVisibleCharacters;
                length -= i;

            }


            //skip rich text stuff
            if (Text.text[Text.maxVisibleCharacters] == '<')
            {
                AudioOff = true;
            }
            if (Text.text[Text.maxVisibleCharacters] == '>')
            {
               AudioOff = false;
            }


            if (!audios.isPlaying)
            {
                if (Text.maxVisibleCharacters < length)
                {
                    audios.volume = DefaultVolume * Game.current.Progress.GetFloatValue("SFXVolume") 
                        * Game.current.Progress.GetFloatValue("MasterVolume");
                    audios.PlayOneShot(sound);
                }
                //if (AudioOff)
                    //print(Text.text[Text.maxVisibleCharacters]);

            }

            if(UpdateSpeed.ContainsKey(Text.maxVisibleCharacters))
            {
                letterPause = 1 / (UpdateSpeed[Text.maxVisibleCharacters] * PauseSpeedMultiplier);

                //print("on " + letterPause + "with Speed: " + UpdateSpeed[Text.maxVisibleCharacters]);
            }


            Text.maxVisibleCharacters += 1;

            yield return new WaitForSeconds(letterPause);
        }

        Text.maxVisibleCharacters = message.Length;

        audios.Stop();
        Space.DispatchEvent(Events.FinishedAutoType);

        /*
        char[] letters = message.ToCharArray();

        for (int i = 0; i < letters.Length; ++ i)
        {
            //if the
            if (letters[i] == '<')
            {
                

                int end = TextParser.SkipRichText(letters, i);

                for (int j = i; j <= end; ++j)
                {
                    Text.text += letters[j];
                }
                i = end;
                continue;
            }

            Text.text += letters[i];

            if(!audios.isPlaying)
            {
                audios.PlayOneShot(sound);
                
            }
            

            if (Text.text == message)
            {
                //dispatch the autype finish event here
                Space.DispatchEvent(Events.FinishedAutoType);
            }

            yield return new WaitForSeconds(letterPause);
        }
        */
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