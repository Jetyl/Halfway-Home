using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public enum NodeTypes
{
    EndingNode = -1,
    StartNode = 0,
    ProgressNode = 1,
    ChangeNode = 2,
    LineNode = 3,
    ChoiceNode = 4,
    DelayNode = 5,
    EventNode = 6,
    AnimateNode = 7,
    ImageNode = 8,
    SoundNode = 9,
    MultiProgressNode = 10,
    InkNode = 11,
    MapNode = 12,
    ToMapNode = 13
}


public class ConversationSystem
{

    private List<ConvNode> Nodes;

    

    public ConversationSystem()
    {
        Nodes = new List<ConvNode>();
    }

    public ConversationSystem(JsonData conversation)
    {
        Nodes = new List<ConvNode>();

        
        for(int i = 0; i < conversation.Count; ++i)
        {
            int id = (int)conversation[i]["TypeID"];
            NodeTypes type = (NodeTypes)id;

            switch (type)
            {
                case NodeTypes.StartNode:

                    Nodes.Add(new ConvStart(conversation[i]));

                    break;
                case NodeTypes.EndingNode:

                    Nodes.Add(new ConvEnd(conversation[i]));

                    break;
                case NodeTypes.LineNode:
                    
                    Nodes.Add(new ConvLine(conversation[i]));
                    
                    break;
                case NodeTypes.ProgressNode:
                    Nodes.Add(new ConvProgress(conversation[i]));
                    //get a seperate file for dialog
                    // string name = (string)data["Actions"][i]["File"];

                    //var file = JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/" + name + ".json"));

                    break;
                case NodeTypes.AnimateNode:
                    Nodes.Add(new ConvAnimate(conversation[i]));
                    break;
                case NodeTypes.ChoiceNode:
                    Nodes.Add(new ConvChoice(conversation[i]));
                    break;
                case NodeTypes.EventNode:
                    Nodes.Add(new ConvCall(conversation[i]));
                    break;
                case NodeTypes.DelayNode:
                    Nodes.Add(new ConvDelay(conversation[i]));
                    break;
                case NodeTypes.ChangeNode:
                    Nodes.Add(new ConvChange(conversation[i]));
                    break;
                case NodeTypes.ImageNode:
                    Nodes.Add(new ConvImage(conversation[i]));
                    break;
                case NodeTypes.SoundNode:
                    Nodes.Add(new ConvSound(conversation[i]));
                    break;
                case NodeTypes.MultiProgressNode:
                    Nodes.Add(new ConvMultiProgress(conversation[i]));
                    break;
                case NodeTypes.MapNode:
                    Nodes.Add(new ConvMap(conversation[i]));
                    break;
                case NodeTypes.ToMapNode:
                    Nodes.Add(new ConvReturn(conversation[i]));
                    break;
                case NodeTypes.InkNode:
                    Nodes.Add(new ConvInk(conversation[i]));
                    break;
                default:
                    break;
            }

            //Nodes.Add(new ConvNode(conversation[i]));
        }


    }

    
    public ConvNode GetNode(int Index)
    {

        
        foreach (ConvNode node in Nodes)
        {

            if (node.ID == Index)
                return node;
        }

        //if (Index < Nodes.Count)
        //    return Nodes[Index];

        return null;

    }

    public void AddNode(ConvNode node)
    {


        if (node == null)
            return;

        Nodes.Add(node);
        node.ID = Nodes.IndexOf(node);


    }

    public void AddOption(string Text, ConvNode node, ConvNode dest)
    {

        if (!Nodes.Contains(node))
            AddNode(node);

        if (!Nodes.Contains(dest))
            AddNode(dest);


        ConvOption opt = new ConvOption();

        if (dest == null)
            opt.DestinationNode = -1;
        else
            opt.DestinationNode = dest.ID;

        opt.choicedata = new Choices(Text, true);

        //node.Options.Add(opt);

    }

    public List<ConvNode> GetAllNodes()
    {
        return Nodes;
    }


}


/**
    * CLASS NAME: ConvNode
    * DESCRIPTION  : basic component of conversation system. this is the basic node, every other version should extend from this
**/
abstract public class ConvNode
{

    public int ID;

    //public List<ConvActions> Acts;

    //when it gets to the end of the acts list, it pops up options, if there are any. else, it goes to...
    //public List<ConvOption> Options;

    //the destination, for nodes that need linear linking. defaults out of conversation
    public int Destination = -1;

    
    abstract public void CallAction();
    

