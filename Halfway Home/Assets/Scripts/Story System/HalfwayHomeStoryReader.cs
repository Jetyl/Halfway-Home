using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus.Modules.InkModule;
using Stratus;
using System;

namespace HalfwayHome
{
  /// <summary>
  /// The class that interacts with ink, managing the stories
  /// </summary>
  public class HalfwayHomeStoryReader : StoryReader<RegexParser>
  {
    //------------------------------------------------------------------------/
    // Fields
    //------------------------------------------------------------------------/
    protected override string saveFileName => "HalfwayHomeStoryReader";

    private string statLabel = "Stat";
    private string valueLabel = "Value";
    private string countLabel = "Count";

    public static string speakerLabel = "Speaker";
    public static string dialogLabel = "Dialog";

    //------------------------------------------------------------------------/
    // Messages
    //------------------------------------------------------------------------/
    protected override void OnBindExternalFunctions(Story story)
    {
      story.runtime.BindExternalFunction(nameof(PlayMusic), new System.Action<string>(PlayMusic));
      //story.runtime.BindExternalFunction(nameof(CharEnter), new System.Action<string, string>(CharEnter)); DEPRECATED
      //story.runtime.BindExternalFunction(nameof(CharExit), new System.Action<string>(CharExit)); DEPRECATED
      story.runtime.BindExternalFunction(nameof(SetValue), new System.Action<string, bool>(SetValue));
      //story.runtime.BindExternalFunction(nameof(AddSocialPoints), new System.Action<string, string>(AddSocialPoints)); DEPRECATED
      //story.runtime.BindExternalFunction(nameof(AlterWellbeing), new System.Action<string, int>(AlterWellbeing)); DEPRECATED
      //story.runtime.BindExternalFunction(nameof(AddSocialTier), new System.Action<string>(AddSocialTier)); DEPRECATED
      story.runtime.BindExternalFunction(nameof(GetValue), (string valueName) => { GetValue(valueName); });
      story.runtime.BindExternalFunction(nameof(GetStringValue), (string valueName) => { GetStringValue(valueName); });
      story.runtime.BindExternalFunction(nameof(SetTimeBlock), new System.Action<int>(SetTimeBlock));
      story.runtime.BindExternalFunction(nameof(CallSleep), new System.Action(CallSleep));
      story.runtime.BindExternalFunction(nameof(GetPlayerData), new System.Action(GetPlayerData));
    }

    protected override void OnConfigureParser(RegexParser parser)
    {
      // @TODO: Change these to use groups
      parser.AddPattern(speakerLabel, RegexParser.Presets.insideSquareBrackets, RegexParser.Target.Line, RegexParser.Scope.Default);
      parser.AddPattern(dialogLabel, RegexParser.Presets.insideDoubleQuotes, RegexParser.Target.Line, RegexParser.Scope.Default);
      
      // Poses
      string posePattern = RegexParser.Presets.ComposeBinaryOperation("Person", "Pose", "=");
      parser.AddPattern("Pose", posePattern, RegexParser.Target.Tag, RegexParser.Scope.Group, OnPoseChange);

      // Social Stat increment
      string incrementStatPattern = RegexParser.Presets.ComposeUnaryOperation(statLabel, countLabel, '+');
      parser.AddPattern("StatUp", incrementStatPattern, RegexParser.Target.Tag, RegexParser.Scope.Group, OnSocialStatIncrement);

      // Wellbeing Stat increment
      string incrementWStatPattern = RegexParser.Presets.ComposeBinaryOperation(statLabel, valueLabel, "+=");
      parser.AddPattern("WellbeingUp", incrementWStatPattern, RegexParser.Target.Tag, RegexParser.Scope.Group, OnWellbeingStatIncrement);

      // Wellbeing Stat decrement
      string decrementWStatPattern = RegexParser.Presets.ComposeBinaryOperation(statLabel, valueLabel, "-=");
      parser.AddPattern("WellbeingDown", decrementWStatPattern, RegexParser.Target.Tag, RegexParser.Scope.Group, OnWellbeingStatDecrement);

      string setWStatPattern = RegexParser.Presets.ComposeBinaryOperation(statLabel, valueLabel, "=>");
      parser.AddPattern("WellbeingSet", setWStatPattern, RegexParser.Target.Tag, RegexParser.Scope.Group, OnWellbeingStatSet);
    }

    protected override void OnStoryLoaded(Story story)
    {

    }

    void OnPoseChange(Parse parse)
    {
      if(parse.Find("Pose") == "Exit")
      {
        Space.DispatchEvent(Events.CharacterExit, new StageDirectionEvent(parse.Find("Person"), "Calm"));
      }
      else
      {
        Space.DispatchEvent(Events.CharacterCall, new StageDirectionEvent(parse.Find("Person"), parse.Find("Pose")));
      }
    }
    
