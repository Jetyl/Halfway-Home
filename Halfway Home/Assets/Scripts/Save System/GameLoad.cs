using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoad : MonoBehaviour
{
    private static GameLoad _instance;

    // Use this for initialization
    void Awake ()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            SaveLoad.Load(); //loads the game file
        }

        

        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static void LoadAt(int slot)
    {
        Game.current = SaveLoad.GetSave(slot);
    }
    
    public static void LoadMostRecent()
    {
        DateTime recent = SaveLoad.GetSave(0).SaveStamp;
        Game.current = SaveLoad.GetSave(0);

        for(int i = 0; i < SaveLoad.GetSize(); ++i)
        {
            if (recent < SaveLoad.GetSave(i).SaveStamp)
                Game.current = SaveLoad.GetSave(i);
        }
    }

}
