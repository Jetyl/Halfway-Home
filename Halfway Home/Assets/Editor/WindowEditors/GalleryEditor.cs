using UnityEngine;
using UnityEditor;
using System.Text;
using LitJson;
using System.IO;
using System.Collections.Generic;
using UnityEditorInternal;
public class GalleryEditor : EditorWindow
{
    [SerializeField]
    public JsonData AppData;

    List<EditableImage> ImageList;

    int SelectedImage;

    Vector2 scroll;

    Vector2 Scroll2;

    private ReorderableList List;

    [MenuItem("Window/Halfway Home/Gallery Editor")]

    public static void ShowWindow()
    {
        GetWindow(typeof(GalleryEditor));
    }

    public void Awake()
    {

    }

    void OnEnable()
    {
        AppData = TextParser.ToJson("ImageListing");

        ImageList = new List<EditableImage>();

        for (int i = 0; i < AppData.Count; ++i)
        {

            ImageList.Add(new EditableImage(AppData[i]));

        }

        SelectedImage = -1;

        OrganizeLines();
    }


    void OnGUI()
    {


        GUILayout.BeginHorizontal();

        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Width(200));


        List.DoLayoutList();
        /*
        //expands the json data with another dream slot
        if (GUILayout.Button("Add New Image"))
        {
            string defaultIdea = JsonMapper.ToJson(AppData[0]);

            string json = JsonMapper.ToJson(AppData);
            json.Insert(json.Length - 2, defaultIdea);
            AppData = JsonMapper.ToObject(json.Insert(json.Length - 1, "," + defaultIdea));

        }

        //adding all current dreams to the side list
        for (int i = 0; i < AppData.Count; ++i)
        {


            if (GUILayout.Button((string)AppData[i]["Slug"]))
            {
                if (SelectedImage != i)
                    SelectedImage = i;
                else
                    SelectedImage = -1;
            }
        }
        */

        EditorGUILayout.EndScrollView();

        Scroll2 = EditorGUILayout.BeginScrollView(Scroll2);

        GUILayout.BeginVertical();

        // display for the currently selected dream
        EditorGUILayout.LabelField("Current Image");

        if (SelectedImage >= 0 && ImageList.Count > SelectedImage)
        {

            ImageList[SelectedImage].Draw();

            /*
            Sprite image = EditorGUILayout.ObjectField(Resources.Load("Sprites/" + (string)AppData[SelectedImage]["Slug"]), typeof(Sprite), allowSceneObjects: true) as Sprite;
            if(image)
            {
                string txt = AssetDatabase.GetAssetPath(image);
                txt = txt.Replace("Assets/Resources/Sprites/", "");
                //removes the file extention off the string
                txt = txt.Remove(txt.Length - 4);

                if (txt != (string)AppData[SelectedImage]["Slug"])
                    AppData[SelectedImage]["Slug"] = txt;
            }

            AppData[SelectedImage]["Set"] = EditorGUILayout.IntField("Set Number", (int)AppData[SelectedImage]["Set"]);

    */

        }

        GUILayout.EndVertical();


        EditorGUILayout.EndScrollView();

        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        // The actual window code goes here
        if (GUILayout.Button("Save Gallery List"))
        {
            SaveItemInfo();

        }


        GUILayout.EndVertical();


    }


    void OrganizeLines()
    {



        List = new ReorderableList(ImageList, typeof(EditableImage), true, true, true, true);

        List.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Gallery List");
        };

        List.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = (EditableImage)List.list[index];
        rect.y += 2;
        if (GUI.Button(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.Name))
        {
            if (SelectedImage != index)
            {
                SelectedImage = index;
            }
            else
                SelectedImage = -1;
        }
        List.list[index] = element;
    };


        // List.onChangedCallback
    }



    public void SaveItemInfo()
    {
        string path = null;

#if UNITY_EDITOR
        path = "Assets/Resources/Json/ImageListing.json";
#endif

        StringBuilder sb = new StringBuilder();
        JsonWriter Jwriter = new JsonWriter(sb);
        Jwriter.PrettyPrint = true;
        Jwriter.IndentValue = 1;


        Jwriter.WriteArrayStart();

        foreach (var image in ImageList)
        {
            Jwriter.WriteObjectStart();

            Jwriter.WritePropertyName("Name");
            Jwriter.Write(image.Name);

            Jwriter.WritePropertyName("Slug");
            if (image.Image != null)
            {
                string txt = AssetDatabase.GetAssetPath(image.Image);
                txt = txt.Replace("Assets/Resources/Sprites/", "");
                //removes the file extention off the string
                txt = txt.Remove(txt.Length - 4);
                Jwriter.Write(txt);
            }
            else
            {
                Jwriter.Write(null);
            }


            Jwriter.WritePropertyName("Set");
            Jwriter.Write(image.Set);

            Jwriter.WriteObjectEnd();
        }

        Jwriter.WriteArrayEnd();


        //JsonMapper.ToJson(AppData, Jwriter);

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