    /*

    

    public ConvNode(JsonData data)
    {
        Acts = new List<ConvActions>();
        Options = new List<ConvOption>();

        ID = (int)data["ID"];

        if(data["Actions"] != null)
        {

            for(int i = 0; i < data["Actions"].Count; ++i)
            {

                string type = (string)data["Actions"][i]["Type"];

                switch (type)
                {
                    case "Lines":


                        JsonData lines = data["Actions"][i]["Lines"];
                        
                        int Time = (int)data["Actions"][i]["Time"];

                        Acts.Add(new ConvLine(Time, lines));

                        

                        break;
                    case "File":

                        //get a seperate file for dialog
                        string name = (string)data["Actions"][i]["File"];

                        var file = JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/" + name + ".json"));

                        break;

                    default:
                        break;
                }

            }
            
        }


        if (data["Options"] != null)
        {
            for(int i = 0; i < data["Options"].Count; ++i)
            {
                ConvOption opt = new ConvOption();

                if (data["Options"][i]["Destination"] == null)
                    opt.DestinationNode = -1;
                else
                    opt.DestinationNode = (int)data["Options"][i]["Destination"];

                opt.choicedata = new Choices((string)data["Options"][i]["Text"], true);

                Options.Add(opt);

            }
        }
        else
        {
            Destination = (int)data["Destination"];
        }

    }
    */

}



/**
    * CLASS NAME: ConvStart
    * DESCRIPTION  : the start of the conversation
**/
public class ConvStart : ConvNode
{

    bool Disable;

    public ConvStart(JsonData start)
    {
        ID = (int)start["ID"];
        Destination = (int)start["NextID"];
        Disable = (bool)start["Disable"];
    }


    public override void CallAction()
    {
        if (Disable)
        {
            Debug.Log("UI Off");
            Space.DispatchEvent(Events.Pause);
        }
            
    }

}

/**
    * CLASS NAME: ConvEnd
    * DESCRIPTION  : the End of the conversation
**/
public class ConvEnd : ConvNode
{

    int EndingID;
    bool Enable;
    public ConvEnd(JsonData end)
    {
        ID = (int)end["ID"];
        Destination = -1;
        EndingID = (int)end["EndID"];
        Enable = (bool)end["Enable"];


    }


    public override void CallAction()
    {
        Space.DispatchEvent(Events.EndConversation, new ConversationEndEvent(EndingID));

        if (Enable)
        {
            Debug.Log("UI On");
            Space.DispatchEvent(Events.UnPause);
        }
            

    }

}

/**
    * CLASS NAME: ConvEnd
    * DESCRIPTION  : checking the progress for the conversation
**/
public class ConvProgress : ConvNode
{

    JsonData data;


    public ConvProgress(JsonData pro)
    {
        ID = (int)pro["ID"];
        Destination = -1;
        data = pro;
    }


    public override void CallAction()
    {
        Destination = TextParser.CheckProgress(data);
    }

}

/**
    * CLASS NAME: ConvChange
    * DESCRIPTION  : changing of stat stuff due to conversation
**/
public class ConvChange : ConvNode
{

    JsonData data;


    public ConvChange(JsonData pro)
    {
        ID = (int)pro["ID"];
        Destination = (int)pro["NextID"];
        data = pro;
    }


    public override void CallAction()
    {
        TextParser.MakeProgress(data);
    }

}


public class ConvMultiProgress : ConvNode
{

    JsonData data;

    List<int> CallOnFinish;

    public ConvMultiProgress(JsonData pro)
    {
        ID = (int)pro["ID"];
        Destination = -1;
        data = TextParser.ToJson( Resources.Load("Progress/" + (string)pro["Progress"]) as TextAsset);

        CallOnFinish = new List<int>();

        for(int i = 0; i < pro["Ends"].Count; ++i)
        {
            CallOnFinish.Add((int)pro["Ends"][i]);
        }

    }


