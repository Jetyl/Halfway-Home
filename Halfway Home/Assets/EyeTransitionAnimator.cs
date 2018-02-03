using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Make sure this thing has a material with EyeTransition as its shader
 * 
 */
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class EyeTransitionAnimator : MonoBehaviour
{
    public float Progress = 0.0f;

    MaterialPropertyBlock matBlock;
    SpriteRenderer ren;

    // Use this for initialization
    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
        InitMatBlock();
    }

    void InitMatBlock()
    {
        matBlock = new MaterialPropertyBlock();
        if (ren.sprite != null)
            matBlock.SetTexture("_MainTex", ren.sprite.texture);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (ren == null)
            ren = GetComponent<SpriteRenderer>();
        if (matBlock == null)
            InitMatBlock();
        else if (ren.sprite != null)
            if (matBlock.GetTexture("_MainTex") != ren.sprite.texture)
                matBlock.SetTexture("_MainTex", ren.sprite.texture);

        matBlock.SetFloat("_Progress", Progress);

        ren.SetPropertyBlock(matBlock);
    }
}
