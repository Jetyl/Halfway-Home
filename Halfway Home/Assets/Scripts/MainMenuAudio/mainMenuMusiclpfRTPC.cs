/******************************************************************************/
/*!
File:   mainMenuMusiclpfRTPC.cs
Author: Christien Ayson
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
// Applies a LPF to main menu Music based on mouse hover
// -1 = Hovering over Quit or while Confirm Quit is open (LPF 1k)
// 0 = Hovering over nothing (LPF 4k)
// 1 = Hovering over New Game or Resume (LPF off)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuMusiclpfRTPC : MonoBehaviour
{
    // ConfirmQuit Dialog
    public GameObject ConfirmationPanel;
    
    // ConfirmClear Dialog
    public GameObject ClearPanel;

    // Current Mouse Position, -1 = Quit, 0 = nothing, 1 = New Game/Resume
    int hoverQuit = -1;
    int hoverNothing = 0;
    int hoverStart = 1;
    [HideInInspector] public int mousePosition = 0;

    // Check to avoid sending redundant messages
    int currentRTPCvalue = 0;
    
    // AkRtpcID  in_rtpcID
    string rtpcID = "mainMenuMouseHover";
    
    // AkRtpcValue  in_value
    float lpf1k = -1f;
    float lpf4k = 0f;
    float lpfOff = 1f;
    
    // AkGameObjectID  in_gameObjectID
    public GameObject gameObjectID;
    
    // AkTimeMs  in_uValueChangeDuration
    int timeMS = 300;
    

    void Update ()
    {
        if (mousePosition == hoverQuit && currentRTPCvalue != hoverQuit)
        {
            AkSoundEngine.SetRTPCValue(rtpcID, lpf1k, gameObjectID, timeMS);
            currentRTPCvalue = hoverQuit;
        }
        else if (mousePosition == hoverNothing && currentRTPCvalue != hoverNothing)
        {
            AkSoundEngine.SetRTPCValue(rtpcID, lpf4k, gameObjectID, timeMS);
            currentRTPCvalue = hoverNothing;
        }
        else if (mousePosition == hoverStart && currentRTPCvalue != hoverStart)
        {
            AkSoundEngine.SetRTPCValue(rtpcID, lpfOff, gameObjectID, timeMS);
            currentRTPCvalue = hoverStart;
        }
    }
    
    public void setHoverQuit ()
    {
        if (!ConfirmationPanel.activeInHierarchy && !ClearPanel.activeInHierarchy)
            mousePosition = hoverQuit;
    }
    public void setHoverNothing ()
    {
        if (!ConfirmationPanel.activeInHierarchy && !ClearPanel.activeInHierarchy)
            mousePosition = hoverNothing;
    }
    public void setHoverStart ()
    {
        if (!ConfirmationPanel.activeInHierarchy && !ClearPanel.activeInHierarchy)
            mousePosition = hoverStart;
    }
    public void closeQuitDialog ()
    {
            mousePosition = hoverNothing;
    }
    
    
    public void postEvent (string event1)
    {
        AkSoundEngine.PostEvent( AkSoundEngine.GetIDFromString(event1), gameObjectID);
    }
}