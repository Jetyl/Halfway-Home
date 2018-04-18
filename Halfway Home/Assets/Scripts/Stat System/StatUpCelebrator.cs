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
  public float OnScreenDuration;
  [Space(10)]
  public Color AwarenessStarColor;
  public Color GraceStarColor;
  public Color ExpressionStarColor;
  private Color curColor;

  // Use this for initialization
  void Start ()
  {
    ProgressBar.maxValue = Game.current.Self.SocialThreshold * 3;
    Scene.Connect<SocialStatManager.SocialStatTierUpEvent>(OnSocialStatTierUpEvent);
	}
	
	// Update is called once per frame
	void Update ()
  {
		
	}

  void OnSocialStatTierUpEvent(SocialStatManager.SocialStatTierUpEvent e)
  {
    ProgressBar.value = e.PrevProgress;
    var IsSpecialUp = ProgressBar.value == e.CurProgress;
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

    var animStar = Star1;
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
    }

    var celebrationSeq = Actions.Sequence(this);
    if(IsSpecialUp) Actions.Call(celebrationSeq, () => SpecialBackground.SetBool("Revealed", true));
    if(IsSpecialUp) Actions.Delay(celebrationSeq, 0.5f);
    Actions.Property(celebrationSeq, () => ProgressBar.value, e.CurProgress, Mathf.Min(InterpTime, OnScreenDuration), Ease.QuadOut);
    Actions.Call(celebrationSeq, ()=>animStar.SetBool("Achieved", true));
    Actions.Delay(celebrationSeq, OnScreenDuration - InterpTime - 0.5f);
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
}