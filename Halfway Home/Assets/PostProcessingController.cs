using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingController : MonoBehaviour
{

    public PostProcessingBehaviour Behaviour;

    public float TransitionSpeed = 2;

    PostProcessingProfile profile;

    [Range(0, 1)]
    public float DeadZone = .25f;

    [Range(0, 1)]
    public float ZeroStressValue = 0;

    [Range(0, 1)]
    public float MaxStressValue = 0.5f;

    GrainModel.Settings grain;
    
    [Range(0, 1)]
    public float ZeroFatigueValue = 0;

    [Range(0, 1)]
    public float MaxFatigueValue = 0.5f;

    VignetteModel.Settings vignette;

    [Range(0, 2)]
    public float ZeroDepressionValue = 1;
    
    [Range(0, 2)]
    public float MaxDepressionValue = 0.5f;

    ColorGradingModel.Settings saturation;

	// Use this for initialization
	void Start ()
    {

        profile = Behaviour.profile;

        grain = profile.grain.settings;
        vignette = profile.vignette.settings;
        saturation = profile.colorGrading.settings;

        Space.Connect<DefaultEvent>(Events.StatChange, OnStatChange);

        StartCoroutine(TextParser.FrameDelay(Events.StatChange));

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnStatChange(DefaultEvent eventdata)
    {
        StopAllCoroutines();

        print("updating!");
        int Dstat = Game.current.Self.GetWellbingStat(Personality.Wellbeing.delusion);
        float Dpercent = (float)Dstat / 100f;
        Dpercent = Mathf.Clamp01((Dpercent - DeadZone) / (1 - DeadZone));

        var newSatValue = Mathf.Lerp(ZeroDepressionValue, MaxDepressionValue, Dpercent);
        
        int Sstat = Game.current.Self.GetWellbingStat(Personality.Wellbeing.stress);
        float Spercent = (float)Sstat / 100f;
        Spercent = Mathf.Clamp01((Spercent - DeadZone) / (1 - DeadZone));

        var newGrainValue = Mathf.Lerp(ZeroStressValue, MaxStressValue, Spercent);


        int Fstat = Game.current.Self.GetWellbingStat(Personality.Wellbeing.fatigue);
        float Fpercent = (float)Fstat / 100f;
        Fpercent = Mathf.Clamp01((Fpercent - DeadZone) / (1 - DeadZone));

        var newVinValue = Mathf.Lerp(ZeroFatigueValue, MaxFatigueValue, Fpercent);


        StartCoroutine(UpdatePost(newVinValue, newGrainValue, newSatValue));
    }
    


    IEnumerator UpdatePost(float f, float s, float d)
    {
        var startf = vignette.intensity;
        var starts = grain.intensity;
        var startd = saturation.basic.saturation;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / TransitionSpeed)
        {
            vignette.intensity = Mathf.Lerp(startf, f, t);
            profile.vignette.settings = vignette;
            grain.intensity = Mathf.Lerp(starts, s, t);
            profile.grain.settings = grain;
            saturation.basic.saturation = Mathf.Lerp(startd, d, t);
            profile.colorGrading.settings = saturation;
            yield return null;
        }


        yield return new WaitForSeconds(Time.deltaTime);

        vignette.intensity = f;
        profile.vignette.settings = vignette;
        grain.intensity = s;
        profile.grain.settings = grain;
        saturation.basic.saturation = d;
        profile.colorGrading.settings = saturation;

    }

}
