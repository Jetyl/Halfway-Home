using UnityEngine;
using System.Collections;

public class DisableEnableMove : MonoBehaviour
{

    public EventListener ListeningOn = EventListener.Owner;

    public Events ActOn = Events.Inspect;


    public bool Enable = false;


	// Use this for initialization
	void Start ()
    {
        if (ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, ActOn, Action);
        else if (ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(ActOn, Action);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Action(DefaultEvent eventdata)
    {

        if (Enable)
        {
            Space.DispatchEvent(Events.EnableMove);
        }
        else
        {
            Space.DispatchEvent(Events.DisableMove);
        }

    }

}
