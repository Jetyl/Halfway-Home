using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class OptionsData
{
    public static OptionsData current { get; set; }

    private static string path;

    public float MasterVolume;
    public float MusicVolume;
    public float SFXVolume;
    public float AmbianceVolume;
    public float InterfaceVolume;
    public bool MuteTextScroll;
    public float TextSpeed;
    

    public OptionsData()
    {
        MasterVolume = 1.0f;
        MusicVolume = 0.8f;
        SFXVolume = 0.8f;
        AmbianceVolume = 0.8f;
        InterfaceVolume = 0.8f;

        MuteTextScroll = false;
        TextSpeed = 1.0f;      

    }

    public OptionsData(OptionsData copy_)
    {

        MasterVolume = copy_.MasterVolume;
        MusicVolume = copy_.MusicVolume;
        SFXVolume = copy_.SFXVolume;
        AmbianceVolume = copy_.AmbianceVolume;
        InterfaceVolume = copy_.InterfaceVolume;

        MuteTextScroll = copy_.MuteTextScroll;
        TextSpeed = copy_.TextSpeed;
        

    }
    
    public static bool LoadOptions()
    {
        if (current != null)
            return true;

        path = Application.persistentDataPath + "/SavedOptions.gd";

        if (File.Exists(path))
        {
            // Unity JSON
            string data = File.ReadAllText(path);
            var wrap = JsonUtility.FromJson<OptionsData>(data);

            current = wrap;
            return true;
            
        }

        return false;
    }

    public static void SaveOptions()
    {
        if (current == null)
            return;

        path = Application.persistentDataPath + "/SavedOptions.gd";

        File.WriteAllText(path, JsonUtility.ToJson(current));



    }

}
