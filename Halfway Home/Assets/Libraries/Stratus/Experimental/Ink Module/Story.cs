using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;

namespace Stratus
{
  namespace InkModule
  {
    /// <summary>
    /// A story represents an Ink file
    /// </summary>
    public static class Story
    {
      /// <summary>
      /// The Ink language provides 4 common value types
      /// </summary>
      public enum Types { Integer, Boolean, String, Float }

      /// <summary>
      /// Represents a variable supported by Ink
      /// </summary>
      [Serializable]
      public class Variable
      {
        public Types type;
        public string name;

        public int intValue { get; set; }
        public bool boolValue { get; set; }
        public string stringValue { get; set; }
        public float floatValue { get; set; }
        public object objectValue
        {
          get
          {
            object value = null;
            switch (type)
            {
              case Types.Integer:
                value = intValue;
                break;
              case Types.Boolean:
                value = boolValue;
                break;
              case Types.String:
                value = stringValue;
                break;
              case Types.Float:
                value = floatValue;
                break;
            }

            return value;
          }
        }
      }

      /// <summary>
      /// A list of regular expression patterns used in order to parse a line 
      /// </summary>
      public class ParsePatterns
      {
        /// <summary>
        /// A single category for a parse type
        /// </summary>
        public class Category
        {
          /// <summary>
          /// The name of the category this parse is for (Example: "Speaker", "Message", etc ...)
          /// </summary>
          public string name;

          /// <summary>
          /// The regular expression pattern that will be used for this parse
          /// </summary>
          public string pattern;
        }

        private List<Category> categories = new List<Category>();

        /// <summary>
        /// "Meowth that's right"
        /// </summary>
        public string insideDoubleQuotes => "\"[^\"]*\"";

        /// <summary>
        /// [Meowth]
        /// </summary>
        public string insideSquareBrackets => @"\[(.*?)\]";
        //public string insideSquareBrackets => @"\[([a-zA-Z0-9-\s]+)\]";

        /// <summary>
        /// All categories set for parsing
        /// </summary>
        public Category[] all => categories.ToArray();

        /// <summary>
        /// Adds a pattern to the parsing filter
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <param name="pattern">A regular expression pattern</param>
        public void Add(string name, string pattern)
        {
          categories.Add(new Category() { name = name, pattern = pattern });
        }

        /// <summary>
        /// Composes a regex that captures everything inside the escaped characters (brackets, quotes, etc)
        /// </summary>
        /// <param name="escapedCharacter"></param>
        /// <returns></returns>
        public static string EverythingWithinEnclosure(char escapedCharacter) => escapedCharacter + "(.*?)" + escapedCharacter;
      }

      /// <summary>
      /// Represents a parsed line of dialog. It may have multiple parses
      /// such as one for the speaker, another for the message, etc...
      /// </summary>
      public struct ParsedLine
      {
        private Dictionary<string, string> parses; // = new Dictionary<string, string>();

        public ParsedLine(Dictionary<string, string> parses, string line)
        {
          this.parses = parses;
          this.line = line;
        }

        /// <summary>
        /// The unparsed line
        /// </summary>
        public string line { get; private set; }

        /// <summary>
        /// Whether this line has any valid parses
        /// </summary>
        public bool isParsed => parses.Count > 0;

        /// <summary>
        /// Whether this line has any valid parses
        /// </summary>
        public bool hasValidParses
        {
          get
          {
            int validParses = parses.Values.Count;
            foreach (var parse in parses)
            {
              if (parse.Value.Length == 0)
                validParses--;
            }
            // If there's at least 1 valid parse, consider the line parsed
            return validParses > 0 ? true : false;
          }
        }

        /// <summary>
        /// Retrieves a specific parse from this line
        /// </summary>
        /// <param name="parseCategory"></param>
        /// <returns></returns>
        public string Find(string parseCategory) => parses[parseCategory];
      }

