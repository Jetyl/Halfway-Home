using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;

[RequireComponent(typeof(UIFader))]
public class JournalNotification : MonoBehaviour
{
  public class JournalNotificationEvent : Stratus.Event { };
  [Tooltip("Duration the notification will remain on screen after transitioning in and before transitioning out.")]
  public float NotificationDuration = 1.0f;
  [Tooltip("Duration the notification will take to fade in and out EACH.")]
  public float TransitionDuration = 0.1f;
  [Tooltip("File name of the sound effect to be played as part of the notification. Blank for none.")]
  public string SoundEffectFileName;
  private UIFader Fader;
  private bool Animating;
  // Use this for initialization
  void Start ()
  {
    Scene.Connect<JournalNotificationEvent>(OnJournalNotificationEvent);
    Fader = GetComponent<UIFader>();
    Animating = false;
	}

  void OnJournalNotificationEvent(JournalNotificationEvent e)
  {
    if (Animating) return;
    else
    {
      var notifSeq = Actions.Sequence(this);
      
      Actions.Call(notifSeq, ToggleAnimating);
      Actions.Call(notifSeq, ()=>Fader.Show(TransitionDuration));
      if (SoundEffectFileName != "") Actions.Call(notifSeq, ()=>Scene.Dispatch(
                                    new AudioManager.AudioEvent(AudioManager.AudioEvent.SoundType.SFX, SoundEffectFileName)));
      Actions.Delay(notifSeq, NotificationDuration);
      Actions.Call(notifSeq, ()=>Fader.Hide(TransitionDuration));
      Actions.Call(notifSeq, ToggleAnimating);
    }
  }

  void ToggleAnimating()
  {
    Animating = !Animating;
  }

  public void PauseGame()
  {
    Space.DispatchEvent(Events.Pause);
  }
}
