using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Game
{

    public static Game current;

    public string PlayerName;

    public int Day;

    public int Hour;
    

    public ProgressSystem Progress;
    
   

    public Game()
    {
       
        Day = 0;
        PlayerName = "";
        Progress = new ProgressSystem();
       

        Progress.UpdateProgress("MasterVolume", 1.0f);
        Progress.UpdateProgress("BackgroundVolume", 1.0f);
        Progress.UpdateProgress("SFXVolume", 1.0f);
        Progress.UpdateProgress("TextSpeed", 1.0f);

        

    }

    public Game(Game copy_)
    {
        
        Day = copy_.Day;
        PlayerName = copy_.PlayerName;
        Progress = copy_.Progress;
        

    }


    public void SaveGame()
    {

    }

    public void LoadGame()
    {
       
    }

    public void SoftReset()
    {

        
        Day = 0;

        Progress.ResetBeats();

        


    }

    public void HardReset()
    {
        

        PlayerName = "";


        
        Day = 0;
        Progress = new ProgressSystem();

    }


    public void NewDay()
    {

       

        Day += 1;

        //reset the daily varibles
        Progress.ResetDaily();

        //update the day in the progression system
        var point = new ProgressPoint("Day", PointTypes.Integer);
        point.IntValue = Day;

        Progress.UpdateProgress("Day", point);
        
       

        
        //checks if any special conditions have occured, to change rooms or anything.
            // prolly do this in scene checks, not here

        //cast new day event
        //Space.DispatchEvent(Events.NewDay);

    }

}

public enum Room
{
    None,
    YourRoom,
    Commons,
    FrontDesk,
    Kitchen,
    Gardens,
    Study,
    ArtRoom,
    Store
}