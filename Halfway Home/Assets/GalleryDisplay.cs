using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryDisplay : MonoBehaviour
{

    public Sprite LockedImage;

    public GameObject GalleyObject; //the individual pictures. a prefab to instantiate

    private static GallerySystem Gallery;

	// Use this for initialization
	void Start ()
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

    void UpdateDisplay()
    {
        //loop thru the images.
        //assign either the locked image, or the gallery's image, if it is unlocked;

        for (int j = 0; j < Gallery.GetSize(); ++j)
        {
            var art = Gallery.GetImage(j);

            if (art.unlocked)
                art.GetImage(); //returns the sprite needed
            //else
                //LockedImage;
        }
    }


    public void dummy(Image test)
  {

  }
}
