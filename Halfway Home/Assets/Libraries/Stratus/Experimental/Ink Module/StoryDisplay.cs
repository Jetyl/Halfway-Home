using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

namespace Stratus
{
  namespace InkModule
  {
    /// <summary>
    /// An abstract interface for displaying an ink story in an event-driven way
    /// </summary>
    public abstract class StoryDisplay : MonoBehaviour 
    {
      protected abstract void OnStart();
      protected abstract void OnStoryStarted();
      protected abstract void OnStoryEnded();
      protected abstract void OnStoryUpdate(Story.ParsedLine parse);
      protected abstract void OnPresentChoices(List<Choice> choices);
      protected abstract void OnChoiceSelected();

      private GameObject readerObject;

      /// <summary>
      /// Initializes the script
      /// </summary>
      void Start()
      {
        Scene.Connect<Story.StartedEvent>(this.OnStoryStartedEvent);
        Scene.Connect<Story.EndedEvent>(this.OnStoryEndedEvent);
        Scene.Connect<Story.UpdateLineEvent>(this.OnStoryUpdateEvent);
        Scene.Connect<Story.PresentChoicesEvent>(this.OnStoryPresentChoicesEvent);
        OnStart();
      }

      //------------------------------------------------------------------------------------------/
      // Events
      //------------------------------------------------------------------------------------------/
      /// <summary>
      /// Received when a story has started
      /// </summary>
      /// <param name="e"></param>
      void OnStoryStartedEvent(Story.StartedEvent e)
      {
        readerObject = e.readerObject;
        OnStoryStarted();
      }

      /// <summary>
      /// Received when a story has ended
      /// </summary>
      /// <param name="e"></param>
      void OnStoryEndedEvent(Story.EndedEvent e)
      {
        OnStoryEnded();
      }

      /// <summary>
      /// Called upon when a new line is read
      /// </summary>
      /// <param name="e"></param>
      void OnStoryUpdateEvent(Story.UpdateLineEvent e)
      {
        OnStoryUpdate(e.parse);
      }

      /// <summary>
      /// Called upon when the current conversation presents choices to the player.
      /// </summary>
      /// <param name="e"></param>
      void OnStoryPresentChoicesEvent(Story.PresentChoicesEvent e)
      {
        OnPresentChoices(e.Choices);
      }
      
      //------------------------------------------------------------------------------------------/
      // Methods
      //------------------------------------------------------------------------------------------/
      /// <summary>
      /// Called upon when a particular choice has been selected
      /// </summary>
      /// <param name="choice"></param>
      public void SelectChoice(int choice)
      {
        // Convert to 0-based indexing
        choice -= 1;
        
        // Inform the current conversation of the choice
        var choiceEvent = new Story.SelectChoiceEvent();
        choiceEvent.Choice = choice;
        readerObject.gameObject.Dispatch<Story.SelectChoiceEvent>(choiceEvent);

        // Now do any extra stuff
        OnChoiceSelected();
      }

      /// <summary>
      /// Called upon to continue the story
      /// </summary>
      public void ContinueStory()
      {
        readerObject.Dispatch<Story.ContinueEvent>(new Story.ContinueEvent());
      }

    } 
  }

}