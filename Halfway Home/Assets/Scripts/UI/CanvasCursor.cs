using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCursor : MonoBehaviour
{
    public Sprite CursorSprite;
    public Vector2 Offset;
    RectTransform child;
    Image ren;

	// Use this for initialization
	void Start ()
    {
        child = transform.GetChild(0).GetComponent<RectTransform>();
        ren = child.GetComponent<Image>();
        Cursor.visible = true;

        //SetCursor(CursorSprite, Offset);
	}
	
    public void SetCursor(Sprite newCursor, Vector2 offset)
    {
        ren.sprite = newCursor;

        ren.rectTransform.sizeDelta = new Vector2(newCursor.rect.width, newCursor.rect.height);
        ren.rectTransform.localPosition = new Vector3(newCursor.rect.width * 0.5f - offset.x, -newCursor.rect.height * 0.5f - offset.y);
        //ren.rectTransform.localPosition = offset;
        //ren = newCursor;
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
    private void FixedUpdate()
    {
       // transform.position = Input.mousePosition;
    }
}
