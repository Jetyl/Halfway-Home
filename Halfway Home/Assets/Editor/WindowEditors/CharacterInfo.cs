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
    }

    public void Draw()
    {
        Name = EditorGUILayout.TextField("Character Name", Name);

        MapIcon = EditorGUILayout.ObjectField(MapIcon, typeof(Sprite), allowSceneObjects: true) as Sprite;
        

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
                Txt = (j - 12) + ":00 PM";

            Schedule[Day][j] = (Room)EditorGUILayout.EnumPopup(Txt, Schedule[Day][j]);
        }



    }



}
