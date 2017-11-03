using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class DescriptionDisplay : MonoBehaviour
{
    

    Animator anime;
    AutoType Decription;
    public GameObject Speaker;

    public Animator NextLine;

    bool Active = false;

    bool isFinished = false;
        
    string Line;
  
    bool AutoOnLast;
    bool Paused = false;

    bool Skipping = false;

    bool Stop = false;

    // Use this for initialization
    void Start ()
    {
        anime = gameObject.GetComponent<Animator>();
        Decription = gameObject.GetComponentInChildren<AutoType>();
        //Speaker = gameObject.transform.Find("DialogBox").Find("Speaker").gameObject;
        

        Speaker.GetComponentInChildren<TextMeshProUGUI>().text = "";

        Space.Connect<DescriptionEvent>(Events.Description, UpdateDescription);
        Space.Connect<DefaultEvent>(Events.FinishedAutoType, OnFinishedTyping);
        Space.Connect<DefaultEvent>(Events.CloseDescription, CloseDisplay);
        Space.Connect<DefaultEvent>(Events.Pause, OnPause);
        Space.Connect<DefaultEvent>(Events.UnPause, OnUnPause);
        Space.Connect<DefaultEvent>(Events.StopSkipTyping, OnStopSkipping);
        Space.Connect<DefaultEvent>(Events.ReturnToMap, OnStopSkipping);


        Space.Connect<DefaultEvent>(Events.GetPlayerInfo, OnStop);
        Space.Connect<DefaultEvent>(Events.GetPlayerInfoFinished, OnNonStop);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!Active)
            return;

        if (Paused)
            return;

        if (Stop)
            return;

        if(Input.GetButtonDown("Skip"))
        {
            Skipping = !Skipping;
            Decription.Skipping = Skipping;
        }

        if(Skipping)
        {
            if (isFinished)
                Finished();
        }
        else if (Input.GetMouseButtonDown(0) == true)
        {

            
            if (!isFinished)
            {
                Decription.gameObject.DispatchEvent(Events.SkipTyping);

                //turn this back on when the animation is working again
                //NextLine.SetBool("Play", true);


                    
                return;
            }

            Finished();
            
        }
        

    }



    public void Finished()
    {
        isFinished = false;
        //turn these on when next line animation is working again
        // NextLine.SetBool("Play", false);
         // NextLine.Play("LinePlaying");
        

        
        Active = false;
            


        Space.DispatchEvent(Events.FinishedDescription);
        

    }


    void UpdateDescription(DescriptionEvent eventdata)
    {
        gameObject.SetActive(true);
        //Space.DispatchEvent(Events.OpenUI, new UIEvent(this));
        //dynamically edit the lines so they adhere to certain parameters
        Line = TextParser.DynamicEdit(eventdata.Line);
        
        AutoOnLast = eventdata.AutoFinish;
        /*
        if(!anime.GetBool("IsUp"))
        {
            anime.SetBool("IsUp", true);
            StartCoroutine(WaitTilOpened());
            return;
        }*/

        //UpdateSpeaker(0);
        Active = true;
        Decription.gameObject.DispatchEvent(Events.AutoType, new AutoTypeEvent(Line));
        isFinished = false;
    }

    void CloseDisplay (DefaultEvent eventdata)
    {
        print("off");
        //Space.DispatchEvent(Events.CloseUI, new UIEvent(this));
        anime.SetBool("IsUp", false);
        Active = false;
        Decription.Clear();
        StartCoroutine(WaitTilClosed());
    }

    


    IEnumerator WaitTilClosed()
    {

        yield return new WaitForSeconds(0.9f);

        Space.DispatchEvent(Events.DescriptionClosed);

    }

    IEnumerator WaitTilOpened()
    {

        Decription.Clear();


        yield return new WaitForSeconds(1.5f);
        
        Active = true;
        Decription.gameObject.DispatchEvent(Events.AutoType, new AutoTypeEvent(Line));
        isFinished = false;
    }

    void OnFinishedTyping(DefaultEvent eventdata)
    {
        isFinished = true;
        
            //NextLine.SetBool("Play", true);
    }

    void OnPause(DefaultEvent eventdata)
    {
        Paused = true;
    }
    void OnUnPause(DefaultEvent eventdata)
    {
        Paused = false;
    }

    void OnStop(DefaultEvent eventdata)
    {
        Stop = true;
    }
    void OnNonStop(DefaultEvent eventdata)
    {
        Stop = false;
    }

    void OnStopSkipping(DefaultEvent eventdata)
    {
        Skipping = false;
        Decription.Skipping = false;
    }

}


public class DescriptionEvent : DefaultEvent
{
    
    public bool AutoFinish;
    //public List<Line> Lines;
    public string Line;
    public string Speaker;
   
    public DescriptionEvent(string _lines, string _speaker, bool autoFinish_ = false)
    {
        Line = _lines;
        AutoFinish = autoFinish_;
        Speaker = _speaker;
    }

}

[Serializable]
public struct Line
{
    public string Speaker;
    public bool NewSpeaker;
    public string Dialog;
    public float Pace;
}
