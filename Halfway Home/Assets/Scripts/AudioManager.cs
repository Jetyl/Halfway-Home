﻿/******************************************************************************/
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
  public class AudioEvent : Stratus.Event
  {
    public enum SoundType
    {
      Music,
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
  };

  public AkAmbient SFXPlayer;
  public AkAmbient MusicPlayer;
  public AkAmbient AmbiencePlayer;

	// Use this for initialization
	void Start ()
  {
        Scene.Connect<AudioEvent>(OnAudioEvent);
        Space.Connect<DefaultEvent>(Events.Load, OnLoad);
	}

  void OnAudioEvent(AudioEvent e)
  {
    //christien da sound designer's spaghettiii code: dont stop all if vertical layering is wanted
    if (e.FileName != "play_music_tension_stem_03" &&
        e.FileName != "play_music_tension_stem_04" &&
        e.FileName != "play_music_tension_stem_05" &&
        e.FileName != "play_music_tension_stem_06" &&
        e.FileName != "play_music_tension_stem_07" &&
        e.FileName != "play_music_tension_intro_01" &&
        e.FileName != "play_music_tension_intro_02" &&
        e.FileName != "stop_music_tension_intro_02" &&
        e.FileName != "play_music_tension_intro_03" &&
        e.FileName != "play_music_tension_intro_04" &&
        e.FileName != "play_ambience_birds" &&
        e.FileName != "play_ambience_fireplace" &&
        e.FileName != "lpf_ambience_fireplace" &&
        e.FileName != "play_music_cafe_jazz_02"
       )
    {
        AkSoundEngine.PostEvent("Stop_All", e.Type == AudioEvent.SoundType.SFX ? SFXPlayer.gameObject : (e.Type == AudioEvent.SoundType.Music ? MusicPlayer.gameObject : AmbiencePlayer.gameObject));
    }

    AkSoundEngine.PostEvent(e.FileName, e.Type == AudioEvent.SoundType.SFX ? SFXPlayer.gameObject : (e.Type == AudioEvent.SoundType.Music ? MusicPlayer.gameObject : AmbiencePlayer.gameObject));
  }

  void OnLoad(DefaultEvent eventdata)
  {
    
    if (Game.current.CurrentTrack != "" && Game.current.CurrentTrack != "Stop_All")
    {
      Trace.Script($"Loading music {Game.current.CurrentTrack}");
      AkSoundEngine.PostEvent("Stop_All", MusicPlayer.gameObject);
      AkSoundEngine.PostEvent(Game.current.CurrentTrack, MusicPlayer.gameObject);
    }
    else
    {
      Trace.Script("Stopping Music");
      AkSoundEngine.PostEvent("Stop_All", MusicPlayer.gameObject);
    }
    
    if (Game.current.CurrentAmbience != "" && Game.current.CurrentAmbience != "Stop_All")
    {
      Trace.Script($"Loading ambience {Game.current.CurrentAmbience}");
      AkSoundEngine.PostEvent("Stop_All", AmbiencePlayer.gameObject);
      AkSoundEngine.PostEvent(Game.current.CurrentAmbience, AmbiencePlayer.gameObject);
    }
    else
    {
      Trace.Script("Stopping Ambience");
      AkSoundEngine.PostEvent("Stop_All", AmbiencePlayer.gameObject);
    }
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
