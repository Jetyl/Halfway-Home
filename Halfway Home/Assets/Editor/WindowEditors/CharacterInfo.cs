using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using LitJson;
using UnityEditor;

public class CharacterInfo
{
    public string Name;

    public Sprite MapIcon;

    public List<List<Room>> Schedule;

    int Day;

    public CharacterInfo()
    {
        Name = "";
        MapIcon = null;
        Schedule = new List<List<Room>>();

        for(int i = 1; i <= 7; ++i)
        {
            var hours = new List<Room>();
            for(int j = 1; j <= 24; ++j)
            {
                hours.Add(Room.None);
            }

            Schedule.Add(hours);
        }

        Day = 0;

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


        Schedule = new List<List<Room>>();

        if (data["Schedule"] != null)
        {
            for (int i = 0; i < 7; ++i)
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

            for (int i = 1; i <= 7; ++i)
            {
                var hours = new List<Room>();
                for (int j = 1; j <= 24; ++j)
                {
                    hours.Add(Room.None);
                }

                Schedule.Add(hours);
            }
        }


        Day = 0;
    }

    public void Draw()
    {
        Name = EditorGUILayout.TextField("Character Name", Name);

        MapIcon = EditorGUILayout.ObjectField(MapIcon, typeof(Sprite), allowSceneObjects: true) as Sprite;
        

        EditorGUILayout.LabelField("Day");
        Day = EditorGUILayout.IntSlider(Day, 0, 7);

        if (Day == 0)
            return;


        EditorGUILayout.LabelField("Schedule for day " + Day);
        int truDay = Day - 1;
        for (int j = 0; j < 24; ++j)
        {
            Schedule[truDay][j] = (Room)EditorGUILayout.EnumPopup("Hour " + (j + 1), Schedule[truDay][j]);
        }



    }



}
