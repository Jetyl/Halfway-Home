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
  public class ReturnEvent : Stratus.Event {};
	// Use this for initialization
	void Start ()
  {
    Stratus.Scene.Connect<ReturnEvent>(OnReturnEvent);
	}
	
  void OnReturnEvent(ReturnEvent e)
  {
    Trace.Script("Load");
    SceneManager.LoadScene("Menu");
  }

	// Update is called once per frame
	void Update ()
  {
    if (Input.GetButtonDown("Cancel")) Stratus.Scene.Dispatch<ReturnEvent>(new ReturnEvent());
	}
}
