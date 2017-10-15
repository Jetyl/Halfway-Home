using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public static class SaveLoad
{

    private static List<Game> savedGames = new List<Game>();

    
    public static void Save() 
    {

        CheckAddSave();

        if(Game.current != null)
            Game.current.SaveGame();
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, savedGames);
        file.Close();
        Debug.Log("on");
    }


    public static void Load()
    {
        MonoBehaviour.print(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
        
    }


    public static void SaveAt(int index)
    {

        if (!savedGames.Contains(Game.current))
            Game.current = new Game(Game.current);

        savedGames.Insert(index, Game.current);
        Save();

    }

    public static void DeleteAt(int index)
    {

        savedGames.RemoveAt(index);
        Save();

    }

    public static void Delete()
    {

        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            File.Delete(Application.persistentDataPath + "/savedGames.gd");
        }

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

        if (savedGames[pos] != null)
            return savedGames[pos];

        return null;
    }

    public static bool CanHaveNewSave()
    {
        if (savedGames.Count < 17)
            return true;
        else
        {
            for (int i = 0; i < savedGames.Count; ++i)
            {

                if (savedGames[i] == null)
                {
                    return true;
                }

            }
        }


        return false;
    }

    public static int GetSize()
    {
        return savedGames.Count;
    }

    private static void CheckAddSave()
    {
        if (Game.current == null)
            return;

        if (!savedGames.Contains(Game.current))
        {
            if (savedGames.Count < 17)
                savedGames.Add(Game.current);
            else if (CanHaveNewSave())
            {
                for (int i = 0; i < savedGames.Count; ++i)
                {

                    if (savedGames[i] == null)
                    {
                        savedGames[i] = Game.current;
                        break;
                    }

                }
            }
        }
    }
}
