/******************************************************************************/
/*!
File:   SaveSystem.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections;

public static class SaveSystem
{


    static int ConfirmationIndex;

    

    //first step of save process. is always called when saving occurs
    private static void SaveStep1(int GameNumber)
    {

        SaveLoad.SaveAt(GameNumber);
    }

    //first step of save process. is always called when saving occurs
    private static void SaveStep1()
    {
        

        SaveLoad.Save();
    }

    //second step of saving process. called when continuing, or starting up the game.
    private static void SaveStep2()
    {
        
        Game.current.NewDay();

        SaveLoad.Save();
    }



    public static void SaveGame(int GameNumber)
    {

        if (SaveLoad.GetSave(GameNumber) != null && SaveLoad.GetSave(GameNumber) != Game.current)
        {
            //call the are you sure message
            AreYouSure(GameNumber);
            return;
        }

        SaveStep1(GameNumber);

        

    }

    public static void SaveGame()
    {
        
        SaveStep1();
        
    }

    //showing overwrite confirmation
    public static void AreYouSure(int GameNumber)
    {
        ConfirmationIndex = GameNumber;
        
    }

    public static void Overwrite()
    {
        SaveStep1(ConfirmationIndex);
        
        
    }


    //player isn't saving, but is continuing
    public static void SkipSaving()
    {
        //ConfirmSkip.SetBool("IsUp", false);
        Game.current.NewDay();
        

    }
    

    public static void DreamOver()
    {
        
        SaveStep2();

        //Dreams.GetComponent<Text>().text = "Day " + Game.current.Day;
        //EventSystem.SendEvent(Dreams, Events.Fade, new FadeEvent(Color.white));


        //EventSystem.DisconnectEvent(Space.Instance.gameObject, Events.FinishedDescription, this);


    }

    

    

}
