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
    AkSoundEngine.PostEvent("Stop_All", e.SFX ? SFXPlayer.gameObject : MusicPlayer.gameObject);
    AkSoundEngine.PostEvent(e.FileName, e.SFX ? SFXPlayer.gameObject : MusicPlayer.gameObject);
  }
	
	// Update is called once per frame
	void Update ()
  {
    AkSoundEngine.RenderAudio();
  }
}
