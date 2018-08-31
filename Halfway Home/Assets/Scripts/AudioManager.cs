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
using Stratus.Modules.InkModule;
using HalfwayHome;

public class AudioManager : MonoBehaviour
{
  float paramFadeTimer = 0;
  public float paramFadeTime = 2.0f;

  // Used to make sure room default tracks are called before normal tracks
  private List<string[]> normalTracks = new List<string[]> {};
  private List<string[]> defaultTracks = new List<string[]> {};
  private List<string> banksToLoad = new List<string> {};
  private List<string> banksToUnload = new List<string> {};
  private float dispatchAudioTimer = 0;        // Used to keep track of the intervals at which audio events are dispatched
  private float dispatchAudioInterval = 0.2f;  // The interval between each batch of dispatched audio events

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
    public bool isDefaultTrack = false;

    public AudioEvent(SoundType type, string fileName, bool defaultTrack = false)
    {
      Type = type;
      FileName = fileName;
      isDefaultTrack = defaultTrack;
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
  private bool Skipping;

	// Use this for initialization
	void Start ()
  {
        Scene.Connect<AudioEvent>(OnAudioEvent);
        Scene.Connect<AudioParamEvent>(OnAudioParamEvent);
        Scene.Connect<AudioParamFadeEvent>(OnAudioParamFadeEvent);
        Scene.Connect<AudioBankEvent>(OnAudioBankEvent);
        
        HalfwayHomeStoryLoader hhsl = GameObject.Find("Dialog Display").GetComponent<HalfwayHomeStoryLoader>();
        hhsl.reader.gameObject.Connect<Story.StartedEvent>(this.OnStoryStartedEvent);
        
        Space.Connect<ConversationEvent>(Events.StartGame, OnStart);
        Space.Connect<DefaultEvent>(Events.OptionsUpdated, OnOptionsUpdate);

        Space.Connect<DescriptionEvent>(Events.Description, OnDescriptionEvent);
        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
        Space.Connect<DefaultEvent>(Events.UnPause, OnUnPause);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);
        Space.Connect<DefaultEvent>(Events.SkipTyping, OnSkipTyping);
        Space.Connect<DefaultEvent>(Events.StopSkipTyping, OnStopSkipTyping);

        MuteTextScroll = OptionsData.current.MuteTextScroll; //Game.current.Progress.GetBoolValue("MuteTextScroll");
        Skipping = false;
	}
  
  void Awake ()
  {
    AkSoundEngine.SetState("Game_State", "In_Game");
  }

    public void OnStart(ConversationEvent eventdata)
    {
        RefreshingOptionsData();
    }

    public void OnOptionsUpdate(DefaultEvent eventdata)
    {
        RefreshingOptionsData();
    }

    public void RefreshingOptionsData()
    {
        var Data = new OptionsData(OptionsData.current);

        MuteTextScroll = Data.MuteTextScroll;
        AkSoundEngine.SetRTPCValue("Master_Slider", Data.MasterVolume * 100);
        AkSoundEngine.SetRTPCValue("Music_Slider", Data.MusicVolume * 100);
        AkSoundEngine.SetRTPCValue("Effects_Slider", Data.SFXVolume * 100);
        AkSoundEngine.SetRTPCValue("Ambience_Slider", Data.AmbianceVolume * 100);
        AkSoundEngine.SetRTPCValue("Menu_Slider", Data.InterfaceVolume * 100);
    }

    void OnStoryStartedEvent(Story.StartedEvent e)
  {
    Trace.Script($"-----STORY STARTED: Resetting RTPC values-----");
    
    AkSoundEngine.SetRTPCValue("ambience_lpf", 0);
    AkSoundEngine.SetRTPCValue("ambience_vol", 0);
    AkSoundEngine.SetRTPCValue("music_lpf", 0);
    AkSoundEngine.SetRTPCValue("music_tension_state", 0);
    AkSoundEngine.SetRTPCValue("music_vol", 0);
    AkSoundEngine.SetRTPCValue("text_vol", 0);
  }

  void OnSkipTyping(DefaultEvent e)
  {
    Skipping = true;
  }

