using UnityEngine;
using UnityEditor;
using System.Text;
using LitJson;
using System.IO;
using System.Collections.Generic;
using UnityEditorInternal;
public class ScheduleEditor : EditorWindow
{
    [SerializeField]
    public JsonData BeatData;

    int SelectedBeat;

    Vector2 scroll;

    Vector2 Scroll2;
    
    List<CharacterInfo> Data;
    
    private ReorderableList Characters;

    [MenuItem("Window/Halfway Home/Schedule Editor")]

    public static void ShowWindow()
    {
        GetWindow(typeof(ScheduleEditor));
    }

    public void Awake()
    {

    }

    void OnEnable()
    {
        BeatData = TextParser.ToJson("Characters");
        LoadInfo();
        OrganizeLines();
        SelectedBeat = -1;
    }


    void OnGUI()
    {


        GUILayout.BeginHorizontal();

        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Width(250));



        EditorGUILayout.LabelField("Characters");
        Characters.DoLayoutList();

        EditorGUILayout.EndScrollView();

        Scroll2 = EditorGUILayout.BeginScrollView(Scroll2);

        GUILayout.BeginVertical();

        // display for the currently selected beat
        EditorGUILayout.LabelField("Current Character's Schedule");

        if (SelectedBeat >= 0)
        {

            if (SelectedBeat >= Data.Count)
                SelectedBeat = Data.Count - 1;



            Data[SelectedBeat].Draw();


        }

        GUILayout.EndVertical();


        EditorGUILayout.EndScrollView();

        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        // The actual window code goes here
        if (GUILayout.Button("Save Schedules"))
        {
            SaveItemInfo();

        }


        GUILayout.EndVertical();


    }

    void OrganizeLines()
    {

        Characters = new ReorderableList(Data, typeof(CharacterInfo), true, true, true, true);

        Characters.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Characters");
        };

        Characters.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (CharacterInfo)Characters.list[index];
        rect.y += 2;
        if (GUI.Button(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.Name))
        {
            if (SelectedBeat != index)
            {
                SelectedBeat = index;
            }
            else
                SelectedBeat = -1;
        }
        Characters.list[index] = element;
    };


        

        // List.onChangedCallback
    }


    public void LoadInfo()
    {

        Data = new List<CharacterInfo>();


        for (int i = 0; i < BeatData.Count; ++i)
        {
            Data.Add(new CharacterInfo(BeatData[i]));
        }
    }


    public void SaveItemInfo()
    {
        string path = null;

#if UNITY_EDITOR
        path = "Assets/Resources/Json/Characters.json";
#endif

        StringBuilder sb = new StringBuilder();
        JsonWriter Jwriter = new JsonWriter(sb);
        Jwriter.PrettyPrint = true;
        Jwriter.IndentValue = 1;

        //JsonMapper.ToJson(BeatData, Jwriter);

        Jwriter.WriteArrayStart();

        foreach (var beat in Data)
        {
            Jwriter.WriteObjectStart();
            Jwriter.WritePropertyName("Name");
            Jwriter.Write(beat.Name);

            Jwriter.WritePropertyName("slug");

            if (beat.MapIcon != null)
            {
                string txt = AssetDatabase.GetAssetPath(beat.MapIcon);
                txt = txt.Replace("Assets/Resources/Sprites/", "");
                //removes the file extention off the string
                txt = txt.Remove(txt.Length - 4);
                Jwriter.Write(txt);
            }
            else
            {
                Jwriter.Write(null);
            }

            Jwriter.WritePropertyName("Schedule");
            Jwriter.WriteArrayStart();
            for (int i = 0; i <= 7; ++i)
            {
                Jwriter.WriteArrayStart();
                for (int j = 0; j < 24; ++j)
                {
                    Jwriter.Write((int)beat.Schedule[i][j]);
                }


                Jwriter.WriteArrayEnd();
            }

            Jwriter.WriteArrayEnd();

            Jwriter.WriteObjectEnd();
        }

        Jwriter.WriteArrayEnd();




        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(sb);
            }
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    JsonData ExpandArray(JsonData array, int size)
    {

        JsonData json = new JsonData();
        json.SetJsonType(JsonType.Array);

        for (int i = 0; i < size; ++i)
        {
            if (i < array.Count)
            {
                json.Add((string)array[i]);
            }
            else
                json.Add("");

        }

        return json;
    }

}

