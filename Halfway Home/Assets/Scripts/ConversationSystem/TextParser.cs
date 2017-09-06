using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using LitJson;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class TextParser
{
    
    //send the Lines Array json data
    public static List<Line> ParseLines(JsonData text)
    {

        if (text == null)
            return null;

        var NumOfLines = text.Count;
        var Lines = new List<Line>();

        for (int i = 0; i < NumOfLines; ++i)
        {
            Line l = new Line();
            if (text[i]["Speaker"] != null)
                l.Speaker = (string)text[i]["Speaker"];

            if (text[i]["Line"] != null)
                l.Dialog = (string)text[i]["Line"];

            l.Pace = (float)(double)text[i]["Pace"];

            Lines.Add(l);
        }


        return Lines;
    }



    #if UNITY_EDITOR
    public static List<Line> ParseLines(SerializedProperty text)
    {

        if (text == null)
            return null;

        var NumOfLines = text.arraySize;
        var Lines = new List<Line>();

        for (int i = 0; i < NumOfLines; ++i)
        {
            Line l = new Line();
            if (text.GetArrayElementAtIndex(i).FindPropertyRelative("Speaker") != null)
                l.Speaker = text.GetArrayElementAtIndex(i).FindPropertyRelative("Speaker").stringValue;

            l.Dialog = text.GetArrayElementAtIndex(i).FindPropertyRelative("Dialog").stringValue;
            l.Pace = text.GetArrayElementAtIndex(i).FindPropertyRelative("Pace").floatValue;
            Lines.Add(l);
        }


        return Lines;
    }

    #endif

    public static List<Line> ParseLines(TextAsset text)
    {

        //how should the "dynamic text work?"
        //for now, just cut it
        if (text == null)
            return null;
        
        var Lines = new List<Line>();
        
        return Lines;
    }

    public static JsonData ToJson(TextAsset text)
    {
        
        var taxt = JsonMapper.ToObject(text.text);
        
        return taxt;
    }

    public static JsonData ToJson(string text_name)
    {
        /*if (File.Exists(Application.streamingAssetsPath + "/" + text_name + ".json"))
        {

            var txt = JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/" + text_name + ".json"));
            return txt;
        }*/

        var k = (TextAsset)Resources.Load("Text/" + text_name);
        if(k != null)
        {
            var res = JsonMapper.ToObject(k.text);
            return res;
        }

        var j = (TextAsset)Resources.Load("Json/" + text_name);

        if (j != null)
        {
            var res = JsonMapper.ToObject(j.text);
            return res;
        }

        var l = (TextAsset)Resources.Load("Conversations/" + text_name);

        if (l != null)
        {
            var res = JsonMapper.ToObject(l.text);
            return res;
        }

        MonoBehaviour.print("Json could not be found");

        return null;
    }


    public static Dictionary<int, float> ExtractTextSpeed(ref string text)
    {

        Dictionary<int, float> newSpeed = new Dictionary<int, float>();
        float oldspeed = 20;

        for(int i = 0; i < text.Length; ++i)
        {
            if (text[i] == '<')
            {
                var check = text.Substring(i, 7);
                if(check == "<speed=" || check == "<Speed=")
                {
                    for (int j = i + 7; j < text.Length; ++j)
                    {
                        if (text[j] == '>')
                        {
                            float speed = float.Parse(text.Substring(i + 7, j - (i + 7)));
                            newSpeed.Add(i, speed);
                            oldspeed = speed;
                            text = text.Remove(i, j + 1 - i);
                            break;
                        }

                    }
                }
                if (check == "<delay=" || check == "<Delay=")
                {
                    for (int j = i + 7; j < text.Length; ++j)
                    {
                        if (text[j] == '>')
                        {
                            float speed = float.Parse(text.Substring(i + 7, j - (i + 7)));
                            newSpeed.Add(i, speed);
                            newSpeed.Add(i + 1, oldspeed);
                            text = text.Remove(i, j + 1 - i);
                            break;
                        }

                    }
                }
            }
        }

        
        return newSpeed;
    }



    //dynamically editing any of the changeable things in text, like the player's name
    public static List<Line> DynamicEdit(List<Line> text)
    {
        List<Line> EditedText = new List<Line>();
        for(int i = 0; i < text.Count; ++i)
        {
            Line l = text[i];
            l.Speaker = DynamicEdit(text[i].Speaker);
            l.Dialog = DynamicEdit(text[i].Dialog);
            EditedText.Add(l);
        }

        return EditedText;
    }


    //dynamically editing any of the changeable things in text, like the player's name
    public static string DynamicEdit(string text)
    {

        if (text == null)
            return text;

        text = text.Replace("#PlayerName", Game.current.PlayerName);
        text = text.Replace("#Desire", Game.current.Progress.GetStringValue("Desired Most"));

        return text;
    }

    
    public static int CheckProgress(JsonData data)
    {


        int ty = (int)data["TypeOfProgress"];

        ProgressType type = (ProgressType)ty;

        int NextID = -1;

        switch (type)
        {
            case ProgressType.None:
                break;
            case ProgressType.ProgressPoint:

                ProgressPoint CheckToMatch = new ProgressPoint(data["CheckToMatch"][0]);

                if (Game.current.Progress.CheckProgress(CheckToMatch))
                    NextID = (int)data["PassID"];
                else
                    NextID = (int)data["FailID"];

                break;
            
            case ProgressType.PlotBeat:

                string BeatName = (string)data["BeatName"];
                int statin = (int)data["Beat"];
                Beat.BeatState beatState = (Beat.BeatState)statin;

                if (Game.current.Progress.CheckBeatState(BeatName, beatState))
                    NextID = (int)data["PassID"];
                else
                    NextID = (int)data["FailID"];
                break;
            
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }


        return NextID;
    }

    
    public static bool CompareValues(ValueCompare Compare, float a, float b)
    {

        switch (Compare)
        {
            case ValueCompare.EqualTo:
                return a == b;
            case ValueCompare.GreaterThan:
                return a > b;
            case ValueCompare.LessThan:
                return a < b;
            default:
                return false;
        }

    }

    

    public static int MakeProgress(JsonData data)
    {

        int ty = (int)data["TypeOfProgress"];

        ProgressType type = (ProgressType)ty;

        int NextID = -1;

        switch (type)
        {
            case ProgressType.None:
                break;
            case ProgressType.ProgressPoint:

                ProgressPoint CheckToMatch = new ProgressPoint(data["CheckToMatch"][0]);

                Game.current.Progress.UpdateProgress(CheckToMatch.ProgressName, CheckToMatch);
                NextID = (int)data["NextID"];
                break;
            
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }


        return NextID;
    }


   


}
