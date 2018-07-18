/******************************************************************************/
/*!
File:   GoalManager.cs
Author: John Myres
All content © 2018 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
  public GameObject MainGoal;
  public GameObject SubGoal;
  public RectTransform GoalParent;

  private List<Task> Objectives;

  private void OnEnable()
  {
    RefreshObjectives();
  }

  void RefreshObjectives()
  {
    Objectives = Game.current.Progress.GetObjectives();
    
    foreach(Transform child in GoalParent.transform)
    {
      GameObject.Destroy(child.gameObject);
    }

    foreach (Task objective in Objectives)
    {
      // If the task is unstarted or an incomplete hidden objective, return
      if (objective.GetState() == Task.TaskState.Unstarted || (objective.GetState() == Task.TaskState.InProgress && objective.Hidden)) return;
      else // Otherwise, present and color text appropriate to its state
      {
        var newGoal = GameObject.Instantiate(MainGoal, GoalParent);
        var goalText = newGoal.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        // For active tasks
        if (objective.GetState() == Task.TaskState.InProgress)
        {
          // present text as-is
          goalText.text = objective.Name;
        }
        // For completed tasks
        else if (objective.GetState() == Task.TaskState.Success)
        {
          // Strike the text
          goalText.text = $"<s>{objective.Name}</s>";
          // Turn it green (TODO: make specific color)
          goalText.color = Color.green;
        }
        // For failed tasks
        else if (objective.GetState() == Task.TaskState.Failed)
        {
          // Strike the text
          goalText.text = $"<s>{objective.Name}</s>";
          // Turn it red (TODO: make specific color)
          goalText.color = Color.red;
        }
        // For every subgoal pertaining to that objective
        foreach (Task subgoal in objective.SubTasks)
        {
          // If the subgoal is unstarted or an incomplete hidden objective, return
          if (subgoal.GetState() == Task.TaskState.Unstarted || (subgoal.GetState() == Task.TaskState.InProgress && objective.Hidden)) return;
          else // Otherwise, complete same operation as above
          {
            var newSubgoal = GameObject.Instantiate(SubGoal, FindComponentInChildWithTag(newGoal, "SubGoalParent").transform);
            var subgoalText = newSubgoal.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            // For active subtasks
            if (subgoal.GetState() == Task.TaskState.InProgress)
            {
              // Present text as-is
              subgoalText.text = subgoal.Name;
            }
            // For completed subtasks
            else if (subgoal.GetState() == Task.TaskState.Success)
            {
              // Strike the text
              subgoalText.text = $"<s>{subgoal.Name}</s>";
              // Turn it green (TODO: make specific color)
              subgoalText.color = Color.green;
            }
            // For failed subtasks
            else if (subgoal.GetState() == Task.TaskState.Failed)
            {
              // Strike the text
              subgoalText.text = $"<s>{subgoal.Name}</s>";
              // Turn it red (TODO: make specific color)
              subgoalText.color = Color.red;
            }
          }
          
        }
      }
    }
  }

  public GameObject FindComponentInChildWithTag(GameObject parent, string tag)
  {
    Transform t = parent.transform;
    foreach (Transform tr in t)
    {
      if (tr.tag == tag)
      {
        return tr.gameObject;
      }
    }
    return null;
  }
}