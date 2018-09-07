/******************************************************************************/
/*!
@file   
@author John Myres
@par    email: john.myres@digipen.edu
@par    DigiPen login: john.myres
@par	All content © 2018 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Stratus;
using UnityEngine.UI;

public class CustomSSLoader : StratusBehaviour
{
  public KeyCode LoadKey;
  public bool UseLeftMouse;
  public float SceneTimer;
  public Image FadeObject;
  public float FadeInDur;
  public float FadeOutDur;
  public bool WaitForFade;

  public string SceneToLoad;

  private float TimeUntilLoad;
  private bool LoadStarted;

  void OnEnable()
  {
    SceneManager.sceneLoaded += OnLoad;
  }

  void OnDisable()
  {
    SceneManager.sceneLoaded -= OnLoad;
  }

  void OnLoad(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
  {
    TimeUntilLoad = SceneTimer;
    FadeFromBlack();
    LoadStarted = false;
  }

  void Update()
  {
    if (((Input.GetKeyDown(LoadKey)) || (UseLeftMouse && Input.GetMouseButtonDown(0))) && SceneManager.GetSceneByName(SceneToLoad) != null)
    {
      if (WaitForFade)
      {
        var loadSeq = Actions.Sequence(this);
        Actions.Call(loadSeq, FadeToBlack);
        Actions.Delay(loadSeq, FadeOutDur);
        Actions.Call(loadSeq, LoadNext);
      }
      else
      {
        LoadNext();
      }

    }
    else if (TimeUntilLoad < 0.0f && SceneManager.GetSceneByName(SceneToLoad) != null && !LoadStarted)
    {
      LoadStarted = true;
      var loadSeq = Actions.Sequence(this);
      Actions.Call(loadSeq, FadeToBlack);
      Actions.Delay(loadSeq, FadeOutDur);
      Actions.Call(loadSeq, LoadNext);
    }

    if (SceneTimer > 0.0f)
    {
      TimeUntilLoad -= Time.deltaTime;
    }
  }

  void LoadNext()
  {
    SceneManager.LoadScene(SceneToLoad);
  }

  void FadeToBlack()
  {
    this.FadeObject.CrossFadeAlpha(1.0f, FadeOutDur, true);
  }

  void FadeFromBlack()
  {
    this.FadeObject.CrossFadeAlpha(0.0f, FadeInDur, true);
  }
}
