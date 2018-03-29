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