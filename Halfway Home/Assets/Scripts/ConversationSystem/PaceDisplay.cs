using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaceDisplay : MonoBehaviour
{
    public GameObject Parent;
    Image manga;
    Button buts;

    Coroutine inProgress;

	// Use this for initialization
	public void Start ()
    {
        manga = GetComponent<Image>();
        buts = GetComponent<Button>();
        //StopPace();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void StartPace(float timer)
    {

        if (inProgress != null)
            StopCoroutine(inProgress);

        Parent.SetActive(true);
        buts.interactable = true;

        inProgress = StartCoroutine(RadialProgress(timer));


    }


    public void StopPace()
    {

        if (inProgress != null)
            StopCoroutine(inProgress);

        Parent.SetActive(false);

        buts.interactable = false;
    }

    IEnumerator RadialProgress(float time)
    {
        float rate = 1 / time;
        float i = 1;
        while (i > 0)
        {
            i -= Time.deltaTime * rate;
            manga.fillAmount = i;
            yield return 0;
        }

        Parent.SetActive(false);
        buts.interactable = false;

    }


}
