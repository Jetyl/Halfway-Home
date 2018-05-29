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
            GalleryData.Add(new ImageData((string)jImage[ima]["Slug"], false));
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

    public ImageData(string slug_, bool unlocked_)
    {
        slug = slug_;
        //image = Resources.Load<Sprite>("Sprites/" + slug);

        unlocked = unlocked_;
    }

    public Sprite GetImage()
    {
        return Resources.Load<Sprite>("Sprites/" + slug);
    }

}