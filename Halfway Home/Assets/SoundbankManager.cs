using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;

public class SoundbankManager : MonoBehaviour {

  private List<string> loadedBanks = new List<string> {};
  
	void Awake ()
  {
    LoadBank("Main");
    LoadBank("Master");
    InitBanks();
  }
  
  public void InitBanks()
  {
    string storyBank = Game.current.CurrentStorySoundbank;
    string roomBank = Game.current.CurrentRoomSoundbank;
    
    if (storyBank != "" && storyBank != null)
      LoadBank(storyBank, true);
    if (roomBank != "" && roomBank != null)
      LoadBank(roomBank, true);
  }
  
  public void LoadBank(string bank, bool init = false)
  {
    // Don't load the bank if it's already loaded
    if (loadedBanks.Contains(bank))
    {
      Debug.Log("CONTAINS" + bank);
      return;
    }
    
    AkBankManager.LoadBank(bank, true, true);
    loadedBanks.Add(bank);
    
    if (init == false)
    {
      if (bank.ToLower().StartsWith("story"))
      {
        Trace.Script($"SETTING STORY BANK TO {bank}");
        
        // Unload previous story bank
        UnloadBank(Game.current.CurrentStorySoundbank);
        
        // Set new story bank
        Game.current.CurrentStorySoundbank = bank;
      }
      else if (bank.ToLower().StartsWith("room"))
      {
        Trace.Script($"SETTING ROOM BANK TO {bank}");
        
        // Unload previous room bank
        UnloadBank(Game.current.CurrentRoomSoundbank);
        
        // Set new room bank
        Game.current.CurrentRoomSoundbank = bank;
      }
    }
    
    Trace.Script($"Loading bank {bank}");
  }
  
  public void UnloadBank(string bank)
  {
    // Don't unload the bank if it's not loaded
    if (!(loadedBanks.Contains(bank)))
    {
      Debug.Log("DOESNT CONTAIN" + bank);
      return;
    }
    
    StartCoroutine(DelayUnloadBank(bank));
  }
  
  IEnumerator DelayUnloadBank(string bank)
  {
    yield return new WaitForSeconds(2.0f);
    
    // Bandaid fix to visiting the same room twice (until I remove unload banks from elsewhere)
    // If the bank is equal to the current Room bank, don't unload it
    if (bank == Game.current.CurrentRoomSoundbank)
      yield break;
    
    AkBankManager.UnloadBank(bank);
    loadedBanks.Remove(bank);
    
    Debug.Log("UNLOADED BANK" + bank);
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
