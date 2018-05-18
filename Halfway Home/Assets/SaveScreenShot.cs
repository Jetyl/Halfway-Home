using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScreenShot : MonoBehaviour
{

    Camera Lens;

	// Use this for initialization
	void Start ()
    {
		Lens = GetComponent<Camera>();
        Lens.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SaveShot()
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

        print(Application.persistentDataPath + "/Games_Saveshot.png");
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/Games_Saveshot.png", bytes);
    }

}
