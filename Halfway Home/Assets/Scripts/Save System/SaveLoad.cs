/******************************************************************************/
/*!
File:   SaveLoad.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using LitJson;

public static class SaveLoad
{
    private static string path = Application.persistentDataPath + "/savedGames.gd";
    private static List<Game> savedGames = new List<Game>();

    
    public static void Save() 
    {

        //CheckAddSave();

        if(Game.current != null)
            Game.current.SaveGame();

        //BinaryFormatter bf = new BinaryFormatter();
        //
        //FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
        //bf.Serialize(file, savedGames);
        //file.Close();
        
        // JSON
        //Debug.Log(JsonUtility.ToJson(savedGames[0]));
        var wrap = new SaveWrapper(savedGames);
        File.WriteAllText(path, JsonUtility.ToJson(wrap));

        
        
    }


    public static void Load()
    {
        MonoBehaviour.print(Application.persistentDataPath);
        if (File.Exists(path))
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            //savedGames = (List<Game>)bf.Deserialize(file);
            //file.Close();

            // Unity JSON
            string data = File.ReadAllText(path);
            var wrap =JsonUtility.FromJson<SaveWrapper>(data);
            savedGames = new List<Game>();
            for (int i = 0; i < wrap.RealData.Count; ++i)
            {
                if (wrap.RealData[i] == true)
                    savedGames.Add(wrap.Savedata[i]);
                else
                    savedGames.Add(null);
                
            }
            //MonoBehaviour.print(savedGames.Count);

            foreach (var game in savedGames)
            {
                if (game != null)
                    game.LoadGame();
            }
                
            
        }
        
    }


    public static void SaveAt(int index)
    {

        //if (!savedGames.Contains(Game.current))
        //    Game.current = new Game(Game.current);
        
        while (savedGames.Count - 1 < index)
        {
            savedGames.Add(null);
        }
        
        savedGames[index] = Game.current;
        Save();

        var SavedGame = new Game(Game.current);
        Game.current = SavedGame;
            
    }

    public static void DeleteAt(int index)
    {

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        if (savedGames.Count > index)
            savedGames[index] = null;
        
        //saving
        Debug.Log(index + " | " + savedGames[index]);
        var wrap = new SaveWrapper(savedGames);
        Debug.Log(index + " | " + savedGames[index]);
        File.WriteAllText(path, JsonUtility.ToJson(wrap));
        Debug.Log(index + " | " + savedGames[index]);

        //Load();

    }
   
   //public static int GetIndex(Game instance)
   //{
   //    for (int i = 0; i < savedGames.Count; ++i)
   //    {
   //        if (savedGames[i] == instance)
   //        {
   //            return i;
   //        }
   //
   //
   //    }
   //    return -1;
   //}

    public static void Delete()
    {

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        savedGames = new List<Game>();

    }

    public static bool isEmpty()
    {

        for (int i = 0; i < savedGames.Count; ++i)
        {

            if (savedGames[i] != null)
            {
                return false;
            }
               

        }


        return true;
    }
    
    public static Game GetSave(int pos)
    {

        if (pos >= savedGames.Count)
            return null;
        //Debug.Log(pos + " | " + savedGames[pos]);
        if (savedGames[pos] != null)
            return new Game(savedGames[pos]);

        return null;
    }
    
    public static int GetSize()
    {
        int Count = 0;
        foreach (var game in savedGames)
        {
            if (game != null)
                Count += 1;
        }

        return Count;
    }

    
}


public struct SaveWrapper
{
    public Game[] Savedata;
    public List<bool> RealData;

    public SaveWrapper(List<Game> data)
    {
        Savedata = data.ToArray();
        RealData = new List<bool>();
        foreach(var game in Savedata)
        {
            if (game == null)
                RealData.Add(false);
            else
                RealData.Add(true);
        }
    }
}