    void OnSocialStatIncrement(Parse parse)
    {
      string stat = parse.Find(statLabel).ToLower();
      string count = parse.Find(countLabel);
      Trace.Script($"{stat} = {count}");

      var eventStat = Personality.Social.awareness;
      if (stat == "grace")
        eventStat = Personality.Social.grace;
      if (stat == "expression")
        eventStat = Personality.Social.expression;

      if(count == "+")
      {
        Space.DispatchEvent(Events.AddStat, new ChangeStatEvent("Minor", eventStat));
      }
      else if(count == "++")
      {
        Space.DispatchEvent(Events.AddStat, new ChangeStatEvent("Major", eventStat));
      }
      else
      {
        if (stat == "awareness")
          Game.current.Self.IncrementSocialTier(Personality.Social.awareness);
        if (stat == "grace")
          Game.current.Self.IncrementSocialTier(Personality.Social.grace);
        if (stat == "expression")
          Game.current.Self.IncrementSocialTier(Personality.Social.expression);
        Space.DispatchEvent(Events.StatChange);
      }
    }

    void OnWellbeingStatIncrement(Parse parse)
    {
      string stat = parse.Find(statLabel).ToLower();
      int value = 0;
      int.TryParse(parse.Find(valueLabel), out value);

      OnWellbeingStatChange(stat, value);
      Trace.Script($"{stat} increased by {value}");
    }

    void OnWellbeingStatDecrement(Parse parse)
    {
      string stat = parse.Find(statLabel).ToLower();
      int value = 0;
      int.TryParse(parse.Find(valueLabel), out value);

      OnWellbeingStatChange(stat, -value);
      Trace.Script($"{stat} decreased by {value}");
    }

    void OnWellbeingStatSet(Parse parse)
    {
      string stat = parse.Find(statLabel).ToLower();
      int value = 0;
      int.TryParse(parse.Find(valueLabel), out value);

      OnWellbeingStatChange(stat, value, true);
      Trace.Script($"{stat} set to {value}");
    }

    void OnWellbeingStatChange(string stat, int value, bool assign = false)
    {
      var eventStat = Personality.Wellbeing.delusion;
      if (stat == "stress")
        eventStat = Personality.Wellbeing.stress;
      if (stat == "fatigue")
        eventStat = Personality.Wellbeing.fatigue;

      Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(value, eventStat, assign));
    }
    //------------------------------------------------------------------------/
    // Story
    //------------------------------------------------------------------------/

    //------------------------------------------------------------------------/
    // External Methods
    //------------------------------------------------------------------------/
    public void PlayMusic(string name)
    {

      Scene.Dispatch<PlayMusicEvent>(new PlayMusicEvent() { track = name });
    }

    //public void CharEnter(string name, string _pose)
    //{
    //  //Scene.Dispatch<CharacterChangeEvent>(new CharacterChangeEvent() { character = name, entering = true });
    //  Space.DispatchEvent(Events.CharacterCall, new StageDirectionEvent(name, _pose));
    //  Trace.Script("called char enter");
    //}

    //public void CharExit(string name)
    //{
    //  //Scene.Dispatch<CharacterChangeEvent>(new CharacterChangeEvent() { character = name, entering = false });
    //  Space.DispatchEvent(Events.CharacterExit, new StageDirectionEvent(name, "Calm"));
    //}

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
            print(ValueName);
            print(Game.current.Progress.GetStringValue(ValueName));
      return Game.current.Progress.GetStringValue(ValueName);
    }

    //public void AlterWellbeing(string WellnessStat, int Value)
    //{
    //  var stat = Personality.Wellbeing.delusion;

    //  if (WellnessStat == "Stress")
    //    stat = Personality.Wellbeing.stress;
    //  if (WellnessStat == "Fatigue")
    //    stat = Personality.Wellbeing.fatigue;

    //  Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(Value, stat));
    //}

    //public void AddSocialPoints(string SocialStat, string Value)
    //{
    //  var stat = Personality.Social.awareness;


    //  if (SocialStat == "Grace")
    //    stat = Personality.Social.grace;
    //  if (SocialStat == "Expression")
    //    stat = Personality.Social.expression;

    //  Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(Value, stat));
    //}

    //public void AddSocialTier(string SocialStat)
    //{
    //  if (SocialStat == "Awareness")
    //    Game.current.Self.IncrementSocialTier(Personality.Social.awareness);
    //  if (SocialStat == "Grace")
    //    Game.current.Self.IncrementSocialTier(Personality.Social.grace);
    //  if (SocialStat == "Expression")
    //    Game.current.Self.IncrementSocialTier(Personality.Social.expression);


    //  Space.DispatchEvent(Events.StatChange);
    //}

    public void SetTimeBlock(int time)
    {
      Game.current.SetTimeBlock(time);

    }

    public void CallSleep()
    {
      Game.current.Slept();
    }

    public void GetPlayerData()
    {
      //Space.DispatchEvent(Events.GetPlayerInfo);
      StartCoroutine(TextParser.FrameDelay(Events.GetPlayerInfo));
    }

        

    }

}