using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using System;

namespace Stratus
{
  namespace InkModule
  {
    public class SimpleStoryReader : StoryReader  
    {
      //------------------------------------------------------------------------------------------/
      // Messages
      //------------------------------------------------------------------------------------------/
      protected override void OnBindExternalFunctions(Ink.Runtime.Story story)
      {
        story.BindExternalFunction("PlayMusic", new Action<string>(PlayMusic));
      }
      
      protected override void OnSetParsingPatterns(Story.ParsePatterns patterns)
      {

      }

      //------------------------------------------------------------------------------------------/
      // External functions
      //------------------------------------------------------------------------------------------/
      public void PlayMusic(string trackName)
      {
        Trace.Script("Playing music track '" + trackName + "'");
      }
      
    }
  }

}