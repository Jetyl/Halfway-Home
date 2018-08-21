using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AK;

public class OptionsMenu : MonoBehaviour
{
    OptionsData Data;

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
        MTSToggle.isOn = Data.MuteTextScroll;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            CancelChanges();
        }
    }

    public void UpdateAll()
    {
        Data = new OptionsData(OptionsData.current);
        
        MTSToggle.isOn = Data.MuteTextScroll;
        TextSlider.value = (Data.TextSpeed - TextSpeedMin) / (TextSpeedMax - TextSpeedMin);
        MasterVolumeSlider.value = Data.MasterVolume;
        AkSoundEngine.SetRTPCValue("Master_Slider", MasterVolumeSlider.value * 100);
        MusicVolumeSlider.value = Data.MusicVolume;
        AkSoundEngine.SetRTPCValue("Music_Slider", MusicVolumeSlider.value * 100);
        SFXVolumeSlider.value = Data.SFXVolume;
        AkSoundEngine.SetRTPCValue("Effects_Slider", SFXVolumeSlider.value * 100);
        AmbianceVolumeSlider.value = Data.AmbianceVolume;
        AkSoundEngine.SetRTPCValue("Ambience_Slider", AmbianceVolumeSlider.value * 100);
        InterfaceVolumeSlider.value = Data.InterfaceVolume;
        AkSoundEngine.SetRTPCValue("Menu_Slider", InterfaceVolumeSlider.value * 100);
    }

    public void UpdateTextSpeed(float newPercent)
    {
        Data.TextSpeed =  Mathf.Lerp(TextSpeedMin, TextSpeedMax, newPercent);        
    }
    public void ToggleFullscreen(bool FSOn)
    {
        Screen.fullScreen = FSOn;
    }
    public void MuteTextScroll(bool MTSOn)
    {
        Data.MuteTextScroll = MTSOn;
        AM.MuteTextScroll = MTSOn;
    }
    public void UpdateMasterVolume(float newPercent)
    {
        Data.MasterVolume = newPercent;
        AkSoundEngine.SetRTPCValue("Master_Slider", newPercent * 100);
    }
    public void UpdateMusicVolume(float newPercent)
    {
        Data.MusicVolume = newPercent;
        AkSoundEngine.SetRTPCValue("Music_Slider", newPercent * 100);
    }
    public void UpdateSFXVolume(float newPercent)
    {
        Data.SFXVolume = newPercent;
        AkSoundEngine.SetRTPCValue("Effects_Slider", newPercent * 100);
    }
    
    public void UpdateAmbienceVolume(float newPercent)
    {
        Data.AmbianceVolume = newPercent;
        AkSoundEngine.SetRTPCValue("Ambience_Slider", newPercent * 100);
    }

    public void UpdateInterfaceVolume(float newPercent)
    {
        Data.InterfaceVolume = newPercent;
      AkSoundEngine.SetRTPCValue("Menu_Slider", newPercent * 100);
    }

    public void ConfirmNewOptions()
    {
        OptionsData.current = Data;
        OptionsData.SaveOptions();
        Space.DispatchEvent(Events.OptionsUpdated);
    }

    public void CancelChanges()
    {
        Space.DispatchEvent(Events.OptionsUpdated);
    }

}
