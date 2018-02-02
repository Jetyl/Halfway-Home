using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SocialStatManager : MonoBehaviour
{
  public Personality.Social SocialStat;
  public Image StatBar;
  public Image StarMarker1;
  public Image StarMarker2;
  public Image StarMarker3;

  public Image SpecialStar1;
  public Image SpecialStar2;

  public TextMeshProUGUI ShorthandText;

  void Start ()
  {
    Space.Connect<DefaultEvent>(Events.StatChange, UpdateStats);
    UpdateDisplay();
  }

  public void UpdateDisplay()
  {
    float barStat = Game.current.Self.GetSocialProgress(SocialStat) / (Game.current.Self.SocialThreshold * 3);
    Debug.Log(Game.current.Self.GetSocialProgress(SocialStat) + ", " + Game.current.Self.SocialThreshold * 3);
    StatBar.fillAmount = barStat;

    int basicTier = Game.current.Self.GetBasicSocialStat(SocialStat);
    int specialTier = Game.current.Self.GetBonusSocialStat(SocialStat);
    int totalTier = basicTier + specialTier;

    ShorthandText.text = totalTier.ToString();

    if (basicTier > 0) StarMarker1.CrossFadeAlpha(1.0f, 0.1f, false);
    else StarMarker1.CrossFadeAlpha(0.5f, 0.1f, false);

    if (basicTier > 1) StarMarker2.CrossFadeAlpha(1.0f, 0.1f, false);
    else StarMarker2.CrossFadeAlpha(0.5f, 0.1f, false);

    if (basicTier > 2) StarMarker3.CrossFadeAlpha(1.0f, 0.1f, false);
    else StarMarker3.CrossFadeAlpha(0.5f, 0.1f, false);

    if (specialTier > 0) SpecialStar1.CrossFadeAlpha(1.0f, 0.1f, false);
    else SpecialStar1.CrossFadeAlpha(0.0f, 0.1f, false);

    if (specialTier > 1) SpecialStar2.CrossFadeAlpha(1.0f, 0.1f, false);
    else SpecialStar2.CrossFadeAlpha(0.0f, 0.1f, false);
  }
	
	void UpdateStats (DefaultEvent e)
  {
    UpdateDisplay();
  }
}
