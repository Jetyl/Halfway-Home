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
      story.BindExternalFunction(nameof(CharEnter), new System.Action<string, string>(CharEnter));
      story.BindExternalFunction(nameof(CharExit), new System.Action<string>(CharExit));
      story.BindExternalFunction(nameof(SetValue), new System.Action<string, bool>(SetValue));
      story.BindExternalFunction(nameof(GetValue), (string valueName) => { GetValue(valueName); });
    }

    protected override void OnSetLineParsing(Stratus.InkModule.Story.ParsePatterns patterns)
    {
      patterns.Add("Speaker", patterns.insideSquareBrackets);
      patterns.Add("Message", patterns.insideDoubleQuotes);
      //patterns.Add("Pose", "");
    }

    public void PlayMusic(string name)
    {
      Scene.Dispatch<PlayMusicEvent>(new PlayMusicEvent() { track = name });
    }

    public void CharEnter(string name, string _pose)
    {
      //Scene.Dispatch<CharacterChangeEvent>(new CharacterChangeEvent() { character = name, entering = true });
      Space.DispatchEvent(Events.CharacterCall, new StageDirectionEvent(name, _pose));
      Trace.Script("called char enter");
    }

    public void CharExit(string name)
    {
      //Scene.Dispatch<CharacterChangeEvent>(new CharacterChangeEvent() { character = name, entering = false });
      Space.DispatchEvent(Events.CharacterExit, new StageDirectionEvent(name, "Test"));
    }

    public void SetValue(string ValueName, bool newValue)
    {
      Game.current.Progress.SetValue(ValueName, newValue);
    }

    public bool GetValue(string ValueName)
    {
      return Game.current.Progress.GetBoolValue(ValueName);
    }

  }
}