    public override void CallAction()
    {

        int CheckingID = 0;
        int EndID = 0;
        bool checking = true;

        for (int i = 0; i < data.Count; ++i)
        {
            if ((NodeTypes)(int)data[i]["TypeID"] == NodeTypes.StartNode)
            {
                CheckingID = (int)data[i]["NextID"];
            }
        }

        while (checking)
        {
            for (int i = 0; i < data.Count; ++i)
            {
                if ((int)data[i]["ID"] == CheckingID)
                {


                    if ((NodeTypes)(int)data[i]["TypeID"] == NodeTypes.EndingNode)
                    {
                        EndID = (int)data[i]["EndID"];
                        checking = false;
                        break;
                    }
                    if ((NodeTypes)(int)data[i]["TypeID"] == NodeTypes.ProgressNode)
                    {
                        //now, the individual checks
                        CheckingID = TextParser.CheckProgress(data[i]);
                    }

                    if ((NodeTypes)(int)data[i]["TypeID"] == NodeTypes.ChangeNode)
                    {
                        //now, the individual checks
                        CheckingID = TextParser.MakeProgress(data[i]);
                    }


                }
            }
        }

        if (CallOnFinish.Count < EndID)
            return;

       

        Destination = CallOnFinish[EndID];
    }

}


/**
    * CLASS NAME: ConvMap
    * DESCRIPTION  : the start of the conversation
**/
public class ConvMap : ConvNode
{

    int Day;
    int Hour;
    int Length;
    public Room RoomLocation;

    public ConvMap(JsonData start)
    {
        ID = (int)start["ID"];
        Destination = (int)start["NextID"];

        Day = (int)start["Day"];
        Hour = (int)start["Hour"];
        Length = (int)start["Length"];
        RoomLocation = (Room)(int)start["Room"];
    }


    public override void CallAction()
    {
        

    }

    public bool AvalibleNow(int day, int hour)
    {

        //if this time is before this day
        if (day < Day)
            return false;

        //if its after, it is most likely false, but there is one edge case to check
        if(day > Day)
        {
            if(Day + 1 == day)
            {
                if(Hour + Length > 24)
                {
                    if (hour + Length - 24 > hour)
                        return true;
                }
            }

            return false;
        }

        //is on the same date

        //if this time is before this hour
        if (hour < Hour)
            return false;

        //if it is this hour, we good
        if (hour == Hour)
            return true;

        //if this hour is within the length given, we good
        if (hour <= Hour + Length)
            return true;

        return false;
    }

}

/**
    * CLASS NAME: ConvReturn
    * DESCRIPTION  : the start of the conversation
**/
public class ConvReturn : ConvNode
{

    

    public ConvReturn(JsonData start)
    {
        ID = (int)start["ID"];
        Destination = (int)start["NextID"];
    }


    public override void CallAction()
    {
        //send event to return to the map setup
        Space.DispatchEvent(Events.ReturnToMap);
        Debug.Log("hello");
    }

}


/**
    * CLASS NAME: ConvReturn
    * DESCRIPTION  : the start of the conversation
**/
public class ConvInk : ConvNode
{

    TextAsset InkData;

    public ConvInk(JsonData key)
    {
        ID = (int)key["ID"];
        Destination = (int)key["NextID"];
        if (key["Story"] != null)
        {
            InkData = Resources.Load((string)key["Story"]) as TextAsset;


        }
    }


    public override void CallAction()
    {
        
        Space.DispatchEvent(Events.NewStory, new StoryEvent(InkData));

    }

}




/**
    * CLASS NAME: ConvChoice
    * DESCRIPTION  : the pop up options, for choices.
**/
public class ConvChoice : ConvNode
{
    public List<ConvOption> Options;

    public List<Choices> choicedata;

    public ConvChoice()
    {
        Options = new List<ConvOption>();
        choicedata = new List<Choices>();
    }

    public ConvChoice(JsonData choices)
    {
        ID = (int)choices["ID"];
        //Destination = (int)choices["Destination"];


        Options = new List<ConvOption>();
        choicedata = new List<Choices>();
        
        for (int i = 0; i < choices["Choices"].Count; ++i)
        {
            ConvOption opt = new ConvOption();

            if (choices["Destinations"][i] == null)
                opt.DestinationNode = -1;
            else
                opt.DestinationNode = (int)choices["Destinations"][i];

            opt.choicedata = new Choices((string)choices["Choices"][i], true);

            choicedata.Add(opt.choicedata);

            Options.Add(opt);

        }
        
    }

    public override void CallAction()
    {
        Space.DispatchEvent(Events.Choice, new ChoiceDisplayEvent(choicedata.ToArray()));
        //send a timer event here
    }


}


/**
    * CLASS NAME: ConvOption
    * DESCRIPTION  : the pop up options, for choices. update to intergrate with choice system in place
**/
public class ConvOption
{

    public int DestinationNode;

    public Choices choicedata;

}


/**
    * CLASS NAME: ConvLine
    * DESCRIPTION  : the class containing the lines as they play
**/
public class ConvLine : ConvNode
{
    
