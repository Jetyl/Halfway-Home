using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoad : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        SaveLoad.Load(); //loads the game file

        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadAt(int slot)
    {
        Game.current = SaveLoad.GetSave(slot);
    }
    
    public void LoadMostRecent()
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
