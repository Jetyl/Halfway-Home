/******************************************************************************/
/*!
@file   CollisionTrigger.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Stratus
{
  /// <summary>
  /// Triggers an event when its (trigger) collider collides with a GameObject with the
  /// given specified tag.
  /// </summary>
  public class CollisionTrigger : Trigger
  {    
    [Header("Collision Type")]
    public CollisionProxy.TriggerType type;
    [Tooltip("The object whose collision messages we are listening for")]
    public Collider source;
    [Tooltip("What targets we are allowed to collide with")]
    public GameObjectField collisionTarget;
    
    protected override void OnAwake()
    {
      CollisionProxy.Construct(source, type, OnCollision, persistent);
    }
    
    private void OnCollision(Collider other)
    {
      if (collisionTarget.IsTarget(other.gameObject))
      {
        this.Activate();
      }
    }

    private void Reset()
    {
      // Attempt to use self as a target first
      source = GetComponent<Collider>();
    }
  }

}