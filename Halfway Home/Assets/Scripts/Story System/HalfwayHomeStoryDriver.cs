using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;
using Stratus.InkModule;
using System;

/// <summary>
/// Signals that a new story file should be read
/// </summary>
public class StoryEvent : DefaultEvent
{
  public TextAsset storyFile;
  public StoryEvent(TextAsset file)
  {
    storyFile = file;
  }
}

/// <summary>
/// Signals that a music track should be played
/// </summary>
public class PlayMusicEvent : Stratus.Event
{
  public string track;
}

public class CharacterChangeEvent : Stratus.Event
{
  public string character;
  public bool entering;
}

namespace HalfwayHome
{
  public class HalfwayHomeStoryDriver : MonoBehaviour
  {
    private class StatAccess
    {
      public string name;
      public void Set(float value) => Game.current.Progress.SetValue(name, value);
      public float Get() => Game.current.Progress.GetFloatValue(name);
    }

    [Header("Story")]
    public HalfwayHomeStoryReader reader;

    private string[] statNames { get; } = { "grace", "expression", "awareness", "stress", "fatigue", "delusion", "week" };
    private Dictionary<string, StatAccess> stats = new Dictionary<string, StatAccess>();

    void Start()
    {
      // Create a dictionary for all stats for quick O(1) access
      foreach (var stat in statNames)
        stats.Add(stat, new StatAccess() { name = stat });

      Space.Connect<StoryEvent>(Events.NewStory, OnNewStory);
      reader.gameObject.Connect<Story.LoadedEvent>(this.OnStoryLoadedEvent);
      reader.gameObject.Connect<Story.StartedEvent>(this.OnStoryStartedEvent);
      reader.gameObject.Connect<Story.EndedEvent>(this.OnStoryEndedEvent);
    }

    /// <summary>
    /// Received when a story should be started by the reader
    /// </summary>
    /// <param name="eventdata"></param>
    void OnNewStory(StoryEvent eventdata)
    {
      Trace.Script("Reading " + eventdata.storyFile.name, this);
      reader.gameObject.Dispatch<Story.LoadEvent>(new Story.LoadEvent() { storyFile = eventdata.storyFile });
    }

    /// <summary>
    /// Received when a story has been loaded. This will load up the initial values from the database
    /// </summary>
    /// <param name="e"></param>
    void OnStoryLoadedEvent(Story.LoadedEvent e)
    {
      // Set the name and gender
      var playerName = Game.current.PlayerName;
      var playerGender = Game.current.Progress.GetStringValue("player_gender");
      reader.SetVariableValue("player_name", playerName);
      reader.SetVariableValue("player_gender", playerGender);

      // Set all initial values
      foreach (var pair in stats)
      {
        var name = pair.Value.name;
        var value = pair.Value.Get();
        Trace.Script($"Setting {name} to {value}", this);
        reader.SetVariableValue(name, value);
      }

      // Set up variable observers after the story ends?
      reader.gameObject.Dispatch<Story.ObserveVariablesEvent>(new Story.ObserveVariablesEvent() { variableNames = statNames, variableObserver = ObserveVariable });
    }

    /// <summary>
    /// Received when a story has been started by the reader
    /// </summary>
    /// <param name="e"></param>
    void OnStoryStartedEvent(Story.StartedEvent e)
    {
      // Do wutvz
    }

    /// <summary>
    /// Received when the StoryReader is done going through the story.
    /// This will return control back to the main system
    /// </summary>
    /// <param name="e"></param>
    void OnStoryEndedEvent(Stratus.InkModule.Story.EndedEvent e)
    {
      Space.DispatchEvent(Events.FinishedStory);
    }

    /// <summary>
    /// Whenever a stat changes, is invoked by ink
    /// </summary>
    /// <param name="variableName"></param>
    /// <param name="value"></param>
    void ObserveVariable(string variableName, object value)
    {
      Trace.Script($"Updating {variableName} to {value}", this);
      stats[variableName].Set((float)value);
    }
  }

}