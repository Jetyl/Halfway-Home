using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCGAnimator : MonoBehaviour
{

    public float CommandSpeed = 1;

    public List<CGDetails> Commands;

	// Use this for initialization
	void Start ()
    {

        EventSystem.ConnectEvent<CustomGraphicEvent>(gameObject, Events.CG, NextCommand);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    void NextCommand(CustomGraphicEvent eventdata)
    {
        
        foreach(var act in eventdata.Actions)
        {
            print("Action:" + act);
            foreach (var command in Commands)
            {
                if(act.ToLower() == command.Tag.ToLower())
                {
                    SendCommand(command);
                    break;
                }
            }

        }

    }

    void SendCommand(CGDetails command)
    {
        var vis = command.Graphic.GetComponent<SpriteRenderer>();

        var col = Color.white;

        if (vis.color.a != 0)
            col.a = 0;

        command.Graphic.DispatchEvent(Events.Fade, new FadeEvent(col, CommandSpeed));

    }



}
