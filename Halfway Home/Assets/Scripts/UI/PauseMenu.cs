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
    public GameObject Confirm;
    public GameObject Options;

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

    public void Save()
    {
        Space.DispatchEvent(Events.Save);
        SaveLoad.Save();
        ReaderReference.Save();
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
        Options.SetActive(false);
    }

    public void OpenOptions()
    {
        Pause.SetActive(false);
        Options.SetActive(true);
    }

    public void ConfirmAction(bool Quit)
    {
        Quiting = Quit;
        Pause.SetActive(false);
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