    List<Line> Lines;

    bool autoLast;

    bool denyPlayerInput;

    public ConvLine( JsonData Dialog)
    {
        ID = (int)Dialog["ID"];
        Destination = (int)Dialog["NextID"];

        //refactor later

        Lines = TextParser.ParseLines(Dialog["Lines"]);

        //truLines = TextParser
        autoLast = (bool)Dialog["ImmediateNext"];
        denyPlayerInput = (bool)Dialog["DenyInput"];
    }

    public override void CallAction()
    {
        //Space.DispatchEvent(Events.Description, new DescriptionEvent(Lines, autoLast, denyPlayerInput));
        //send a timer event here
    }


}


/**
    * CLASS NAME: ConvAnimate
    * DESCRIPTION  : calling the animation system on an object
**/
/*
public class ConvThought : ConvNode
{

    Thoughts Idea;
    Vector3 Pos;
    bool Immediate;

    public ConvThought(JsonData idea_key)
    {

        ID = (int)idea_key["ID"];
        Destination = (int)idea_key["NextID"];

        Idea = ThoughtSystem.GetIdeaFromList((string)idea_key["IdeaID"]);

        if (Idea != null)
        {

            if (idea_key["Description"] != null)
            {
                Idea.Description = (string)idea_key["Description"];
            }

            if (Idea.Description == "")
            {
                Idea.Description = Idea.ID;
            }

            if (idea_key["FurtherDetails"] != null)
            {
                Idea.Details = (string)idea_key["FurtherDetails"];
            }

            Immediate = (bool)idea_key["PlacedManually"];

            Pos = new Vector3();
            
            Pos.x = (float)(double)idea_key["Position"][0];
            Pos.y = (float)(double)idea_key["Position"][1];
            Pos.z = (float)(double)idea_key["Position"][2];

        }
        else
        {
            Debug.Log("Error! " + (string)idea_key["IdeaID"] + " is not a registered thought!! In a Conversation");
        }

    }

    public override void CallAction()
    {
        if(Immediate)
            HaveThought.GenerateThoughtAt(Idea, Pos);
        else
            Game.current.Thoughts.AddIdleThought(Idea);
            //Game.current.Thoughts.RemoveIdleThought(Idea);
            //GenerateThoughtAt(Idea);
    }

}
*/
/**
    * CLASS NAME: ConvDelay
    * DESCRIPTION  : calls for a delay of input for a set amount of time
**/
public class ConvDelay : ConvNode
{

    
    public float Time;


    public ConvDelay(JsonData delaykey)
    {

        ID = (int)delaykey["ID"];
        Destination = (int)delaykey["NextID"];

        Time = (float)(double)delaykey["Delay"];
        
    }

    public override void CallAction()
    {
        //this shouldn't have anything in it.
    }

    


}

/**
    * CLASS NAME: ConvAnimate
    * DESCRIPTION  : calling the animation system on an object
**/
public class ConvAnimate : ConvNode
{
    public string ObjectName;
    public string animationKey;
    public bool BoolState;

    public ConvAnimate(JsonData animation_key)
    {

        ID = (int)animation_key["ID"];
        Destination = (int)animation_key["NextID"];

        if (animation_key["Name"] != null)
            ObjectName = (string)animation_key["Name"];
        else
            ObjectName = null;
        animationKey = (string)animation_key["key"];
        BoolState = (bool)animation_key["bool"];

        



    }

    public override void CallAction()
    {
        //Space.DispatchEvent(Events.Description, new DescriptionEvent(Lines));
        //var anime = GameObject.Find(ObjectName);

        //if(anime != null)
            //anime.DispatchEvent(Events.Animate, new AnimateEvent(animationKey, BoolState));
        //dispatch event
        //send a timer event here
    }

}

/**
    * CLASS NAME: ConvImage
    * DESCRIPTION  : displaying an image
**/
public class ConvImage : ConvNode
{
    public Sprite ImageToDisplay;

    public bool ClickToRemove = false;

    public ConvImage(JsonData Image_key)
    {

        ID = (int)Image_key["ID"];
        Destination = (int)Image_key["NextID"];

        if (Image_key["Slug"] != null)
        {
            ImageToDisplay = Resources.Load("Sprites/" + (string)Image_key["Slug"]) as Sprite;
        }



    }

