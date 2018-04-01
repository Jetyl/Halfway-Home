using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public float TextSpeedMin = 0.5f;
    public float TextSpeedMax = 4;

    public Slider TextSlider;
    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateAll()
    {
        TextSlider.value = Game.current.Progress.GetFloatValue("TextSpeed") / TextSpeedMax;
        MasterVolumeSlider.value = Game.current.Progress.GetFloatValue("MasterVolume");
        MusicVolumeSlider.value = Game.current.Progress.GetFloatValue("BackgroundVolume");
        SFXVolumeSlider.value = Game.current.Progress.GetFloatValue("SFXVolume");

    }

    public void UpdateTextSpeed(float newPercent)
    {
        Game.current.Progress.SetValue("TextSpeed", Mathf.Lerp(TextSpeedMin, TextSpeedMax, newPercent));
    }
    public void UpdateMasterVolume(float newPercent)
    {
        Game.current.Progress.SetValue("MasterVolume", newPercent);
    }
    public void UpdateMusicVolume(float newPercent)
    {
        Game.current.Progress.SetValue("BackgroundVolume", newPercent);
    }
    public void UpdateSFXVolume(float newPercent)
    {
        Game.current.Progress.SetValue("SFXVolume", newPercent);
    }
}
