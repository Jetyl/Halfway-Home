/******************************************************************************/
/*!
@file   ListExtensions.cs
@author Christian Sagel
@par    email: c.sagel\@digipen.edu
@par    DigiPen login: c.sagel
*/
/******************************************************************************/
using System;
using System.Collections.Generic;

namespace Stratus
{
  public static partial class Extensions
  {
    public static T Last<T>(this List<T> list)
    {
      return list[list.Count - 1];
    }

    public static T First<T>(this List<T> list)
    {
      return list[0];
    }

  }

}