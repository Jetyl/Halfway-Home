using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using LitJson;

public class ConversationDisplay : MonoBehaviour
{

    static public bool ConversationModeOn = false;

    ConversationSystem Converse;
    

    int ActionIndex;

    int NodeIndex;

    ConvNode CurrentNode;

    //interuption related stuff
    //List<ConvInteruprt> interuptFlags;

    int InteruptFailNode = 0;

    //does opening/closing the phone count as an interupt rn
    bool PhoneInterupt = false;

    bool ExaminedItem = false;
    Sprite ItemExamined;
    bool ExamineInterupt = false;

	// Use this for initialization
	void Start ()
    {
        
        Space.Connect<ConversationEvent>(Events.StartConversation, OnStart);

        Space.Connect<DefaultEvent>(Events.FinishedDescription, NextNode);
        Space.Connect<DefaultEvent>(Events.NextConversationNode, NextNode);

        Space.Connect<ChoiceEvent>(Events.ConversationChoice, NextNode);

        //Space.Connect<InteruptionOptionEvent>(Events.Counter, InteruptionOptionOpen);
        //Space.Connect<InteruptionEvent>(Events.Interupt, AttemptInterupt);
        //Space.Connect<ValueChangeEvent>(Events.ConversationCheckpoint, SetInteruptFailDestination);

        //Space.Connect<DefaultEvent>(Events.PutAwayPhone, OnPhoneClose);
        //Space.Connect<ItemEvent>(Events.ExamineItem, OnItemExamined);
        //Space.Connect<DefaultEvent>(Events.CloseInventory, OnInventoryClose);

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnStart(ConversationEvent eventdata)
    {
        ConversationModeOn = true;
        //Space.DispatchEvent(Events.OpenUI, new UIEvent(this));
        Converse = eventdata.conversation;
        //interuptFlags = new List<ConvInteruprt>();
        //InteruptFailNode = 0;
       // PhoneInterupt = false;
        //ExaminedItem = false;
        //ExamineInterupt = false;
        //ItemExamined = null;

        NextNode(0);
        //print("on");
    }


    public void NextNode(DefaultEvent eventdata)
    {

        if (!ConversationModeOn)
            return;

        if (ExaminedItem)
            return;


        NextNode(CurrentNode.Destination);

    }

    public void NextAction()
    {

        if (!ConversationModeOn)
            return;


        CurrentNode.CallAction();


        if (CallNext())
            NextNode(CurrentNode.Destination);


    }


    public void NextNode(ChoiceEvent eventdata)
    {

        if (!ConversationModeOn)
            return;

        var curOP = (ConvChoice)CurrentNode;

        foreach (var choice in curOP.Options)
        {
            if (choice.choicedata == eventdata.choicedata)
            {
                NextNode(choice.DestinationNode);
                return;
            }
                
        }

        

    }

    public void NextNode(int index)
    {

        if (!ConversationModeOn)
            return;

        if (index == -1)
        {
            //exit system
            ConversationModeOn = false;
            //in case an unclosed end, send again
            //Space.DispatchEvent(Events.EndConversation);
            //Space.DispatchEvent(Events.CloseUI, new UIEvent(this));
            
            Converse = null;
            CurrentNode = null;

            //if (UIControl.IsOpened())
               // Space.DispatchEvent(Events.CloseDescription);

            //print(UIControl.IsOpened());

            return;
        }
        CurrentNode = Converse.GetNode(index);
        NodeIndex = index;
        
        NextAction();

    }


    private bool CallNext()
    {
        if (CurrentNode is ConvLine)
            return false;
        if (CurrentNode is ConvChoice)
            return false;
        if(CurrentNode is ConvDelay)
        {
            var curDEL = (ConvDelay)CurrentNode;
            StartCoroutine(Delay(curDEL.Time, curDEL.Destination));
            return false;
        }
        

        return true;
    }

    /*
    void AttemptInterupt(InteruptionEvent eventdata)
    {

        for(int i = 0; i < interuptFlags.Count; ++i)
        {

            if(interuptFlags[i].CorrectInterupt(eventdata.answer))
            {
                //do a thing
                NextNode(interuptFlags[i].DestinationID);
                return;
            }

        }

        //if failed, go to this point
        NextNode(InteruptFailNode);

        //assuming it is not set, it will just default to the begining



    }

    void InteruptionOptionOpen(InteruptionOptionEvent eventdata)
    {
        
        for(int i = 0; i < eventdata.interupts.Count; ++i)
        {
            if (eventdata.Open)
            {
                if (!interuptFlags.Contains(eventdata.interupts[i]))
                {
                    interuptFlags.Add(eventdata.interupts[i]);

                    if (!PhoneInterupt && eventdata.interupts[i].type == InteruptionTypes.Phone)
                        PhoneInterupt = true;
                    if (!ExamineInterupt && eventdata.interupts[i].type == InteruptionTypes.Inventory)
                        ExamineInterupt = true;

                }
            }
            else
            {
                if (interuptFlags.Contains(eventdata.interupts[i]))
                {
                    interuptFlags.Remove(eventdata.interupts[i]);

                    if (PhoneInterupt && eventdata.interupts[i].type == InteruptionTypes.Phone)
                        PhoneInterupt = false;
                    if (ExamineInterupt && eventdata.interupts[i].type == InteruptionTypes.Inventory)
                        ExamineInterupt = false;
                }
            }
                
        }

    }
    */
    /*
    void SetInteruptFailDestination(ValueChangeEvent eventdata)
    {
        InteruptFailNode = eventdata.Value_int;
    }


    void OnPhoneClose(DefaultEvent eventdata)
    {
        if (!ConversationModeOn)
            return;

        if (!PhoneInterupt)
            return;

        AttemptInterupt(new InteruptionEvent(new ConvAnswer(InteruptionTypes.Phone)));


    }

    void OnItemExamined(ItemEvent eventdata)
    {
        ExaminedItem = true;
        ItemExamined = eventdata.item;
    }

    void OnInventoryClose(DefaultEvent eventdata)
    {
        if (!ConversationModeOn)
            return;

        if (!ExaminedItem)
            return;

        ExaminedItem = false;

        if (ExamineInterupt)
        {
            var ans = new ConvAnswer(InteruptionTypes.Inventory);
            ans.item = ItemExamined;
            AttemptInterupt(new InteruptionEvent(ans));
        }
        else
        {
            //reset conversation
            NextNode(NodeIndex);
        }
    }
    */
    IEnumerator Delay(float time_, int Des)
    {

        yield return new WaitForSeconds(time_);

        NextNode(Des);

    }

}


public class ConversationEvent : DefaultEvent
{

    public ConversationSystem conversation;

    public ConversationEvent(JsonData conversationSource)
    {
        conversation = new ConversationSystem(conversationSource);
    }

    public ConversationEvent(TextAsset conversationSource)
    {
        conversation = new ConversationSystem(TextParser.ToJson(conversationSource));
    }

    public ConversationEvent(string conversationSource, GameObject caller_ = null)
    {
        conversation = new ConversationSystem(TextParser.ToJson(conversationSource));
    }

}

public class ConversationEndEvent : DefaultEvent
{

    public int EndID;

    public ConversationEndEvent(int DestinationKey)
    {
        EndID = DestinationKey;
    }
    

}

/*
public class InteruptionOptionEvent : DefaultEvent
{
    public List<ConvInteruprt> interupts;

    public bool Open;

    public InteruptionOptionEvent(List<ConvInteruprt> _interupts, bool opening)
    {
        interupts = _interupts;
        Open = opening;
    }
}

public class InteruptionEvent : DefaultEvent
{
    public ConvAnswer answer;

    public InteruptionEvent(ConvAnswer answer_)
    {
        answer = answer_;
    }
}
*/