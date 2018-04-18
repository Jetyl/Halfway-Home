using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearCGAnimator : MonoBehaviour
{

    public SpriteRenderer FrontCurtain;
    public SpriteRenderer BackCuratin;

    public Sprite[] Cells;

    public float TransitionSpeed = 1;

    public string NextCellCall = "Next";

    int CellCount = 0;
    int CurrentCount = 0;

    // Use this for initialization
    void Start ()
    {
        EventSystem.ConnectEvent<CustomGraphicEvent>(gameObject, Events.CG, NextCell);

        EventSystem.ConnectEvent<DefaultEvent>(gameObject, Events.CloseCG, OnClose);
        
        Space.Connect<DefaultEvent>(Events.Save, OnSave);
        EventSystem.ConnectEvent<DefaultEvent>(gameObject, Events.Load, OnLoad);

        FrontCurtain.sprite = Cells[0];
        StartCoroutine(TextParser.FrameDelay(FrontCurtain.gameObject, Events.Fade, new FadeEvent(Color.white, TransitionSpeed)));
        //FrontCurtain.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, TransitionSpeed));

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnSave(DefaultEvent eventdata)
    {
        Game.current.CGCalls = new List<string>();

        for(int i = 0; i < CellCount; ++i)
        {
            Game.current.CGCalls.Add(NextCellCall);
        }
    }

    void OnLoad(DefaultEvent eventdata)
    {
        CellCount = Game.current.CGCalls.Count;
        CurrentCount = CellCount - 1;

        NextCell(new CustomGraphicEvent("", NextCellCall));

    }


    void OnClose(DefaultEvent eventdata)
    {
        var Awhite = Color.white;
        Awhite.a = 0;
        FrontCurtain.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Awhite, TransitionSpeed));


    }

    void NextCell(CustomGraphicEvent eventdata)
    {

        if (eventdata.ContainsAct(NextCellCall) && CellCount < Cells.Length)
            CellCount += 1;

        if(CellCount != CurrentCount)
        {
            StartCoroutine(CrossFade(Cells[CellCount]));
            CurrentCount += 1;
        }
        
    }



    IEnumerator CrossFade(Sprite newCell)
    {
        BackCuratin.sprite = FrontCurtain.sprite;
        BackCuratin.color = Color.white;
        var Awhite = Color.white;
        Awhite.a = 0;
        FrontCurtain.color = Awhite;
        FrontCurtain.sprite = newCell;
        FrontCurtain.gameObject.DispatchEvent(Events.Fade, new FadeEvent(Color.white, TransitionSpeed));

        yield return new WaitForSeconds(TransitionSpeed);
        
        BackCuratin.color = Awhite;

    }

}
