using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class ChoiceDisplay : MonoBehaviour
{

    public Choices More; //the default choice for when more than 4 choices are offered
    
    private Choices[] choices;
    

    private List<GameObject> buttons = new List<GameObject>();
    


    // Use this for initialization
    void Start ()
    {

        buttons = new List<GameObject>();

        buttons.Add(transform.Find("FirstButton").gameObject);
        buttons.Add(transform.Find("SecondButton").gameObject);
        buttons.Add(transform.Find("ThirdButton").gameObject);
        buttons.Add(transform.Find("FourthButton").gameObject);

        Space.Connect<DefaultEvent>(Events.ChoiceMade, CleanUpChoice);
        Space.Connect<ChoiceDisplayEvent>(Events.Choice, DisplayChoices);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}


    void CleanUpChoice(DefaultEvent eventdata)
    {

        for (int i = 0; i < buttons.Count; ++i)
        {
            //call cleanup

            buttons[i].GetComponent<Animator>().SetBool("Open", false);
        }

    }
    
    void DisplayChoices(ChoiceDisplayEvent eventdata)
    {
        choices = eventdata.choiceArray;

        //set up the more option
        if(choices.Length > 4)
        {

        }
        else
        {
            for (int i = choices.Length - 1; i >= 0; --i)
            {
                buttons[i].DispatchEvent(Events.Choice, new ChoiceEvent(choices[choices.Length - 1 - i]));
                buttons[i].GetComponent<Animator>().SetBool("Open", true);
            }
        }

    }


}


public class ChoiceDisplayEvent : DefaultEvent
{

    public Choices[] choiceArray;
    

    public ChoiceDisplayEvent(Choices[] array)
    {
        choiceArray = array;
    }
}



[System.Serializable]
public class Choices
{
    public string text;

    public EventListener CallTo;
    public Events DoOnChose = Events.ChoiceMade;

    [HideInInspector]
    public GameObject OwnerRef;

    [HideInInspector]
    public bool ConvMode = false;


    public Choices()
    {
    }

    //copy constructor
    public Choices (Choices copy)
    {
        text = copy.text;
        DoOnChose = copy.DoOnChose;
        CallTo = copy.CallTo;
        OwnerRef = copy.OwnerRef;
        ConvMode = copy.ConvMode;
    }


    public Choices(string text_, bool Conv_ = false)
    {
        text = text_;
        ConvMode = Conv_;
    }

    
}


#if UNITY_EDITOR
namespace CustomInspector
{
    using UnityEditor;

    [CustomEditor(typeof(Choices))]
    public class ChoicesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("text"));
        }
    }
}
#endif
