using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryDisplay : MonoBehaviour
{

    public Sprite LockedImage;

    public Image[] GalleryPanels; //the individual pictures.

    public Button NextPage;
    public Button BackPage;

    private static GallerySystem Gallery;

    private int index = 0;

	// Use this for initialization
	void Awake ()
    {
        Gallery = new GallerySystem();

        for (int i = 0; i < SaveLoad.GetSize(); ++i)
        {
            var data = SaveLoad.GetSave(i);

            for (int j = 0; j < Gallery.GetSize(); ++j)
            {
                var art = data.Memory.GetImage(j);

                if (art.unlocked)
                    Gallery.UnlockImage(j);
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateDisplay()
    {
        //loop thru the images.
        //assign either the locked image, or the gallery's image, if it is unlocked;
        int j = index;

        foreach(Image frame in GalleryPanels)
        {
            if(j >= Gallery.GetSize())
            {
                frame.sprite = null;
                ++j;
                continue;
            }

            var art = Gallery.GetImage(j);

            if (art.unlocked)
                frame.sprite = art.GetImage(); //returns the sprite needed
            else
                frame.sprite = LockedImage;

            ++j;
        }

        
    }

    public void ExpandPicture(Image picture)
    {
        //makes the big picture via fullscreen
    }

    public void ForwardPage()
    {
        if (index < Gallery.GetSize() - GalleryPanels.Length)
            index += GalleryPanels.Length;

        UpdateDisplay();
    }

    public void BackwardsPage()
    {
        if (index != 0)
            index -= GalleryPanels.Length;

        UpdateDisplay();
    }

}
