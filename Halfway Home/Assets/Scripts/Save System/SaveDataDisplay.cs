using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveDataDisplay : MonoBehaviour
{

    public int DataIndex;

    Text description;


	// Use this for initialization
	void Start ()
    {

        description = gameObject.transform.Find("Description").gameObject.GetComponent<Text>();

        UpdateDisplay();

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    public void UpdateDisplay()
    {

        Game gameData = SaveLoad.GetSave(DataIndex);


        if (gameData != null)
        {

            description.text = "";

            if (gameData.PlayerName != "")
                description.text = gameData.PlayerName + " - ";

            

            description.text += "Day " + (gameData.Day + 1) + " - ";

            if (gameData.Room == SceneList.Apartment)
            {
                description.text += "Your Apartment";
            }

        }

        else
        {
            description.text = "No SavedData";

        }


    }
}
