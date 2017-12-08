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

public class PauseMenu : MonoBehaviour
{
  public HalfwayHome.HalfwayHomeStoryReader ReaderReference;
	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
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

    public void Quit()
    {
        print("off");
        Application.Quit();
    }

}