      /// <summary>
      /// Signals that a story has been loaded
      /// </summary>
      public class LoadedEvent : Stratus.Event
      {
        /// <summary>
        /// A reference to the story that has just been loaded
        /// </summary>
        public Ink.Runtime.Story story;
      }

      /// <summary>
      /// Signals that a story has started
      /// </summary>
      public class StartedEvent : Stratus.Event
      {
        public GameObject readerObject;
      }

      /// <summary>
      /// Signals that a story has ended
      /// </summary>
      public class EndedEvent : Stratus.Event
      {
      }

      /// <summary>
      /// Signals that the story should continue onto the next node
      /// </summary>
      public class ContinueEvent : Stratus.Event
      {
      }

      /// <summary>
      /// Signals that a choice should be presented to the reader
      /// </summary>
      public class PresentChoicesEvent : Stratus.Event
      {
        public List<Choice> Choices = new List<Choice>();
      }

      /// <summary>
      /// Signals that a choice has been selected by the reader
      /// </summary>
      public class SelectChoiceEvent : Stratus.Event
      {
        public Choice choice;
      }

      /// <summary>
      /// Represents a parsed line of dialog. It may have multiple parses
      /// such as one for the speaker, another for the message, etc...
      /// </summary>
      public class UpdateLineEvent : Stratus.Event
      {
        public ParsedLine parse;
      }

      /// <summary>
      /// Base class for events involving ink variables
      /// </summary>
      public abstract class VariableValueEvent : Stratus.Event
      {
        /// <summary>
        /// Represents a variable in Ink
        /// </summary>
        public Variable variable;
      }

      /// <summary>
      /// Retrieves the value of a specified variable from an ink story
      /// </summary>
      public class RetrieveVariableValueEvent : VariableValueEvent
      {
      }

      /// <summary>
      /// Retrieves the value of a specified variable from an ink story
      /// </summary>
      public class SetVariableValueEvent : VariableValueEvent
      {
      }

      /// <summary>
      /// Observes the state of a variable
      /// </summary>
      public class ObserveVariableEvent : Stratus.Event
      {
        public string variableName;
        public Ink.Runtime.Story.VariableObserver variableObserver;
      }

      /// <summary>
      /// Observes the state of a multiple variables. It will call the function
      /// once per each variable
      /// </summary>
      public class ObserveVariablesEvent : Stratus.Event
      {
        public string[] variableNames;
        public Ink.Runtime.Story.VariableObserver variableObserver;
      }

      /// <summary>
      /// Dispatches an event in order to retrieve the value of a given variable.
      /// </summary>
      /// <typeparam name="EventType"></typeparam>
      /// <typeparam name="ValueType"></typeparam>
      /// <param name="story">The Ink story object.</param>
      /// <param name="variableName">The name of the variable.</param>
      /// <param name="target">The recipient of the event</param>
      static public ValueType GetVariableValue<ValueType>(Ink.Runtime.Story story, string variableName)
      {
        // Edge case
        if (typeof(ValueType) == typeof(bool))
        {
          var value = (int)story.variablesState[variableName];
          return (ValueType)System.Convert.ChangeType(value, typeof(ValueType));
        }

        return (ValueType)story.variablesState[variableName];
      }

      /// <summary>
      /// Dispatches an event in order to retrieve the value of a given variable.
      /// </summary>
      /// <typeparam name="EventType"></typeparam>
      /// <typeparam name="ValueType"></typeparam>
      /// <param name="story">The Ink story object.</param>
      /// <param name="variableName">The name of the variable.</param>
      /// <param name="target">The recipient of the event</param>
      public static void SetVariableValue<ValueType>(Ink.Runtime.Story story, string variableName, ValueType value)
      {
        story.variablesState[variableName] = value;
      }

      //public static void ObserveVariable(Ink.Runtime.Story story, string variableName, Ink.Runtime.Story.VariableObserver onChange)
      //{
      //  story.ObserveVariable(variableName, onChange);
      //}

    }

  }
}
