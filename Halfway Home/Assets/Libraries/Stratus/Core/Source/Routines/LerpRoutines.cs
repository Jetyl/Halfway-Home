using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stratus
{
  public static partial class Routines
  {
    public static IEnumerator Lerp<T>(T initialValue, T finalValue, float duration, System.Action<T> setter, System.Func<T ,T, float,T> lerpFunction, System.Action onFinished = null, TimeScale timeScale = TimeScale.Delta)
    {
      System.Action<float> lerp = (float t) =>
      {
        T currentValue = lerpFunction(initialValue, finalValue, t);
        setter.Invoke(currentValue);
      };

      yield return Lerp(lerp, duration, timeScale);
      onFinished?.Invoke();
    }

  }

}