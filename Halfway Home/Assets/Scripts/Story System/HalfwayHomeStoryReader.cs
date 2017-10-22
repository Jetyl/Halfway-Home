using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus.InkModule;
using Ink.Runtime;
using Stratus;
using System;

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
      story.BindExternalFunction(nameof(AddSocialPoints), new System.Action<string, string>(AddSocialPoints));
      story.BindExternalFunction(nameof(AlterWellbeing), new System.Action<string, int>(AlterWellbeing));
      story.BindExternalFunction(nameof(AddSocialTier), new System.Action<string>(AddSocialTier));
      story.BindExternalFunction(nameof(GetValue), (string valueName) => { GetValue(valueName); });
      story.BindExternalFunction(nameof(GetStringValue), (string valueName) => { GetStringValue(valueName); });
      story.BindExternalFunction(nameof(SetTimeBlock), new System.Action<int>(SetTimeBlock));
      story.BindExternalFunction(nameof(CallSleep), new System.Action(CallSleep));
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
      Space.DispatchEvent(Events.CharacterExit, new StageDirectionEvent(name, "Calm"));
    }

    public void SetValue(string ValueName, bool newValue)
    {
      Game.current.Progress.SetValue(ValueName, newValue);
    }

    public bool GetValue(string ValueName)
    {
      return Game.current.Progress.GetBoolValue(ValueName);
    }

    public string GetStringValue(string ValueName)
    {
      return Game.current.Progress.GetStringValue(ValueName);
    }

    public void AlterWellbeing(string WellnessStat, int Value)
    {
        var stat = Personality.Wellbeing.delusion;


        if (WellnessStat == "Stress")
            stat = Personality.Wellbeing.stress;
        if (WellnessStat == "Fatigue")
            stat = Personality.Wellbeing.fatigue;

        Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(Value, stat));
    }

        public void AddSocialPoints(string SocialStat, string Value)
    {
            var stat = Personality.Social.awareness;


            if (SocialStat == "Grace")
                stat = Personality.Social.grace;
            if (SocialStat == "Expression")
                stat = Personality.Social.expression;

            Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(Value, stat));
    }
    
    public void AddSocialTier(string SocialStat)
    {
        if (SocialStat == "Awareness")
            Game.current.Self.IncrementSocialTier(Personality.Social.awareness);
        if (SocialStat == "Grace")
            Game.current.Self.IncrementSocialTier(Personality.Social.grace);
        if (SocialStat == "Expression")
            Game.current.Self.IncrementSocialTier(Personality.Social.expression);


        Space.DispatchEvent(Events.StatChange);
    }

    public void SetTimeBlock(int time)
    {
        Game.current.SetTimeBlock(time);
      
    }
    
    public void CallSleep()
    {
        Game.current.Slept();
    }
    
  }
}