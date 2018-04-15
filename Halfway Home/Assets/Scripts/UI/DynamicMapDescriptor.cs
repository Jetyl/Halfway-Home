using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;
using TMPro;

public class DynamicMapDescriptor : MonoBehaviour
{
  
  public TextMeshProUGUI RoomText;
  public TextMeshProUGUI EffectText;
  public List<RoomDescriptor> Descriptors;
    
	void Start ()
  {
    Scene.Connect<HoverOverRoomEvent>(OnHoverOverRoomEvent);
	}

  void OnHoverOverRoomEvent(HoverOverRoomEvent e)
  {
        RoomText.text = GetRoomName(e.Place);
        if (e.Open)
            EffectText.text = GetDescriptor(e.Place);
        else
        {
            EffectText.text = e.AccessRules.GetToolTipInfo();
        }

  }
    

    string GetDescriptor(Room r)
    {
        foreach(var rooms in Descriptors)
        {
            if (rooms.Room == r)
                return rooms.GetText().DescriptorText;
        }

        return "";
    }

    string GetRoomName(Room r)
    {
        foreach (var rooms in Descriptors)
        {
            if (rooms.Room == r)
                return rooms.name;
        }

        return "";
    }


    // Update is called once per frame
    void Update ()
  {
		
	}
}

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


[System.Serializable]
public class DescriptorCondition
{
    public List<ProgressPoint> Conditions;
    public string DescriptorText;

    public bool ConditionsMet()
    {
        foreach(var condition in Conditions)
        {
            if (Game.current.Progress.CheckProgress(condition) == false)
                return false;
        }

        return true;
    }

}

[System.Serializable]
public class RoomDescriptor
{
    public string name;
    public Room Room;
    public DescriptorCondition DefaultCondition;
    public List<DescriptorCondition> Conditions;

    public DescriptorCondition GetText()
    {
        foreach (var point in Conditions)
        {
            if (point.ConditionsMet())
            {
                return point;
            }
        }

        return DefaultCondition;
    }

}