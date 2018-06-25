using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundbankManager : MonoBehaviour {

  private List<string> loadedBanks = new List<string> {};
  
	void Awake ()
  {
    LoadBank("Main");
    LoadBank("Master");
    LoadBank("memory");
  }
  
  public void LoadBank(string bank)
  {
    AkBankManager.LoadBankAsync(bank);
    loadedBanks.Add(bank);
    
    if (bank != "Main" && bank != "Master" && bank != "MusicMain" && bank != "MainMenu")
    {
      Game.current.CurrentSoundbank = bank;
    }
  }
  
  public void UnloadBank(string bank)
  {
    AkBankManager.UnloadBank(bank);
    loadedBanks.Remove(bank);
  }
  
  public void UnloadAllBanks()
  {
    foreach (string bank in loadedBanks)
    {
      AkBankManager.UnloadBank(bank);
    }
    
    loadedBanks.Clear();
  }
}
