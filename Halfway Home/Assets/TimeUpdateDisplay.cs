using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpdateDisplay : MonoBehaviour
{

    public AnimationCurve SwingCurve;

    public float AnimationTime;

    public float ClockFadeTime = 1;

    [Range(0, 23)]
    public int DayTimeStart = 6; //military time
    [Range(0, 23)]
    public int DayTimeEnd = 18; //military time

    public Color DayTimeColor;
    public Color NightTimeColor;

    public float MultiDayChangeMultiplier = 0.8f;

    int currentDay = 0;
    int currentHour = 0;
    float CurRot;

    public Image Hand;
    public Image Face;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.TimeChange, UpdateTime);

        currentHour = Game.current.Hour;
        currentDay = Game.current.Day;
        CurRot = -((currentHour + (currentDay * 24)) / 12.0f) * (360);
        transform.eulerAngles = new Vector3(0, 0, CurRot);


        Color aHand = Hand.color;
        aHand.a = 0;
        Hand.color = aHand;

        Color aFace = Face.color;
        aFace.a = 0;
        Face.color = aFace;


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void RemoveClock()
    {
        Color aHand = Hand.color;
        aHand.a = 0;
        Hand.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aHand, ClockFadeTime));

        Color aFace = Face.color;
        aFace.a = 0;
        Face.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aFace, ClockFadeTime));


    }

    void ShowClock()
    {
        Color aHand = Hand.color;
        aHand.a = 1;
        Hand.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aHand, ClockFadeTime));

        Color aFace = Face.color;
        aFace.a = 1;
        Face.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aFace, ClockFadeTime));
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
        ShowClock();

        yield return new WaitForSeconds(ClockFadeTime);

        float rot = -((currentHour + (currentDay *24)) / 12.0f) * (360);

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            SwingAnimation(rot, t);
            yield return null;
        }

        CurRot = rot;
        transform.eulerAngles = new Vector3(0, 0, CurRot);

        yield return new WaitForSeconds(Time.deltaTime);

        RemoveClock();
        
        yield return new WaitForSeconds(ClockFadeTime);

        Space.DispatchEvent(Events.ClockFinished);

    }

    public void SwingAnimation(float rot, float _counterTween)
    {
        float evaluatedValue = SwingCurve.Evaluate(_counterTween);
        float valueAdded = (rot - CurRot) * evaluatedValue;
        

        transform.eulerAngles = new Vector3(0, 0, CurRot + valueAdded);
    }

}
