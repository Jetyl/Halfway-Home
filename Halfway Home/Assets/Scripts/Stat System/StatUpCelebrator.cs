using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Stratus;
using TMPro;

[RequireComponent(typeof(UIFader))]
public class StatUpCelebrator : MonoBehaviour
{
  public Slider ProgressBar;
  public TextMeshProUGUI StatText;
  public Animator Star1;
  public Animator Star2;
  public Animator Star3;
  public Animator SpecialBackground;
  public Animator SpecialStar1;
  public Animator SpecialStar2;
  [Space(10)]
  public float InterpTime;
  public float OnScreenDurationLong;
  public float OnScreenDurationShort;
  [Space(10)]
  public Color AwarenessStarColor;
  public Color GraceStarColor;
  public Color ExpressionStarColor;
  private Color curColor;
  [Space(10)]
  public CelebrationFrequency Frequency = CelebrationFrequency.All;

  // Use this for initialization
  void Start ()
  {
    ProgressBar.maxValue = Game.current.Self.SocialThreshold * 3;
    Scene.Connect<SocialStatManager.SocialStatTierUpEvent>(OnSocialStatTierUpEvent);
	}

  void OnSocialStatTierUpEvent(SocialStatManager.SocialStatTierUpEvent e)
  {
    if (Frequency == CelebrationFrequency.TierUpOnly && !e.IsTierUp) return;
    else if (Frequency == CelebrationFrequency.FirstAndTierUp && !e.IsTierUp && e.PrevProgress > 0) return;
    ProgressBar.value = e.PrevProgress;
    var IsSpecialUp = ProgressBar.value == e.CurProgress;
    var IsTierUp = e.IsTierUp;
    StatText.text = ToTitleCase($"{e.Stat} Up!");
    var curTier = Game.current.Self.GetBasicSocialStat(e.Stat);
    var specTier = Game.current.Self.GetBonusSocialStat(e.Stat);
    if (e.Stat == Personality.Social.Awareness) curColor = AwarenessStarColor;
    else if (e.Stat == Personality.Social.Grace) curColor = GraceStarColor;
    else curColor = ExpressionStarColor;
    switch(curTier)
    {
      case 3:
        Star1.SetBool("Achieved", true);
        Star2.SetBool("Achieved", true);
        Star3.SetBool("Achieved", IsSpecialUp);
        break;
      case 2:
        Star1.SetBool("Achieved", true);
        Star2.SetBool("Achieved", IsSpecialUp);
        Star3.SetBool("Achieved", false);
        break;
      default:
        Star1.SetBool("Achieved", IsSpecialUp);
        Star2.SetBool("Achieved", false);
        Star3.SetBool("Achieved", false);
        break;
    }
    ActivateStars();
    switch(specTier)
    {
      case 2:
        SpecialBackground.SetBool("SeenBefore", true);
        SpecialStar1.SetBool("Achieved", true);
        SpecialStar2.SetBool("Achieved", false);
        break;
      case 1:
        SpecialBackground.SetBool("SeenBefore", false);
        SpecialStar1.SetBool("Achieved", false);
        SpecialStar2.SetBool("Achieved", false);
        break;
      default:
        SpecialBackground.SetBool("SeenBefore", false);
        SpecialStar1.SetBool("Achieved", false);
        SpecialStar2.SetBool("Achieved", false);
        break;
    }
    SpecialStar1.gameObject.GetComponent<Image>().color = curColor;
    SpecialStar2.gameObject.GetComponent<Image>().color = curColor;
    GetComponent<UIFader>().Show();

    Animator animStar = null;
    if (IsSpecialUp)
    {
      animStar = specTier == 1 ? SpecialStar1 : SpecialStar2;
    }
    else
    {
      if (Game.current.Self.GetBasicSocialStat(e.Stat) == 3)
      {
        animStar = Star3;
      }
      else if (Game.current.Self.GetBasicSocialStat(e.Stat) == 2) animStar = Star2;
      else if (Game.current.Self.GetBasicSocialStat(e.Stat) == 1) animStar = Star1;
    }

    var celebrationSeq = Actions.Sequence(this);
    if(IsSpecialUp) Actions.Call(celebrationSeq, () => SpecialBackground.SetBool("Revealed", true));
    if(IsSpecialUp) Actions.Delay(celebrationSeq, 0.2f);
    Actions.Property(celebrationSeq, () => ProgressBar.value, e.CurProgress, Mathf.Min(InterpTime, IsTierUp ? OnScreenDurationLong:OnScreenDurationShort), Ease.QuadOut);
    if(IsTierUp) Actions.Call(celebrationSeq, ()=>animStar.SetBool("Achieved", true));
    Actions.Delay(celebrationSeq, (IsTierUp ? OnScreenDurationLong : OnScreenDurationShort) - InterpTime - 0.2f);
    Actions.Call(celebrationSeq, ()=>GetComponent<UIFader>().Hide());
    Actions.Call(celebrationSeq, ResetStars);
    if(IsSpecialUp) Actions.Call(celebrationSeq, () => SpecialBackground.SetBool("Revealed", false));
  }

  void ActivateStars()
  {
    Star1.SetBool("Active", true);
    Star2.SetBool("Active", true);
    Star3.SetBool("Active", true);
    SpecialStar1.SetBool("Active", true);
    SpecialStar2.SetBool("Active", true);
  }

  void ResetStars()
  {
    Star1.SetBool("Active", false);
    Star2.SetBool("Active", false);
    Star3.SetBool("Active", false);
    SpecialStar1.SetBool("Active", false);
    SpecialStar2.SetBool("Active", false);
  }


  string ToTitleCase(string stringToConvert)
  {
    string firstChar = stringToConvert[0].ToString();
    return (stringToConvert.Length > 0 ? firstChar.ToUpper() + stringToConvert.Substring(1) : stringToConvert);
  }

  public enum CelebrationFrequency
  {
    FirstAndTierUp,
    TierUpOnly,
    All
  };
}