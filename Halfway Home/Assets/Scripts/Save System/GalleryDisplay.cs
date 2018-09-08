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
    public Image FullScreen;
    
    private int index = 0;

    public bool DebugMode;

	// Use this for initialization
	void Awake ()
    {
        FullScreen.gameObject.SetActive(false);
        if(DebugMode && !Debug.isDebugBuild) DebugMode = false;

        if(DebugMode)
        {
            for (int j = 0; j < GallerySystem.current.GetSize(); ++j)
            {
                GallerySystem.current.UnlockImage(j);
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
            if(j >= GallerySystem.current.GetSize())
            {
                frame.sprite = null;
                frame.enabled = false;
                ++j;
                continue;
            }

            var art = GallerySystem.current.GetImage(j);
            frame.enabled = true;

            if (art.unlocked)
            {
              frame.sprite = art.GetImage(); //returns the sprite needed
              frame.gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
              frame.sprite = LockedImage;
              frame.gameObject.GetComponent<Button>().interactable = false;
            }
                
            ++j;
        }

        UpdatePageText();
        CaptionText.text = "";

        if (index == 0) BackPage.interactable = false;
        else BackPage.interactable = true;

        if (index >= GallerySystem.current.GetSize() - GalleryPanels.Length) NextPage.interactable = false;
        else NextPage.interactable = true;
    }

    public void ExpandPicture(Image picture)
    {
        //makes the big picture via fullscreen
        for(int i = 0; i < GallerySystem.current.GetSize(); ++i)
        {

            var art = GallerySystem.current.GetImage(i);

            if (art.GetImage() == picture.sprite)
            {
                if (art.unlocked)
                {
                    FullScreen.sprite = picture.sprite;
                    FullScreen.gameObject.SetActive(true);
                }
                break;
            }

            
        }

        

    }

    public void SetCaptionText(Image frame)
    {

        int j = index;

        foreach (Image isme in GalleryPanels)
        {
            if (j >= GallerySystem.current.GetSize())
            {
                ++j;
                continue;
            }

            if(isme == frame)
            {
                var art = GallerySystem.current.GetImage(j);

                if (art.unlocked)
                    CaptionText.text = art.Caption;
                else
                    CaptionText.text = "Locked";
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
      return Mathf.CeilToInt((float)GallerySystem.current.GetSize() / (float)GalleryPanels.Length);
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
