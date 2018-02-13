/******************************************************************************/
/*!
File:   WellbeingStatDisplay.cs
Author: Jesse Lozano & John Myres
All content © 2018 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellbeingStatDisplay : MonoBehaviour
{
    public Image FrontGauge;
    public Image BackGauge;
    public float ChangeTime = 2;

    public Personality.Wellbeing WellnessStat;

    public List<UIStatColorMarkers> ColorChanges;

    public List<UIStatColorMarkers> BackColorChanges;

    // Use this for initialization
    void Start ()
    {

        Space.Connect<DefaultEvent>(Events.StatChange, UpdateStats);
        UpdateStats(new DefaultEvent());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void UpdateStats(DefaultEvent eventdata)
    {
        StopAllCoroutines();

        int stat = Game.current.Self.GetWellbingStat(WellnessStat);
        float percent = (float)stat / 100f;

        StartCoroutine(UpdateGauges(percent, ChangeTime));
    }


    IEnumerator UpdateGauges(float Percentage, float aTime)
    {

        var startVal = FrontGauge.fillAmount;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            float percentagepoint = Mathf.Lerp(startVal, Percentage, t);
            FrontGauge.fillAmount = percentagepoint;
            var newFrontColor = FrontGauge.color;
            var newBackColor = BackGauge.color;
            foreach(var color in ColorChanges)
            {
                if (percentagepoint >= color.PercentagePastPoint)
                    newFrontColor = color.statColor;
            }
            FrontGauge.color = newFrontColor;

            foreach(var color in BackColorChanges)
            {
                if (percentagepoint >= color.PercentagePastPoint)
                    newBackColor = color.statColor;
            }
            FrontGauge.color = newFrontColor;
            BackGauge.color = newBackColor;

            yield return null;
        }

        FrontGauge.fillAmount = Percentage;
    }
}

[System.Serializable]
public class UIStatColorMarkers
{
    [Range(0, 1)]
    public float PercentagePastPoint;
    public Color statColor = Color.green;
}