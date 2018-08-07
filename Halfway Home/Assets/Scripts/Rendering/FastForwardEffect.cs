using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FastForwardEffect : MonoBehaviour
{
    Shader effectShader;
    Material effectMaterial;

    float intensity = 1.0f;

    //Offset for color bleeding to the side
    public float colorBleedDistortAmount = 5.0f;

    //Intensity of the vertical curvy distortion that makes everything look like bad
    public float curvyDistortion = 1.0f;

    //How fast the curvy distortion looks
    public float curvyDistortionSpeed = 5.0f;

    //The intensity of the color, 0 is just grayscale
    public float colorDull = 1.0f;

    //How prominent the bad lines are 
    public float lineStaticIntensity = 2.0f;

    //Intensity of rolling frame offset thing iunno what to call it
    public float scanLineDistortion = 1.0f;

    //Radius for blurring the color texture
    //Note: this is a width offset from 0, going from -blurRadius to blurRadius
    //so the time complexity is actually O( (1 + 2 * blurRadius)^2 )
    //i.e., blurRadius=1 does 9 samples, radius=2 does 25 samples, and etc
    //...Actually, it's 3 times that, since it does a blurred sample for the R, G , and B separately
    //so, blurRadius=1 is 27 samples, blurradius=2 is 75
    //lotta samples, so you can adjust this is you want
    //But, the texture is downscaled from the regular screen texture, so it should be pretty small
    //i.e., most of it fits in cache, so samples will be real fast boiiiiiiiiiiiiiiiiii
    public int blurRadius = 1;

    //Downscales the texture so we can blur for less computation
    //Requires a shutdown and re-enabling to work
    public float downscaleScalar = 0.25f;


    RenderTexture smallTex;
    Vector2Int screenSize;
    
    float t = 0.0f;
    float maxT = 5.0f;
    float tDir = 1.0f;
    public void StartEffect(float rampUpTime)
    {
        tDir = 1.0f;
        maxT = rampUpTime;
        t = 0.0f;

        enabled = true;
    }

    public void EndEffect(float rampDownTime)
    {
        tDir = -1.0f;
        t = rampDownTime;
        maxT = rampDownTime;

        enabled = true;
    }
    
    private void Update()
    {
        if (tDir > 0.0f)
        {
            if (t  < maxT)
            {
                t += Time.deltaTime;

                if (t > maxT)
                    t = maxT;
            }
        }
        else
        {
            if (t > 0.0f)
            {
                t -= Time.deltaTime;

                if (t < 0.0f)
                {
                    t = 0.0f;
                    enabled = false;
                }
            }
        }

        intensity = t / maxT;
    }


    private void OnEnable()
    {
        effectShader = Shader.Find("Post/FastForward");
        effectMaterial = new Material(effectShader);
        
        screenSize = new Vector2Int(Camera.main.pixelWidth, Camera.main.pixelHeight);
        smallTex = new RenderTexture((int)(screenSize.x * downscaleScalar), (int)(screenSize.y * downscaleScalar), 16, RenderTextureFormat.ARGB32);
        effectMaterial.SetTexture("_SmallTex", smallTex);

        effectMaterial.SetFloat("_colorBleedDistortAmount", colorBleedDistortAmount);
        effectMaterial.SetFloat("_colorDull", colorDull);
        effectMaterial.SetFloat("_curvyDistortion", curvyDistortion);
        effectMaterial.SetFloat("_vhsLineIntensity", lineStaticIntensity);
        effectMaterial.SetFloat("_curvyDistortionSpeed", curvyDistortionSpeed);
        effectMaterial.SetFloat("_scanlineDistortion", scanLineDistortion);
        effectMaterial.SetInt("_blurRadius", blurRadius);
        effectMaterial.SetFloat("_intensity", intensity);
    }

    private void OnDisable()
    {
        smallTex.Release();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //NOTE:
        //Comment these lines out once you're gonna ship it
        //Setting uniform variables is pretty slow, so when the stuff is finalized, you don't
        //want to be doing it for no reason since these won't change
        effectMaterial.SetFloat("_colorBleedDistortAmount", colorBleedDistortAmount);
        effectMaterial.SetFloat("_colorDull", colorDull);
        effectMaterial.SetFloat("_curvyDistortion", curvyDistortion);
        effectMaterial.SetFloat("_vhsLineIntensity", lineStaticIntensity);
        effectMaterial.SetFloat("_curvyDistortionSpeed", curvyDistortionSpeed);
        effectMaterial.SetFloat("_scanlineDistortion", scanLineDistortion);
        effectMaterial.SetInt("_blurRadius", blurRadius);


        //But not this one. Keep this one.
        effectMaterial.SetFloat("_intensity", intensity);


        Graphics.Blit(source, smallTex);
        Graphics.Blit(source, destination, effectMaterial);
    }
}
