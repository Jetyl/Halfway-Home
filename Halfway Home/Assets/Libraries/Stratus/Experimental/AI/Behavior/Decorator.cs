/******************************************************************************/
/*!
@file   Decorator.cs
@author Christian Sagel
@par    email: c.sagel\@digipen.edu
@par    DigiPen login: c.sagel
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;
using Stratus;

namespace Stratus
{
  namespace AI
  {
    /// <summary>
    /// A branch in a tree with only a single child.
    /// </summary>
    public abstract class Decorator : Behavior
    {
      Behavior Child;
    }  

  }
}
