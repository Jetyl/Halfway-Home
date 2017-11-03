using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableDisplay : MonoBehaviour
{

    public float FadeTime = 0.2f;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<DefaultEvent>(Events.ReturnToMap, Disable);
        Space.Connect<DefaultEvent>(Events.Description, Enable);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Disable(DefaultEvent eventdata)
    {
        gameObject.SetActive(false);
        
        //for(int i = 0; i < transform.childCount; ++i)
        //{
            //transform.GetChild(i).gameObject.DispatchEvent(Events.Fade, new FadeEvent( , FadeTime));
        //}
    }

    void Enable(DefaultEvent eventdata)
    {
        //gameObject.SetActive(true);
    }

}
