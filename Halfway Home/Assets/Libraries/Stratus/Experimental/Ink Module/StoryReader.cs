using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;
using System.Text.RegularExpressions;

namespace Stratus
{
  namespace InkModule
  {
    /// <summary>
    /// An abstract interface for reading an ink story file in an event-driven way
    /// </summary>
    public abstract class StoryReader : Triggerable
    {
      //------------------------------------------------------------------------------------------/
      // Public Fields
      //------------------------------------------------------------------------------------------/
      [Header("Story")]
      [Tooltip("Select an .ink file here!")]
      public TextAsset storyFile;
      [Tooltip("What knot in the conversation to start on")]
      public string knot;
      [Tooltip("Allow the story to be restarted if it has ended already when triggered")]
      public bool allowRestart = false;

      //------------------------------------------------------------------------------------------/
      // Private Fields
      //------------------------------------------------------------------------------------------/
      /// <summary>
      /// /The current knot (sub-section) of the story we are on
      /// </summary>
      private string stitch { get; set; }

      //------------------------------------------------------------------------------------------/
      // Properties
      //------------------------------------------------------------------------------------------/
      /// <summary>
      /// The data structure that contains the story. We iterate through it as we go through
      /// the story structure.
      /// </summary>
      private Ink.Runtime.Story story { get; set; }

      /// <summary>
      /// The list of all selected parses for this reader
      /// </summary>
      private Story.ParsePatterns parsePatterns = new Story.ParsePatterns();

      //------------------------------------------------------------------------------------------/
      // Virtual Functions
      //------------------------------------------------------------------------------------------/
      protected virtual void OnStoryLoaded(Ink.Runtime.Story story) {}
      protected abstract void OnSetParsingPatterns(Story.ParsePatterns patterns);
      protected abstract void OnBindExternalFunctions(Ink.Runtime.Story story);

      //------------------------------------------------------------------------------------------/
      // Messages
      //------------------------------------------------------------------------------------------/
      protected override void OnAwake()
      {
        // Connect to common events
        this.gameObject.Connect<Story.ContinueEvent>(this.OnContinueEvent);
        this.gameObject.Connect<Story.SelectChoiceEvent>(this.OnSelectChoiceEvent);
        this.gameObject.Connect<Story.RetrieveVariableValueEvent>(this.OnRetrieveVariableValueEvent);
        this.gameObject.Connect<Story.SetVariableValueEvent>(this.OnSetVariableValueEvent);
        this.gameObject.Connect<Story.ObserveVariableEvent>(this.OnObserveVariableEvent);
        this.gameObject.Connect<Story.ObserveVariablesEvent>(this.OnObserveVariablesEvent);
      }

      protected override void OnTrigger()
      {
        this.LoadStory();

        if (!story.canContinue)
        {
          if (allowRestart)
          {
            if (logging)
              Trace.Script("Restarting the story '" + storyFile.name + "'!", this);
            story.ResetState();
          }
          else
          {
            if (logging)
              Trace.Script("The story '" + storyFile.name + "' is over!", this);
            return;
          }

        }

        this.StartStory();
      }

      /// <summary>
      /// Once a story has been set, loads it
      /// </summary>
      void LoadStory()
      {
        // Construct the ink story data structure from the text file
        ConstructStory();

        // Bind external functions to it
        OnBindExternalFunctions(story);

        // Set parsing patterns
        OnSetParsingPatterns(parsePatterns);

        // Announce it
        this.gameObject.Dispatch<Story.LoadedEvent>(new Story.LoadedEvent() { story = this.story });
        OnStoryLoaded(story);
      }

      //------------------------------------------------------------------------------------------/
      // Events
      //------------------------------------------------------------------------------------------/ 
      void OnContinueEvent(Story.ContinueEvent e)
      {
        this.ContinueStory();
      }

      void OnSelectChoiceEvent(Story.SelectChoiceEvent e)
      {
        this.SelectChoice(e.choice);
        this.ContinueStory();
      }

      void OnRetrieveVariableValueEvent(Story.RetrieveVariableValueEvent e)
      {
        switch (e.variable.type)
        {
          case Story.Types.Integer:
            e.variable.intValue = Story.GetVariableValue<int>(story, e.variable.name);
            break;
          case Story.Types.Boolean:
            e.variable.boolValue = Story.GetVariableValue<bool>(story, e.variable.name);
            break;
          case Story.Types.Float:
            e.variable.floatValue = Story.GetVariableValue<float>(story, e.variable.name);
            break;
          case Story.Types.String:
            e.variable.stringValue = Story.GetVariableValue<string>(story, e.variable.name);
            break;
        }
      }

      void OnSetVariableValueEvent(Story.SetVariableValueEvent e)
      {
        switch (e.variable.type)
        {
          case Story.Types.Integer:
            Story.SetVariableValue<int>(story, e.variable.name, e.variable.intValue);
            break;
          case Story.Types.Boolean:
            Story.SetVariableValue<bool>(story, e.variable.name, e.variable.boolValue);
            break;
          case Story.Types.String:
            Story.SetVariableValue<string>(story, e.variable.name, e.variable.stringValue);
            break;
          case Story.Types.Float:
            Story.SetVariableValue<float>(story, e.variable.name, e.variable.floatValue);
            break;
        }
      }

      void OnObserveVariableEvent(Story.ObserveVariableEvent e)
      {
        if (logging)
          Trace.Script("Observing " + e.variableName);
        story.ObserveVariable(e.variableName, e.variableObserver);
      }

