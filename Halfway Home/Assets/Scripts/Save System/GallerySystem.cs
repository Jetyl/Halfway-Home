using UnityEngine;
using System.Collections.Generic;
using LitJson;
using System.IO;


[System.Serializable]
public class GallerySystem
{
    public static GallerySystem current { get; set; }

    private static string path;

    [SerializeField]
    List<ImageData> GalleryData;
    
    public GallerySystem()
    {

        GalleryData = new List<ImageData>();

        var jImage = TextParser.ToJson("ImageListing");

        for (int ima = 0; ima < jImage.Count; ++ima)
        {
            GalleryData.Add(new ImageData((string)jImage[ima]["Slug"], false, (string)jImage[ima]["Caption"]));
        }

    }

    public GallerySystem (GallerySystem copy)
    {

        GalleryData = new List<ImageData>();
        

        for (int ima = 0; ima < copy.GalleryData.Count; ++ima)
        {
            GalleryData.Add(new ImageData(copy.GalleryData[ima]));
        }
    }

    public void UnlockImage(Sprite image_)
    {
        for (int i = 0; i < GalleryData.Count; ++i)
        {
            if (GalleryData[i].GetImage() == image_)
            {
                GalleryData[i].unlocked = true;
                
            }
        }

        UnlockdAchievment();
        SaveGallery();
        
    }

    public void UnlockImage(int index)
    {
        if (index < GalleryData.Count)
            GalleryData[index].unlocked = true;
        
    }

    public ImageData GetImage(int index)
    {
        if (index < GalleryData.Count)
            return GalleryData[index];
        else
            return null;
    }


    public int GetSize()
    {
        return GalleryData.Count;
    }

    public static bool LoadGallery()
    {
        if (current != null)
            return true;

        path = Application.persistentDataPath + "/GalleryData.gd";

        if (File.Exists(path))
        {
            // Unity JSON
            string data = File.ReadAllText(path);
            var wrap = JsonUtility.FromJson<GallerySystem>(data);

            current = wrap;
            return true;

        }

        return false;
    }

    public static void SaveGallery()
    {
        if (current == null)
            return;

        path = Application.persistentDataPath + "/GalleryData.gd";

        File.WriteAllText(path, JsonUtility.ToJson(current));



    }


    bool UnlockdAchievment()
    {
        foreach(var image in GalleryData)
        {
            if (!image.unlocked)
                return false;
        }

        //if here, all are unlocked
        //Steamworks.SteamUserStats.SetAchievement("ACH_PHOTO");
        //return Steamworks.SteamUserStats.StoreStats();
        return true;

    }

}


[System.Serializable]
public class ImageData
{
    public bool unlocked;
    //public Sprite image;
    [SerializeField]
    string slug;

    public string Caption;

    public ImageData(string slug_, bool unlocked_, string cap_)
    {
        slug = slug_;
        //image = Resources.Load<Sprite>("Sprites/" + slug);

        unlocked = unlocked_;
        Caption = cap_;
    }

    public ImageData(ImageData copy_)
    {
        slug = copy_.slug;
        unlocked = copy_.unlocked;
    }

    public Sprite GetImage()
    {
        return Resources.Load<Sprite>("Sprites/" + slug);
    }

}