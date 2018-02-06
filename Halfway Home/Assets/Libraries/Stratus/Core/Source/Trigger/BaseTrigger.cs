using UnityEngine;

namespace Stratus
{
  /// <summary>
  /// Base class for components in the Stratus triggers framework
  /// </summary>
  public abstract class BaseTrigger : MonoBehaviour
  {
    [Tooltip("A short description of what this is for")]
    public string description;
    protected virtual void OnReset() {}

    private void Reset()
    {
      // If a trigger system is present, hide this component and set its default
      CheckForTriggerSystem();
      // Call subclass reset
      OnReset();
    }

    private void CheckForTriggerSystem()
    {
      var triggerSystem = gameObject.GetComponent<TriggerSystem>();
      if (triggerSystem)
      {
        this.hideFlags = HideFlags.HideInInspector;
        triggerSystem.Add(this);
      }
      else
      {
        this.hideFlags = HideFlags.None;
      }
    }

  }

}