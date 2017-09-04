using UnityEngine;
using System.Collections;

public class ConversationStarter : MonoBehaviour
{

    public EventListener ListeningOn = EventListener.Owner;

    public Events ConverseOn = Events.Inspect;

    public TextAsset Conversation;


    public EventListener[] callOnFinish;
    public Events[] SayOnFinish;
    public bool[] CallOtherObjectOnFinish;
    public GameObject[] OtherObject;

    bool Active;

    // Use this for initialization
    void Start ()
    {
        if (ListeningOn == EventListener.Owner)
            EventSystem.ConnectEvent<DefaultEvent>(gameObject, ConverseOn, Converse);
        else if (ListeningOn == EventListener.Space)
            Space.Connect<DefaultEvent>(ConverseOn, Converse);

        Space.Connect<ConversationEndEvent>(Events.EndConversation, OnEnd);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Converse(DefaultEvent eventdata)
    {
        Active = true;
        print(gameObject);
        Space.DispatchEvent(Events.StartConversation, new ConversationEvent(Conversation));
    }


    void OnEnd(ConversationEndEvent eventdata)
    {
        if (!Active)
            return;

        int EndID = eventdata.EndID;
        if (callOnFinish.Length < EndID)
            return;


        if (CallOtherObjectOnFinish[EndID] == false)
        {
            if (callOnFinish[EndID] == EventListener.Owner)
                EventSystem.DispatchEvent(gameObject, SayOnFinish[EndID]);
            else if (callOnFinish[EndID] == EventListener.Space)
                Space.DispatchEvent(SayOnFinish[EndID]);
        }
        else
        {
            EventSystem.DispatchEvent(OtherObject[EndID], SayOnFinish[EndID]);
        }

        Active = false;
    }

    void OnDestroy()
    {

        if (ListeningOn == EventListener.Owner)
            EventSystem.DisconnectEvent(gameObject, ConverseOn, this);

        if (Space.Instance != null)
        {
            if (ListeningOn == EventListener.Space)
                EventSystem.DisconnectEvent(Space.Instance.gameObject, ConverseOn, this);
        }


        //EventSystem.DisconnectEvent(Space.Instance.gameObject, Events.EndConversation, this);

        gameObject.DispatchEvent(Events.Destroy);
    }

}
