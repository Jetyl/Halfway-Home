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
    public enum MousePosition
    {
        hoverNothing,
        hoverQuit,
        hoverStart
    };

    [HideInInspector] public MousePosition mousePosition = MousePosition.hoverNothing;

    // Check to avoid sending redundant messages
    MousePosition currentRTPCvalue = MousePosition.hoverNothing;
    
    // AkRtpcID  in_rtpcID
    string rtpcID = "Menu_Mouse_Hover";
    
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
        if (mousePosition == MousePosition.hoverQuit && currentRTPCvalue != MousePosition.hoverQuit)
        {
            AkSoundEngine.SetRTPCValue(rtpcID, lpf1k, gameObjectID, timeMS);
            currentRTPCvalue = MousePosition.hoverQuit;
        }
        else if (mousePosition == MousePosition.hoverNothing && currentRTPCvalue != MousePosition.hoverNothing)
        {
            AkSoundEngine.SetRTPCValue(rtpcID, lpf4k, gameObjectID, timeMS);
            currentRTPCvalue = MousePosition.hoverNothing;
        }
        else if (mousePosition == MousePosition.hoverStart && currentRTPCvalue != MousePosition.hoverStart)
        {
            AkSoundEngine.SetRTPCValue(rtpcID, lpfOff, gameObjectID, timeMS);
            currentRTPCvalue = MousePosition.hoverStart;
        }
    }
    
    public void setHoverQuit ()
    {
        if (!ConfirmationPanel.activeInHierarchy && !ClearPanel.activeInHierarchy)
            mousePosition = MousePosition.hoverQuit;
    }
    public void setHoverNothing ()
    {
        if (!ConfirmationPanel.activeInHierarchy && !ClearPanel.activeInHierarchy)
            mousePosition = MousePosition.hoverNothing;
    }
    public void setHoverStart ()
    {
        if (!ConfirmationPanel.activeInHierarchy && !ClearPanel.activeInHierarchy)
            mousePosition = MousePosition.hoverStart;
    }
    public void closeQuitDialog ()
    {
            mousePosition = MousePosition.hoverNothing;
    }
    
    
    public void postEvent (string event1)
    {
        AkSoundEngine.PostEvent( AkSoundEngine.GetIDFromString(event1), gameObjectID);
    }
}