  void OnStopSkipTyping(DefaultEvent e)
  {
    Skipping = false;
  }
  
  void OnDescriptionEvent(DescriptionEvent e)
  {
    string sp = e.Speaker;
    
    // If it's monologue
    if (sp == "")
      AkSoundEngine.SetState("Text_Type", "Monologue");

    // If the player is speaking
    else if (sp == Game.current.PlayerName)
      AkSoundEngine.SetState("Text_Type", "Player");
    
    // If someone else is speaking
    else
      AkSoundEngine.SetState("Text_Type", "Other");
  }
  
  void OnPause(DefaultEvent eventdata)
  {
    AkSoundEngine.PostEvent("play_menupause", SFXPlayer.gameObject);
    AkSoundEngine.SetState("Pause", "Paused");
  }
  
  void OnUnPause(DefaultEvent eventdata)
  {
    AkSoundEngine.PostEvent("play_menuunpause", SFXPlayer.gameObject);
    AkSoundEngine.SetState("Pause", "Unpaused");
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
    
    // Post the event immediately if its a sound effect (ignore if skipping)
    if (e.Type == AudioEvent.SoundType.SFX && !Skipping)
      AkSoundEngine.PostEvent(e.FileName, SFXPlayer.gameObject);

    // Otherwise, collect the event and dispatch all events after an interval in the right order
    else if (e.isDefaultTrack == false)
      normalTracks.Add(new string[] {e.FileName, e.Type.ToString()});
    else if (e.isDefaultTrack == true)
      defaultTracks.Add(new string[] {e.FileName, e.Type.ToString()});
  }

  // Making sure audio calls occur in the right order
  void DispatchAudioEvents()
  {
    // Load banks
    DispatchLoadBanks();
    
    // Call default room tracks
    foreach (string[] e in defaultTracks)
    {
      CallAudioEvent(e);
      
      //DEPRECATED
      //  // Hard-coded LPF for fireplace ambience far away from Commons
      //  if (e[0] == "play_ambience_fireplace_far")
      //  {
      //    AudioParamFadeEvent lpfEvent = new AudioParamFadeEvent ("ambience_lpf", 30);
      //    AudioParamFadeEvent volEvent = new AudioParamFadeEvent ("ambience_vol", -2.1f);
      //    
      //    OnAudioParamFadeEvent(lpfEvent);
      //    OnAudioParamFadeEvent(volEvent);
      //  }
      //  
      //  // Reset LPF for fireplace in commons
      //  else if (e[0] == "play_ambience_fireplace")
      //  {
      //    AudioParamFadeEvent lpfEvent = new AudioParamFadeEvent ("ambience_lpf", 0);
      //    AudioParamFadeEvent volEvent = new AudioParamFadeEvent ("ambience_vol", 0);
      //    
      //    OnAudioParamFadeEvent(lpfEvent);
      //    OnAudioParamFadeEvent(volEvent);
      //  }
    }

    // Call other events
    foreach (string[] e in normalTracks)
      CallAudioEvent(e);
    
    // Unload banks
    DispatchUnloadBanks();
    
    // Reset lists
    defaultTracks.Clear();
    normalTracks.Clear();
    banksToLoad.Clear();
    banksToUnload.Clear();
  }

  void CallAudioEvent(string[] e)
  {
    switch (e[1])
    {
      case "Music":
        AkSoundEngine.PostEvent("Stop_All", MusicPlayer.gameObject);
        AkSoundEngine.PostEvent(e[0], MusicPlayer.gameObject);
        Game.current.CurrentTrack = e[0];
        Trace.Script($"Loading music {Game.current.CurrentTrack}");
        return;
      case "Ambience":
        AkSoundEngine.PostEvent("Stop_All", AmbiencePlayer.gameObject);
        AkSoundEngine.PostEvent(e[0], AmbiencePlayer.gameObject);
        Game.current.CurrentAmbience = e[0];
        Trace.Script($"Loading ambience {Game.current.CurrentAmbience}");
        return;
      case "MLayer":
        AkSoundEngine.PostEvent(e[0], MusicPlayer.gameObject);
        //Game.current.CurrentTrack = e[0];
        Trace.Script($"Layering music {Game.current.CurrentTrack}");
        return;
      case "ALayer":
        AkSoundEngine.PostEvent(e[0], AmbiencePlayer.gameObject);
        //Game.current.CurrentAmbience = e[0];
        Trace.Script($"Layering ambience {Game.current.CurrentAmbience}");
        return;
      default:
        Trace.Script($"Error loading {e[0]}");
        return;
    }
  }

