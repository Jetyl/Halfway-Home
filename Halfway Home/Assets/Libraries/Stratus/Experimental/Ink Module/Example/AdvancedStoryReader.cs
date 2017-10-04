using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Stratus
{
  namespace InkModule
  {
    public class AdvancedStoryReader : StoryReader 
    {
      protected override void OnBindExternalFunctions(Ink.Runtime.Story story)
      {
        
      }

      protected override void OnSetLineParsing(Story.ParsePatterns patterns)
      {
        // [Speaker] =  \[([a-zA-Z0-9-\s]+)\]
        string speakerPattern = @"\[([a-zA-Z0-9-\s]+)\]";
        speakerPattern = @"\[(.*)\]";
        patterns.Add("Speaker", speakerPattern);

        // "Blah blah blah"
        string messagePattern = "\"[^\"]*\"";
        messagePattern = "\".*?\"";        
        patterns.Add("Message", messagePattern);
      }

    }
  }

}