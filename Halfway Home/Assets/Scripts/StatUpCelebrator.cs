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
  public Animator SpecialStar1;
  public Animator SpecialStar2;
  [Space(10)]
  public float InterpTime;
  public float OnScreenDuration;

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
    StatText.text = ToTitleCase($"{e.Stat} Up!");
    var curTier = Game.current.Self.GetBasicSocialStat(e.Stat);
    switch(curTier)
    {
      case 3:
        Star1.SetBool("Achieved", true);
        Star2.SetBool("Achieved", true);
        Star3.SetBool("Achieved", false);
        break;
      case 2:
        Star1.SetBool("Achieved", true);
        Star2.SetBool("Achieved", false);
        Star3.SetBool("Achieved", false);
        break;
      default:
        Star1.SetBool("Achieved", false);
        Star2.SetBool("Achieved", false);
        Star3.SetBool("Achieved", false);
        break;
    }
    ActivateStars();
    GetComponent<UIFader>().Show();

    var animStar = Star1;
    if (Game.current.Self.GetBasicSocialStat(e.Stat) == 3)
    {
      animStar = Star3;
    }
    else if(Game.current.Self.GetBasicSocialStat(e.Stat) == 2) animStar = Star2;

    var celebrationSeq = Actions.Sequence(this);
    Actions.Property(celebrationSeq, () => ProgressBar.value, e.CurProgress, Mathf.Min(InterpTime, OnScreenDuration), Ease.QuadOut);
    Actions.Call(celebrationSeq, ()=>animStar.SetBool("Achieved", true));
    Actions.Delay(celebrationSeq, OnScreenDuration - InterpTime);
    Actions.Call(celebrationSeq, ()=>GetComponent<UIFader>().Hide());
    Actions.Call(celebrationSeq, ResetStars);
  }

  void ActivateStars()
  {
    Star1.SetBool("Active", true);
    Star2.SetBool("Active", true);
    Star3.SetBool("Active", true);
  }

  void ResetStars()
  {
    Star1.SetBool("Active", false);
    Star2.SetBool("Active", false);
    Star3.SetBool("Active", false);
  }

  string ToTitleCase(string stringToConvert)
  {
    string firstChar = stringToConvert[0].ToString();
    return (stringToConvert.Length > 0 ? firstChar.ToUpper() + stringToConvert.Substring(1) : stringToConvert);

  }
}