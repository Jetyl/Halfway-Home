/******************************************************************************/
/*!
File:   splashScreen.cs
Author: Christien Ayson
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class splashScreen : MonoBehaviour
{
    IEnumerator Start()
    {
        Debug.Log("Showing splash screen");
        SplashScreen.Begin();
        while (!SplashScreen.isFinished)
        {
            SplashScreen.Draw();
            yield return null;
        }
        Debug.Log("Finished showing splash screen");
    }
}
