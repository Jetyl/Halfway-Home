using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCGAnimator : MonoBehaviour
{

    public float CommandSpeed = 1;

    public List<CGGrouping> Commands;

	// Use this for initialization
	void Start ()
    {

        EventSystem.ConnectEvent<CustomGraphicEvent>(gameObject, Events.CG, NextCommand);

        EventSystem.ConnectEvent<DefaultEvent>(gameObject, Events.CloseCG, OnClose);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    void NextCommand(CustomGraphicEvent eventdata)
    {
        int i = 0;
        foreach(var act in eventdata.Actions)
        {
            print("Action:" + act);
            foreach (var Group in Commands)
            {

                if(act.ToLower() == Group.CloseTag.ToLower())
                {
                    ClearGrouping(Group);
                }
                else
                {
                    foreach (var command in Group.Details)
                    {
                        if (act.ToLower() == command.Tag.ToLower())
                        {
                            ClearGrouping(Group, command.Tag);
                            print(command.Tag.ToLower());
                            SendCommand(command);
                            break;
                        }
                    }
                }
            }

            ++i;

        }

    }

    void SendCommand(CGDetails command)
    {
        var vis = command.Graphic.GetComponent<SpriteRenderer>();

        var col = Color.white;
        
        command.Graphic.DispatchEvent(Events.Fade, new FadeEvent(col, CommandSpeed));

    }

    void OnClose(DefaultEvent eventdata)
    {  

        foreach (var command in Commands)
        {
            ClearGrouping(command);
        }

        
    }

    void ClearGrouping(CGGrouping group)
    {
        foreach (var command in group.Details)
        {
            var col = Color.white;
            col.a = 0;

            command.Graphic.DispatchEvent(Events.Fade, new FadeEvent(col, CommandSpeed));
        }
    }

    void ClearGrouping(CGGrouping group, string exceptionTag)
    {
        foreach (var command in group.Details)
        {
            if (command.Tag.ToLower() == exceptionTag.ToLower())
                continue;

            var col = Color.white;
            col.a = 0;

            command.Graphic.DispatchEvent(Events.Fade, new FadeEvent(col, CommandSpeed));
        }
    }


}
