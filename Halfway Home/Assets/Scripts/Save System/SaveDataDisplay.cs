﻿/******************************************************************************/
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
    public bool IsLoad;

    public Image Screenshot;
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI RealDate;
    public TextMeshProUGUI GameTime;
    public TextMeshProUGUI RealTime;
    [Header("Stats")]
    public Graphic[] AStars;
    public Graphic[] GStars;
    public Graphic[] EStars;
    public Slider FSlider;
    public Slider SSlider;
    public Slider DSlider;
    public Image Icon;
    [Header("Colors")]
    [Range(0, 1)]
    public float EmptyAlpha;
    public Color FilledStar;
    public Color EmptyStar;
    [Header("Icons")]
    public Sprite Save;
    public Sprite Overwrite;
    [Space]
    [Tooltip("Only necessary for Load Display.")]
    public Button Delete;

    void Start()
    {

      Space.Connect<DefaultEvent>(Events.PostSave, OnSave);
      UpdateDisplay();

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
        //Debug.Log($"Save for Slot {DataIndex} is {gameData != null}");

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

            //JESSE, REPLACE 0 VALUES WITH SAVE DATA //done
            var awarenessTier = gameData.Self.GetTrueSocialStat(Personality.Social.Awareness);
            var graceTier = gameData.Self.GetTrueSocialStat(Personality.Social.Grace);
            var expressionTier = gameData.Self.GetTrueSocialStat(Personality.Social.Expression);

            for(int i = 0; i < AStars.Length; i++) 
            {
              AStars[i].color = (i < awarenessTier ? FilledStar : EmptyStar);
            }

            for (int i = 0; i < GStars.Length; i++)
            {
              GStars[i].color = (i < graceTier ? FilledStar : EmptyStar);
            }

            for (int i = 0; i < EStars.Length; i++)
            {
              EStars[i].color = (i < expressionTier ? FilledStar : EmptyStar);
            }

            //JESSE, REPLACE 0 VALUES WITH SAVE DATA //done
            FSlider.value = gameData.Self.GetWellbingStat(Personality.Wellbeing.Fatigue);
            SSlider.value = gameData.Self.GetWellbingStat(Personality.Wellbeing.Stress);
            DSlider.value = gameData.Self.GetWellbingStat(Personality.Wellbeing.Depression);

            if (IsLoad)
            {
              Screenshot.GetComponent<Button>().interactable = true;
              Screenshot.raycastTarget = true;
              Delete.interactable = true;
            } 
            else Icon.sprite = Overwrite;

            SetChildAlpha(1);
        }
        else
        {
            Screenshot.sprite = null;
            PlayerName.text = "???";

            RealDate.text = "-/-/-";

            GameTime.text = "???";

            RealTime.text = "-:-";

            if (IsLoad)
            {
              Screenshot.GetComponent<Button>().interactable = false;
              Screenshot.raycastTarget = false;
              Delete.OnDeselect(null);
              Delete.interactable = false;
            } 
            else Icon.sprite = Save;

            SetChildAlpha(EmptyAlpha);

            FSlider.value = 0;
            SSlider.value = 0;
            DSlider.value = 0;

            for (int i = 0; i < AStars.Length; i++)
            {
                AStars[i].color = EmptyStar;
            }

            for (int i = 0; i < GStars.Length; i++)
            {
                GStars[i].color = EmptyStar;
            }

            for (int i = 0; i < EStars.Length; i++)
            {
                EStars[i].color = EmptyStar;
            }

        }


    }

    public void PleaseWork()
    {
        StartCoroutine(ScreenshotUpdate());
    }

    // For fading out when returning to title
    IEnumerator ScreenshotUpdate()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        Screenshot.enabled = false;
        Screenshot.enabled = true;
    }

    void SetChildAlpha(float alpha)
    {
      foreach(Graphic g in GetComponentsInChildren<Graphic>())
      {
        g.CrossFadeAlpha(alpha, 0f, true);
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
