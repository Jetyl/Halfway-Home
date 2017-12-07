/******************************************************************************/
/*!
@file   StorySaveData.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
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
      public class StorySave : SaveData<StorySave>
      {
        /// <summary>
        /// The saved states of all stories that were loaded by a reader
        /// </summary>
        public List<Story> stories = new List<Story>();

        /// <summary>
        /// The story currently being read
        /// </summary>
        public Story currentStory;
      } 

    }
  }
}