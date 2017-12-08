/******************************************************************************/
/*!
@file   RendererExtensions.cs
@author Christian Sagel
@par    email: ckpsm@live.com
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
using UnityEngine;

namespace Stratus
{
  public static partial class Extensions
  {
    /// <summary>
    /// Checks if the specified renderer is being seen by a specific Camera. 
    /// </summary>
    /// <param name="renderer"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
      Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
      return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
  }
}
