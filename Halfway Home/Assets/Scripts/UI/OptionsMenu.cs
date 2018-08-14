using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AK;

public class OptionsMenu : MonoBehaviour
{
    public float TextSpeedMin = 0.5f;
    public float TextSpeedMax = 4;

    public Slider TextSlider;
    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;
    public Slider AmbianceVolumeSlider;
    public Slider InterfaceVolumeSlider;
    public Toggle FSToggle;
    public Toggle MTSToggle;

    public AudioManager AM;

    public void Start()
    {
        UpdateAll();
        FSToggle.isOn = Screen.fullScreen;
        MTSToggle.isOn = Game.current.Progress.GetBoolValue("MuteTextScroll");
    }

    public void UpdateAll()
    {
        TextSlider.value = (Game.current.Progress.GetFloatValue("TextSpeed") - TextSpeedMin) / (TextSpeedMax - TextSpeedMin);
        MasterVolumeSlider.value = Game.current.Progress.GetFloatValue("MasterVolume");
        AkSoundEngine.SetRTPCValue("Master_Slider", MasterVolumeSlider.value * 100);
        MusicVolumeSlider.value = Game.current.Progress.GetFloatValue("MusicVolume");
        AkSoundEngine.SetRTPCValue("Music_Slider", MusicVolumeSlider.value * 100);
        SFXVolumeSlider.value = Game.current.Progress.GetFloatValue("SFXVolume");
        AkSoundEngine.SetRTPCValue("Effects_Slider", SFXVolumeSlider.value * 100);
        AmbianceVolumeSlider.value = Game.current.Progress.GetFloatValue("AmbianceVolume");
        AkSoundEngine.SetRTPCValue("Ambience_Slider", AmbianceVolumeSlider.value * 100);
        InterfaceVolumeSlider.value = Game.current.Progress.GetFloatValue("InterfaceVolume");
        AkSoundEngine.SetRTPCValue("Menu_Slider", InterfaceVolumeSlider.value * 100);
    }

    public void UpdateTextSpeed(float newPercent)
    {
        Game.current.Progress.SetValue("TextSpeed", Mathf.Lerp(TextSpeedMin, TextSpeedMax, newPercent));        
    }
    public void ToggleFullscreen(bool FSOn)
    {
        Screen.fullScreen = FSOn;
    }
    public void MuteTextScroll(bool MTSOn)
    {
        Game.current.Progress.SetValue("MuteTextScroll", MTSOn);
        AM.MuteTextScroll = MTSOn;
    }
    public void UpdateMasterVolume(float newPercent)
    {
        Game.current.Progress.SetValue("MasterVolume", newPercent);
        AkSoundEngine.SetRTPCValue("Master_Slider", newPercent * 100);
    }
    public void UpdateMusicVolume(float newPercent)
    {
        Game.current.Progress.SetValue("MusicVolume", newPercent);
        AkSoundEngine.SetRTPCValue("Music_Slider", newPercent * 100);
    }
    public void UpdateSFXVolume(float newPercent)
    {
        Game.current.Progress.SetValue("SFXVolume", newPercent);
        AkSoundEngine.SetRTPCValue("Effects_Slider", newPercent * 100);
    }
    
    public void UpdateAmbienceVolume(float newPercent)
    {
        Game.current.Progress.SetValue("AmbianceVolume", newPercent);
        AkSoundEngine.SetRTPCValue("Ambience_Slider", newPercent * 100);
    }

    public void UpdateInterfaceVolume(float newPercent)
    {
      Game.current.Progress.SetValue("InterfaceVolume", newPercent);
      AkSoundEngine.SetRTPCValue("Menu_Slider", newPercent * 100);
    }
}
