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
      story.BindExternalFunction(nameof(CharEnter), new System.Action<string, string, int>(CharEnter));
      story.BindExternalFunction(nameof(CharExit), new System.Action<string>(CharExit));
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

    public void CharChange(string name, string _pose, int stagePos, bool entering)
    {
      //Scene.Dispatch<CharacterChangeEvent>(new CharacterChangeEvent() { character = name, entering = true });
      Space.DispatchEvent(Events.CharacterCall, new StageDirectionEvent(name, _pose, (StagePosition)stagePos, !entering));
    }

    public void CharEnter(string name, string _pose, int stagePos)
    {
      //Scene.Dispatch<CharacterChangeEvent>(new CharacterChangeEvent() { character = name, entering = true });
      Space.DispatchEvent(Events.CharacterCall, new StageDirectionEvent(name, _pose, (StagePosition)stagePos, false));
    }

    public void CharExit(string name)
    {
      //Scene.Dispatch<CharacterChangeEvent>(new CharacterChangeEvent() { character = name, entering = false });
      Space.DispatchEvent(Events.CharacterCall, new StageDirectionEvent(name, "Test", StagePosition.None, true));
    }

  }
}