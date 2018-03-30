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
using UnityEngine.UI;
using Stratus;

namespace HalfwayHome
{
  public class MapDisplay : MonoBehaviour
  {

        public TransitionTypes MapTransitions;
        public bool AllowTimeDilation = true;
        public List<DepressionTimeMultiplier> TimeDilation;
        public TextAsset DefaultActions;
        public Image Background;
        public GameObject MapRooms;
        public GameObject SocialStats;
        public float MapTransitionDuration = 1.0f;
        //script for handling the map logic of the game.
        List<ConvMap> ChoicesAvalible;

        bool LoadedToMap = false;

        Image Body;
        float backgroundAlpha;

        Vector3 MapOffset;
        Vector3 StatsOffset;

        bool Active;

        // Use this for initialization
        void Start()
        {

            Body = GetComponent<Image>();
            backgroundAlpha = Background.color.a;


            StatsOffset = SocialStats.transform.localPosition;
            StatsOffset.x -= SocialStats.GetComponent<RectTransform>().rect.width;
            
            MapOffset = MapRooms.transform.localPosition;
            MapOffset.x += MapRooms.GetComponent<RectTransform>().rect.width;

            //Space.Connect<DefaultEvent>(Events.ReturnToMap, TurnMapOn);
            Space.Connect<DefaultEvent>(Events.ReturnToMap, ClockMapDelay);

            Space.Connect<DefaultEvent>(Events.ClockFinished, TurnMapOn);

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
            {
                Color aBody = Body.color;
                aBody.a = 0;
                Body.color = aBody;

                Color aBack = Background.color;
                aBack.a = 0;
                Background.color = aBack;

                //default the map itself, and stats, off screen
                SocialStats.transform.localPosition = StatsOffset;
                MapRooms.transform.localPosition = MapOffset;
                //til then, this line.
                //gameObject.SetActive(false);
            }

            Game.current.Progress.SetValue("Depression Time Dilation", AllowTimeDilation);

        }

    
        void ClockMapDelay(DefaultEvent eventdata)
        {
            Active = true;
            Game.current.AlterTime();

            Color aBody = Body.color;
            aBody.a = 1;
            gameObject.DispatchEvent(Events.Fade, new FadeEvent(aBody));

            Color aBack = Background.color;
            aBack.a = backgroundAlpha;
            Background.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aBack));
        }

        void TurnMapOn(DefaultEvent Eventdata)
        {

            if (!Active)
                return;

            Active = false;
            //print("here");
            //Game.current.AlterTime();

            //psudo code time!
            //grab the transitions script, and put it on the map object.
            //call it here, to pull the map and stats onto the screen.
            MapRooms.DispatchEvent(Events.Translate, new TransformEvent(Vector3.zero, MapTransitionDuration));
            SocialStats.DispatchEvent(Events.Translate, new TransformEvent(Vector3.zero, MapTransitionDuration));

            //gameObject.SetActive(true);
            ChoicesAvalible = TimelineSystem.Current.GetOptionsAvalible(Game.current.Day, Game.current.Hour);
            AllowTimeDilation = Game.current.Progress.GetBoolValue("Depression Time Dilation");

            StartCoroutine(TextParser.FrameDelay(Events.UpdateMap));

        }

        void TurnMapOff()
        {
            Color aBody = Body.color;
            aBody.a = 0;
            gameObject.DispatchEvent(Events.Fade, new FadeEvent(aBody));

            Color aBack = Background.color;
            aBack.a = 0;
            Background.gameObject.DispatchEvent(Events.Fade, new FadeEvent(aBack));

            MapRooms.DispatchEvent(Events.Translate, new TransformEvent(MapOffset, MapTransitionDuration));
            SocialStats.DispatchEvent(Events.Translate, new TransformEvent(StatsOffset, MapTransitionDuration));
            var delaySeq = Actions.Sequence(this);
            Actions.Call(delaySeq, ()=>Space.DispatchEvent(Events.MapTransitionOutCompleted, new DefaultEvent()), MapTransitionDuration*2);
            //grab the transitions script, and put it on the map object.
            //call it here, to pull the map and stats off the screen.
        }

        void MapChoice(MapEvent eventdata)
        {

            Space.DispatchEvent(Events.Backdrop, new StageDirectionEvent(eventdata.Destination, "", MapTransitions));

            for (int i = 0; i < ChoicesAvalible.Count; ++i)
            {
                if (ChoicesAvalible[i].RoomLocation == eventdata.Destination)
                {
                    Game.current.CurrentRoom = eventdata.Destination;
                    ChoicesAvalible[i].SetSceneTime();
                    TurnMapOff();
                    //gameObject.SetActive(false);
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
                    if (multiple.DepressionValue <= Game.current.Self.GetWellbingStat(Personality.Wellbeing.Depression))
                        multiplier = multiple.Multiplier;
                }
            }
            
            //Game.current.Progress.SetValue("CurrentRoom", eventdata.Destination.ToString());
            Game.current.CurrentRoom = eventdata.Destination;
            //gameObject.SetActive(false);
            TurnMapOff();
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