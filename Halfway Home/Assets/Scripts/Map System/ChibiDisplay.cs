using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChibiDisplay : MonoBehaviour
{
    public float ActionTime = 1;
    public float ActionWaitTimeMin = 2;
    public float ActionWaitTimeMax = 5;
    public float HopDistance = 1;
    public AnimationCurve[] HopCurves;
    public float BoundsBuffer = 25; //how far out of its rect is the chibi allowed to go

    Image sprite;
    RectTransform trans;
    Vector2 startpoint;
    [HideInInspector]
    public bool Active;
    float Timer;

	// Use this for initialization
	void Start ()
    {
        sprite = GetComponent<Image>();
        trans = GetComponent<RectTransform>();
        startpoint = trans.anchoredPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!Active)
            return;

        if (Timer < 0)
        {
            Timer = Random.Range(ActionWaitTimeMin, ActionWaitTimeMax);
            HopAnimation();

        }
        else
            Timer -= Time.deltaTime;

	}

    public void Clear()
    {
        sprite.sprite = null;

        var colo = sprite.color;
        colo.a = 0;
        sprite.color = colo;

        trans.anchoredPosition = startpoint;
        trans.localScale = Vector3.one;
        Active = false;
        StopAllCoroutines();
    }

    public void SetSprite(Sprite image)
    {
        sprite.sprite = image;

        var colo = sprite.color;
        colo.a = 1;
        sprite.color = colo;

        Active = true;
    }
    
    void HopAnimation()
    {
        StagePosition hop = (StagePosition)Random.Range(0, System.Enum.GetValues(typeof(StagePosition)).Length);

        switch(hop)
        {
            case StagePosition.Left:
                if (trans.offsetMin.x < -Mathf.Abs(BoundsBuffer))
                    return;

                trans.localScale = Vector3.one;
                AnimationCurve curve = HopCurves[Random.Range(0, HopCurves.Length)];
                StartCoroutine(Animate(curve, -HopDistance));
                break;
            case StagePosition.Right:
                if (trans.offsetMax.x > Mathf.Abs(BoundsBuffer))
                    return;

                trans.localScale = new Vector3(-1, 1, 1);
                AnimationCurve curve2 = HopCurves[Random.Range(0, HopCurves.Length)];
                StartCoroutine(Animate(curve2, HopDistance));
                break;
            case StagePosition.Center:
                AnimationCurve curve3 = HopCurves[Random.Range(0, HopCurves.Length)];
                StartCoroutine(Animate(curve3, 0));
                break;
            default:
                break;
        }
    }


    IEnumerator Animate(AnimationCurve Curve, float distance)
    {
        var StartX = trans.anchoredPosition.x;
        var EndX = StartX + distance;
        var startY = trans.anchoredPosition.y;
        

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / ActionTime)
        {
            float yValue = Curve.Evaluate(t) * HopDistance;
            float xValue = Mathf.Lerp(StartX, EndX, t);
            Vector2 newpos = new Vector2(xValue, startY + yValue);

            trans.anchoredPosition = newpos;

            yield return null;
        }
        


    }
    

}
