using UnityEngine;
using UnityEditor;
using System.Text;
using LitJson;
using System.IO;
using System.Collections.Generic;
using UnityEditorInternal;

public class ObjectiveEditor : EditorWindow
{
    [SerializeField]
    public JsonData AppData;

    int SelectedTask;

    Vector2 scroll;

    Vector2 Scroll2;


    List<Task> TiskTask;

    private ReorderableList List;


    private ReorderableList SubList;

    bool ShowSub;

    [MenuItem("Window/Halfway Home/Objective Editor")]

    public static void ShowWindow()
    {
        GetWindow(typeof(ObjectiveEditor));
    }

    public void Awake()
    {

    }

    void OnEnable()
    {
        AppData = TextParser.ToJson("TaskListing");
        LoadInfo();
        OrganizeLines();
        SelectedTask = -1;
    }


    void OnGUI()
    {


        GUILayout.BeginHorizontal();

        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Width(300));

        List.DoLayoutList();


        EditorGUILayout.EndScrollView();

        Scroll2 = EditorGUILayout.BeginScrollView(Scroll2);

        GUILayout.BeginVertical();

        // display for the currently selected dream
        EditorGUILayout.LabelField("Current Objective");

        if (SelectedTask >= 0)
        {

            if (SelectedTask >= TiskTask.Count)
                SelectedTask = TiskTask.Count - 1;


            TiskTask[SelectedTask].Name = EditorGUILayout.TextField("Objective", TiskTask[SelectedTask].Name);

            //TiskTask[SelectedTask].Number = EditorGUILayout.IntField("Task Number", TiskTask[SelectedTask].Number);

            //TiskTask[SelectedTask].Objective = EditorGUILayout.TextField("Objective", TiskTask[SelectedTask].Objective);

            //TiskTask[SelectedTask].Set = EditorGUILayout.IntField("Set Number", TiskTask[SelectedTask].Set);

            //TiskTask[SelectedTask].ConnectedBeat = EditorGUILayout.TextField("Beat Connected To:", TiskTask[SelectedTask].ConnectedBeat);
            
            TiskTask[SelectedTask].Hidden = EditorGUILayout.Toggle("Hidden Objective", TiskTask[SelectedTask].Hidden);

            TiskTask[SelectedTask].RemoveWeekly = EditorGUILayout.Toggle("Remove on Week Reset?", TiskTask[SelectedTask].RemoveWeekly);

            ShowSub = EditorGUILayout.Foldout(ShowSub, "Show Sub-Goals");

            if(ShowSub)
            {
                SubList.DoLayoutList();

                TiskTask[SelectedTask].AllShow = EditorGUILayout.ToggleLeft("Show all subtasks when main task is activated?", TiskTask[SelectedTask].AllShow);
                TiskTask[SelectedTask].AllSuccess = EditorGUILayout.ToggleLeft("Main task set to complete when all subtasks are complete?", TiskTask[SelectedTask].AllSuccess);
                TiskTask[SelectedTask].AllFail = EditorGUILayout.ToggleLeft("Main task set to fail when any subtask is failed?", TiskTask[SelectedTask].AllFail);
            }


        }

        GUILayout.EndVertical();


        EditorGUILayout.EndScrollView();

        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        // The actual window code goes here
        if (GUILayout.Button("Save Objective List"))
        {
            SaveItemInfo();

        }


        GUILayout.EndVertical();


    }


    void OrganizeLines()
    {


        List = new ReorderableList(TiskTask, typeof(Task), true, true, true, true);

        List.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Task List");
        };

        List.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (Task)List.list[index];
        rect.y += 2;
        if (GUI.Button(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
            "#" + index + ": " + element.Name))
        {
            if (SelectedTask != index)
            {
                SelectedTask = index;
                OrganizeSublist(element.SubTasks);
            }
            else
                SelectedTask = -1;
        }
        element.Number = index;
        List.list[index] = element;
    };

        List.onAddCallback = (ReorderableList l) => {
            List.list.Add(new Task(FindNextNumber()));
        };
        // List.onChangedCallback
    }


    public int FindNextNumber()
    {
        int num = 0;


        for (int i = 0; i < TiskTask.Count; ++i)
        {
            if (num == TiskTask[i].Number)
            {
                num += 1;
                i = -1;
            }
        }


        return num;

    }

    void OrganizeSublist(List<SubTask> list)
    {
        
        SubList = new ReorderableList(list, typeof(SubTask), true, true, true, true);

        SubList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Sub Goals");
        };

        SubList.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (SubTask)SubList.list[index];
        rect.y += 2;
        element.Number = index;
        element.Name = GUI.TextField(new Rect(rect.x, rect.y, rect.width/2, EditorGUIUtility.singleLineHeight), element.Name);
        element.Hidden = GUI.Toggle(new Rect(rect.x + rect.width / 2, rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight), element.Hidden, "Hidden");
        GUI.Label(new Rect(rect.x + rect.width * 0.75f, rect.y, rect.width * 0.25f, EditorGUIUtility.singleLineHeight), "Sub-Task #" + index);
        SubList.list[index] = element;
    };

        SubList.onAddCallback = (ReorderableList l) => {
            SubList.list.Add(new SubTask(TiskTask[SelectedTask].SubTasks.Count));
        };

        // List.onChangedCallback
    }

    public void LoadInfo()
    {

        TiskTask = new List<Task>();


        for (int i = 0; i < AppData.Count; ++i)
        {
            TiskTask.Add(new Task(AppData[i]));

        }
    }


    public void SaveItemInfo()
    {
        string path = null;

#if UNITY_EDITOR
        path = "Assets/Resources/Json/TaskListing.json";
#endif

        StringBuilder sb = new StringBuilder();
        JsonWriter Jwriter = new JsonWriter(sb);
        Jwriter.PrettyPrint = true;
        Jwriter.IndentValue = 1;

        //JsonMapper.ToJson(AppData, Jwriter);

        Jwriter.WriteArrayStart();

        foreach (var task in TiskTask)
        {
            Jwriter.WriteObjectStart();
            Jwriter.WritePropertyName("Number");
            Jwriter.Write(task.Number);
            Jwriter.WritePropertyName("Name");
            Jwriter.Write(task.Name);
            //Jwriter.WritePropertyName("Objective");
            //Jwriter.Write(task.Objective);
            Jwriter.WritePropertyName("Hidden");
            Jwriter.Write(task.Hidden);

            Jwriter.WritePropertyName("Weekly");
            Jwriter.Write(task.RemoveWeekly);


            Jwriter.WritePropertyName("ShowAll");
            Jwriter.Write(task.AllShow);
            Jwriter.WritePropertyName("GoalAll");
            Jwriter.Write(task.AllSuccess);
            Jwriter.WritePropertyName("FailAll");
            Jwriter.Write(task.AllFail);

            Jwriter.WritePropertyName("SubCount");
            Jwriter.Write(task.SubTasks.Count);

            Jwriter.WritePropertyName("SubTasks");
            Jwriter.WriteArrayStart();
            foreach(SubTask sub in task.SubTasks)
            {
                Jwriter.WriteObjectStart();
                Jwriter.WritePropertyName("Number");
                Jwriter.Write(sub.Number);
                Jwriter.WritePropertyName("Name");
                Jwriter.Write(sub.Name);
                Jwriter.WritePropertyName("Hidden");
                Jwriter.Write(sub.Hidden);
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

