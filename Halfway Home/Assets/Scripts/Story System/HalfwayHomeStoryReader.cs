/******************************************************************************/
/*!
File:   HalfwayHomeStoryReader.cs
Author: Christian Sagel
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
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
      story.runtime.BindExternalFunction(nameof(SetValue), new System.Action<string, bool>(SetValue));
      story.runtime.BindExternalFunction(nameof(SetIntValue), new System.Action<string, int>(SetIntValue));
      story.runtime.BindExternalFunction(nameof(SetStringValue), new System.Action<string, string>(SetStringValue));
      story.runtime.BindExternalFunction(nameof(GetValue), (string valueName) => GetValue(valueName));
      story.runtime.BindExternalFunction(nameof(GetIntValue), (string valueName) => GetIntValue(valueName));
      story.runtime.BindExternalFunction(nameof(GetStringValue), (string valueName) => GetStringValue(valueName));
      story.runtime.BindExternalFunction(nameof(GetSelfStat), (string stat_name) => GetSelfStat(stat_name));
      story.runtime.BindExternalFunction(nameof(GetHour), () => GetHour());
      story.runtime.BindExternalFunction(nameof(SetTimeBlock), new System.Action<int>(SetTimeBlock));
      story.runtime.BindExternalFunction(nameof(CallSleep), new System.Action(CallSleep));
      story.runtime.BindExternalFunction(nameof(AlterTime), new System.Action(AlterTime));
      story.runtime.BindExternalFunction(nameof(SetPlayerGender), new System.Action<string>(SetPlayerGender));
      story.runtime.BindExternalFunction(nameof(GetPlayerName), new System.Action(GetPlayerName));
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

      // Wellbeing Stat set
      string setWStatPattern = RegexParser.Presets.ComposeBinaryOperation(statLabel, valueLabel, "=>");
      parser.AddPattern("WellbeingSet", setWStatPattern, RegexParser.Target.Tag, RegexParser.Scope.Group, OnWellbeingStatSet);

      // Music & SFX
      string playAudio = RegexParser.Presets.ComposeBinaryOperation("Mode", "Event", ":");
      parser.AddPattern("AudioTrigger", playAudio, RegexParser.Target.Tag, RegexParser.Scope.Group, OnAudioTrigger);

      // Background Change
      string setBackground = RegexParser.Presets.ComposeBinaryOperation("Background", "Image", "/");
      parser.AddPattern("SetBackground", setBackground, RegexParser.Target.Tag, RegexParser.Scope.Group, OnSetBackground);


      // Time Change
      string changeTime = RegexParser.Presets.ComposeBinaryOperation("time", "value", "%");
      parser.AddPattern("ChangeTime", changeTime, RegexParser.Target.Tag, RegexParser.Scope.Group, OnChangeTime);

      // UI Toggle
      string displayUI = RegexParser.Presets.ComposeBinaryOperation("UI", "display", "*");
      parser.AddPattern("DisplayUI", displayUI, RegexParser.Target.Tag, RegexParser.Scope.Group, OnUIDisplay);

      // Objective Updates
      string updateTask = RegexParser.Presets.ComposeBinaryOperation("ID", "state", "&");
      parser.AddPattern("UpdateTask", updateTask, RegexParser.Target.Tag, RegexParser.Scope.Group, OnUpdateObjectives);

      // skipline
      string skipLine = RegexParser.Presets.ComposeSpecificAssignment("skip", "this");
      parser.AddPattern("Skip", skipLine, RegexParser.Target.Tag, RegexParser.Scope.Default, OnSkip);

      string tooltipText = RegexParser.Presets.ComposeBinaryOperation("tooltip", "type", "^");
      parser.AddPattern("Tooltip", tooltipText, RegexParser.Target.Tag, RegexParser.Scope.Group, OnTooltip);
    }

    protected override void OnStoryLoaded(Story story)
    {
      story.filePath = Game.current.ScenePath[story.fileName];
    }

    protected override void OnSave()
    {
      Game.current.StorySave.Overwrite(storySave);
      //base.OnSave(stories);
    }

    protected override void OnLoad()
    {
      storySave.Overwrite(Game.current.StorySave, true);
    }


    //------------------------------------------------------------------------/
    // Methods
    //------------------------------------------------------------------------/

    void OnSkip(Parse parse)
    {
            print("gerer");
       Space.DispatchEvent(Events.NextLine);
    }

    void OnUpdateObjectives(Parse parse)
    {
      foreach (var match in parse.matches)
      {
        if (match.ContainsKey("state"))
        {

          var state = match["state"].Trim().ToLower();
          var NewTaskState = Task.TaskState.Unstarted;

          switch (state)
          {
            case "start":
            case "in progress":
            case "inprogress":
              NewTaskState = Task.TaskState.InProgress;
              break;
            case "fail":
            case "failure":
            case "failed":
              NewTaskState = Task.TaskState.Failed;
              break;
            case "success":
            case "clear":
            case "finished":
            case "goal":
            case "complete":
              NewTaskState = Task.TaskState.Success;
              break;
            default:
              break;
          }

          var id = match["ID"].Trim().Split('.');
          int num1 = Convert.ToInt32(id[0]);
          int num2 = -1;
          if (id.Length > 1)
            num2 = Convert.ToInt32(id[1]);


          Game.current.Progress.UpdateTask(num1, NewTaskState, num2);

        }
      }

    }

    void OnTooltip(Parse parse)
    {
      if(parse.firstMatch.ContainsKey("tooltip"))
      {
        string tooltip;
        string type;
        parse.firstMatch.TryGetValue("tooltip", out tooltip);
        parse.firstMatch.TryGetValue("type", out type);

        TextTooltipBehavior.TooltipLineEvent tooltipEvent = new TextTooltipBehavior.TooltipLineEvent(Color.black, "");

        if (tooltip.Trim().ToLower() == "awareness")
        {
          if(type.Trim().ToLower() == "good")
          {
            tooltipEvent = new TextTooltipBehavior.TooltipLineEvent(Color.cyan, "<color=#59FD64FF>You have ample <color=#2075DFFF>Awareness.");
          }
          else if(type.Trim().ToLower() == "poor")
          {
            tooltipEvent = new TextTooltipBehavior.TooltipLineEvent(Color.cyan, "<color=#FD5959FF>You lack <color=#2075DFFF>Awareness.");
          }
        }
        else if (tooltip.Trim().ToLower() == "grace")
        {
          if (type.Trim().ToLower() == "good")
          {
            tooltipEvent = new TextTooltipBehavior.TooltipLineEvent(Color.yellow, "<color=#59FD64FF>You deftly apply what you know about <color=#DE9E20FF>Grace.");
          }
          else if (type.Trim().ToLower() == "poor")
          {
            tooltipEvent = new TextTooltipBehavior.TooltipLineEvent(Color.yellow, "<color=#FD5959FF>You have more to learn about <color=#DE9E20FF>Grace.");
          }
        }
        else if (tooltip.Trim().ToLower() == "expression")
        {
          if (type.Trim().ToLower() == "good")
          {
            tooltipEvent = new TextTooltipBehavior.TooltipLineEvent(Color.magenta, "<color=#59FD64FF>You have proven your newfound powers of <color=#C33B61FF>Expression.");
          }
          else if (type.Trim().ToLower() == "poor")
          {
            tooltipEvent = new TextTooltipBehavior.TooltipLineEvent(Color.magenta, "<color=#FD5959FF>You still have trouble with <color=#C33B61FF>Expression.");
          }
        }
        else
        {

        }

        Stratus.Scene.Dispatch(tooltipEvent);
      }
    }

    void OnChangeTime(Parse parse)
    {
      foreach (var match in parse.matches)
      {
        if (match.ContainsKey("time"))
        {
          if (match["time"].ToLower().Trim() == "set_time" || match["time"].ToLower().Trim() == "time_set")
          {
            string[] set = match["value"].Replace(" ", "").Split(',');
            Game.current.Day = int.Parse(set[0]);
            Game.current.Hour = int.Parse(set[1]);
            Space.DispatchEvent(Events.TimeChange);
          }
          else if (match["time"].ToLower().Trim() == "sleep")
          {
            int hour = int.Parse(match["value"].Trim());
            Game.current.SetTimeBlock(hour, false);
            Game.current.AlterTime();
          }
          else
          {
            int hour = int.Parse(match["value"].Trim());
            Game.current.SetTimeBlock(hour);
            Game.current.AlterTime();
          }

          //return;
        }

      }
    }

    void OnSetBackground(Parse parse)
    {

      foreach (var match in parse.matches)
      {
        if (match.ContainsKey("Background"))
        {
          if (match["Background"].Trim().ToLower() == "background")
          {
            var Image = match["Image"].Trim();
            Space.DispatchEvent(Events.Backdrop, new StageDirectionEvent(Image));
          }
          else
          {
            var Image = match["Image"].Trim();
            Space.DispatchEvent(Events.CG, new CustomGraphicEvent(match["Background"].Trim(), Image));
          }
        }

      }
    }

    void OnPoseChange(Parse parse)
    {
      foreach (var match in parse.matches)
      {
        if (match.ContainsKey("Pose"))
        {
          var pose = match["Pose"].Trim();
          var person = match["Person"].Trim();

          Trace.Script(parse.FindFirst("Person").Trim());

          Space.DispatchEvent(Events.CharacterCall, new CastDirectionEvent(person, pose));

        }
      }

    }

    void OnSocialStatIncrement(Parse parse)
    {
      string stat = parse.FindFirst(statLabel).ToLower().Trim();
      string count = parse.FindFirst(countLabel).Trim();
      Trace.Script($"{stat} = {count}");

      var eventStat = Personality.Social.Awareness;
      if (stat == "grace")
        eventStat = Personality.Social.Grace;
      if (stat == "expression")
        eventStat = Personality.Social.Expression;

      if (count == "+")
      {
        Space.DispatchEvent(Events.AddStat, new ChangeStatEvent("Minor", eventStat));
      }
      else if (count == "++")
      {
        Space.DispatchEvent(Events.AddStat, new ChangeStatEvent("Major", eventStat));
      }
      else
      {
        if (stat == "awareness")
          Game.current.Self.AddBonusSocialStar(eventStat);
        if (stat == "grace")
          Game.current.Self.AddBonusSocialStar(eventStat);
        if (stat == "expression")
          Game.current.Self.AddBonusSocialStar(eventStat);
        Space.DispatchEvent(Events.StatChange);
      }
    }

    void OnWellbeingStatIncrement(Parse parse)
    {
      string stat = parse.FindFirst(statLabel).Trim().ToLower();
      int value = 0;
      int.TryParse(parse.FindFirst(valueLabel).Trim(), out value);

      OnWellbeingStatChange(stat, value);
      Trace.Script($"{stat} increased by {value}");
    }

    void OnWellbeingStatDecrement(Parse parse)
    {
      string stat = parse.FindFirst(statLabel).ToLower().Trim();
      int value = 0;
      int.TryParse(parse.FindFirst(valueLabel).Trim(), out value);

      OnWellbeingStatChange(stat, -value);
      Trace.Script($"{stat} decreased by {value}");
    }

    void OnWellbeingStatSet(Parse parse)
    {
      string stat = parse.FindFirst(statLabel).Trim().ToLower();
      int value = 0;
      int.TryParse(parse.FindFirst(valueLabel).Trim(), out value);

      OnWellbeingStatChange(stat, value, true);
      Trace.Script($"{stat} set to {value}");
    }

    void OnWellbeingStatChange(string stat, int value, bool assign = false)
    {
      var eventStat = Personality.Wellbeing.Depression;
      if (stat == "stress")
        eventStat = Personality.Wellbeing.Stress;
      if (stat == "fatigue")
        eventStat = Personality.Wellbeing.Fatigue;

      Space.DispatchEvent(Events.AddStat, new ChangeStatEvent(value, eventStat, assign));
    }

    void OnUIDisplay(Parse parse)
    {
      foreach (var match in parse.matches)
      {
        if (match.ContainsKey("UI"))
        {
          Trace.Script($"{parse.FindFirst("display").Trim().ToLower()} {match["UI"].Trim().ToLower()}");
          // Send event with element and show/hide state
          Stratus.Scene.Dispatch<TagToggler.TutorialDisplayChange>(new TagToggler.TutorialDisplayChange(match["UI"].Trim().ToLower(), parse.FindFirst("display").Trim().ToLower() == "hide"));
        }
      }
    }

    void OnAudioTrigger(Parse parse)
    {
      foreach (var match in parse.matches)
      {
        if (match.ContainsKey("Mode"))
        {
          if (match["Mode"].Trim().ToLower() == "play")
          {
            Trace.Script($"Play {parse.FindFirst("Event").Trim()} music");
            Game.current.CurrentTrack = parse.FindFirst("Event");
            Scene.Dispatch<AudioManager.AudioEvent>(new AudioManager.AudioEvent(AudioManager.AudioEvent.SoundType.Music, parse.FindFirst("Event")));
          }
          else if (match["Mode"].Trim().ToLower() == "sfx")
          {
            Trace.Script($"Play {parse.FindFirst("Event").Trim()} sound effect");
            Scene.Dispatch<AudioManager.AudioEvent>(new AudioManager.AudioEvent(AudioManager.AudioEvent.SoundType.SFX, parse.FindFirst("Event")));
          }
          else if (match["Mode"].Trim().ToLower() == "ambience")
          {
            Trace.Script($"Play {parse.FindFirst("Event").Trim()} ambience");
            Game.current.CurrentAmbience = parse.FindFirst("Event");
            Scene.Dispatch<AudioManager.AudioEvent>(new AudioManager.AudioEvent(AudioManager.AudioEvent.SoundType.Ambience, parse.FindFirst("Event")));
          }
        }

      }
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

    public void SetValue(string ValueName, bool newValue)
    {
      Game.current.Progress.SetValue(ValueName, newValue);
    }

    public void SetIntValue(string ValueName, int newValue)
    {
      Game.current.Progress.SetValue(ValueName, newValue);
    }

    public bool GetValue(string ValueName)
    {
      return Game.current.Progress.GetBoolValue(ValueName);

    }

    public int GetIntValue(string ValueName)
    {
      return Game.current.Progress.GetIntValue(ValueName);
    }

    public void SetStringValue(string ValueName, string value)
    {
      Game.current.Progress.SetValue(ValueName, value);
    }


    public string GetStringValue(string ValueName)
    {
      return Game.current.Progress.GetStringValue(ValueName);
    }

    public void SetTimeBlock(int time)
    {
      Game.current.SetTimeBlock(time);

    }
    public int GetSelfStat(string stat_name)
    {
      return Game.current.Self.GetStat(stat_name);
    }

    public void AlterTime()
    {
      Game.current.AlterTime();
    }

    public int GetHour()
    {
      return Game.current.Hour;
    }
    public void CallSleep()
    {
      Game.current.Slept();
    }

    public void SetPlayerGender(string genderPicked)
    {
      Game.current.Progress.SetValue<string>("PlayerGender", genderPicked);
    }

    public void GetPlayerName()
    {
      var nameInfo = new IdentityDisplay.PlayerGetInfoEvent();
      Space.DispatchEvent(Events.GetPlayerInfo, nameInfo);
    }

  }

}