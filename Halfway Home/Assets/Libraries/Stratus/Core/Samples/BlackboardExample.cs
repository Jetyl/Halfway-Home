/******************************************************************************/
/*!
@file   BlackboardExample.cs
@author Christian Sagel
@par    email: ckpsm@live.com

All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using Stratus.AI;
using Stratus.Types;

namespace Stratus
{
  namespace Examples
  {
    public class BlackboardExample : MonoBehaviour
    {
      public Stratus.AI.Blackboard Blackboard;
      public string KeyName;
      public Variant.Types KeyType;

      private void Start()
      {
        Blackboard = ScriptableObject.Instantiate(Blackboard);
      }

      void Test()
      {
        var variantValue = Blackboard.Locals.GetValue<int>(KeyName);
        Trace.Script(variantValue, this);
      }
    } 
  } 
}
