/******************************************************************************/
/*!
File:   PauseMenu.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  public HalfwayHome.HalfwayHomeStoryReader ReaderReference;

    public GameObject Pause;
    public GameObject SubmenuBackground;
    public GameObject Confirm;
    public GameObject OptionsPage;
    public GameObject ToDoPage;
    public GameObject SavePage;
    public GameObject LoadPage;

    bool Quiting;

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
        Confirm.SetActive(false);
    }

    public void Continue()
    {
        Space.DispatchEvent(Events.UnPause);
    }

    public void OpenSave()
    {
        SubmenuBackground.SetActive(true);
        Pause.SetActive(false);
        SavePage.SetActive(true);
    }
    
    public void SaveGame(int slot)
    {
        Space.DispatchEvent(Events.Save);
        ReaderReference.Save();
        SaveLoad.SaveAt(slot);
        ReaderReference.LoadSave();
        //Space.DispatchEvent(Events.PostSave, new SaveShotEvent(slot));
        StartCoroutine(
        TextParser.FrameDelay<SaveShotEvent>(Events.PostSave, new SaveShotEvent(slot)));
    }
    
    public void LoadGame(int slot)
    {
        Game data = SaveLoad.GetSave(slot);

        if(data != null)
        {
            Game.current = SaveLoad.GetSave(slot);
            ReaderReference.Clear();
            //ReaderReference.LoadSave();
            SceneManager.LoadScene(1);
        }

        
    }

    public void DeleteSaveSlot(int slot)
    {
        SaveLoad.DeleteAt(slot);
    }

    public void OldSaveGame()
    {
        // old save
        Space.DispatchEvent(Events.Save);
        SaveLoad.Save();
        ReaderReference.Save();

    }

    public void OpenLoad()
    {
        SubmenuBackground.SetActive(true);
        Pause.SetActive(false);
        LoadPage.SetActive(true);
    }

    public void OpenToDo()
    {
        SubmenuBackground.SetActive(true);
        Pause.SetActive(false);
        ToDoPage.SetActive(true);
    }

    public void Clear() //for debug purposes
    {
        //SaveLoad.Delete();
        ReaderReference.Clear();
    }

    public void Back()
    {
        Pause.SetActive(true);
        Confirm.SetActive(false);
        OptionsPage.SetActive(false);
        SubmenuBackground.SetActive(false);
        ToDoPage.SetActive(false);
        SavePage.SetActive(false);
        LoadPage.SetActive(false);
    }

    public void OpenOptions()
    {
        SubmenuBackground.SetActive(true);
        Pause.SetActive(false);
        OptionsPage.SetActive(true);
    }

    public void ConfirmAction(bool Quit)
    {
        Quiting = Quit;
        //Pause.SetActive(false);
        Confirm.SetActive(true);

    }

    public void OnConfirmed()
    {
        if (Quiting)
            Quit();
        else
            ReturnToMain();
    }

    public void Quit()
    {
        print("off");
        //var seq = Actions.Sequence(this);
        //Actions.Delay(seq, 0.25f);
        //Actions.Call(seq, () => Application.Quit());


        if (!Application.isEditor)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        else
            Application.Quit();
        //StartCoroutine(iQuit());
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

}
