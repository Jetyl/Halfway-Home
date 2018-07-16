/******************************************************************************/
/*!
File:   AudioManager.cs
Author: John Myres
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK;
using Stratus;

public class AudioManager : MonoBehaviour
{
  float paramFadeTimer = 0;
  public float paramFadeTime = 2.0f;
  
  public class AudioEvent : Stratus.Event
  {
    public enum SoundType
    {
      Music,
      MLayer,
      ALayer,
      SFX,
      Ambience
    };
    public SoundType Type;
    public string FileName;

    public AudioEvent(SoundType type, string fileName)
    {
      Type = type;
      FileName = fileName;
    }
  }

  public class AudioParamEvent : Stratus.Event
  {
    public string ParamName;
    public float ParamValue;

    public AudioParamEvent(string name, float value)
    {
      ParamName = name;
      ParamValue = value;
    }
  }
  
  public class AudioParamFadeEvent : Stratus.Event
  {
    public string ParamName;
    public float ParamValue;

    public AudioParamFadeEvent(string name, float value)
    {
      ParamName = name;
      ParamValue = value;
    }
  }
  
  public class AudioBankEvent : Stratus.Event
  {
    public enum LoadType
    {
      Load,
      Unload
    };
    public LoadType Type;
    public string BankName;
    
    public AudioBankEvent(LoadType type, string bankName)
    {
      Type = type;
      BankName = bankName;
    }
  }

  public AkAmbient SFXPlayer;
  public AkAmbient MusicPlayer;
  public AkAmbient AmbiencePlayer;
  public SoundbankManager soundbankManager;
  [HideInInspector]
  public bool MuteTextScroll;

	// Use this for initialization
	void Start ()
  {
        Scene.Connect<AudioEvent>(OnAudioEvent);
        Scene.Connect<AudioParamEvent>(OnAudioParamEvent);
        Scene.Connect<AudioParamFadeEvent>(OnAudioParamFadeEvent);
        Scene.Connect<AudioBankEvent>(OnAudioBankEvent);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);
        MuteTextScroll = Game.current.Progress.GetBoolValue("MuteTextScroll");
	}

  void OnAudioEvent(AudioEvent e)
  {
    if (e.FileName == "play_textscroll" && MuteTextScroll) return; // MuteTextScroll set by player in options menu
    // DEPRECATED
    //christien da sound designer's spaghettiii code: dont stop all if vertical layering is wanted
    //if (e.FileName != "play_music_tension_stem_03" &&
    //    e.FileName != "play_music_tension_stem_04" &&
    //    e.FileName != "play_music_tension_stem_05" &&
    //    e.FileName != "play_music_tension_stem_06" &&
    //    e.FileName != "play_music_tension_stem_07" &&
    //    e.FileName != "play_music_tension_intro_01" &&
    //    e.FileName != "play_music_tension_intro_02" &&
    //    e.FileName != "stop_music_tension_intro_02" &&
    //    e.FileName != "play_music_tension_intro_03" &&
    //    e.FileName != "play_music_tension_intro_04" &&
    //    e.FileName != "play_ambience_birds" &&
    //    e.FileName != "play_ambience_fireplace" &&
    //    e.FileName != "lpf_ambience_fireplace" &&
    //    e.FileName != "play_music_cafe_jazz_02"
    //   )
    if(e.Type != AudioEvent.SoundType.MLayer) // Only stop all if file not tagged as layer
    {
        AkSoundEngine.PostEvent("Stop_All", e.Type == AudioEvent.SoundType.SFX ? SFXPlayer.gameObject : (e.Type == AudioEvent.SoundType.Ambience ? AmbiencePlayer.gameObject : MusicPlayer.gameObject));
    }

    AkSoundEngine.PostEvent(e.FileName, e.Type == AudioEvent.SoundType.SFX ? SFXPlayer.gameObject : (e.Type == AudioEvent.SoundType.Ambience ? AmbiencePlayer.gameObject : MusicPlayer.gameObject));
  }

  void OnAudioParamEvent(AudioParamEvent e)
  {
    AkSoundEngine.SetRTPCValue(e.ParamName, e.ParamValue);
    setCurrentGameParam(e.ParamName, e.ParamValue);
  }
  
  void OnAudioParamFadeEvent(AudioParamFadeEvent e)
  {
    // Set game save variables instantly, so that if the game is paused/saved during a fade, it doesn't bug out
    setCurrentGameParam(e.ParamName, e.ParamValue);
    
    // Reset timer
    paramFadeTimer = 0;
    
    // Find current value of RTPC
    // 0 is Playing ID
    int type = 1; // RTPCValue_type.RTPCValue_Global
    float currentParamValue;
    AkSoundEngine.GetRTPCValue(e.ParamName, GameObject.Find("AkAmbientMusic"), 0, out currentParamValue, ref type);
    
    // Fade the value over fade time
    StartCoroutine(FadeParam(e.ParamName, currentParamValue, e.ParamValue));
  }
  
  IEnumerator FadeParam (string ParamName, float currentParamValue, float endParamValue)
  {
    while (paramFadeTimer < paramFadeTime)
    {
      paramFadeTimer += Time.deltaTime;
      float newParamValue = Mathf.Lerp(currentParamValue, endParamValue, (paramFadeTimer / paramFadeTime) );
      AkSoundEngine.SetRTPCValue(ParamName, newParamValue);
      yield return null;
    }
    
    // Hard set it after the fade is done, to correct overflow
    AkSoundEngine.SetRTPCValue(ParamName, endParamValue);
  }
  
  void setCurrentGameParam (string ParamName, float ParamValue)
  {
    Game currentGame = Game.current;
    
    switch (ParamName)
    {
      case "ambience_lpf":
        currentGame.CurrentAmbienceLPF = ParamValue;
        return;
      case "ambience_vol":
        currentGame.CurrentAmbienceVol = ParamValue;
        return;
      case "music_lpf":
        currentGame.CurrentMusicLPF = ParamValue;
        return;
      case "music_tension_state":
        currentGame.CurrentMusicTensionState = ParamValue;
        return;
      case "music_vol":
        currentGame.CurrentMusicVol = ParamValue;
        return;
      case "text_vol":
        currentGame.CurrentTextVol = ParamValue;
        return;
      default:
        return;
    }
  }
  
  void OnAudioBankEvent(AudioBankEvent e)
  {
    AudioBankEvent.LoadType lt = e.Type;
    if (lt == AudioBankEvent.LoadType.Load)
      soundbankManager.LoadBank(e.BankName, false);
    else if (lt == AudioBankEvent.LoadType.Unload)
      soundbankManager.UnloadBank(e.BankName);
  }

  void OnLoad(DefaultEvent eventdata)
  {
    Game currentGame = Game.current;
    
    if (currentGame.CurrentTrack != "" && currentGame.CurrentTrack != "Stop_All")
    {
      Trace.Script($"Loading music {Game.current.CurrentTrack}");
      AkSoundEngine.PostEvent("Stop_All", MusicPlayer.gameObject);
      AkSoundEngine.PostEvent(currentGame.CurrentTrack, MusicPlayer.gameObject);
    }
    else
    {
      Trace.Script("Stopping Music");
      AkSoundEngine.PostEvent("Stop_All", MusicPlayer.gameObject);
    }
    
    if (currentGame.CurrentAmbience != "" && currentGame.CurrentAmbience != "Stop_All")
    {
      Trace.Script($"Loading ambience {Game.current.CurrentAmbience}");
      AkSoundEngine.PostEvent("Stop_All", AmbiencePlayer.gameObject);
      AkSoundEngine.PostEvent(currentGame.CurrentAmbience, AmbiencePlayer.gameObject);
    }
    else
    {
      Trace.Script("Stopping Ambience");
      AkSoundEngine.PostEvent("Stop_All", AmbiencePlayer.gameObject);
    }
    
    // Reset RTPC values
    AkSoundEngine.SetRTPCValue("ambience_lpf", currentGame.CurrentAmbienceLPF);
    AkSoundEngine.SetRTPCValue("ambience_vol", currentGame.CurrentAmbienceVol);
    AkSoundEngine.SetRTPCValue("music_lpf", currentGame.CurrentMusicLPF);
    AkSoundEngine.SetRTPCValue("music_tension_state", currentGame.CurrentMusicTensionState);
    AkSoundEngine.SetRTPCValue("music_vol", currentGame.CurrentMusicVol);
    AkSoundEngine.SetRTPCValue("text_vol", currentGame.CurrentTextVol);
  }

  public void StopEverything()
  {
    AkSoundEngine.PostEvent("Stop_All", AmbiencePlayer.gameObject);
    AkSoundEngine.PostEvent("Stop_All", MusicPlayer.gameObject);
    AkSoundEngine.PostEvent("Stop_All", SFXPlayer.gameObject);
  }

  // Update is called once per frame
  void Update ()
  {
    AkSoundEngine.RenderAudio();
  }
}
