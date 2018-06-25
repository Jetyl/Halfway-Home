/******************************************************************************/
/*!
File:   CreditsManager.cs
Author: John Myres
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
  public float fadeOutTime = 2.1f;
  bool returned = false;
  
  public class ReturnEvent : Stratus.Event {};
	// Use this for initialization
	void Start ()
  {
    Stratus.Scene.Connect<ReturnEvent>(OnReturnEvent);
	}
  
  void Awake()
    {
        AkBankManager.LoadBankAsync("Credits");
        AkBankManager.LoadBankAsync("Master");
        AkSoundEngine.PostEvent("play_music_fakeit", GameObject.Find("Music"));
    }
	
  void OnReturnEvent(ReturnEvent e)
  {
    Trace.Script("Load");
    AkSoundEngine.PostEvent("Stop_All", GameObject.Find("Music"));
    StartCoroutine(FadeOutCredits(fadeOutTime));
    
  }
  
  IEnumerator FadeOutCredits (float time)
  {
    GameObject.Find("Fade").DispatchEvent(Events.Fade, new FadeEvent(Color.black, time));
    yield return new WaitForSeconds(time);
    SceneManager.LoadScene("Menu");
  }

	// Update is called once per frame
	void Update ()
  {
    if (Input.GetButtonDown("Cancel")) Stratus.Scene.Dispatch<ReturnEvent>(new ReturnEvent());
	}
}
