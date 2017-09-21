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
        StoryStarted,
        StoryContinue,
        StoryEnded
      }

      [Tooltip("The story this trigger is reacting to")]
      public StoryReader reader;
      [Tooltip("What type of event this is being triggered by")]
      public Type type;
      [Tooltip("What variable we are ")]
      public Story.Variable variable;
      
      protected override void OnInitialize()
      {
        switch (type)
        {
          case Type.StoryStarted:
            reader.gameObject.Connect<Story.StartedEvent>(this.OnStoryStartedEvent);
            break;
          case Type.StoryContinue:
            reader.gameObject.Connect<Story.ContinueEvent>(this.OnStoryContinueEvent);
            break;
          case Type.StoryEnded:
            reader.gameObject.Connect<Story.EndedEvent>(this.OnStoryEndedEvent);
            break;
        }
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