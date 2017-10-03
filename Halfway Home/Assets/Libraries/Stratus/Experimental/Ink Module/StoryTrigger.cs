using UnityEngine;

namespace Stratus
{
  namespace InkModule
  {
    /// <summary>
    /// A trigger which reacts to changes in an ink story reader
    /// </summary>
    public class StoryTrigger : Trigger
    {
      public enum Type
      {
        Loaded,
        Started,
        Continue,
        Ended
      }

      [Tooltip("The story this trigger is reacting to")]
      public StoryReader reader;
      [Tooltip("What type of event this is being triggered by")]
      public Type storyEvent;
      [Tooltip("What variable we are ")]
      public Story.Variable variable;

      // Fields
      private TextAsset storyFile;
      
      protected override void OnInitialize()
      {
        switch (storyEvent)
        {
          case Type.Loaded:
            reader.gameObject.Connect<Story.LoadedEvent>(this.OnStoryLoadedEvent);
            break;
          case Type.Started:
            reader.gameObject.Connect<Story.StartedEvent>(this.OnStoryStartedEvent);
            break;
          case Type.Continue:
            reader.gameObject.Connect<Story.ContinueEvent>(this.OnStoryContinueEvent);
            break;
          case Type.Ended:
            reader.gameObject.Connect<Story.EndedEvent>(this.OnStoryEndedEvent);
            break;
        }
      }

      void OnStoryLoadedEvent(Story.LoadedEvent e)
      {
        Activate();
      }

      void OnStoryStartedEvent(Story.StartedEvent e)
      {
        this.Activate();
      }

      void OnStoryContinueEvent(Story.ContinueEvent e)
      {
        this.Activate();
      }

      void OnStoryEndedEvent(Story.EndedEvent e)
      {
        this.Activate();
      }

    } 

  }
}