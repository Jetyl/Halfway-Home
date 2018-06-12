using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InvertColorEffect : MonoBehaviour
{
    Shader invertShader;
    Material invertMaterial;

    //strenf is that value what you want to make the thing be inverted
    //0.0 = Not inverted
    //1.0 = inverted
    //0.5 = just gray
    public void SetInvertAmount(float strenf)
    {
        invertMaterial.SetFloat("_InvertAmount", Mathf.Clamp01(strenf));
    }
    
	// Use this for initialization
	void Start ()
    {
        invertShader = Shader.Find("Post/InvertColorEffect");

        invertMaterial = new Material(invertShader);
	}

    //despacito 2
    void OnRenderImage(RenderTexture src, RenderTexture trg)
    {
        Graphics.Blit(src, trg, invertMaterial);
    }
}
