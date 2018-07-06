using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatUpdateText : MonoBehaviour
{
    public GameObject TextPrefab;

    public Vector3 StartPosition;

    public Vector3 EndPosition;

    public float TranslationTime = 1;
    public AnimationCurve TranslationCurve;
    public float FadeTime = 0.5f;

    public Color WellnessDown = Color.green;
    public Color WellnessUp = Color.red;

    public Color AwarenessColor = Color.cyan;
    public Color GraceColor = Color.yellow;
    public Color ExpressionColor = Color.magenta;

    public float MultiDelayValue = 0.5f;

    int ActiveTexts;

	// Use this for initialization
	void Start ()
    {

        Space.Connect<ChangeStatEvent>(Events.AddStat, OnAddValue);
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAddValue(ChangeStatEvent eventdata)
    {

        GameObject TextObj = Instantiate(TextPrefab, transform);
        TextMeshProUGUI Text = TextObj.GetComponent<TextMeshProUGUI>();

        Color nextColor = Color.white;
        TextObj.transform.localPosition = StartPosition;

        if (eventdata.Wellbeing)
        {

            string Addative = "+";
            nextColor = WellnessUp;
            
            if (eventdata.Assign)
            {
                var value = Game.current.Self.GetWellbingStat(eventdata.WellnessStat);
                if (value < eventdata.Value)
                {
                    Addative = "-";
                    nextColor = WellnessDown;
                }

                Text.text = Addative + " " + (Mathf.Abs(value - eventdata.Value)) + " " + eventdata.WellnessStat;
            }
            else
            {

                if (eventdata.Value < 0)
                {
                    Addative = "-";
                    nextColor = WellnessDown;
                }

                Text.text = Addative + " " + Mathf.Abs(eventdata.Value) + " " + eventdata.WellnessStat;
            }
            
        }
        else
        {
            switch(eventdata.SocialStat)
            {
                case Personality.Social.Awareness:
                    nextColor = AwarenessColor;
                    break;
                case Personality.Social.Grace:
                    nextColor = GraceColor;
                    break;
                case Personality.Social.Expression:
                    nextColor = ExpressionColor;
                    break;
            }

            Text.text = eventdata.SocialStat + " Up";
        }

        StartCoroutine(Animate(Text, nextColor));

    }


    IEnumerator Animate(TextMeshProUGUI Text, Color color)
    {
        
        Color aColor = color;
        aColor.a = 0;
        Text.color = aColor;
        ActiveTexts += 1;

        yield return new WaitForSeconds(MultiDelayValue * (ActiveTexts - 1));
        
        Text.gameObject.DispatchEvent(Events.Translate, new TransformEvent(EndPosition, TranslationTime, TranslationCurve));
        Text.gameObject.DispatchEvent(Events.Fade, new FadeEvent(color, FadeTime));

        yield return new WaitForSeconds(TranslationTime - FadeTime);

        Text.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aColor, FadeTime));

        yield return new WaitForSeconds(FadeTime);

        ActiveTexts -= 1;
        Destroy(Text.gameObject, 1);
    }


}
