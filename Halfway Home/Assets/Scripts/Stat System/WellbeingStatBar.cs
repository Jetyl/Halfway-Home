using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WellbeingStatBar : MonoBehaviour
{
  public Image FillBar;
  public Image BackBar;
  public Slider StatBar;
  public TextMeshProUGUI StatText;
  public TextMeshProUGUI DescText;
  public TextMeshProUGUI FeelingText;
  public Image Penalty1;
  public Image Penalty2;
  public Sprite DepressionPenalty1;
  public Sprite DepressionPenalty2;
  public Sprite StressPenalty1;
  public Sprite StressPenalty2;

  public List<UIStatDescriptions> TextAndColorChanges;

  public List<UIStatColorMarkers> BackColorChanges;

  private Personality.Wellbeing DisplayedStat;
  // Use this for initialization
  void Start ()
  {
    Space.Connect<DefaultEvent>(Events.StatChange, UpdateStats);
    UpdateStats(new DefaultEvent());
  }

  public void UpdateDisplayedStat(Personality.Wellbeing newStat)
  {
    DisplayedStat = newStat;
  }
	
	// Update is called once per frame
	void UpdateStats (DefaultEvent e)
  {
    UpdateDisplay();
  }

  public void SetNewDisplay(int stat)
  {
    DisplayedStat = (Personality.Wellbeing)stat;
    UpdateDisplay();

    if (DisplayedStat == Personality.Wellbeing.Depression)
    {
      Penalty1.gameObject.SetActive(true);
      Penalty2.gameObject.SetActive(true);
      Penalty1.sprite = DepressionPenalty1;
      Penalty2.sprite = DepressionPenalty2;
    }
    else if (DisplayedStat == Personality.Wellbeing.Stress)
    {
      Penalty1.gameObject.SetActive(true);
      Penalty2.gameObject.SetActive(true);
      Penalty1.sprite = StressPenalty1;
      Penalty2.sprite = StressPenalty2;
    }
    else
    {
      Penalty1.gameObject.SetActive(false);
      Penalty2.gameObject.SetActive(false);
    }
  }

  void UpdateDisplay()
  {
    int stat = Game.current.Self.GetWellbingStat(DisplayedStat);
    DescText.text = DisplayedStat.ToString();
    StatBar.value = stat;

    Color newFrontColor = FillBar.color;
    string newFeel = FeelingText.text;
    foreach (var stage in TextAndColorChanges)
    {
      if ((float)stat / 100f >= stage.PercentagePastPoint)
      {
        newFrontColor = stage.statColor;
        if (DisplayedStat == Personality.Wellbeing.Depression)
        {
          newFeel = stage.DepressionStageText;
        }
        else if (DisplayedStat == Personality.Wellbeing.Fatigue)
        {
          newFeel = stage.FatigueStageText;
        }
        else
        {
          newFeel = stage.StressStageText;
        }
      }
    }
    FillBar.color = newFrontColor;
    FeelingText.text = newFeel;

    Color newBackColor = BackBar.color;
    foreach (var stage in BackColorChanges)
    {
      if ((float)stat / 100f >= stage.PercentagePastPoint)
        newBackColor = stage.statColor;
    }
    BackBar.color = newBackColor;

    StatText.text = stat.ToString();
  }
}

[System.Serializable]
public class UIStatDescriptions : UIStatColorMarkers
{
  public string FatigueStageText;
  public string StressStageText;
  public string DepressionStageText;
}