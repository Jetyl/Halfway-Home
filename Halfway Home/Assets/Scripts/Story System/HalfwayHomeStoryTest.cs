/******************************************************************************/
/*!
File:   HalfwayHomeStoryTest.cs
Author: Christian Sagel
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;

namespace HalfwayHome
{
  public class HalfwayHomeStoryTest : MonoBehaviour
  {
    public TextAsset storyFile;
    public RuntimeMethodField testing;

    void Start()
    {
      testing = new RuntimeMethodField(DispatchStoryEvent);
    }

    void DispatchStoryEvent()
    {
      Space.DispatchEvent(Events.NewStory, new StoryEvent(storyFile));
    }

  }

}