using UnityEngine;
using UnityEditor;
using System.Text;
using LitJson;
using System.IO;
using System.Collections.Generic;
using UnityEditorInternal;
public class PlotBeatEditor : EditorWindow
{
    [SerializeField]
    public JsonData BeatData;
    
    int SelectedBeat;

    Vector2 scroll;

    Vector2 Scroll2;

    List<ProgressPoint> Track;
    List<Beat> Plots;

    private ReorderableList List;
    private ReorderableList PlotList;

    [MenuItem("Window/a-0/PlotBeatEditor")]

    public static void ShowWindow()
    {
        GetWindow(typeof(PlotBeatEditor));
    }

    public void Awake()
    {

    }

    void OnEnable()
    {
        BeatData = TextParser.ToJson("PlotBeats");
        LoadInfo();
        OrganizeLines();
        SelectedBeat = -1;
    }
    

    void OnGUI()
    {


        GUILayout.BeginHorizontal();

        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Width(200));
        
        PlotList.DoLayoutList();

        EditorGUILayout.EndScrollView();

        Scroll2 = EditorGUILayout.BeginScrollView(Scroll2);

        GUILayout.BeginVertical();

        // display for the currently selected beat
        EditorGUILayout.LabelField("Current Beat");

        if (SelectedBeat >= 0)
        {

            if (SelectedBeat >= Plots.Count)
                SelectedBeat = Plots.Count - 1;


            Plots[SelectedBeat].PlotName = EditorGUILayout.TextField("Plot Name", Plots[SelectedBeat].PlotName);

            //make sure no number's overlap

            Plots[SelectedBeat].BeatNumber = EditorGUILayout.IntField("Beat Number", Plots[SelectedBeat].BeatNumber);

            Plots[SelectedBeat].BeatName = EditorGUILayout.TextField("Beat Name", Plots[SelectedBeat].BeatName);
            
            List.DoLayoutList();
            

        }

        GUILayout.EndVertical();


        EditorGUILayout.EndScrollView();

        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        // The actual window code goes here
        if (GUILayout.Button("Save Plot Beats"))
        {
            SaveItemInfo();
            
        }
        

        GUILayout.EndVertical();
        

    }

    void OrganizeLines()
    {

        PlotList = new ReorderableList(Plots, typeof(Beat), true, true, true, true);

        PlotList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Plot Beats");
        };

        PlotList.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (Beat)PlotList.list[index];
        rect.y += 2;
        if (GUI.Button(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.BeatName))
        {
            if (SelectedBeat != index)
            {
                Track = element.Points;
                OrganizeLines();
                SelectedBeat = index;
            }
            else
                SelectedBeat = -1;
        }
        PlotList.list[index] = element;
    };



        List = new ReorderableList(Track, typeof(ProgressPoint), true, true, true, true);

        List.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Tracking Points");
        };

        List.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (ProgressPoint)List.list[index];
        rect.y += 2;
        element.TypeID = (PointTypes)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight),
            element.TypeID);
        element.ProgressName = EditorGUI.TextField(new Rect(rect.x + 50, rect.y, rect.width - 50, EditorGUIUtility.singleLineHeight),
            element.ProgressName);
        List.list[index] = element;
    };


        // List.onChangedCallback
    }


    public void LoadInfo()
    {

        Plots = new List<Beat>();
        Track = new List<ProgressPoint>();


        for (int i = 0; i < BeatData.Count; ++i)
        {
            Plots.Add(new Beat(BeatData[i]));
            
        }
    }


    public void SaveItemInfo()
    {
        string path = null;

        #if UNITY_EDITOR
        path = "Assets/Resources/Json/PlotBeats.json";
#endif

        StringBuilder sb = new StringBuilder();
        JsonWriter Jwriter = new JsonWriter(sb);
        Jwriter.PrettyPrint = true;
        Jwriter.IndentValue = 1;

        //JsonMapper.ToJson(BeatData, Jwriter);

        Jwriter.WriteArrayStart();

        foreach (var beat in Plots)
        {
            Jwriter.WriteObjectStart();
            Jwriter.WritePropertyName("PlotName");
            Jwriter.Write(beat.PlotName);
            Jwriter.WritePropertyName("BeatNumber");
            Jwriter.Write(beat.BeatNumber);
            Jwriter.WritePropertyName("BeatName");
            Jwriter.Write(beat.BeatName);


            Jwriter.WritePropertyName("Point");
            Jwriter.WriteArrayStart();
            for (int i = 0; i < beat.Points.Count; ++i)
            {
                Jwriter.WriteObjectStart();

                Jwriter.WritePropertyName("Name");
                Jwriter.Write(beat.Points[i].ProgressName);
                Jwriter.WritePropertyName("Type");
                Jwriter.Write((int)beat.Points[i].TypeID);

                Jwriter.WriteObjectEnd();
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
