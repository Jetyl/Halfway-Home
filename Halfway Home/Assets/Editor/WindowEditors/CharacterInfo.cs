using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using LitJson;
using UnityEditor;
using TMPro;

public class CharacterInfo
{
    public string Name;

    public Sprite MapIcon;

    public Color SpeakerColor = Color.white;
    public TMP_FontAsset Font;
    public int FontSizeMin = 20;
    public int FontSizeMax = 72;

    bool showSchedule;
    bool ShowQuirk;
    public TypingQuirk StartEndQuirk;

    public List<TypingQuirk> ReplacingQuirks;
    private ReorderableList Quirks;

    public List<List<Room>> Schedule;

    int Day;

    public CharacterInfo()
    {
        Name = "";
        MapIcon = null;
        Schedule = new List<List<Room>>();
        ReplacingQuirks = new List<TypingQuirk>();

        for(int i = 0; i <= 7; ++i)
        {
            var hours = new List<Room>();
            for(int j = 0; j < 24; ++j)
            {
                hours.Add(Room.None);
            }

            Schedule.Add(hours);
        }

        Day = 0;

        OrganizeLines();
    }


    public CharacterInfo(JsonData data)
    {
        Name = (string)data["Name"];
        string slug = null;

        if (data["slug"] != null)
        {
            slug = (string)data["slug"];
            MapIcon = Resources.Load<Sprite>("Sprites/" + slug);
        }

        if (data["font"] != null)
        {
            slug = (string)data["font"];
            Font = Resources.Load<TMP_FontAsset>(slug);
        }

        FontSizeMin = (int)data["FontSizeMin"];
        FontSizeMax = (int)data["FontSizeMax"];


        float r = (float)(double)data["r"];
        float g = (float)(double)data["g"];
        float b = (float)(double)data["b"];
        float a = (float)(double)data["a"];

        SpeakerColor = new Color(r, g, b, a);

        if (data["FrontQuirk"] != null)
            StartEndQuirk.NormalText = (string)data["FrontQuirk"];
        if (data["EndQuirk"] != null)
            StartEndQuirk.QuirkedText = (string)data["EndQuirk"];

        ReplacingQuirks = new List<TypingQuirk>();

        if (data["Quirks"] != null)
        {
            for (int i = 0; i < data["Quirks"].Count; ++i)
            {
                TypingQuirk replace;
                replace.NormalText = (string)data["Quirks"][i]["NormalText"];
                replace.QuirkedText = (string)data["Quirks"][i]["QuirkText"];
                
                ReplacingQuirks.Add(replace);
            }

        }

        Schedule = new List<List<Room>>();

        if (data["Schedule"] != null)
        {
            

            for (int i = 0; i <= 7; ++i)
            {
                var hours = new List<Room>();
                for (int j = 0; j < 24; ++j)
                {
                    var lol = (Room)(int)data["Schedule"][i][j];
                    hours.Add(lol);
                }

                Schedule.Add(hours);
            }
            
        }
        else
        {

            for (int i = 0; i <= 7; ++i)
            {
                var hours = new List<Room>();
                for (int j = 0; j < 24; ++j)
                {
                    hours.Add(Room.None);
                }

                Schedule.Add(hours);
            }
        }


        Day = 0;
        OrganizeLines();
    }

    public void Draw()
    {
        Name = EditorGUILayout.TextField("Character Name", Name);

        MapIcon = EditorGUILayout.ObjectField(MapIcon, typeof(Sprite), allowSceneObjects: true) as Sprite;

        SpeakerColor = EditorGUILayout.ColorField("Speaker Color", SpeakerColor);

        Font = EditorGUILayout.ObjectField(Font, typeof(TMP_FontAsset), allowSceneObjects: true) as TMP_FontAsset;
        FontSizeMin = EditorGUILayout.IntSlider("Default Font Size Min", FontSizeMin, 8, 108);
        FontSizeMax = EditorGUILayout.IntSlider("Default Font Size MAx", FontSizeMax, FontSizeMin, 108);

        ShowQuirk = EditorGUILayout.Foldout(ShowQuirk, "Show Text Quirks");

        if(ShowQuirk)
        {
            StartEndQuirk.NormalText = EditorGUILayout.TextField("Insert in front of line:", StartEndQuirk.NormalText);
            StartEndQuirk.QuirkedText = EditorGUILayout.TextField("Insert End of of line:", StartEndQuirk.QuirkedText);

            Quirks.DoLayoutList();
        }

        showSchedule = EditorGUILayout.Foldout(showSchedule, "Show Schedules");

        if(showSchedule)
        {
            EditorGUILayout.LabelField("Day");
            Day = EditorGUILayout.IntSlider(Day, 0, 7);
            
            EditorGUILayout.LabelField("Schedule for day " + Day);

            for (int j = 0; j < 24; ++j)
            {
                string Txt = j + ":00";

                if (j < 12)
                {
                    if (j == 0)
                        Txt = "12:00 AM";
                    else
                        Txt = j + ":00 AM";

                }
                else
                {
                    if (j == 12)
                        Txt = "12:00 PM";
                    else
                        Txt = (j - 12) + ":00 PM";
                }

                Schedule[Day][j] = (Room)EditorGUILayout.EnumPopup(Txt, Schedule[Day][j]);
            }
        }
        
    }


    void OrganizeLines()
    {

        Quirks = new ReorderableList(ReplacingQuirks, typeof(TypingQuirk), true, true, true, true);

        Quirks.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Replacing Text Quirks");
        };

        Quirks.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (TypingQuirk)Quirks.list[index];
        rect.y += 2;

        element.NormalText = EditorGUI.TextField(new Rect(rect.x, rect.y, (rect.width / 2) - 10, EditorGUIUtility.singleLineHeight),
            "Base: ", element.NormalText);
        element.QuirkedText = EditorGUI.TextField(new Rect(rect.x + (rect.width / 2) + 10, rect.y, (rect.width / 2) - 10, EditorGUIUtility.singleLineHeight),
            "Quirk:", element.QuirkedText);

        Quirks.list[index] = element;
    };




        // List.onChangedCallback
    }




}

