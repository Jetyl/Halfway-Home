using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stratus
{
  namespace Modules
  {
    namespace InkModule
    {
      /// <summary>
      /// Interface for easily saving the state of an ink runtime story
      /// </summary>
      [SaveData(extension = ".story")]
      public class StorySave : JsonSaveData<StorySave>
      {
        /// <summary>
        /// The saved states of all stories that were loaded by a reader
        /// </summary>
        public List<Story> storyList = new List<Story>();

        /// <summary>
        /// The story currently being read
        /// </summary>
        public Story currentStory;

        /// <summary>
        /// All persistent stories are tracked here. When a story that was loaded is marked as persistent,
        /// once its ended, we will save its state so next time its asked to be loaded, we will
        /// load from a previous state.
        /// </summary>
        public Dictionary<string, Story> stories { get; set; } = new Dictionary<string, Story>();

        //------------------------------------------------------------------------------------------/
        // Messages
        //------------------------------------------------------------------------------------------/
        protected override void OnSave()
        {
          storyList.Clear();
          foreach(var story in stories)
          {
            story.Value.filePath = story.Value.name;
            storyList.Add(story.Value);
          }

          currentStory.filePath = currentStory.file.name;
        }

        protected override bool OnLoad()
        {
          foreach(var story in storyList)
          {
            story.file = Resources.Load(story.filePath) as TextAsset;
            stories.Add(story.name, story);

            if (story.file == null)
            {
              Trace.Error($"Failed to load {story.filePath}");
              return false;
            }
          }

          currentStory.file = Resources.Load(currentStory.filePath) as TextAsset;
          if (currentStory.file == null)
          {
            Trace.Error($"Failed to load {currentStory.filePath}");
            return false;
          }

          return true;
        }

      } 

    }
  }
}