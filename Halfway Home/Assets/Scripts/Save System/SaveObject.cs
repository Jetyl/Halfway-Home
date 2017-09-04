using UnityEngine;
using System.Collections;

public class SaveObject : MonoBehaviour
{

    public EventListener ListeningOn = EventListener.Owner;

    public Events SaveOn = Events.Inspect;


    public Choices[] SaveOptions = new Choices[3];

    // Use this for initialization
    void Start ()
    {

        if (ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, SaveOn, OnSave);
        else if (ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(SaveOn, OnSave);


        foreach (var choice in SaveOptions)
        {
            choice.OwnerRef = gameObject;
        }

        EventSystem.ConnectEvent<DefaultEvent>(gameObject, Events.Dream, SaveAndContinue);
        EventSystem.ConnectEvent<DefaultEvent>(gameObject, Events.ReturnToMainMenu, SaveAndMenu);
        EventSystem.ConnectEvent<DefaultEvent>(gameObject, Events.QuitGame, SaveAndQuit);


    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    void OnSave(DefaultEvent eventdata)
    {
        //Space.DispatchEvent(Events.DisableMove);
        //Instantiate(Resources.Load("Prefabs/SaveMenu"));

        Space.DispatchEvent(Events.Choice, new ChoiceDisplayEvent(SaveOptions));



    }

    public void SaveAndContinue(DefaultEvent eventdata)
    {
        SaveSystem.SaveGame();


        Space.DispatchEvent(Events.Dream);

    }

    public void SaveAndMenu(DefaultEvent eventdata)
    {
        SaveSystem.SaveGame();

        Space.DispatchEvent(Events.ReturnToMainMenu);
    }

    public void SaveAndQuit(DefaultEvent eventdata)
    {
        SaveSystem.SaveGame();

        Space.DispatchEvent(Events.QuitGame);
    }





}
