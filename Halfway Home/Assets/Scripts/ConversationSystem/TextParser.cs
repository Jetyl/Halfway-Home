/******************************************************************************/
/*!
File:   TextParser.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using LitJson;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class TextParser
{

    public static IEnumerator FrameDelay(Events tag)
    {

        yield return new WaitForSeconds(Time.deltaTime);

        Space.DispatchEvent(tag);

    }

    public static IEnumerator FrameDelay(GameObject target, Events tag)
    {

        yield return new WaitForSeconds(Time.deltaTime);

        target.DispatchEvent(tag);

    }

    public static IEnumerator FrameDelay<T>(Events tag, T eventdata) where T : EventData
    {

        yield return new WaitForSeconds(Time.deltaTime);

        Space.DispatchEvent(tag, eventdata);

    }

    public static IEnumerator FrameDelay<T>(GameObject target, Events tag, T eventdata) where T : EventData
    {

        yield return new WaitForSeconds(Time.deltaTime);

        target.DispatchEvent(tag, eventdata);

    }

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
    
    public static Dictionary<int, float> ExtractTextSpeed(ref string text, float defautSpeed)
    {

        Dictionary<int, float> newSpeed = new Dictionary<int, float>();
        float oldspeed = defautSpeed;
        int cutback = 0; //for other rich text functions, so they do not get in the way

        for (int i = 0; i < text.Length; ++i)
        {
            if (text[i] == '<')
            {
                if (text.Length < i + 7)
                    continue;

                var check = text.Substring(i, 7);
                if (check.ToLower() == "<speed=")
                {
                    for (int j = i + 7; j < text.Length; ++j)
                    {
                        if (text[j] == '>')
                        {
                            string num = text.Substring(i + 7, j - (i + 7));

                            if(num.Contains("%"))
                            {
                                float percent = float.Parse(num.Replace("%", ""));
                                float speed = oldspeed * (percent / 100f);
                                newSpeed.Add(i - cutback, speed);
                                oldspeed = speed;
                            }
                            else
                            {
                                float speed = float.Parse(num);
                                newSpeed.Add(i - cutback, speed);
                                oldspeed = speed;
                            }


                            text = text.Remove(i, j + 1 - i);
                            break;
                        }

                    }
                }
                else if (check.ToLower() == "<delay=")
                {
                    for (int j = i + 7; j < text.Length; ++j)
                    {
                        if (text[j] == '>')
                        {
                            float speed = float.Parse(text.Substring(i + 7, j - (i + 7)));
                            newSpeed.Add(i - cutback - 1, speed);
                            newSpeed.Add((i) - cutback, oldspeed);
                            text = text.Remove(i, j + 1 - i);
                            break;
                        }

                    }
                }
                else
                {
                    for (int j = i; j < text.Length; ++j)
                    {
                        if (text[j] == '>')
                        {
                            cutback += j - i;
                            cutback += 2; //add two, for the < and >
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


        //text = text.Replace("#PlayerName", Game.current.PlayerName);
        text = text.Replace("@", Environment.NewLine);
        text = text.Replace("<color=", "<color=#");
        
        //var regex = new Regex(Regex.Escape("\""));
        //text = regex.Replace(text, "“", 1);

        text = text.Replace("\"", "");

        var regret = new Regex(Regex.Escape("`"));
        text = regret.Replace(text, "“", 1);

        text = text.Replace("`", "\"");

        
        //color stuff
        text = text.Replace("color_wellbeing_relief", "1FD118");
        text = text.Replace("color_wellbeing_penalty", "ED3913");
        text = text.Replace("color_awareness", "2075DF");
        text = text.Replace("color_expression", "C34182");
        text = text.Replace("color_grace", "DE9E20");
        text = text.Replace("color_descriptor", "6999A6");

        return text;
    }


    public static string AddQuirk(string text, string Speaker)
    {


        return text;
    }
    
    public static int CheckProgress(JsonData data)
    {
        int NextID = -1;

        ProgressPoint CheckToMatch = new ProgressPoint(data["CheckToMatch"][0]);

        if (Game.current.Progress.CheckProgress(CheckToMatch))
            NextID = (int)data["PassID"];
        else
            NextID = (int)data["FailID"];

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
            case ProgressType.CG:
                Sprite Image = null;
                if (data["ImageSlug"] != null)
                {
                    Image = Resources.Load<Sprite>("Sprites/" + (string)data["ImageSlug"]);

                    Game.current.Memory.UnlockImage(Image);
                }
                break;
            case ProgressType.Objective:
                string num = (string)data["TaskNumber"];
                string[] id = num.Split('.');
                int num1 = Convert.ToInt32(id[0]);
                int num2 = -1;
                if (id.Length > 1)
                    num2 = Convert.ToInt32(id[1]);
                
                int state = (int)data["TaskState"];
                Task.TaskState NewTaskState = (Task.TaskState)state;
                Game.current.Progress.UpdateTask(num1, NewTaskState, num2);
                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }

        


        return NextID;
    }


   


}
