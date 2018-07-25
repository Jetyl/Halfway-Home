using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUpdateDisplay : MonoBehaviour
{

    public AnimationCurve SwingCurve;

    public float AnimationTime;

    public float ClockFadeTime = 1;

    [Range(0, 23)]
    public int DayTimeStart = 6; //military time
    [Range(0, 23)]
    public int DayTimeEnd = 18; //military time

    public Color NightTimeBacking;
    public Color NightTimeNumbers;

    Color DayTimeBacking;
    Color DayTimeNumbers;

    public float MultiDayChangeMultiplier = 0.8f;

    int currentDay = 0;
    int currentHour = 0;
    float CurRot;

    public Image Hand;
    public Image Face;
    public Image Numbers;
    public Image Case;
    public TextMeshProUGUI MeridianText;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.TimeChange, UpdateTime);

        currentHour = Game.current.Hour;
        currentDay = Game.current.Day;
        CurRot = -((currentHour + (currentDay * 24)) / 12.0f) * (360);
        transform.eulerAngles = new Vector3(0, 0, CurRot);

        DayTimeBacking = Face.color;
        DayTimeNumbers = Numbers.color;

        Color aHand = Hand.color;
        aHand.a = 0;
        Hand.color = aHand;

        Color aFace = Face.color;
        aFace.a = 0;
        Face.color = aFace;


        Color aNum = Numbers.color;
        aNum.a = 0;
        Numbers.color = aNum;
        MeridianText.color = aNum;

        Color aCase = Case.color;
        aCase.a = 0;
        Case.color = aCase;


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void CallFade(Image object_, bool NoAlpha = false)
    {
        Color  aColor = object_.color;

        if (NoAlpha)
            aColor.a = 0;
        else
            aColor.a = 1;

        object_.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aColor, ClockFadeTime));
    }

    void RemoveClock()
    {
        CallFade(Hand, true);
        CallFade(Face, true);
        CallFade(Numbers, true);
        CallFade(Case, true);

        Color aColor = MeridianText.color;
        aColor.a = 0;
        MeridianText.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aColor, ClockFadeTime));

    }

    void ShowClock()
    {
        CallFade(Hand);
        CallFade(Face);
        CallFade(Numbers);
        CallFade(Case);


        Color aColor = MeridianText.color;
        aColor.a = 1;
        MeridianText.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aColor, ClockFadeTime));
    }

    void UpdateTime(DefaultEvent eventdata)
    {
        var oldHour = currentHour;
        currentHour = Game.current.Hour;
        var oldday = currentDay;
        currentDay = Game.current.Day;

        float multiplier = 1;

        if (oldHour == currentHour && currentDay == oldday)
        {
            Space.DispatchEvent(Events.ClockFinished);
            return;
        }
            

        if (currentDay - oldday != 0)
        {
            multiplier = Mathf.Abs(currentDay - oldday) * MultiDayChangeMultiplier;
            if (multiplier < 1)
                multiplier = 1;
        }



        StartCoroutine(FlipMeridian(AnimationTime * multiplier));

        StartCoroutine(Swing(AnimationTime * multiplier));
    }

    IEnumerator FlipMeridian(float aTime)
    {
        yield return new WaitForSeconds(ClockFadeTime + (aTime/2));

        if (currentHour < 12)
            MeridianText.text = "AM";
        else
            MeridianText.text = "PM";

    }

    IEnumerator Swing(float aTime)
    {
        ShowClock();

        yield return new WaitForSeconds(ClockFadeTime);

        if(currentHour >= DayTimeStart && currentHour <= DayTimeEnd)
        {

            //Hand.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aHand, ClockFadeTime));
            Face.gameObject.DispatchEvent(Events.Fade, new FadeEvent(DayTimeBacking, aTime/2f));
            Numbers.gameObject.DispatchEvent(Events.Fade, new FadeEvent(DayTimeNumbers, aTime / 2f));

            MeridianText.gameObject.DispatchEvent(Events.Fade, new FadeEvent(DayTimeNumbers, aTime / 2f));
        }
        else
        {
            Face.gameObject.DispatchEvent(Events.Fade, new FadeEvent(NightTimeBacking, aTime/2f));
            Numbers.gameObject.DispatchEvent(Events.Fade, new FadeEvent(NightTimeNumbers, aTime / 2f));
            MeridianText.gameObject.DispatchEvent(Events.Fade, new FadeEvent(NightTimeNumbers, aTime / 2f));
        }


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
