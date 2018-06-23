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
    public Slider AmbienceVolumeSlider;

    public void UpdateAll()
    {
        TextSlider.value = (Game.current.Progress.GetFloatValue("TextSpeed") - TextSpeedMin) / TextSpeedMax;
        MasterVolumeSlider.value = Game.current.Progress.GetFloatValue("MasterVolume");
        AkSoundEngine.SetRTPCValue("Master_Slider", MasterVolumeSlider.value * 100);
        MusicVolumeSlider.value = Game.current.Progress.GetFloatValue("BackgroundVolume");
        AkSoundEngine.SetRTPCValue("Music_Slider", MusicVolumeSlider.value * 100);
        SFXVolumeSlider.value = Game.current.Progress.GetFloatValue("SFXVolume");
        AkSoundEngine.SetRTPCValue("Effects_Slider", SFXVolumeSlider.value * 100);
        AmbienceVolumeSlider.value = Game.current.Progress.GetFloatValue("AmbienceVolume");
        AkSoundEngine.SetRTPCValue("Ambience_Slider", AmbienceVolumeSlider.value * 100);
    }

    public void UpdateTextSpeed(float newPercent)
    {
        Game.current.Progress.SetValue("TextSpeed", Mathf.Lerp(TextSpeedMin, TextSpeedMax, newPercent));        
    }
    public void UpdateMasterVolume(float newPercent)
    {
        Game.current.Progress.SetValue("MasterVolume", newPercent);
        AkSoundEngine.SetRTPCValue("Master_Slider", newPercent * 100);
    }
    public void UpdateMusicVolume(float newPercent)
    {
        Game.current.Progress.SetValue("BackgroundVolume", newPercent);
        AkSoundEngine.SetRTPCValue("Music_Slider", newPercent * 100);
    }
    public void UpdateSFXVolume(float newPercent)
    {
        Game.current.Progress.SetValue("SFXVolume", newPercent);
        AkSoundEngine.SetRTPCValue("Effects_Slider", newPercent * 100);
    }
    
    public void UpdateAmbienceVolume(float newPercent)
    {
        Game.current.Progress.SetValue("AmbienceVolume", newPercent);
        AkSoundEngine.SetRTPCValue("Ambience_Slider", newPercent * 100);
    }
}
