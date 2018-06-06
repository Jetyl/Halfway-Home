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
      [System.Serializable]
      public class StorySave : JsonSaveData<StorySave>
      {
        /// <summary>
        /// The saved states of all stories that were loaded by a reader
        /// </summary>
        public List<Story> storyList = new List<Story>();

        /// <summary>
        /// The index of the current story
        /// </summary>
        public int currentStoryIndex;

        /// <summary>
        /// The story currently being read
        /// </summary>
        public Story currentStory => storyList.NotEmpty() ? storyList[currentStoryIndex] : null;

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
          //storyList.Clear();
          //foreach(var story in stories)
          //{
          //  story.Value.filePath = story.Value.name;
          //  storyList.Add(story.Value);
          //}

          //Resources.
          //currentStory.filePath = currentStory.file.name;
        }

        protected override bool OnLoad()
        {
          stories = new Dictionary<string, Story>();
          foreach(var story in storyList)
          {
            //story.file = Assets.LoadResource<TextAsset>(story.fileName);            
            story.file = Resources.Load(story.filePath != null ? story.filePath : story.fileName) as TextAsset;
            if (story.file == null)
            {
              Trace.Error($"Failed to load the story {story.filePath}");
              return false;
            }

            stories.Add(story.fileName, story);
          }

          return true;
        }

        

        //------------------------------------------------------------------------------------------/
        // Methods
        //------------------------------------------------------------------------------------------/
        public bool HasStory(Story story) => stories.ContainsKey(story.fileName);
        public bool HasStory(string storyName) => stories.ContainsKey(storyName);
        public bool HasStory(TextAsset storyFile) => stories.ContainsKey(storyFile.name);
        public void AddStory(Story story)
        {
          story.filePath = story.fileName;
          storyList.Add(story);
          stories.Add(story.fileName, story);
        }
        public void SetCurrentStory(Story story)
        {
          currentStoryIndex = storyList.FindIndex(x => x.fileName == story.fileName);
        }


      }

    }
  }
}