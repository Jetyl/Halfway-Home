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
	
	// Update is called once per frame
	void Update ()
    {
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
            SceneManager.LoadScene("Menu");
        }


        FadeScreen.DispatchEvent(Events.Fade, new FadeEvent(Color.clear, TransitionTime));

        yield return new WaitForSeconds(TransitionTime);

        Waiting = true;

    }
}
