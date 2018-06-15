using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryDisplay : MonoBehaviour
{

    public Sprite LockedImage;

    public Image[] GalleryPanels; //the individual pictures.
    
    public TextMeshProUGUI CaptionText;
    public TextMeshProUGUI PageText;
    public Button NextPage;
    public Button BackPage;

    private static GallerySystem Gallery;

    private int index = 0;

    public bool DebugMode;

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

        if(DebugMode)
        {
            for (int j = 0; j < Gallery.GetSize(); ++j)
            {
                Gallery.UnlockImage(j);
            }
        }

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
                frame.enabled = false;
                ++j;
                continue;
            }

            var art = Gallery.GetImage(j);
            frame.enabled = true;

            if (art.unlocked)
            {
              frame.sprite = art.GetImage(); //returns the sprite needed
              frame.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
              frame.sprite = LockedImage;
              frame.gameObject.GetComponent<Button>().interactable = false;
            }
                
            ++j;
        }

        UpdatePageText();

        if (index == 0) BackPage.interactable = false;
        else BackPage.interactable = true;

        if (index >= Gallery.GetSize() - GalleryPanels.Length) NextPage.interactable = false;
        else NextPage.interactable = true;
    }

    public void ExpandPicture(Image picture)
    {
        //makes the big picture via fullscreen
    }

    public void SetCaptionText(Image frame)
    {

        int j = index;

        foreach (Image isme in GalleryPanels)
        {
            if (j >= Gallery.GetSize())
            {
                ++j;
                continue;
            }

            if(isme == frame)
            {
                var art = Gallery.GetImage(j);

                if (art.unlocked)
                    CaptionText.text = art.Caption;
                else
                    CaptionText.text = "???";
                break;
            }
            

            ++j;
        }


    }

    public void UpdatePageText()
    {
      PageText.text = $"{GetCurrentPage()}/{GetPageCount()}";
    }

    public int GetPageCount()
    {
      return Mathf.CeilToInt((float)Gallery.GetSize() / (float)GalleryPanels.Length);
    }

    public int GetCurrentPage()
    {
      return Mathf.FloorToInt(index / GalleryPanels.Length) + 1;
    }

    public void ForwardPage()
    {
        index += GalleryPanels.Length;
        UpdateDisplay();
    }

    public void BackwardsPage()
    {
        index -= GalleryPanels.Length;
        UpdateDisplay();
    }

}
