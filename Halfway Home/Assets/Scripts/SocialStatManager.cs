using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialStatManager : MonoBehaviour
{
  public Personality.Social SocialStat;
  public Image StatBar;
  public Image StarMarker1;
  public Image StarMarker2;
  public Image StarMarker3;

  public Image SpecialStar1;
  public Image SpecialStar2;

  void Start ()
  {
    Space.Connect<DefaultEvent>(Events.StatChange, UpdateStats);
  }
	
	void UpdateStats (DefaultEvent e)
  {
    float stat = Game.current.Self.GetTrueSocialStat(SocialStat);
    Debug.Log(stat);
	}
}
