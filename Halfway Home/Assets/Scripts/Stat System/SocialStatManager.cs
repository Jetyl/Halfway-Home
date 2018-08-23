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

  public Color NormalColorText;
  public Color LoweredColorText;

  public Color NormalColorStar;
  public Color LoweredColorStar;
  public Color SpecialColorStar;

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
    int realTier = Game.current.Self.GetModifiedSocialStat(SocialStat);

    if (totalTier > PreviousTier) Celebrate();
    PreviousProgress = barStat;
    PreviousTier = totalTier;

    if (ShorthandText != null)
    {
      ShorthandText.text = realTier.ToString();
      if (realTier < totalTier) ShorthandText.color = LoweredColorText;
      else ShorthandText.color = NormalColorText;
    }

        if (basicTier > 0)
        {
            StarMarker1.CrossFadeAlpha(1.0f, 0.001f, false);
            if (realTier < 1) StarMarker1.color = LoweredColorStar;
            else StarMarker1.color = NormalColorStar;
        }
        else
        {
            var col1 = StarMarker1.color;
            col1.a = 0.2f;
            StarMarker1.color = col1;
            //StarMarker1.CrossFadeAlpha(0.2f, 0.001f, false);
        }


        if (basicTier > 1)
        {
            //StarMarker2.CrossFadeAlpha(1.0f, 0.001f, false);
            if (realTier < 2) StarMarker2.color = LoweredColorStar;
            else StarMarker2.color = NormalColorStar;
        }
        else
        {
            var col2 = StarMarker2.color;
            col2.a = 0.2f;
            StarMarker2.color = col2;
            //StarMarker2.CrossFadeAlpha(0.2f, 0.001f, false);
        }

        if (basicTier > 2)
        {
            //StarMarker3.CrossFadeAlpha(1.0f, 0.001f, false);
            if (realTier < 3) StarMarker3.color = LoweredColorStar;
            else StarMarker3.color = NormalColorStar;
            
        }
        else
        {
            var col3 = StarMarker3.color;
            col3.a = 0.2f;
            StarMarker3.color = col3;
            //StarMarker3.CrossFadeAlpha(0.2f, 0.001f, false);
        }

    if (SpecialStar1 == null || SpecialStar2 == null) return;

        if (specialTier > 0)
        {
            SpecialStar1.enabled = true;
            SpecialStar1.CrossFadeAlpha(1.0f, 0.001f, false);
            if (realTier < 4) SpecialStar1.color = LoweredColorStar;
            else SpecialStar1.color = SpecialColorStar;
        }
        else
        {
            SpecialStar1.CrossFadeAlpha(0.0f, 0.001f, false);
            SpecialStar1.enabled = false;
        }

        if (specialTier > 1)
        {
            SpecialStar2.enabled = true;
            SpecialStar2.CrossFadeAlpha(1.0f, 0.001f, false);
            if (realTier < 5) SpecialStar2.color = LoweredColorStar;
            else SpecialStar2.color = SpecialColorStar;
        }
        else
        {
            SpecialStar2.enabled = false;
            SpecialStar2.CrossFadeAlpha(0.0f, 0.001f, false);
        }
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
        UpdateDisplay();
    }

}
