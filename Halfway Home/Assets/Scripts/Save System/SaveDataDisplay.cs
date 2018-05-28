/******************************************************************************/
/*!
File:   SaveDataDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SaveDataDisplay : MonoBehaviour
{

    public int DataIndex;

    public Image Screenshot;
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI RealDate;
    public TextMeshProUGUI GameTime;
    public TextMeshProUGUI RealTime;


	// Use this for initialization
	void Start ()
    {

        

        UpdateDisplay();

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnEnable()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {

        Game gameData = SaveLoad.GetSave(DataIndex);


        if (gameData != null)
        {

            string path = Application.persistentDataPath + "/Games_Saveshot_" + DataIndex + ".png";

            if (File.Exists(path))
            {
                var bytes = File.ReadAllBytes(path);
                
                Texture2D test = new Texture2D((int)Screenshot.preferredWidth, (int)Screenshot.preferredHeight);
                test.LoadRawTextureData(bytes);
                Screenshot.sprite = Sprite.Create(test, new Rect(0, 0, 128, 128), Screenshot.rectTransform.pivot);
                
            }

            PlayerName.text = gameData.PlayerName;

            RealDate.text = "" + gameData.SaveStamp.Month + "/" + gameData.SaveStamp.Day + "/" + gameData.SaveStamp.Year;

            GameTime.text = "Day:" + gameData.Day + " Hour:" + gameData.Hour;

            RealTime.text = "" + gameData.SaveStamp.Hour + ":" + gameData.SaveStamp.Minute + ":" + gameData.SaveStamp.Second;

           

        }
        


    }
}
