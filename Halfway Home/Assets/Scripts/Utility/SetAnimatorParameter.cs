using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SetAnimatorParameter : MonoBehaviour
{
  public string ParameterName;

  public void SetAnimatorInt(int state)
  {
    GetComponent<Animator>().SetInteger(ParameterName, state);
  }

  public void SetAnimatorBool(bool state)
  {
    GetComponent<Animator>().SetBool(ParameterName, state);
  }
}
