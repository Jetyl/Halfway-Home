/******************************************************************************/
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

    public GameObject Buttons;
    public Button ContinueButton;

    public GameObject ConfirmationPanel;
    public GameObject ClearSavePanel;
    public GameObject LoadPanel;
    public GameObject GalleryPanel;

    public GameObject FadeScreeen;

    public StatBasedSpriteChanger FlowerA;
    public StatBasedSpriteChanger FlowerG;
    public StatBasedSpriteChanger FlowerE;
    
    public float StartGameFadeTime = 1.0f;
    public float CreditsFadeTime = 2.1f;

  // Use this for initialization
  void Start ()
    {
        //SaveLoad.Load();

        //default the game.current to no save
        //Game.current = null;

        //if a save exists, set that to the main game
        if (SaveLoad.GetSize() == 0)
        {
            ContinueButton.interactable = false;
            FlowerA.SetState(0);
            FlowerG.SetState(0);
            FlowerE.SetState(0);
        }
        else
        {
            FlowerA.SetState(GetStateLevel(Personality.Social.Awareness));
            FlowerG.SetState(GetStateLevel(Personality.Social.Grace));
            FlowerE.SetState(GetStateLevel(Personality.Social.Expression));
        }


        ConfirmationPanel.SetActive(false);
        ClearSavePanel.SetActive(false);
        LoadPanel.SetActive(false);
        GalleryPanel.SetActive(false);
        
        
    }
    
    void Awake()
    {
        // Load Soundbanks
        Debug.Log("MAIN MENU SOUNDBANKS LOADED");
        AkBankManager.LoadBankAsync("MainMenu");
        AkBankManager.LoadBankAsync("Master");
        
        // Reset RTPCs
        AkSoundEngine.SetRTPCValue("music_lpf", 0);
        AkSoundEngine.SetRTPCValue("music_vol", 0);
    }

    public void NewGame()
    {
        ConfirmNew();
            
    }
    public void ContinueGame()
    {
        LoadLevel();
    }

    public void ConfirmNew()
    {

        ClearSavePanel.SetActive(false);
        //SaveLoad.Delete();
        Game.current = null;
        LoadLevel();
    }
    
    public void LoadGame()
    {
      LoadPanel.SetActive(true);
      Buttons.SetActive(false);
    }

    public void OpenGallery()
    {
      GalleryPanel.SetActive(true);
      Buttons.SetActive(false);
    }

    public void Credits()
    {
      AkSoundEngine.PostEvent("Stop_All", GameObject.Find("Music"));
      StartCoroutine(LoadCredits(CreditsFadeTime));
    }
    
    IEnumerator LoadCredits(float aTime)
    {
        GameObject.Find("Fade").DispatchEvent(Events.Fade, new FadeEvent(Color.black, aTime));
        yield return new WaitForSeconds(aTime);
        
        // Unload SoundBanks
        AkBankManager.UnloadBank("MainMenu");
        AkBankManager.UnloadBank("Master");
        
        SceneManager.LoadScene("Credits");

    }

    void LoadLevel()
    {
        StartCoroutine(LoadLevel(StartGameFadeTime));
    }

    public void LoadAt(int slot)
    {
      Game.current = SaveLoad.GetSave(slot);
      LoadLevel();
    }

    IEnumerator LoadLevel(float aTime)
    {
        GameObject.Find("Fade").DispatchEvent(Events.Fade, new FadeEvent(Color.black, aTime));
        yield return new WaitForSeconds(aTime);
        
        // Unload SoundBanks
        AkBankManager.UnloadBank("MainMenu");
        AkBankManager.UnloadBank("Master");
        
        SceneManager.LoadScene(MainLevel);

    }

    public void DeleteSaveSlot(int slot)
    {
      SaveLoad.DeleteAt(slot);
    }

    public int GetStateLevel(Personality.Social stat)
    {
        int level = 0;
        for(int i = 0; i < SaveLoad.GetSize(); ++i)
        {
            if (SaveLoad.GetSave(i) == null)
              continue;

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