    public override void CallAction()
    {
        //call description to say when image is closed
        //var gameObject = GameObject.Find("Description");

        //Space.DispatchEvent(Events.OpenImage, new ImageEvent(ImageToDisplay, gameObject, ClickToRemove));
    }

}


/**
    * CLASS NAME: ConvChara
    * DESCRIPTION  : displaying/changing an character sprite
**/
public class ConvChara : ConvNode
{


    public ConvChara(JsonData Character_key)
    {

        ID = (int)Character_key["ID"];
        Destination = (int)Character_key["NextID"];
       



    }

    public override void CallAction()
    {

    }

}

/**
    * CLASS NAME: ConvSound
    * DESCRIPTION  : calling the jukebox to play a sound/song. for in space sounds, use ConvCall & an audioObject
**/
public class ConvSound : ConvNode
{
    public AudioClip sound;
    public bool Song = false;
    public bool remove = false;
    public bool Music = false;

    public ConvSound(JsonData key)
    {

        ID = (int)key["ID"];
        Destination = (int)key["NextID"];

        if (key["Slug"] != null)
            sound = Resources.Load("Sounds/" + (string)key["Slug"]) as AudioClip;
        else
            sound = null;
            
        Song = (bool)key["bool"];
        remove = (bool)key["stop"];
        Music = (bool)key["music"];


    }

    public override void CallAction()
    {

        //Space.DispatchEvent(Events.Jukebox, new SoundEvent(sound, Song, remove, Music));

    }

}


/**
    * CLASS NAME: ConvCall
    * DESCRIPTION  : calling events outside of conv system. must take default event
**/
public class ConvCall : ConvNode
{
    public Events eventToCall;
    public string Target;

    public ConvCall(JsonData key)
    {

        ID = (int)key["ID"];
        Destination = (int)key["NextID"];
        
        eventToCall = (string)key["Event"];
        if (key["Target"] != null)
            Target = (string)key["Target"];
        else
            Target = null;
        



    }

    public override void CallAction()
    {
        GameObject tar = GameObject.Find(Target);
        //Debug.Log(Target);
        if (tar == null)
            Space.DispatchEvent(eventToCall);
        else
            tar.DispatchEvent(eventToCall);

    }

}

/**
    * CLASS NAME: ConvCounter
    * DESCRIPTION  : the interution of a line, seeing if it succeds or fails.
**/
/*
public class ConvCounter : ConvNode
{
    
    List<ConvInteruprt> Interuptions;

    public ConvCounter(JsonData Interupt)
    {

        ID = (int)Interupt["ID"];
        Destination = (int)Interupt["NextID"];
        
        
        Interuptions = new List<ConvInteruprt>();

        

        for (int i = 0; i < Interupt["Interupts"].Count; ++i)
        {
            int itype = (int)Interupt["Interupts"][i]["InteruptType"];
            InteruptionTypes type = (InteruptionTypes)itype;

            switch (type)
            {
                case InteruptionTypes.Item:

                    Sprite item = Resources.Load<Sprite>("Sprites/" + Interupt["Interupts"][i]["Slug"]);

                    Interuptions.Add(new ConvInteruprt(item, (int)Interupt["Interupts"][i]["Destinations"]));
                    break;
                case InteruptionTypes.Idea:

                    string idea = (string)Interupt["Interupts"][i]["Idea"];

                    Interuptions.Add(new ConvInteruprt(idea, (int)Interupt["Interupts"][i]["Destinations"]));
                    break;
                case InteruptionTypes.Inventory:

                    Sprite examine = Resources.Load<Sprite>("Sprites/" + Interupt["Interupts"][i]["Slug"]);

                    Interuptions.Add(new ConvInteruprt(type, examine, (int)Interupt["Interupts"][i]["Destinations"]));
                    break;
                case InteruptionTypes.Phone:

                    Interuptions.Add(new ConvInteruprt(type, (int)Interupt["Interupts"][i]["Destinations"]));
                    break;
                default:
                    break;

            }
        }

        

    }

    public override void CallAction()
    {
        Space.DispatchEvent(Events.Counter, new InteruptionOptionEvent(Interuptions, true));
        //send a timer event here
    }


}

*/
/**
    * CLASS NAME: ConvCarryOn
    * DESCRIPTION  : the counter to ConvCounter. closes interupt opertunities.
**/
/*
public class ConvCarryOn : ConvNode
{

    List<ConvInteruprt> Interuptions;

    public ConvCarryOn(JsonData Interupt)
    {

        ID = (int)Interupt["ID"];
        Destination = (int)Interupt["NextID"];


        Interuptions = new List<ConvInteruprt>();



        for (int i = 0; i < Interupt["Interupts"].Count; ++i)
        {
            int itype = (int)Interupt["Interupts"][i]["InteruptType"];
            InteruptionTypes type = (InteruptionTypes)itype;

            switch (type)
            {
                case InteruptionTypes.Item:

                    Sprite item = Resources.Load<Sprite>("Sprites/" + Interupt["Interupts"][i]["Slug"]);

                    Interuptions.Add(new ConvInteruprt(item, (int)Interupt["Interupts"][i]["Destinations"]));
                    break;
                case InteruptionTypes.Idea:

                    string idea = (string)Interupt["Interupts"][i]["Idea"];

                    Interuptions.Add(new ConvInteruprt(idea, (int)Interupt["Interupts"][i]["Destinations"]));
                    break;
                case InteruptionTypes.Inventory:

                    Sprite examine = Resources.Load<Sprite>("Sprites/" + Interupt["Interupts"][i]["Slug"]);

                    Interuptions.Add(new ConvInteruprt(type, examine, (int)Interupt["Interupts"][i]["Destinations"]));
                    break;
                case InteruptionTypes.Phone:

                    Interuptions.Add(new ConvInteruprt(type, (int)Interupt["Interupts"][i]["Destinations"]));
                    break;
                default:
                    break;

            }
        }



    }

    public override void CallAction()
    {
        Space.DispatchEvent(Events.Counter, new InteruptionOptionEvent(Interuptions, false));
        //send a timer event here
    }


}

    */
