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
    SetRoomText(e.Place);
    switch (e.Place)
    {
      case Room.ArtRoom:
        MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
        if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        {
          Trace.Script("Room is open: " + e.Open);
          if (e.Open) type = MapTooltipDescriptorType.Tutorial;
          else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
          else type = MapTooltipDescriptorType.TutorialUnavailable;
        }
        else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.CharlottesRoom:
        //MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
        //if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        //{
        //  if (e.Open) type = MapTooltipDescriptorType.Tutorial;
        //  else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
        //  else type = MapTooltipDescriptorType.TutorialUnavailable;
        //}
        //else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        //EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.Commons:
        //MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
        //if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        //{
        //  if (e.Open) type = MapTooltipDescriptorType.Tutorial;
        //  else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
        //  else type = MapTooltipDescriptorType.TutorialUnavailable;
        //}
        //else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        //EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.EduardosRoom:
        //MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
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
        //MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
        //if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        //{
        //  if (e.Open) type = MapTooltipDescriptorType.Tutorial;
        //  else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
        //  else type = MapTooltipDescriptorType.TutorialUnavailable;
        //}
        //else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        //EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.Kitchen:
        //MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
        //if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        //{
        //  if (e.Open) type = MapTooltipDescriptorType.Tutorial;
        //  else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
        //  else type = MapTooltipDescriptorType.TutorialUnavailable;
        //}
        //else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        //EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.Library:
        //MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
        //if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        //{
        //  if (e.Open) type = MapTooltipDescriptorType.Tutorial;
        //  else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
        //  else type = MapTooltipDescriptorType.TutorialUnavailable;
        //}
        //else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        //EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.Store:
        //MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
        //if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        //{
        //  if (e.Open) type = MapTooltipDescriptorType.Tutorial;
        //  else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
        //  else type = MapTooltipDescriptorType.TutorialUnavailable;
        //}
        //else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        //EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      case Room.YourRoom:
        //MapTooltipDescriptorType type = MapTooltipDescriptorType.Default;
        //if (Game.current.Progress.GetBoolValue("Tutorial") == true)
        //{
        //  if (e.Open) type = MapTooltipDescriptorType.Tutorial;
        //  else if (Game.current.Progress.GetBoolValue("Been to Art") == true) type = MapTooltipDescriptorType.TutorialShown;
        //  else type = MapTooltipDescriptorType.TutorialUnavailable;
        //}
        //else if (e.AccessRules.FatigueCloseLimit < Game.current.Self.GetWellbingStat(Personality.Wellbeing.Fatigue)) type = MapTooltipDescriptorType.Fatigue;
        //EffectText.text = FindDescriptor(Room.ArtRoom, type);
        break;
      default:
        break;
    }

  }

  void SetRoomText(Room r)
  {
    switch (r)
    {
      case Room.ArtRoom:
        RoomText.text = "Art Room";
        break;
      case Room.CharlottesRoom:
        RoomText.text = "Charlotte & Trissa's Room";
        break;
      case Room.Commons:
        RoomText.text = "Common Room";
        break;
      case Room.EduardosRoom:
        RoomText.text = "Eduardo & Isaac's Room";
        break;
      case Room.Garden:
        RoomText.text = "Garden";
        break;
      case Room.Kitchen:
        RoomText.text = "Cafe";
        break;
      case Room.Library:
        RoomText.text = "Library";
        break;
      case Room.Store:
        RoomText.text = "Store";
        break;
      case Room.YourRoom:
        RoomText.text = "My and Timothy's Room";
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
