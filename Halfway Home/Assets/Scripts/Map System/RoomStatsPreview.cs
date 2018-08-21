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
    public List<StatIcon> StatIcons;
  }
  [System.Serializable]
  public class StatIcon
  {
    public bool DayOnly;
    public bool NightOnly;
    public Personality.Social Social;
    public int SocialMin;
    public int SocialMax;
    public Image Icon;
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
        foreach(StatIcon s in r.StatIcons)
        {
          // additional logic here
          Image im = Instantiate(s.Icon, transform);
          im.tag = "NotHideable";
        }
      }
    }
  }
}