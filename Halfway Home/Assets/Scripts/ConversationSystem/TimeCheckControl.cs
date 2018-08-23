using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCheckControl : MonoBehaviour
{
    public TextAsset Progress;

    ConversationSystem TimeCheck;

    int ActionIndex;

    int NodeIndex;

    ConvNode CurrentNode;

    int CurrentHour;
    int CurrentDay;

    // Use this for initialization
    void Start ()
    {
        //CurrentDay = Game.current.Day;
        //CurrentHour = Game.current.Hour;
        OnStart(new ConversationEvent(Progress));

        Space.Connect<DefaultEvent>(Events.TimeChange, OnChangedTime);

        OnChangedTime(new DefaultEvent());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnStart(ConversationEvent eventdata)
    {

        TimeCheck = eventdata.conversation;
        NextNode(0);
    }

    public void NextAction()
    {

        CurrentNode.CallAction();
        print("an action occured now");

        if (CallNext())
            NextNode(CurrentNode.Destination);


    }

    public void OnChangedTime(DefaultEvent eventdata)
    {
        //is positive
        if(Game.current.TimeDifference(CurrentHour, CurrentDay) > 0)
        {
            while(Game.current.TimeDifference(CurrentHour, CurrentDay) !=0)
            {
                CurrentHour += 1;
                if(CurrentHour > 23)
                {
                    CurrentHour = 0;
                    CurrentDay += 1;
                }

                NextNode(CheckTimeForChange(CurrentDay, CurrentHour));
            }
        }


        CurrentDay = Game.current.Day;
        CurrentHour = Game.current.Hour;

    }

    public void NextNode(int index)
    {

        //print(index);
        if (index == -1)
        {
            return;
        }
        CurrentNode = TimeCheck.GetNode(index);
        NodeIndex = index;

        NextAction();

    }


    private bool CallNext()
    {

        if (CurrentNode is ConvNull)
            return false;
        return true;
    }



    public int CheckTimeForChange(int Day, int Hour)
    {
        var options = new List<ConvMap>();

        foreach (var node in TimeCheck.GetAllNodes())
        {
            if (node is ConvTime)
            {
                if (((ConvTime)node).AvalibleNow(Day, Hour))
                    return node.Destination;
            }

        }
        
        return -1;
    }

}
