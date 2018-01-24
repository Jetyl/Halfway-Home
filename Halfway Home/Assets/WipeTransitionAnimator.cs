using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Make sure this thing has a material with WipeTransition as its shader
 * 
 */
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class WipeTransitionAnimator : MonoBehaviour
{
    public float Progress = 0.0f;
    public Vector2 FadeDirection = new Vector2(1.0f, 0.0f);
    public float FadeWidth = 0.2f;

    MaterialPropertyBlock matBlock;
    SpriteRenderer ren;

	// Use this for initialization
	void Start ()
    {
        ren = GetComponent<SpriteRenderer>();
	}

    void InitMatBlock()
    {
        matBlock = new MaterialPropertyBlock();
        matBlock.SetVector("_Dir", new Vector4(FadeDirection.x, FadeDirection.y, 0.0f, 0.0f));
        matBlock.SetFloat("_Width", FadeWidth);
        matBlock.SetTexture("_MainTex", ren.sprite.texture);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ren == null)
            ren = GetComponent<SpriteRenderer>();
        if (matBlock == null)
            InitMatBlock();
        else
        {

            matBlock.SetVector("_Dir", new Vector4(FadeDirection.x, FadeDirection.y, 0.0f, 0.0f));

            if (matBlock.GetTexture("_MainTex") != ren.sprite.texture)
                matBlock.SetTexture("_MainTex", ren.sprite.texture);
        }
        
        
        matBlock.SetFloat("_Progress", Progress);

        ren.SetPropertyBlock(matBlock);
	}
}
