/******************************************************************************/
/*!
File:   TimelineSystem.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineSystem : MonoBehaviour
{

    public static TimelineSystem Current;

    ConversationSystem TimeLine;
    
    int ActionIndex;

    int NodeIndex;

    ConvNode CurrentNode;


    private void Awake()
    {

        if (Current != null && Current != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Current = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

        Space.Connect<ConversationEvent>(Events.StartGame, OnStart);

        Space.Connect<DefaultEvent>(Events.FinishedStory, StoryOver);
        Space.Connect<DestinationNodeEvent>(Events.LeaveMap, NextNode);

        //Space.Connect<ChoiceEvent>(Events.ConversationChoice, NextNode);



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStart(ConversationEvent eventdata)
    {
        
        TimeLine = eventdata.conversation;
        NextNode(0);
    }


    public void NextNode(DestinationNodeEvent eventdata)
    {
        
        NextNode(eventdata.NodeID);

    }

    public void StoryOver(DefaultEvent eventdata)
    {
        Game.current.InCurrentStory = false;
        NextNode(CurrentNode.Destination);

    }

    public void NextAction()
    {

        CurrentNode.CallAction();


        if (CallNext())
            NextNode(CurrentNode.Destination);


    }

    

    public void NextNode(int index)
    {
        print(index);
        if (index == -1)
        {
            //Game Over
            Space.DispatchEvent(Events.EndGame);
            return;
        }
        CurrentNode = TimeLine.GetNode(index);
        NodeIndex = index;

        NextAction();

    }


    private bool CallNext()
    {
        
        if (CurrentNode is ConvReturn)
            return false;
        if (CurrentNode is ConvInk)
            return false;
        if (CurrentNode is ConvLoad)
            return false;
        return true;
    }

    

    public List<ConvMap> GetOptionsAvalible(int Day, int Hour)
    {
        var options = new List<ConvMap>();

        foreach(var node in TimeLine.GetAllNodes())
        {
            if (node is ConvMap)
            {
                if (((ConvMap)node).AvalibleNow(Day, Hour))
                    options.Add((ConvMap)node);
            }

        }


        return options;
    }
    
    public bool TryCheatCode(string code)
    {
        foreach (var node in TimeLine.GetAllNodes())
        {
            if (node is ConvCheat)
            {
                if (((ConvCheat)node).code == code)
                {
                    NextNode(node.Destination);
                    return true;
                }
            }

        }

        return false;
    }

}


public class DestinationNodeEvent : DefaultEvent
{
    public int NodeID;

    public DestinationNodeEvent(int ID)
    {
        NodeID = ID;
    }

}