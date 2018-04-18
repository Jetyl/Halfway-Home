/******************************************************************************/
/*!
File:   EndToCredits.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndToCredits : MonoBehaviour
{
    public int CreditScene = 2;
    public GameObject FadeScreeen;

    // Use this for initialization
    void Start ()
    {
        Space.Connect<DefaultEvent>(Events.EndGame, OnEnd);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnEnd(DefaultEvent eventdata)
    {
        StartCoroutine(LoadLevel(1));

    }


    IEnumerator LoadLevel(float aTime)
    {
        Instantiate(FadeScreeen, transform.parent);
        yield return new WaitForSeconds(aTime);

        SceneManager.LoadScene(CreditScene);

    }
}
