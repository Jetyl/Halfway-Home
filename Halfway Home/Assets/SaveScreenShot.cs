using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveScreenShot : MonoBehaviour
{

    Camera Lens;

	// Use this for initialization
	void Start ()
    {
		Lens = GetComponent<Camera>();
        Lens.enabled = false;

        Space.Connect<DefaultEvent>(Events.PostSave, SaveShot);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SaveShot(DefaultEvent eventdata)
    {
        var rend = new RenderTexture(Screen.width, Screen.height, 24);
        
        Lens.enabled = true;
        Lens.targetTexture = rend;
        Lens.Render();
        RenderTexture.active = rend;
        Lens.enabled = false;

        int width = Screen.width;
        int height = Screen.height;

        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();

        //var timestamp = DateTime.Now;

        //var second = timestamp.Second;
        //var minute = timestamp.Minute;
        //var hour = timestamp.Hour;
        //var Day = timestamp.DayOfYear;
        //var year = timestamp.Year;

        var index = SaveLoad.GetIndex(Game.current);

        print(Application.persistentDataPath + "/Games_Saveshot_" + index + ".png");
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/Games_Saveshot_" + index + ".png", bytes);
    }

}
