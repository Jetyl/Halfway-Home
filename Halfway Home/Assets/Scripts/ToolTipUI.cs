using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipUI : MonoBehaviour
{
    public Vector3 offset;
    

    TextMeshProUGUI text;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<TextMeshProUGUI>();
        Space.Connect<ToolTipEvent>(Events.Tooltip, UpdateDisplay);
        var rec = GetComponent<RectTransform>();
        offset += new Vector3(rec.anchorMax.x * -rec.rect.width, rec.anchorMax.y * -rec.rect.height);
        //print(offset);
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + offset;
    }

    void UpdateDisplay(ToolTipEvent eventdata)
    {
        text.text = eventdata.info;
        text.color = eventdata.color;
    }


}

[System.Serializable]
public class ToolTipEvent:DefaultEvent
{
    public string info;
    public Color color;
    public int Value;

    public ToolTipEvent ()
    {
        info = "";
        color = Color.black;
        color.a = 0;
    }

    public ToolTipEvent (ToolTipEvent copy)
    {
        info = copy.info;
        color = copy.color;
        Value = copy.Value;
    }

}