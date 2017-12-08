/******************************************************************************/
/*!
File:   CheckProgressChain.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class CheckProgressChain : MonoBehaviour
{

    public EventListener ListeningOn = EventListener.Owner;

    public Events CheckOn = Events.Null;

    public TextAsset ProgressChain;

    JsonData ChainData;

    public EventListener[] callOnFinish;
    public Events[] SayOnFinish;
    public bool[] CallOtherObjectOnFinish;
    public GameObject[] OtherObject;


    // Use this for initialization
    void Start ()
    {
		
        if(ProgressChain != null)
        {

            ChainData = TextParser.ToJson(ProgressChain);

            if(ChainData != null)
            {
                if (ListeningOn == EventListener.Owner)
                    EventSystem.ConnectEvent<DefaultEvent>(gameObject, CheckOn, CheckChain);
                else if (ListeningOn == EventListener.Space)
                    Space.Connect<DefaultEvent>(CheckOn, CheckChain);
            }


            
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    void CheckChain(DefaultEvent eventdata)
    {
        int CheckingID = 0;
        int EndID = 0;
        bool checking = true;

        for (int i = 0; i < ChainData.Count; ++i)
        {
            if ((NodeTypes)(int)ChainData[i]["TypeID"] == NodeTypes.StartNode)
            {
                CheckingID = (int)ChainData[i]["NextID"];
            }
        }

        while (checking)
        {
            for (int i = 0; i < ChainData.Count; ++i)
            {
                if ((int)ChainData[i]["ID"] == CheckingID)
                {
                    

                    if ((NodeTypes)(int)ChainData[i]["TypeID"] == NodeTypes.EndingNode)
                    {
                        EndID = (int)ChainData[i]["EndID"];
                        checking = false;
                        break;
                    }
                    if ((NodeTypes)(int)ChainData[i]["TypeID"] == NodeTypes.ProgressNode)
                    {
                        //now, the individual checks
                        CheckingID = TextParser.CheckProgress(ChainData[i]);
                    }

                    if ((NodeTypes)(int)ChainData[i]["TypeID"] == NodeTypes.ChangeNode)
                    {
                        //now, the individual checks
                        CheckingID = TextParser.MakeProgress(ChainData[i]);
                    }


                }
            }
        }

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

    }
    

}
