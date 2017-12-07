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

    public Button ContinueButton;

    public GameObject ConfirmationPanel;
    public GameObject ClearSavePanel;

    public GameObject FadeScreeen;

	// Use this for initialization
	void Start ()
    {
        SaveLoad.Load();

        if (SaveLoad.GetSave(0) == null) //if a save exists, set that to the main game
            ContinueButton.interactable = false;

        ConfirmationPanel.SetActive(false);
        ClearSavePanel.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void NewGame()
    {
        if (SaveLoad.GetSave(0) == null) //if a save exists, set that to the main game
        {
            ConfirmNew();
        }
        else
        {
            ClearSavePanel.SetActive(true);
        }
            
    }
    public void ContinueGame()
    {
        LoadLevel();
    }

    public void ConfirmNew()
    {

        ClearSavePanel.SetActive(false);
        SaveLoad.Delete();
        LoadLevel();
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
