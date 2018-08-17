using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipControl : MonoBehaviour
{

    public bool DebugSkipping;

    public HistoryDisplay SkipCheck;
    public QuirkDisplay QuirkControl;
    
    public UIFader SkipSprite;
    public UIFader NoSkipSprite;

    public FastForwardEffect SkipEffects;

    public string SkipSoundPlayEvent;
    public string SkipSoundStopEvent;

    bool Skipping = false;
    bool CanSkip = false;

    // Use this for initialization
    void Start ()
    {
        SkipEffects.EndEffect(0.0001f);

        Space.Connect<DescriptionEvent>(Events.Description, UpdateDescription);
        Space.Connect<DefaultEvent>(Events.StopSkipTyping, OnStopSkipTyping);
        Space.Connect<DefaultEvent>(Events.ReturnToMap, OnMapEvent);
        Space.Connect<DefaultEvent>(Events.ConversationChoice, OnChoiceEvent);
        Space.Connect<DefaultEvent>(Events.Debug, OnDebug);

    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetButtonDown("Skip")) RequestSkip();

        if (Input.GetButtonUp("Skip")) RequestStopSkip();

    }


    public void RequestSkip()
    {
        bool SkipAllowed = CanSkip || DebugSkipping;
        if (SkipAllowed && !Skipping) OnSkip();
        else if (!SkipAllowed)
        {
            NoSkipSprite.Show(0.1f);
        }
    }

    public void RequestStopSkip()
    {
        if (Skipping) Space.DispatchEvent(Events.StopSkipTyping);
        else NoSkipSprite.Hide(0.1f);
    }

    void UpdateDescription(DescriptionEvent eventdata)
    {
        //dynamically edit the lines so they adhere to certain parameters
        var Line = TextParser.DynamicEdit(eventdata.Line);
        Line = QuirkControl.UpdateText(Line, eventdata.TrueSpeaker);

        CanSkip = SkipCheck.HasSeenLine(eventdata.Line);
        //print("Can Skip? " + CanSkip);
        if (!DebugSkipping)
        {
            if (!CanSkip && Skipping)
            {
                Space.DispatchEvent(Events.StopSkipTyping);
            }
        }
        
    }
    void OnSkip()
    {
        //Space.DispatchEvent(Events.SkipTyping);
        Skipping = true;

        Space.DispatchEvent(Events.SkipTyping);
        
        SkipSprite.Show(0.1f);
        SkipEffects.StartEffect(0.1f);
        Stratus.Scene.Dispatch<AudioManager.AudioEvent>(new AudioManager.AudioEvent(AudioManager.AudioEvent.SoundType.ALayer, SkipSoundPlayEvent));
    }

    void OnStopSkipTyping(DefaultEvent eventdata)
    {
        if(Skipping)
        {
            SkipSprite.Hide(0.1f);
            SkipEffects.EndEffect(0.1f);
            Stratus.Scene.Dispatch<AudioManager.AudioEvent>(new AudioManager.AudioEvent(AudioManager.AudioEvent.SoundType.ALayer, SkipSoundStopEvent));
        }

        Skipping = false;
    }

    void OnMapEvent(DefaultEvent eventdata)
    {
        // Stop skipping upon going to map
        Space.DispatchEvent(Events.StopSkipTyping);
    }

    void OnChoiceEvent(DefaultEvent eventdata)
    {
        //stop skipping when a choice appears
        CanSkip = false;
        Space.DispatchEvent(Events.StopSkipTyping);
    }

    void OnDebug(DefaultEvent eventdata)
    {
        DebugSkipping = true;
    }

    void OnDestroy()
    {

        Space.DisConnect<DefaultEvent>(Events.StopSkipTyping, OnStopSkipTyping);
        Space.DisConnect<DefaultEvent>(Events.ReturnToMap, OnMapEvent);
        Space.DisConnect<DefaultEvent>(Events.Debug, OnDebug);
    }

}
