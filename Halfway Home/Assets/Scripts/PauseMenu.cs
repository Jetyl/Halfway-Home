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
        SaveLoad.Save();
        ReaderReference.Save();
    }

    public void Clear()
    {
        //SaveLoad.Delete();
        ReaderReference.Clear();
    }

    public void Quit()
    {
        print("on");
        Application.Quit();
    }

}
