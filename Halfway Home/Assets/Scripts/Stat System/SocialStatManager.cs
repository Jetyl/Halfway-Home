using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Stratus;

public class SocialStatManager : MonoBehaviour
{
  public class SocialStatTierUpEvent : Stratus.Event
  {
    public Personality.Social Stat;
    public float PrevProgress;
    public float CurProgress;
    public SocialStatTierUpEvent(Personality.Social stat, float prevProgress, float curProgress)
    {
      Stat = stat;
      PrevProgress = prevProgress;
      CurProgress = curProgress;
    }
  };
  public Personality.Social SocialStat;
  public Slider StatBar;
  public Image StarMarker1;
  public Image StarMarker2;
  public Image StarMarker3;

  public Image SpecialStar1;
  public Image SpecialStar2;

  public TextMeshProUGUI ShorthandText;

  private int PreviousTier;
  private float PreviousProgress;
  private Animator Anime;

  void Start ()
  {
    Anime = GetComponent<Animator>();
    PreviousTier = Game.current.Self.GetBasicSocialStat(SocialStat) + Game.current.Self.GetBonusSocialStat(SocialStat);
    PreviousProgress = Game.current.Self.GetSocialProgress(SocialStat);
    Space.Connect<DefaultEvent>(Events.StatChange, UpdateStats);
    Space.Connect<DefaultEvent>(Events.UpdateMap, UpdateStats);
    StatBar.maxValue = Game.current.Self.SocialThreshold * 3;
    UpdateDisplay();
  }

  public void UpdateDisplay()
  {
    float barStat = Game.current.Self.GetSocialProgress(SocialStat);
    //Debug.Log(Game.current.Self.GetSocialProgress(SocialStat) + ", " + Game.current.Self.SocialThreshold * 3);
    //print($"BAR {SocialStat.ToString()} SET TO {barStat}");
    StatBar.value = barStat;

    int basicTier = Game.current.Self.GetBasicSocialStat(SocialStat);
    int specialTier = Game.current.Self.GetBonusSocialStat(SocialStat);
    //Debug.Log($"{SocialStat} special star value is {specialTier}");
    int totalTier = basicTier + specialTier;

    if (totalTier > PreviousTier) Celebrate();
    PreviousProgress = barStat;
    PreviousTier = totalTier;

    if (ShorthandText != null) ShorthandText.text = totalTier.ToString();

    if (basicTier > 0) StarMarker1.CrossFadeAlpha(1.0f, 0.1f, false);
    else StarMarker1.CrossFadeAlpha(0.2f, 0.1f, false);

    if (basicTier > 1) StarMarker2.CrossFadeAlpha(1.0f, 0.1f, false);
    else StarMarker2.CrossFadeAlpha(0.2f, 0.1f, false);

    if (basicTier > 2) StarMarker3.CrossFadeAlpha(1.0f, 0.1f, false);
    else StarMarker3.CrossFadeAlpha(0.2f, 0.1f, false);

    if (SpecialStar1 == null || SpecialStar2 == null) return;

    if (specialTier > 0) SpecialStar1.CrossFadeAlpha(1.0f, 0.1f, false);
    else SpecialStar1.CrossFadeAlpha(0.0f, 0.1f, false);

    if (specialTier > 1) SpecialStar2.CrossFadeAlpha(1.0f, 0.1f, false);
    else SpecialStar2.CrossFadeAlpha(0.0f, 0.1f, false);
  }
	
	void UpdateStats (DefaultEvent e)
  {
    UpdateDisplay();
  }

  void Celebrate()
  {
    Scene.Dispatch(new SocialStatTierUpEvent(SocialStat, PreviousProgress, Game.current.Self.GetSocialProgress(SocialStat)));
  }

    public void BarState(bool Opened)
    {
        Anime.SetBool("Open", Opened);
    }

}
