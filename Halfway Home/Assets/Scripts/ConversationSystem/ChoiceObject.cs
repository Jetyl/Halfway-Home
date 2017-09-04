using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChoiceObject : MonoBehaviour
{

    public EventListener ListeningOn = EventListener.Owner;

    public Events ChoiceOn = Events.Inspect;


    public Choices[] choices;

    // Use this for initialization
    void Start ()
    {

        if(ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, ChoiceOn, SpawnChoice);
        else if(ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(ChoiceOn, SpawnChoice);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void SpawnChoice (DefaultEvent eventdata)
    {

        foreach(var choice in choices)
        {
            choice.OwnerRef = gameObject;
        }
        
        Space.DispatchEvent(Events.Choice, new ChoiceDisplayEvent(choices));

    }

}

