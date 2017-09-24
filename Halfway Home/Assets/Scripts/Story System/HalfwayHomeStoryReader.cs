using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus.InkModule;
using Ink.Runtime;
using Stratus;

namespace HalfwayHome
{
  public class HalfwayHomeStoryReader : StoryReader
  {   
    protected override void OnBindExternalFunctions(Ink.Runtime.Story story)
    {
      story.BindExternalFunction(nameof(PlayMusic), new System.Action<string>(PlayMusic));
    }

    protected override void OnSetParsingPatterns(Stratus.InkModule.Story.ParsePatterns patterns)
    {
      patterns.Add("Speaker", patterns.insideSquareBrackets);
      patterns.Add("Message", patterns.insideDoubleQuotes);
    }    

    public void PlayMusic(string name)
    {
      Scene.Dispatch<PlayMusicEvent>(new PlayMusicEvent() { track = name });
    }   

    

  }
}