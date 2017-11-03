using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellbeingStatDisplay : MonoBehaviour
{
    public Image Bar;
    public float ChangeTime = 2;

    public Personality.Wellbeing WellnessStat;

    public List<UIStatColorMarkers> ColorChanges;

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
        int stat = Game.current.Self.GetWellbingStat(WellnessStat);
        float percent = (float)stat / 100f;
        //Bar.transform.localScale = new Vector3(percent, 1, 1);
        StartCoroutine(ChangeSize(percent, ChangeTime));
    }


    IEnumerator ChangeSize(float Percentage, float aTime)
    {

        var startVal = Bar.transform.localScale.x;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            float percentagepoint = Mathf.Lerp(startVal, Percentage, t);
            Bar.transform.localScale = new Vector3(percentagepoint, 1, 1);
            var newcolor = Bar.color;
            foreach(var color in ColorChanges)
            {
                if (percentagepoint >= color.PercentagePastPoint)
                    newcolor = color.statColor;
            }
            Bar.color = newcolor;

            yield return null;
        }



        Bar.transform.localScale = new Vector3(Percentage, 1, 1);

    }


}

[System.Serializable]
public class UIStatColorMarkers
{
    [Range(0, 1)]
    public float PercentagePastPoint;
    public Color statColor = Color.green;
}