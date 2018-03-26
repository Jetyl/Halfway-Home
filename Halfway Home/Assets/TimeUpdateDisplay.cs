using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUpdateDisplay : MonoBehaviour
{

    public AnimationCurve SwingCurve;

    public float AnimationTime;

    public Color DayTimeColor;
    public Color NightTimeColor;

    public float MultiDayChangeMultiplier = 0.8f;

    int currentDay = 0;
    int currentHour = 0;
    float CurRot;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.TimeChange, UpdateTime);

        currentHour = Game.current.Hour;
        currentDay = Game.current.Day;
        CurRot = -((currentHour + (currentDay * 24)) / 12.0f) * (360);
        transform.eulerAngles = new Vector3(0, 0, CurRot);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void UpdateTime(DefaultEvent eventdata)
    {
        currentHour = Game.current.Hour;
        var oldday = currentDay;
        currentDay = Game.current.Day;

        float multiplier = 1;

        if(currentDay - oldday != 0)
        {
            multiplier = Mathf.Abs(currentDay - oldday) * MultiDayChangeMultiplier;
            if (multiplier < 1)
                multiplier = 1;
        }

        StartCoroutine(Swing(AnimationTime * multiplier));
    }



    IEnumerator Swing(float aTime)
    {
        float rot = -((currentHour + (currentDay *24)) / 12.0f) * (360);

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            SwingAnimation(rot, t);
            yield return null;
        }

        CurRot = rot;
        transform.eulerAngles = new Vector3(0, 0, CurRot);

    }

    public void SwingAnimation(float rot, float _counterTween)
    {
        float evaluatedValue = SwingCurve.Evaluate(_counterTween);
        float valueAdded = (rot - CurRot) * evaluatedValue;
        

        transform.eulerAngles = new Vector3(0, 0, CurRot + valueAdded);
    }

}
