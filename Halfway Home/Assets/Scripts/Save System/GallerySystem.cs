using UnityEngine;
using System.Collections.Generic;
using LitJson;


[System.Serializable]
public class GallerySystem
{

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
}


[System.Serializable]
public class ImageData
{
    public bool unlocked;
    //public Sprite image;
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