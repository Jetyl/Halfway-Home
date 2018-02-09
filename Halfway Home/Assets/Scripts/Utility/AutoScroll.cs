using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Stratus;

[RequireComponent(typeof(Image))]
public class AutoScroll : MonoBehaviour
{
  public float ScrollTime = 1f;
  private Vector3 StartPos;
  public Vector3 EndPos;
	// Use this for initialization
	void Start ()
  {
    StartPos = gameObject.GetComponent<RectTransform>().localPosition;
    ResetPos();
	}

  void ResetPos()
  {
    gameObject.GetComponent<RectTransform>().localPosition = StartPos;
    Move();
  }

  void Move()
  {
    var moveSeq = Actions.Sequence(this);
    Actions.Property(moveSeq, () => gameObject.GetComponent<RectTransform>().localPosition, EndPos, ScrollTime, Ease.Linear);
    Actions.Call(moveSeq, ResetPos);
  }
}
