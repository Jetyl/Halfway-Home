using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomStatsPreview : MonoBehaviour
{
  [System.Serializable]
  public class RoomPreview
  {
    public string Name;
    public List<Image> DisplayImages;
  }
  public List<RoomPreview> Rooms = new List<RoomPreview>(0);

  public void SetDisplayToRoom(string room)
  {
    var children = GetComponentsInChildren<Image>();
    foreach (Image i in children) Destroy(i.gameObject);

    foreach(RoomPreview r in Rooms)
    {
      if(r.Name == room)
      {
        foreach(Image i in r.DisplayImages)
        {
          Instantiate(i, transform);
        }
      }
    }
  }
}