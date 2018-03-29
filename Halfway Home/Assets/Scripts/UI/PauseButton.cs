using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
  public void OpenPauseMenu()
  {
    Space.DispatchEvent(Events.Pause);
  }
}