  void OnAudioParamEvent(AudioParamEvent e)
  {
    Trace.Script($"Setting {e.ParamName} to {e.ParamValue}");
    
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
    if (e.BankName == null || e.BankName == "")
      return;
    
    AudioBankEvent.LoadType lt = e.Type;
    if (lt == AudioBankEvent.LoadType.Load)
      banksToLoad.Add(e.BankName);
    else if (lt == AudioBankEvent.LoadType.Unload)
      banksToUnload.Add(e.BankName);
  }
  
  void DispatchLoadBanks()
  {
    foreach (string bank in banksToLoad)
      soundbankManager.LoadBank(bank, false);
  }
  
  void DispatchUnloadBanks()
  {
    foreach (string bank in banksToUnload)
      soundbankManager.UnloadBank(bank);
  }

  void OnLoad(DefaultEvent eventdata)
  {
    // Reset pause menu mute
    AkSoundEngine.SetState("Pause", "Unpaused");
    
    Game currentGame = Game.current;
    
    // Load sound banks for current story and room
    soundbankManager.InitBanks();
    
    Debug.Log("          CURRENT STORY SOUNDBANK:" + currentGame.CurrentStorySoundbank);
    
    Trace.Script($"Loading music {Game.current.CurrentTrack}");
    //AkSoundEngine.PostEvent("Stop_All", MusicPlayer.gameObject);
    //AkSoundEngine.PostEvent(currentGame.CurrentTrack, MusicPlayer.gameObject);
    string[] newTrack = new string[] {currentGame.CurrentTrack, "Music"};
    normalTracks.Add(newTrack);
    
    Trace.Script($"Loading ambience {Game.current.CurrentAmbience}");
    //AkSoundEngine.PostEvent("Stop_All", AmbiencePlayer.gameObject);
    //AkSoundEngine.PostEvent(currentGame.CurrentAmbience, AmbiencePlayer.gameObject);
    string[] newAmbience = new string[] {currentGame.CurrentAmbience, "Ambience"};
    normalTracks.Add(newAmbience);
    
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
    // Collect incoming audio events, and dispatch them at intervals
    // to make sure default room tracks are called before normal tracks
    dispatchAudioTimer += Time.deltaTime;
    if (dispatchAudioTimer >= dispatchAudioInterval)
    {
      dispatchAudioTimer = 0;
      DispatchAudioEvents();
    }

    AkSoundEngine.RenderAudio();
  }
  
  public void PlayMenuHoverSFX()
  {
    AkSoundEngine.PostEvent("play_menumouseover", SFXPlayer.gameObject);
  }
  
  public void PlayMenuClickSFX()
  {
    AkSoundEngine.PostEvent("play_menuclick", SFXPlayer.gameObject);
  }
  
  public void PlayMenuSliderScrollSFX(string slider)
  {
    switch (slider.ToLower())
    {
      case "ambience":
        AkSoundEngine.PostEvent("play_sfx_menu_ambience_slider_scroll", SFXPlayer.gameObject);
        return;
      case "sfx":
      case "effects":
        AkSoundEngine.PostEvent("play_sfx_menu_effects_slider_scroll", SFXPlayer.gameObject);
        return;
      case "menu":
      case "interface":
        AkSoundEngine.PostEvent("play_sfx_menu_interface_slider_scroll", SFXPlayer.gameObject);
        return;
      case "master":
        AkSoundEngine.PostEvent("play_sfx_menu_master_slider_scroll", SFXPlayer.gameObject);
        return;
      case "music":
        AkSoundEngine.PostEvent("play_sfx_menu_music_slider_scroll", SFXPlayer.gameObject);
        return;
      default:
        AkSoundEngine.PostEvent("play_sfx_menu_slider_scroll", SFXPlayer.gameObject);
        return;
    }
  }
}
