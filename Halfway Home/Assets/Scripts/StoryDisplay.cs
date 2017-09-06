using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryDisplay : MonoBehaviour
{
    //script for managing the ink story stuff

    Story InkStory;

	// Use this for initialization
	void Start ()
    {
        Space.Connect<StoryEvent>(Events.NewStory, OnNewStory);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnNewStory(StoryEvent eventdata)
    {
        InkStory = new Story(eventdata.Script.text);
    }

}

public class StoryEvent : DefaultEvent
{
    public TextAsset Script;

    public StoryEvent(TextAsset text)
    {
        Script = text;
    }
}