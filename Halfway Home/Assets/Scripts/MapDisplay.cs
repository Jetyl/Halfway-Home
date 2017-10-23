using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HalfwayHome
{
  public class MapDisplay : MonoBehaviour
  {

    public TextAsset DefaultActions;

    //script for handling the map logic of the game.
    List<ConvMap> ChoicesAvalible;


    // Use this for initialization
    void Start()
    {
      Space.Connect<DefaultEvent>(Events.ReturnToMap, TurnMapOn);

      Space.Connect<MapEvent>(Events.MapChoiceConfirmed, MapChoice);

      StartCoroutine(DelayStart());

    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator DelayStart()
    {
      yield return new WaitForSeconds(Time.deltaTime);
      gameObject.SetActive(false);
    }


    void TurnMapOn(DefaultEvent Eventdata)
    {
      gameObject.SetActive(true);
      Game.current.AlterTime();
      ChoicesAvalible = TimelineSystem.Current.GetOptionsAvalible(Game.current.Day, Game.current.Hour);

    }

    void MapChoice(MapEvent eventdata)
    {

      for (int i = 0; i < ChoicesAvalible.Count; ++i)
      {
        if (ChoicesAvalible[i].RoomLocation == eventdata.Destination)
        {

          gameObject.SetActive(false);
          Game.current.SetTimeBlock(eventdata.Length, eventdata.DrainEnergy);
          Space.DispatchEvent(Events.LeaveMap, new DestinationNodeEvent(ChoicesAvalible[i].ID));


          return;
        }
      }

      //if gets here, no scene was there. send a default sorta dealie

      //Game.current.Progress.SetValue("CurrentRoom", eventdata.Destination.ToString());
      Game.current.CurrentRoom = eventdata.Destination.ToString();
      gameObject.SetActive(false);
      Game.current.SetTimeBlock(eventdata.Length, eventdata.DrainEnergy);
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
}