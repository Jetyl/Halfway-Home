using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class DescriptionDisplay : MonoBehaviour
{
    private static DescriptionDisplay _instance;

    Animator anime;
    AutoType Decription;
    public GameObject Speaker;

    public Animator NextLine;

    bool Active = false;

    bool isFinished = false;

    int aCounter = 0;
    
    List<Line> Lines;

    float PaceCounter;

    float Pace = -1;

    PaceDisplay PD;

    bool AutoOnLast;

    bool DenyPlayerInput;

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        anime = gameObject.GetComponent<Animator>();
        Decription = gameObject.GetComponentInChildren<AutoType>();
        //Speaker = gameObject.transform.Find("DialogBox").Find("Speaker").gameObject;

        PD = GetComponentInChildren<PaceDisplay>();
        PD.Start();
        PD.StopPace();

        Speaker.GetComponentInChildren<TextMeshProUGUI>().text = "";

        Space.Connect<DescriptionEvent>(Events.Description, UpdateDescription);
        Space.Connect<DefaultEvent>(Events.FinishedAutoType, OnFinishedTyping);
        Space.Connect<DefaultEvent>(Events.CloseDescription, CloseDisplay);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!Active)
            return;

        if (Input.GetMouseButtonDown(0) == true)
        {

            if(canPress())
            {
                if (!isFinished)
                {
                    Decription.gameObject.DispatchEvent(Events.SkipTyping);

                    if (!DenyPlayerInput)
                        NextLine.SetBool("Play", true);


                    
                    return;
                }

                Finished();
            }


            

        }

        if (!isFinished)
            return;

        //if the description has a pace
        if(Pace > 0)
        {

            if(PaceCounter == 0)
            {
                if(!DenyPlayerInput)
                PD.StartPace(Pace);
            }

            if (PaceCounter >= Pace)
            {
                PaceCounter = 0;
                Finished();
            }
            else
                PaceCounter += Time.deltaTime;


        }
        else if(DenyPlayerInput)
        {
            //if player cannot provide input, and no pace is set, next line
            Finished();
        }

        if(AutoOnLast && aCounter + 1 == Lines.Count)
        {
            Finished();
        }

    }



    public void Finished()
    {
        PD.StopPace();
        isFinished = false;
        ++aCounter;

        if (!DenyPlayerInput)
        {
            NextLine.SetBool("Play", false);
            NextLine.Play("LinePlaying");
        }


        if (aCounter < Lines.Count)
        {

            UpdateSpeaker(aCounter);
            Pace = Lines[aCounter].Pace;
            Decription.gameObject.DispatchEvent(Events.AutoType, new AutoTypeEvent(Lines[aCounter].Dialog));
            
            
        }
        else
        {
            Active = false;
            if(DenyPlayerInput)
                Decription.Clear();

            Space.DispatchEvent(Events.FinishedDescription);
        }

    }


    void UpdateDescription(DescriptionEvent eventdata)
    {

        //Space.DispatchEvent(Events.OpenUI, new UIEvent(this));
        
        //dynamically edit the lines so they adhere to certain parameters
        Lines = TextParser.DynamicEdit(eventdata.Lines);

        AutoOnLast = eventdata.AutoFinish;
        DenyPlayerInput = eventdata.DenyInput;

        if (Lines[0].Speaker == null || Lines[0].Speaker == "")
        {
            Speaker.gameObject.SetActive(false);
        }
        
        

        if(!anime.GetBool("IsUp"))
        {
            anime.SetBool("IsUp", true);
            StartCoroutine(WaitTilOpened());
            return;
        }

        UpdateSpeaker(0);
        Active = true;
        aCounter = 0;
        Pace = Lines[0].Pace;
        Decription.gameObject.DispatchEvent(Events.AutoType, new AutoTypeEvent(Lines[0].Dialog));
        isFinished = false;
    }

    void CloseDisplay (DefaultEvent eventdata)
    {

        //Space.DispatchEvent(Events.CloseUI, new UIEvent(this));
        anime.SetBool("IsUp", false);
        Active = false;
        Pace = -1;
        Decription.Clear();
        StartCoroutine(WaitTilClosed());
    }

    void UpdateSpeaker(int count)
    {

        //if the speaker is null, thus has not changed
        if (Lines[count].Speaker == null)
            return;
        
        
        if (Lines[count].Speaker == " " || Lines[count].Speaker == "")
        {
            Speaker.gameObject.SetActive(false);
            
        }
        else
        {
            Speaker.gameObject.SetActive(true);

            Speaker.GetComponentInChildren<TextMeshProUGUI>().text = Lines[count].Speaker;

        }

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

        UpdateSpeaker(0);
        Active = true;
        aCounter = 0;
        Pace = Lines[0].Pace;
        Decription.gameObject.DispatchEvent(Events.AutoType, new AutoTypeEvent(Lines[0].Dialog));
        isFinished = false;
    }

    void OnFinishedTyping(DefaultEvent eventdata)
    {
        isFinished = true;

        if (!DenyPlayerInput)
            NextLine.SetBool("Play", true);
    }


    bool canPress()
    {

        if (DenyPlayerInput)
            return false;
        
        


        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return true;

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && 
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
            return true;

        return false;
    }

}


public class DescriptionEvent : DefaultEvent
{
    
    public bool AutoFinish;
    public bool DenyInput;
    public List<Line> Lines;

   
    public DescriptionEvent(List<Line> _lines, bool autoFinish_ = false, bool denyInput_ = false)
    {
        Lines = _lines;
        AutoFinish = autoFinish_;
        DenyInput = denyInput_;
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
