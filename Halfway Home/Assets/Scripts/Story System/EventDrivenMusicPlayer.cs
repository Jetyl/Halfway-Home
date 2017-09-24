using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stratus;

namespace HalfwayHome
{


  [RequireComponent(typeof(AudioSource))]
  public class EventDrivenMusicPlayer : MonoBehaviour
  {
    public string musicPath;
    protected Dictionary<string, AudioClip> AvailableTracks = new Dictionary<string, AudioClip>();
    protected AudioSource player;
    private AudioClip CurrentTrack;
    private AudioClip PreviousTrack;

    void Start()
    {
      player = GetComponent<AudioSource>();
      Scene.Connect<PlayMusicEvent>(this.OnPlayMusicEvent);
    }
    
    void OnPlayMusicEvent(PlayMusicEvent e)
    {
      Trace.Script($"Playing {e.track}", this);
    }

  }

}