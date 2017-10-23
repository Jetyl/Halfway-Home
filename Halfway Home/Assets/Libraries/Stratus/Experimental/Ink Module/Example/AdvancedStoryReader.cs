using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Stratus.Modules.InkModule
{
  public class AdvancedStoryReader : StoryReader<RegexParser>
  {
    private string saveFile = "AdvancedReaderSave";
    private string saveFolder = "Stratus Example";
    private StorySave storySave = new StorySave();
    
    protected override void OnBindExternalFunctions(Story story)
    {
    }

    protected override void OnConfigureParser(RegexParser parser)
    {
      parser.AddPattern("Speaker", RegexParser.Presets.insideSquareBrackets, RegexParser.Target.Line, RegexParser.Scope.Default);
      parser.AddPattern("Message", RegexParser.Presets.insideDoubleQuotes, RegexParser.Target.Line, RegexParser.Scope.Default);

      parser.AddPattern("Assignment", RegexParser.Presets.assignment, RegexParser.Target.Tag,
       RegexParser.Scope.Group); //, (Parse parse) => { Trace.Script(parse.groupInformation); });

      string incrementPattern = RegexParser.Presets.ComposeUnaryOperation("Stat", '+');
      parser.AddPattern("Increment", incrementPattern, RegexParser.Target.Tag,
       RegexParser.Scope.Group, (Parse parse) => { Trace.Script(parse.groupInformation); });

    }

    protected override void OnSave(Dictionary<string, Story> stories)
    {
      // From dictionary to list
      List<Story> storyList = new List<Story>();
      foreach(var story in stories)
        storyList.Add(story.Value);      

      // Now save it!
      storySave.stories = storyList;
      StorySave.Save(storySave, saveFile, saveFolder);

      Trace.Script("Saved!");
    }

    protected override void OnLoad(Dictionary<string, Story> stories)
    {
      if (StorySave.Exists(saveFile, saveFolder))
      {
        storySave = StorySave.Load(saveFile, saveFolder);

        // From list to dictionary!
        foreach (var story in storySave.stories)
        {
          Trace.Script($"Loaded {story.name}");
          stories.Add(story.name, story);
        }

        Trace.Script("Loaded!");
      }
    }

    protected override void OnClear()
    {
      StorySave.Delete(saveFile, saveFolder);
    }

  }
}
