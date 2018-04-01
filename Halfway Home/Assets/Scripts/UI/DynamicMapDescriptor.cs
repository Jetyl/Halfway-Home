using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;
using TMPro;

public class DynamicMapDescriptor : MonoBehaviour
{
  public class HoverOverRoomEvent : Stratus.Event
  {
    public Room Place;
    public HalfwayHome.MapAccessTime AccessRules;
    public bool Open;

    public HoverOverRoomEvent(Room place, HalfwayHome.MapAccessTime accessRules, bool open)
    {
      Place = place;
      AccessRules = accessRules;
      Open = open;
    }
  }

  public enum MapTooltipDescriptorType
  {
    Tutorial,
    TutorialUnavailable,
    TutorialShown,
    Default,
    Fatigue,
    Stress,
    Special1,
    Special2
  };

  [System.Serializable]
  public class DescriptorCondition
  {
    public string name;
    public MapTooltipDescriptorType DescriptorType;
    public string DescriptorText;
  }

  [System.Serializable]
  public class RoomDescriptor
  {
    public string name;
    public Room Room;
    public List<DescriptorCondition> Conditions;
  }

  public TextMeshProUGUI RoomText;
  public TextMeshProUGUI EffectText;
  public List<RoomDescriptor> Descriptors;

	void Start ()
  {
    Scene.Connect<HoverOverRoomEvent>(OnHoverOverRoomEvent);
	}

  void OnHoverOverRoomEvent(HoverOverRoomEvent e)
  {
    MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
    switch (e.Place)
    {
      case Room.ArtRoom:
        RoomText.text = "Art Room";
        if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        {
          if (e.Open) type = MapTooltipDescriptorType.Tutorial;
          else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
          else type = MapTooltipDescriptorType.TutorialUnavailable;
        }
        else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.CharlottesRoom:
        RoomText.text = "Charlotte & Trissa's Room";
        if (Game.current.Progress.GetBoolValue("Tutorial") == true) type = MapTooltipDescriptorType.Tutorial;
        else if (e.Open) type = MapTooltipDescriptorType.Special1;
        EffectText.text = FindDescriptor(Room.CharlottesRoom, type);
        break;
      case Room.Commons:
        RoomText.text = "Common Room";
        if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        {
          if (e.Open) type = MapTooltipDescriptorType.Tutorial;
          else if (Game.current.Progress.GetBoolValue("Been to Commons") == true) type = MapTooltipDescriptorType.TutorialShown;
          else type = MapTooltipDescriptorType.TutorialUnavailable;
        }
        else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        else if (e.AccessRules.StressCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Stress)) type = MapTooltipDescriptorType.Stress;
        EffectText.text = FindDescriptor(Room.Commons, type);
        break;
      case Room.EduardosRoom:
        // Currently not hoverable
        //RoomText.text = "Eduardo & Isaac's Room";
        //if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        //{
        //  if (e.Open) type = MapTooltipDescriptorType.Tutorial;
        //  else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
        //  else type = MapTooltipDescriptorType.TutorialUnavailable;
        //}
        //else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        //EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.Garden:
        RoomText.text = "Garden";
        if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        {
          if (e.Open) type = MapTooltipDescriptorType.Tutorial;
          else if (Game.current.Progress.GetBoolValue("Been to Garden") == true) type = MapTooltipDescriptorType.TutorialShown;
          else type = MapTooltipDescriptorType.TutorialUnavailable;
        }
        else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        EffectText.text = FindDescriptor(Room.Garden, type);
        break;
      case Room.Kitchen:
        RoomText.text = "Cafeteria";
        if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        {
          if (e.Open) type = MapTooltipDescriptorType.Tutorial;
          else if (Game.current.Progress.GetBoolValue("Been to Cafe") == true) type = MapTooltipDescriptorType.TutorialShown;
          else type = MapTooltipDescriptorType.TutorialUnavailable;

          EffectText.text = FindDescriptor(Room.Kitchen, type);
        }
        else if (!e.Open)
        {
          type = MapTooltipDescriptorType.Special1;

          var nextTime = e.AccessRules.LimitedAccessNextAvailableTime;
          string t = "";
          if (nextTime == 0) t = "12pm";
          else if (nextTime > 12) t = (nextTime - 12).ToString() + "pm";
          else t = nextTime.ToString() + "am";

          string final = FindDescriptor(Room.Kitchen, type);
          Trace.Script(t);
          EffectText.text = final.Replace("replace", t);
        }
        else
        {
          EffectText.text = FindDescriptor(Room.Kitchen, type);
        }
        break;
      case Room.Library:
        RoomText.text = "Library";
        if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        {
          if (e.Open) type = MapTooltipDescriptorType.Tutorial;
          else if (Game.current.Progress.GetBoolValue("Been to Library") == true) type = MapTooltipDescriptorType.TutorialShown;
          else type = MapTooltipDescriptorType.TutorialUnavailable;
        }
        else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        EffectText.text = FindDescriptor(Room.Library, type);
        break;
      case Room.Store:
        RoomText.text = "Store";
        if (Game.current.Progress.GetBoolValue("Tutorial") == true) type = MapTooltipDescriptorType.Tutorial;
        else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        else if (e.AccessRules.StressCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Stress)) type = MapTooltipDescriptorType.Stress;
        else if (!e.Open) type = MapTooltipDescriptorType.Special1;
        EffectText.text = FindDescriptor(Room.Store, type);
        break;
      case Room.YourRoom:
        RoomText.text = "My & Timothy's Room";
        if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        {
          if (e.Open) type = MapTooltipDescriptorType.Tutorial;
          else type = MapTooltipDescriptorType.TutorialUnavailable;
        }
        EffectText.text = FindDescriptor(Room.YourRoom, type);
        break;
      default:
        break;
    }

  }

  string FindDescriptor(Room r, MapTooltipDescriptorType t)
  {
    return Descriptors.Find(index => index.Room == r).Conditions.Find(ind => ind.DescriptorType == t).DescriptorText;
  }
	
	// Update is called once per frame
	void Update ()
  {
		
	}
}
