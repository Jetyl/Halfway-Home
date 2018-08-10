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
    public string DailyAccessTooltip = "Come back at #time, to do a thing";

    public string ManualAccess;
    public string DynamicTooltip;

    public int FatigueCloseLimit = 100;
    public string FatigueCloseTooltip = "I'm too tired for this";
    public int StressCloseLimit = 100;
    public string StressCloseTooltip = "I'm too stressed for this";
    public int DepressionCloseLimit = 100;
    public string DepressionCloseTooltip = "I'm too tired for this";

        public List<ModularLocker> Modularity;

        [HideInInspector]
    public int LimitedAccessNextAvailableTime;

    Button self;

        string ClosedReason = "";

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
      Space.Connect<DefaultEvent>(Events.StatChange, OnStatUpdate);
      Space.Connect<DefaultEvent>(Events.Load, OnStatUpdate);

    }

    public void OnStatUpdate(DefaultEvent eventdata)
    {
        foreach(var Locks in Modularity)
        {
            if(Game.current.Self.GetTrueSocialStat(Locks.Soc) >= Locks.Level)
            {
                switch(Locks.Well)
                {
                    case Personality.Wellbeing.Fatigue:
                        FatigueCloseLimit = Locks.NewCloseLimit;
                        break;
                    case Personality.Wellbeing.Stress:
                        StressCloseLimit = Locks.NewCloseLimit;
                        break;
                    case Personality.Wellbeing.Depression:
                        DepressionCloseLimit = Locks.NewCloseLimit;
                        break;
                }
            }
        }
    }
    
        
    void CheckAccess(DefaultEvent Eventdata)
    {
      self.interactable = true;
      if (Game.current.Progress.GetBoolValue(ManualAccess) == true)
      {
        ClosedReason = Game.current.Progress.GetStringValue(DynamicTooltip);
        self.interactable = false;
      }
      if (LimitedDailyAccess)
      {
        int hour = Game.current.Progress.GetIntValue(HourVisited);
        int day = Game.current.Progress.GetIntValue(DayVisited);
        int length = Game.current.Progress.GetIntValue(AccessPoint) * VisitMulitplier;
        LimitedAccessNextAvailableTime = Game.current.GetNewTimeAfterDuration(hour, length);
        //if last time visited, plus times visited (times multiplier) is greater that current time
        if (Game.current.WithinTimeDifference(hour, day, length))
        {
          ClosedReason = DailyAccessTooltip;
          ClosedReason = ClosedReason.Replace("#time", GetTime(LimitedAccessNextAvailableTime));
          self.interactable = false;
          return;
        }
      }

      if (Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue) > FatigueCloseLimit)
            {
                self.interactable = false;
                ClosedReason = FatigueCloseTooltip;
            }

      if (Game.current.Self.GetWellbingStat(Personality.Wellbeing.Stress) > StressCloseLimit)
            {
                self.interactable = false;
                ClosedReason = StressCloseTooltip;
            }

      if (Game.current.Self.GetWellbingStat(Personality.Wellbeing.Depression) > DepressionCloseLimit)
            {
                self.interactable = false;
                ClosedReason = DepressionCloseTooltip;
            }

      foreach (var point in ClosedTimeContainer)
      {
        if (point.IsClosed(Game.current.Day, Game.current.Hour))
        {
          self.interactable = false;
          ClosedReason = point.ToolTipInfo;
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

    public string GetToolTipInfo()
        {
            return ClosedReason;
        }


        string GetTime(int time)
        {
            while (time > 24)
            {
                time -= 24;
            }

            string Txt = "";

            if (time < 12)
            {
                if (time == 0)
                    Txt = "12:00 AM";
                else
                    Txt = time + ":00 AM";

            }
            else
            {
                if (time == 12)
                    Txt = "12:00 PM";
                else
                    Txt = (time - 12) + ":00 PM";
            }

            return Txt;

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
    public string ToolTipInfo;

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

[System.Serializable]
public class ModularLocker
{
    public Personality.Social Soc;
    [Range(1, 5)]
    public int Level;
    public Personality.Wellbeing Well;
    [Range(0, 100)]
    public int NewCloseLimit;
   
}
