﻿/******************************************************************************/
/*!
File:   MainMenu.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public int MainLevel = 1;

    public Button ContinueButton;

    public GameObject ConfirmationPanel;
    public GameObject ClearSavePanel;

    public GameObject FadeScreeen;

    public StatBasedSpriteChanger FlowerA;
    public StatBasedSpriteChanger FlowerG;
    public StatBasedSpriteChanger FlowerE;

  // Use this for initialization
  void Start ()
    {
        SaveLoad.Load();
        
        //if a save exists, set that to the main game
        if (SaveLoad.GetSave(0) == null)
        {
            ContinueButton.interactable = false;
            FlowerA.SetState(0);
            FlowerG.SetState(0);
            FlowerE.SetState(0);
        }
        else
        {
            // JESSE PLS FIX //Done
            FlowerA.SetState(GetStateLevel(Personality.Social.Awareness));
            FlowerG.SetState(GetStateLevel(Personality.Social.Grace));
            FlowerE.SetState(GetStateLevel(Personality.Social.Expression));
        }


    ConfirmationPanel.SetActive(false);
        ClearSavePanel.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void NewGame()
    {
        //if (SaveLoad.GetSave(0) == null) //if a save exists, set that to the main game
       // {
            ConfirmNew();
       // }
        //else
        //{
        //    ClearSavePanel.SetActive(true);
        //}
            
    }
    public void ContinueGame()
    {
        LoadLevel();
    }

    public void ConfirmNew()
    {

        ClearSavePanel.SetActive(false);
        SaveLoad.Delete();
        Game.current = null;
        LoadLevel();
    }

    public void Credits()
    {
      SceneManager.LoadScene("Credits");
    }

    void LoadLevel()
    {
        //iTween.CameraFadeAdd();
        //SceneManager.LoadScene(MainLevel);
        
        StartCoroutine(LoadLevel(1));

    }


    IEnumerator LoadLevel(float aTime)
    {
        Instantiate(FadeScreeen, transform.parent);
        yield return new WaitForSeconds(aTime);
        
        SceneManager.LoadScene(MainLevel);

    }

    public int GetStateLevel(Personality.Social stat)
    {
        int level = 0;
        for(int i = 0; i < SaveLoad.GetSize(); ++i)
        {
            int lv = SaveLoad.GetSave(i).Self.GetTrueSocialStat(stat);
            if (lv > level)
                level = lv;
        }

        return level;
    }

    public void ConfirmQuit()
    {
        ConfirmationPanel.SetActive(true);
    }

    public void Clear()
    {
        SaveLoad.Delete();
    }
    public void Quit()
    {
        Application.Quit();
    }

}
