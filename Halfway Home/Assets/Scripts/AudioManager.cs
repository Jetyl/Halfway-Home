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
  public class AudioEvent : Stratus.Event
  {
    public bool SFX;
    public string FileName;

    public AudioEvent(bool sfx, string fileName)
    {
      SFX = sfx;
      FileName = fileName;
    }
  };

  public AkAmbient SFXPlayer;
  public AkAmbient MusicPlayer;

	// Use this for initialization
	void Start ()
  {
    Scene.Connect<AudioEvent>(OnAudioEvent);
	}

  void OnAudioEvent(AudioEvent e)
  {
    // christien da sound designer's spaghettiii code: dont stop all if vertical layering is wanted
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
        e.FileName != "lpf_ambience_fireplace")
    {
        AkSoundEngine.PostEvent("Stop_All", e.SFX ? SFXPlayer.gameObject : MusicPlayer.gameObject);
    }
    
    AkSoundEngine.PostEvent(e.FileName, e.SFX ? SFXPlayer.gameObject : MusicPlayer.gameObject);
  }
	
	// Update is called once per frame
	void Update ()
  {
    AkSoundEngine.RenderAudio();
  }
}
