/******************************************************************************/
/*!
File:   MapAccessTime.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HalfwayHome
{

  public class MapAccessTime : MonoBehaviour
  {

    public List<AccessLocker> ClosedTimeContainer;
    public List<List<bool>> TimeClosed;

    public bool LimitedDailyAccess;

    public string AccessPoint;

    public int VisitMulitplier;

    public string HourVisited;
    public string DayVisited;

    public string ManualAccess;

    public int FatigueCloseLimit = 100;
    public int StressCloseLimit = 100;
    public int DelusionCloseLimit = 100;

    Button self;

    // Use this for initialization
    void Start()
    {

      TimeClosed = new List<List<bool>>();

      for (int i = 0; i <= 7; ++i)
      {
        var hours = new List<bool>();
        for (int j = 0; j < 24; ++j)
        {
          hours.Add(false);
        }

        TimeClosed.Add(hours);
      }

      foreach (var point in ClosedTimeContainer)
      {
        for (int i = point.starttime; i <= point.endTime; ++i)
        {
          TimeClosed[point.Day][i] = true;
        }
      }

      self = GetComponent<Button>();
      Space.Connect<DefaultEvent>(Events.TimeChange, CheckAccess);
      Space.Connect<MapEvent>(Events.MapChoiceConfirmed, MapChoice);

    }

    // Update is called once per frame
    void Update()
    {
            
    }
        
    void CheckAccess(DefaultEvent Eventdata)
    {
      self.interactable = true;
      if (Game.current.Progress.GetBoolValue(ManualAccess) == true)
      {
        self.interactable = false;
      }
      if (LimitedDailyAccess)
      {
        int hour = Game.current.Progress.GetIntValue(HourVisited);
        int day = Game.current.Progress.GetIntValue(DayVisited);
        int length = Game.current.Progress.GetIntValue(AccessPoint) * VisitMulitplier;
        //if last time visited, plus times visited (times multiplier) is greater that current time
        if (Game.current.WithinTimeDifference(hour, day, length))
        {
          self.interactable = false;
          return;
        }
      }

      if (Game.current.Self.GetWellbingStat(Personality.Wellbeing.fatigue) > FatigueCloseLimit)
        self.interactable = false;

      if (Game.current.Self.GetWellbingStat(Personality.Wellbeing.stress) > StressCloseLimit)
        self.interactable = false;

      if (Game.current.Self.GetWellbingStat(Personality.Wellbeing.delusion) > DelusionCloseLimit)
        self.interactable = false;

      foreach (var point in ClosedTimeContainer)
      {
        if (point.IsClosed(Game.current.Day, Game.current.Hour))
        {
          self.interactable = false;
        }

      }

    }


    void MapChoice(MapEvent eventdata)
    {
      if (LimitedDailyAccess)
      {
        if (eventdata.Destination == gameObject.GetComponent<MapButton>().Location)
        {
          Game.current.Progress.SetValue<int>(HourVisited, Game.current.Hour);
          Game.current.Progress.SetValue<int>(DayVisited, Game.current.Day);
          Game.current.Progress.SetValue<int>(AccessPoint, Game.current.Progress.GetIntValue(AccessPoint) + 1);
        }
      }
    }



  }

  [System.Serializable]
  public class AccessLocker
  {
    public int Day;
    public int starttime;
    public int endTime;
    public bool ProgressLocked;
    public string ProgressKey = "";

    public bool IsClosed(int day, int hour)
    {
            
      if (ProgressLocked)
      {
        if (!Game.current.Progress.GetBoolValue(ProgressKey))
          return false;
      }

      if (Day != day)
        return false;

      for (int i = starttime; i <= endTime; ++i)
      {
        if (i == hour)
          return true;
      }

      return false;
    }

  } 
}