/**
    * CLASS NAME: ConvFail
    * DESCRIPTION  : the reset node for failing to interupt the conversation
**/
/*
public class ConvFail : ConvNode
{

    int FailDestination;

    public ConvFail(JsonData Interupt)
    {

        ID = (int)Interupt["ID"];
        Destination = (int)Interupt["NextID"];


        FailDestination = (int)Interupt["FailID"];


    }

    public override void CallAction()
    {
        Space.DispatchEvent(Events.ConversationCheckpoint, new ValueChangeEvent(FailDestination));
        //send a timer event here
    }


}



public enum InteruptionTypes
{
    Item,
    Idea,
    Phone,
    Inventory
}
*/
/**
    * CLASS NAME: ConvInteruprt
    * DESCRIPTION  : the interution of a line, seeing if it succeds or fails.
**/
/*
public class ConvInteruprt
{

    public Sprite item;
    public string idea;

    public InteruptionTypes type;

    public int DestinationID;

    public ConvInteruprt()
    {
        idea = "";
    }

    public ConvInteruprt(Sprite item_, int node)
    {
        item = item_;
        DestinationID = node;
        type = InteruptionTypes.Item;
    }

    public ConvInteruprt(string idea_ID, int node)
    {
        idea = idea_ID;
        DestinationID = node;
        type = InteruptionTypes.Idea;
    }

    public ConvInteruprt(InteruptionTypes type_ID, int node)
    {
        DestinationID = node;
        type = type_ID;
        idea = "";
    }

    public ConvInteruprt(InteruptionTypes type_ID, Sprite item_, int node)
    {
        item = item_;
        DestinationID = node;
        type = type_ID;
        idea = "";
    }

    public bool CorrectInterupt(ConvAnswer answer)
    {

        if (answer.type != type)
            return false;

        switch(answer.type)
        {
            case InteruptionTypes.Item:
                if (item == answer.item)
                    return true;
                break;
            case InteruptionTypes.Idea:
                Debug.Log(idea);
                if (idea == answer.idea.ID)
                    return true;
                break;
            case InteruptionTypes.Phone:
                return true;
            case InteruptionTypes.Inventory:
                if (item == answer.item)
                    return true;
                break;
        }

        return false;
    }


}
*/
/**
    * CLASS NAME: ConvInteruprt
    * DESCRIPTION  : the the brother class to ConvInterupt. is what the player sends to try and interupt
**/
/*
public class ConvAnswer
{

    public Sprite item;
    public Thoughts idea;

    public InteruptionTypes type;
    

    public ConvAnswer(Sprite item_)
    {
        item = item_;
        type = InteruptionTypes.Item;
    }

    public ConvAnswer(Thoughts idea_ID)
    {
        idea = idea_ID;
        type = InteruptionTypes.Idea;
    }
    
    public ConvAnswer(InteruptionTypes type_)
    {
        type = type_;
    }


}
*/