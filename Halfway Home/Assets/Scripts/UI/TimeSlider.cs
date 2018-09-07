/******************************************************************************/
/*!
File:   TimeSlider.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HalfwayHome
{

    public class TimeSlider : MonoBehaviour
    {

        Room Location;

        public Slider TimeSpendingThere;

        public MapDisplay DepressionDialator;
        public TextMeshProUGUI RoomText;
        [Tooltip("replaces {room} with the rooms name.")]
        public string RoomDesciption = "Go to {room}";


        public TextMeshProUGUI TimeText;
        [Tooltip("replaces {time} with the accurate time spent.")]
        public string TimeDescription = "Estimatated Duration: {time}";
        public Color DepressionEffectorColor;

        public TextMeshProUGUI SceneText;
        [Tooltip("replaces {visited_state} with the accurate time spent.")]
        public string UnknownSceneDescription = "An <u>{visited_state}</u> scene is available.";
        public string VisitedSceneDescription = "A <u>{visited_state}</u> scene is available.";
        public string UnknownSceneTag = "Unknown";
        public string VisitedSceneTag = "Visited";
        public Color UnknownSceneColor;
        public Color VisitedSceneColor;

        public int Time;
        string ShowTime;

        [SerializeField]
        public List<RoomStrings> RoomStrings;
        Dictionary<Room, string> SceneTitles;
        Dictionary<Room, string> TimeEstimates;

        bool DrainEnergy;

        // Use this for initialization
        void Start()
        {
            Space.Connect<MapEvent>(Events.MapChoiceMade, GotToRoom);

            Space.Connect<DefaultEvent>(Events.UpdateMap, TurnMapOn);
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateSlider()
        {
            //AssignText((int)TimeSpendingThere.value);
        }
        void TurnMapOn(DefaultEvent Eventdata)
        {
            SceneTitles = new Dictionary<Room, string>();
            TimeEstimates = new Dictionary<Room, string>();

            var scenes = TimelineSystem.Current.GetOptionsAvalible(Game.current.Day, Game.current.Hour);

            foreach (ConvMap scene in scenes) //for every current scene availbe
            {
                if (scene.WatchedScene())
                    SceneTitles.Add(scene.RoomLocation, scene.Title);
                else
                    SceneTitles.Add(scene.RoomLocation, UnknownSceneTag);

                TimeEstimates.Add(scene.RoomLocation, scene.TimeEstimate);
            }

        }

        public void GotToRoom(MapEvent EventData)
        {

            if (Location == EventData.Destination && gameObject.activeSelf)
            {
                Location = Room.None;
                gameObject.SetActive(false);
                return;
            }

            Location = EventData.Destination;
            Time = EventData.Length;
            DrainEnergy = EventData.DrainEnergy;
            AssignText(Location);
            //turn on visiblity
            gameObject.SetActive(true);

        }


        public void ConfirmDestination()
        {
            Space.DispatchEvent(Events.MapChoiceConfirmed, new MapEvent(Location, Time, DrainEnergy));

            gameObject.SetActive(false);
        }


        public void AssignText(Room value)
        {
            string room = "";
            foreach(var des in RoomStrings)
            {
                if(des.location == value)
                {
                    room = des.text;
                    break;
                }
            }

            RoomText.text = RoomDesciption;
            RoomText.text = RoomText.text.Replace("{room}", room);

            string Estimate = "";
            if(TimeEstimates.TryGetValue(value, out Estimate) && Estimate != "")
            {
                if (SceneTitles[value] == UnknownSceneTag)
                    ShowTime = "???";
                else
                    ShowTime = Estimate;
            }
            else
            {
                int multiple = DepressionDialator.TimeDilationMultiple(DrainEnergy);

                ShowTime = "";
                string Hours = " Hours";

                if (multiple > 1)
                    ShowTime += "<#" + ColorUtility.ToHtmlStringRGBA(DepressionEffectorColor) + ">";

                if (Time * multiple == 1)
                    Hours = " Hour";

                ShowTime += (Time * multiple) + Hours;

            }
            
            TimeText.text = TimeDescription;
            TimeText.text = TimeText.text.Replace("{time}", ShowTime);

            string why;
            if(SceneTitles.TryGetValue(value, out why))
            {
                SceneText.gameObject.SetActive(true);

                if(why == UnknownSceneTag)
                {

                    SceneText.text = UnknownSceneDescription;
                    var add = "<#" + ColorUtility.ToHtmlStringRGBA(UnknownSceneColor) + ">";

                    why = add + why + "</color>";
                }
                else
                {
                    SceneText.text = VisitedSceneDescription;
                    var add = "<#" + ColorUtility.ToHtmlStringRGBA(VisitedSceneColor) + ">";

                    why = add + VisitedSceneTag + "</color>";
                }

                SceneText.text = SceneText.text.Replace("{visited_state}", why);
            }
            else
            {
                SceneText.gameObject.SetActive(false);
            }

        }


    }

    [System.Serializable]
    public struct RoomStrings
    {
        public Room location;
        public string text;
    }

}