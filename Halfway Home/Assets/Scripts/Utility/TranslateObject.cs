using UnityEngine;
using System.Collections;

public class TranslateObject : MonoBehaviour
{
    public EventListener ListeningOn = EventListener.Owner;

    public Events TranslateOn = Events.DefaultEvent;
    
    public GameObject Target;

    public float translateTime = 1;

    public AnimationCurve curve;
    public Vector3 Destination;


    // Use this for initialization
    void Start ()
    {

        if (ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, TranslateOn, OnTranslate);
        else if (ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(TranslateOn, OnTranslate);

        

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnTranslate(DefaultEvent eventdata)
    {
        Target.DispatchEvent(Events.Translate, new TransformEvent(Destination, translateTime, curve));
    }
    

}
