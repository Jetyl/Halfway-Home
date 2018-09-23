using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    
    private Transform pos;
    private RectTransform tangle;

    private float translateTime;

    public AnimationCurve curve;
    public bool moveOnInitialize;

    private Vector3 currentStartPos;
    public Vector3 currentEndPos;

    // Use this for initialization
    public void Start()
    {
        
        tangle = gameObject.GetComponent<RectTransform>();
        pos = transform;

        EventSystem.ConnectEvent<TransformEvent>(gameObject, Events.Translate, OnTranslateEvent);

        if (moveOnInitialize)
        {
            gameObject.DispatchEvent(Events.Translate, new TransformEvent(currentEndPos, translateTime, curve));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void OnTranslateEvent(TransformEvent eventdata)
    {
        StopAllCoroutines();
        
        currentEndPos = eventdata.Vector;
        translateTime = eventdata.Time;
        if (eventdata.Curve != null)
            curve = eventdata.Curve;


        if (tangle)
            StartCoroutine(MoveRect(currentEndPos, translateTime));
        else
            StartCoroutine(MoveNorm(currentEndPos, translateTime));

    }

    void OnTranslateFinished()
    {
        gameObject.DispatchEvent(Events.FinishedTranslate);
    }


    IEnumerator MoveRect(Vector3 Value, float aTime)
    {
        currentStartPos = tangle.anchoredPosition;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            MoveAnimation(tangle, t);

            yield return null;
        }

        MoveAnimation(tangle, 1);
        OnTranslateFinished();
    }

    public void MoveAnimation(RectTransform _rectTransform, float _counterTween)
    {
        float evaluatedValue = curve.Evaluate(_counterTween);
        Vector3 valueAdded = (currentEndPos - currentStartPos) * evaluatedValue;

        _rectTransform.anchoredPosition = (Vector2)(currentStartPos + valueAdded);
    }

    IEnumerator MoveNorm(Vector3 Value, float aTime)
    {
        currentStartPos = pos.localPosition;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            MoveAnimation(pos, t);

            yield return null;
        }

        MoveAnimation(pos, 1);
        OnTranslateFinished();
    }

    public void MoveAnimation(Transform _Transform, float _counterTween)
    {
        float evaluatedValue = curve.Evaluate(_counterTween);
        Vector3 valueAdded = (currentEndPos - currentStartPos) * evaluatedValue;

        _Transform.localPosition = currentStartPos + valueAdded;
    }

}

public class TransformEvent : DefaultEvent
{
    public Vector3 Vector;
    public float Time;
    public AnimationCurve Curve;

    public TransformEvent(Vector3 Vector_, float time_, AnimationCurve curve_ = null)
    {
        Vector = Vector_;
        Curve = curve_;
        Time = time_;
    }

}