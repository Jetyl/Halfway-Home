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

        Space.Connect<DefaultEvent>(Events.PostSave, OnSave);

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

    void OnSave(DefaultEvent eventdata)
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
                
                Texture2D test = new Texture2D(2, 2);
                test.LoadImage(bytes); //this will resize it

                Screenshot.sprite = Sprite.Create(test, new Rect(0, 0, test.width, test.height), new Vector2(0,0));
                
            }

            PlayerName.text = gameData.PlayerName;

            RealDate.text = "" + gameData.SaveStamp.Month + "/" + gameData.SaveStamp.Day + "/" + gameData.SaveStamp.Year;

            GameTime.text = "" + (System.DayOfWeek)gameData.Day;

            RealTime.text = GetTime(gameData.SaveStamp.Hour, gameData.SaveStamp.Minute);

           

        }
        


    }

    string GetTime(int hour, int min)
    {
        string sHour;
        string sMin;
        string sNoon;

        if (hour < 12)
        {
            sNoon = "AM";
            if (hour == 0)
                sHour = "12";
            else
                sHour = hour + "";

        }
        else
        {
            sNoon = "PM";
            if (hour == 12)
                sHour = "12";
            else
                sHour = (hour - 12) + "";
        }

        if (min < 10)
            sMin = "0" + min;
        else
            sMin = "" + min;
            

        return sHour + ":" + sMin + " " + sNoon;

    }
}
