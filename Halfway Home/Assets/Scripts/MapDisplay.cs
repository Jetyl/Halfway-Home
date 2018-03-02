/******************************************************************************/
/*!
File:   MapDisplay.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HalfwayHome
{
  public class MapDisplay : MonoBehaviour
  {

        public TransitionTypes MapTransitions;
        public bool AllowTimeDilation = true;
        public List<DepressionTimeMultiplier> TimeDilation;
        public TextAsset DefaultActions;

        //script for handling the map logic of the game.
        List<ConvMap> ChoicesAvalible;

        bool LoadedToMap = false;

        // Use this for initialization
        void Start()
        {
            Space.Connect<DefaultEvent>(Events.ReturnToMap, TurnMapOn);

            Space.Connect<MapEvent>(Events.MapChoiceConfirmed, MapChoice);

            Space.Connect<DefaultEvent>(Events.Load, OnLoad);

            StartCoroutine(DelayStart());

        }
       

        void OnLoad(DefaultEvent eventdata)
        {

            if (!Game.current.InCurrentStory)
            {
                LoadedToMap = true;
            }
        }

        IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (!LoadedToMap)
                gameObject.SetActive(false);

            Game.current.Progress.SetValue("Depression Time Dilation", AllowTimeDilation);

        }


    void TurnMapOn(DefaultEvent Eventdata)
    {
            //print("here");
            Game.current.AlterTime();
            gameObject.SetActive(true);
            ChoicesAvalible = TimelineSystem.Current.GetOptionsAvalible(Game.current.Day, Game.current.Hour);
            AllowTimeDilation = Game.current.Progress.GetBoolValue("Depression Time Dilation");

            StartCoroutine(TextParser.FrameDelay(Events.UpdateMap));

    }

    void MapChoice(MapEvent eventdata)
    {

      Space.DispatchEvent(Events.Backdrop, new StageDirectionEvent(eventdata.Destination, "", MapTransitions));

          for (int i = 0; i < ChoicesAvalible.Count; ++i)
          {
            if (ChoicesAvalible[i].RoomLocation == eventdata.Destination)
            {
                    Game.current.CurrentRoom = eventdata.Destination;
                    gameObject.SetActive(false);
                    Game.current.SetTimeBlock(eventdata.Length, eventdata.DrainEnergy);
                    Space.DispatchEvent(Events.LeaveMap, new DestinationNodeEvent(ChoicesAvalible[i].ID));


                    return;
            }
          }

            //if gets here, no scene was there. send a default sorta dealie
            int multiplier = 1;

            if(AllowTimeDilation && eventdata.DrainEnergy)
            {
                foreach (var multiple in TimeDilation)
                {
                    if (multiple.DepressionValue <= Game.current.Self.GetWellbingStat(Personality.Wellbeing.delusion))
                        multiplier = multiple.Multiplier;
                }
            }
            
            //Game.current.Progress.SetValue("CurrentRoom", eventdata.Destination.ToString());
            Game.current.CurrentRoom = eventdata.Destination;
            gameObject.SetActive(false);
            Game.current.SetTimeBlock(eventdata.Length * multiplier, eventdata.DrainEnergy);
            Space.DispatchEvent(Events.NewStory, new StoryEvent(DefaultActions));

        }



  }


  public class MapEvent : DefaultEvent
  {

    public Room Destination;
    public int Length;
    public bool DrainEnergy;

    public MapEvent(Room spot, int timeThere, bool fatigue)
    {
      Destination = spot;
      Length = timeThere;
      DrainEnergy = fatigue;
    }

  }


    [Serializable]
    public struct DepressionTimeMultiplier
    {
        [Range(1, 100)]
        public int DepressionValue;
        public int Multiplier;
    }
}