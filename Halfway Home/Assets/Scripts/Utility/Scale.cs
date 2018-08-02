using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private Transform pos;
    private RectTransform tangle;

    private float scaleTime;

    public AnimationCurve curve;
    public bool actOnInitialize;

    private Vector3 currentStart;
    public Vector3 currentEnd;

    // Use this for initialization
    public void Start()
    {

        tangle = gameObject.GetComponent<RectTransform>();
        pos = transform;

        EventSystem.ConnectEvent<TransformEvent>(gameObject, Events.Scale, OnScaleEvent);

        if (actOnInitialize)
        {
            gameObject.DispatchEvent(Events.Scale, new TransformEvent(currentEnd, scaleTime, curve));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnScaleEvent(TransformEvent eventdata)
    {
        StopAllCoroutines();

        currentEnd = eventdata.Vector;
        scaleTime = eventdata.Time;
        if (eventdata.Curve != null)
            curve = eventdata.Curve;
        
        if (tangle)
            StartCoroutine(MoveRect(currentEnd, scaleTime));
        else
            StartCoroutine(MoveNorm(currentEnd, scaleTime));

    }

    void OnScaleFinished()
    {
        gameObject.DispatchEvent(Events.FinishedScale);
    }


    IEnumerator MoveRect(Vector3 Value, float aTime)
    {
        currentStart = tangle.localScale;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            MoveAnimation(tangle, t);

            yield return null;
        }

        OnScaleFinished();
    }

    public void MoveAnimation(RectTransform _rectTransform, float _counterTween)
    {
        float evaluatedValue = curve.Evaluate(_counterTween);
        Vector3 valueAdded = (currentEnd - currentStart) * evaluatedValue;

        _rectTransform.localScale = (Vector2)(currentStart + valueAdded);
    }

    IEnumerator MoveNorm(Vector3 Value, float aTime)
    {
        currentStart = pos.localScale;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            MoveAnimation(pos, t);

            yield return null;
        }

        OnScaleFinished();
    }

    public void MoveAnimation(Transform _Transform, float _counterTween)
    {
        float evaluatedValue = curve.Evaluate(_counterTween);
        Vector3 valueAdded = (currentEnd - currentStart) * evaluatedValue;

        _Transform.localScale = currentStart + valueAdded;
    }
}