      void OnObserveVariablesEvent(Story.ObserveVariablesEvent e)
      {
        story.ObserveVariables(e.variableNames, e.variableObserver);
      }

      //------------------------------------------------------------------------------------------/
      // Methods: Public
      //------------------------------------------------------------------------------------------/
      /// <summary>
      /// Sets the value of a variable from the current story
      /// </summary>
      /// <param name="name"></param>
      /// <param name="value"></param>
      public void SetVariableValue(string name, object value)
      {
        story.variablesState[name] = value;
      }

      /// <summary>
      /// Returns the value of a variable from the current story
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public object GetVariableValue(string name)
      {
        return story.variablesState[name];
      }

      //------------------------------------------------------------------------------------------/
      // Methods: Parsing
      //------------------------------------------------------------------------------------------/
      /// <summary>
      /// Constructs the ink story runtime object
      /// </summary>
      void ConstructStory()
      {
        story = new Ink.Runtime.Story(storyFile.text);
        if (!story)
          Trace.Error("Failed to load the story", this, true);
        
      }

      /// <summary>
      /// Starts the current dialog.
      /// </summary>
      void StartStory()
      {
        if (logging)
          Trace.Script($"The story {storyFile.name} has started!");

        // If a knot has been selected...
        if (this.knot.Length > 0)
        {
          this.JumpToKnot(this.knot);
        }

        // Inform the space that dialog has started
        var startedEvent = new Story.StartedEvent();
        startedEvent.readerObject = this.gameObject;
        this.gameObject.Dispatch<Story.StartedEvent>(startedEvent);
        Scene.Dispatch<Story.StartedEvent>(startedEvent);

        // Update the first line of dialog
        this.ContinueStory();
      }

      /// <summary>
      /// Updates the current dialog. This will check if the conversation can
      /// be continued.If it can't ,it will then check if there are any choices
      /// to be made.If there aren't, it will end the dialog.
      /// </summary>
      void ContinueStory()
      {
        // If there is more dialog
        if (story.canContinue)
        {
          var line = story.Continue();
          UpdateCurrentLine(line);
        }
        // If we are given a choice
        else if (story.currentChoices.Count > 0)
        {
          PresentChoices();
        }
        // If we are done with the conversation, end story
        else
        {
          this.EndStory();
        }
      }

      /// <summary>
      /// Ends the dialog.
      /// </summary>
      void EndStory()
      {
        if (logging)
          Trace.Script("Ending story!");

        // Dispatch the ended event
        var storyEnded = new Story.EndedEvent();
        this.gameObject.Dispatch<Story.EndedEvent>(storyEnded);
        Scene.Dispatch<Story.EndedEvent>(storyEnded);
      }

      //------------------------------------------------------------------------------------------/
      // Methods: Story
      //------------------------------------------------------------------------------------------/
      /// <summary>
      /// Updates the current line of the story
      /// </summary>
      protected virtual void UpdateCurrentLine(string line)
      {
        if (logging)
          Trace.Script($"\"{line}\" ");

        var updateEvent = new Story.UpdateLineEvent
        {
          parse = Parse(line)
        };
        Scene.Dispatch<Story.UpdateLineEvent>(updateEvent);
      }

      /// <summary>
      /// Parses a line of ink dialog, using whatever parses have been set
      /// </summary>
      /// <param name="line"></param>
      /// <returns></returns>
      protected virtual Story.ParsedLine Parse(string line)
      {
        Dictionary<string, string> parses = new Dictionary<string, string>();

        // Try every parse
        foreach (var parse in parsePatterns.all)
        {
          Regex speaker = new Regex(parse.pattern, RegexOptions.IgnoreCase);
          Match m = speaker.Match(line);
          if (m.Success)
            parses[parse.name] = m.Value;
        }

        var parsedLine = new Story.ParsedLine(parses, line);
        return parsedLine;
      }

      /// <summary>
      /// Presents choices at the current story node
      /// </summary>
      void PresentChoices()
      {
        if (logging)
          Trace.Script("Presenting dialog choices!");

        var choicesEvent = new Story.PresentChoicesEvent();
        choicesEvent.Choices = story.currentChoices;
        Scene.Dispatch<Story.PresentChoicesEvent>(choicesEvent);
      }

      /// <summary>
      /// Selects a choice for the current conversation.
      /// </summary>
      /// <param name="choice">A 0-indexed choice.</param>
      void SelectChoice(int choice)
      {
        this.story.ChooseChoiceIndex(choice);
      }


      /// <summary>
      /// Selects a choice for the current conversation.
      /// </summary>
      /// <param name="choice">A 0-indexed choice.</param>
      void SelectChoice(Choice choice)
      {
        this.story.ChooseChoiceIndex(choice.index);
      }

      /// <summary>
      /// Jumps to the specified knot in the story.
      /// </summary>
      /// <param name="knotName">The name of the knot.</param>
      void JumpToKnot(string knotName)
      {
        //Trace.Script("Jumping to the knot '" + knotName + "'", this);
        this.story.ChoosePathString(knotName + this.stitch);
      }
      /// <summary>
      /// Updates the current stitch
      /// </summary>
      /// <param name="stitchName"></param>
      void UpdateStitch(string stitchName)
      {
        if (stitchName.Length == 0)
          return;

        this.stitch = "." + stitchName;
        if (logging)
          Trace.Script("Updating stitch to '" + stitch + "'", this);
      }

      /// <summary>
      /// Checks if a string consists of whitespace only
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      bool ConsistsOfWhiteSpace(string s)
      {
        foreach (char c in s)
        {
          if (c != ' ') return false;
        }
        return true;

      }

    }

  }
}