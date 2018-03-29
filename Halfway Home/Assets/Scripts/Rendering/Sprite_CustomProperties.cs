using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[System.Serializable]
public class MProperty
{
    public string name;
    public string description;
    public ShaderUtil.ShaderPropertyType type;
    public bool hidden = false;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Sprite_CustomProperties))]
public class CustomSpriteEditor : Editor
{
    Material m;
    Shader s;

    bool isCanvas;

    MaterialPropertyBlock block;

    //When an object is selected
    void OnEnable()
    {
        //First, we gotta get the sprite or image component on the thing
        //Jesus Christ, Jesse, your keyboard is infuriating
        Sprite_CustomProperties comp = serializedObject.targetObject as Sprite_CustomProperties;
        SpriteRenderer sprite = comp.GetComponent<SpriteRenderer>();
        Image image = comp.GetComponent<Image>();
        CanvasRenderer canv = comp.GetComponent<CanvasRenderer>();

        if (sprite != null)
        {
            m = sprite.material;
            sprite.GetPropertyBlock(block);
            isCanvas = false;

            comp.IsCanvasRenderer = true;
        }
        else if (image != null)
        {
            isCanvas = true;
            m = image.material;

            comp.IsCanvasRenderer = true;
        }

        s = m.shader;

        numProperties = ShaderUtil.GetPropertyCount(s);
        materialProperties = new List<MProperty>();
        
        for (int i = 0; i < numProperties; i++)
        {
            MProperty newProp = new MProperty();
            newProp.name = ShaderUtil.GetPropertyName(s, i);
            newProp.description = ShaderUtil.GetPropertyDescription(s, i);
            newProp.type = ShaderUtil.GetPropertyType(s, i);
            newProp.hidden = ShaderUtil.IsShaderPropertyHidden(s, i);

            //These are hardcoded values that Unity already takes care of for us
            if (!(newProp.name == "_MainTex" || newProp.name == "_Color"))
                materialProperties.Add(newProp);
        }
    }

    int numProperties;
    List<MProperty> materialProperties;

    public override void OnInspectorGUI()
    {
        //Canvases use different stuff than Sprites, so we gotta have a different editor for them
        if (isCanvas)
        {
            for (int i = 0; i < materialProperties.Count; i++)
            {
                MProperty p = materialProperties[i];

                if (!p.hidden)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(p.name);
                    EditorGUILayout.LabelField(p.description);
                    //p.type = ShaderUtil.ShaderPropertyType.
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.LabelField(p.hidden.ToString());
                }
            }
        }

        //base.OnInspectorGUI();
    }
}
#endif

public class Sprite_CustomProperties : MonoBehaviour
{
    MaterialPropertyBlock matBlock;
    public bool IsCanvasRenderer;
    List<MProperty> materialProperties;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
