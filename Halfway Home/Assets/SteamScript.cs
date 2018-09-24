using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamScript : MonoBehaviour
{
    void Start()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
            Debug.Log(SteamAppList.GetAppName((AppId_t)950250, out name, 100));
            Debug.Log(name);
        }
    }
}
