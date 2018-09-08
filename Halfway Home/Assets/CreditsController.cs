using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{

    public GameObject[] Panels; //in order

    public GameObject FadeScreen;

    public float WaitTimer = 2;
    float Timer;
    public float TransitionTime = 1;

    int PanelNumber = 0;
    bool Waiting;

	// Use this for initialization
	void Start ()
    {
		for(int i = 1; i < Panels.Length; ++i)
        {
            Panels[i].SetActive(false);
        }

        Waiting = true;
	}

  void Awake()
  {
    AkBankManager.LoadBankAsync("Credits");
    AkBankManager.LoadBankAsync("Master");
    AkSoundEngine.PostEvent("play_music_fakeit", GameObject.Find("Music"));
  }

  // Update is called once per frame
  void Update ()
    {
        if (Input.GetButtonDown("Cancel")) EndCredits(true);
        if (!Waiting)
            return;

        if (Timer >= WaitTimer)
        {
            Timer = 0;
            StartCoroutine(NextPanel());
            Waiting = false;
        }
        else
            Timer += Time.deltaTime;
	}



    IEnumerator NextPanel()
    {

        FadeScreen.DispatchEvent(Events.Fade, new FadeEvent(Color.black, TransitionTime));

        yield return new WaitForSeconds(TransitionTime);
        Panels[PanelNumber].SetActive(false);
        PanelNumber += 1;

        if (PanelNumber < Panels.Length)
            Panels[PanelNumber].SetActive(true);
        else
        {
            EndCredits(false);
        }


        FadeScreen.DispatchEvent(Events.Fade, new FadeEvent(Color.clear, TransitionTime));

        yield return new WaitForSeconds(TransitionTime);

        Waiting = true;

    }

    void EndCredits(bool skip)
    {
        AkSoundEngine.PostEvent("Stop_All", GameObject.Find("Music"));
        if(skip)
        {
          FadeScreen.DispatchEvent(Events.Fade, new FadeEvent(Color.black, TransitionTime));
          var waitSeq = Stratus.Actions.Sequence(this);
          Stratus.Actions.Delay(waitSeq, TransitionTime);
          Stratus.Actions.Call(waitSeq, ()=>SceneManager.LoadScene("Menu"));
        }
        else
        {
          SceneManager.LoadScene("Menu");
        }
    }
}
