using UnityEngine;
using System.Collections;

public class MakeProgress : MonoBehaviour
{

    public EventListener ListeningOn = EventListener.Owner;

    public Events ChangeOn = Events.Inspect;


    public ProgressPoint Progress;

    // Use this for initialization
    void Start ()
    {

        //Game.current.Progress.AddBool(Progress.EventName, !Progress.BoolName);
        if (Game.current.Progress.Contains(Progress.ProgressName) != true)
            print(Progress.ProgressName + " not set on Object " + gameObject.name);

        if (ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, ChangeOn, UpdateProgress);
        else if (ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(ChangeOn, UpdateProgress);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void UpdateProgress(DefaultEvent eventData)
    {

        Game.current.Progress.UpdateProgress(Progress.ProgressName, Progress);

    }

}
