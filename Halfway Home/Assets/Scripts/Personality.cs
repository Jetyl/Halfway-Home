using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Personality
{

    public enum Wellbeing
    {
        Fatigue,
        Stress,
        Delusion
    }

    public enum Social
    {
        Awareness,
        Grace,
        Expression
    }

    int GraceTier = 0;
    int GraceValue = 0;
    int ExpressionTier = 0;
    int ExpressionValue = 0;
    int AwarenessTier = 0;
    int AwarenessValue = 0;

    int DelusionValue = 1;

    int FatigueValue = 0;
    
    int StressValue = 1;
    int StressThreshold1 = 20; //value of stress, where it will begin decrimetting social stats by 1
    int StressThreshold2 = 50; //value of stress, where it will begin decrimetting social stats by 2
    int StressThreshold3 = 80; //value of stress, where it will begin decrimetting social stats by 3


    int SocialThreshold = 50;
    int MaxSocialGrowth = 3; //maximum number of tiers the player can get normally (not counting character growths)
    int MaxBonusTiers = 2;

    int MaxWellbeingValue = 100;

    public Personality()
    {

    }

    public int GetStat(string statName)
    {
        for (var i = 0; i < Enum.GetValues(typeof(Wellbeing)).Length; ++i)
        {
            if(Enum.GetName(typeof(Wellbeing), (Wellbeing)i) == statName)
            {
                return GetWellbingStat((Wellbeing)i);
            }

        }

        for (var i = 0; i < Enum.GetValues(typeof(Social)).Length; ++i)
        {
            if (Enum.GetName(typeof(Social), (Social)i) == statName)
            {
                return GetModifiedSocialStat((Social)i);
            }

        }

        return 0;


    }

    public void SetStat(string statName, int value)
    {
        for (var i = 0; i < Enum.GetValues(typeof(Wellbeing)).Length; ++i)
        {
            if (Enum.GetName(typeof(Wellbeing), (Wellbeing)i) == statName)
            {
                SetWellbeingStat((Wellbeing)i, value);
            }

        }

        for (var i = 0; i < Enum.GetValues(typeof(Social)).Length; ++i)
        {
            if (Enum.GetName(typeof(Social), (Social)i) == statName)
            {
                 SetSocialStat((Social)i, value);
            }

        }
        
    }


    public int GetTrueSocialStat(Social stat)
    {
        switch (stat)
        {
            case Social.Awareness:
                return AwarenessTier + (AwarenessValue / SocialThreshold);
            case Social.Expression:
                return ExpressionTier + (ExpressionValue / SocialThreshold);
            case Social.Grace:
                return GraceTier + (GraceValue / SocialThreshold);
            default:
                return 0;
        }
    }

    public int GetModifiedSocialStat(Social stat)
    {
        int stressReduction = GetStressReduction();
        int socialstat = 0;

        switch (stat)
        {
            case Social.Awareness:
                socialstat = AwarenessTier + (AwarenessValue/SocialThreshold) - stressReduction;
                break;
            case Social.Expression:
                socialstat = ExpressionTier + (GraceValue / SocialThreshold) - stressReduction;
                break;
            case Social.Grace:
                socialstat = GraceTier + (GraceValue / SocialThreshold) - stressReduction;
                break;
            default:
                return 0;
        }

        if (socialstat < 0)
            socialstat = 0;

        return socialstat;
    }

    public void SetSocialStat(Social stat, int Value)
    {
        switch (stat)
        {
            case Social.Awareness:

                //AwarenessValue = Value;
                if(Value <= MaxBonusTiers)
                AwarenessTier = Value;// / SocialThreshold;
                else
                {
                    AwarenessTier = MaxBonusTiers;
                    AwarenessValue = (Value - MaxBonusTiers) * SocialThreshold;
                }
                
                break;
            case Social.Expression:


                //ExpressionTier = Value;
                if (Value <= MaxBonusTiers)
                    ExpressionTier = Value;// / SocialThreshold;
                else
                {
                    ExpressionTier = MaxBonusTiers;
                    ExpressionValue = (Value - MaxBonusTiers) * SocialThreshold;

                }
                //ExpressionTier = Value / SocialThreshold;

                break;
            case Social.Grace:

                //GraceTier = Value;
                if (Value <= MaxBonusTiers)
                    GraceTier = Value;// / SocialThreshold;
                else
                {
                    GraceTier = MaxBonusTiers;
                    GraceValue = (Value - MaxBonusTiers) * SocialThreshold;

                }
                //GraceTier = Value / SocialThreshold;

                break;
            default:
                break;
        }

    }

    public void IncrementSocialStat(Social stat, int addition)
    {
        if (addition < 0)
            addition = 0;

        switch (stat)
        {
            case Social.Awareness:
                if ((AwarenessValue/SocialThreshold) >= MaxSocialGrowth)
                    break;

                AwarenessValue += addition;
                
                break;
            case Social.Expression:
                if ((ExpressionTier/SocialThreshold) >= MaxSocialGrowth)
                    break;

                ExpressionValue += addition;
                
                break;
            case Social.Grace:
                if ((GraceTier/SocialThreshold) >= MaxSocialGrowth)
                    break;

                GraceValue += addition;
                if (GraceValue >= SocialThreshold * GraceTier)
                {
                    IncrementSocialTier(stat);
                }
                break;
            default:
                break;
        }
        

    }

    public void IncrementSocialTier(Social stat)
    {
        switch (stat)
        {
            case Social.Awareness:
                AwarenessTier += 1;
                if (AwarenessTier > MaxBonusTiers)
                    AwarenessTier = MaxBonusTiers;
                break;
            case Social.Expression:
                ExpressionTier += 1;
                if (ExpressionTier > MaxBonusTiers)
                    ExpressionTier = MaxBonusTiers;
                break;
            case Social.Grace:
                GraceTier += 1;
                if (GraceTier > MaxBonusTiers)
                    GraceTier = MaxBonusTiers;
                break;
            default:
                break;
        }
    }

    int GetStressReduction()
    {

        if (StressValue < StressThreshold1)
            return 0;
        else if (StressValue < StressThreshold2)
            return 1;
        else if (StressValue < StressThreshold3)
            return 2;
        else
            return 3;
    }

    public int GetWellbingStat(Wellbeing stat)
    {
        switch (stat)
        {
            case Wellbeing.Delusion:
                return DelusionValue;
            case Wellbeing.Fatigue:
                return FatigueValue;
            case Wellbeing.Stress:
                return StressValue;
            default:
                return 0;
        }
    }

    public void SetWellbeingStat(Wellbeing stat, int value)
    {

        if (value < 0)
            value = 0;
        if (value > MaxWellbeingValue)
            value = MaxWellbeingValue;

        switch (stat)
        {
            case Wellbeing.Delusion:
                DelusionValue = value;
                break;
            case Wellbeing.Fatigue:
                FatigueValue = value;
                break;
            case Wellbeing.Stress:
                StressValue = value;
                break;
            default:
                break;
        }
    }

    public void IncrementWellbeingStat(Wellbeing stat, int value)
    {
        switch (stat)
        {
            case Wellbeing.Delusion:
                DelusionValue += value;
                if (DelusionValue > MaxWellbeingValue)
                    DelusionValue = MaxWellbeingValue;
                if (DelusionValue < 0)
                    DelusionValue = 0;
                break;
            case Wellbeing.Fatigue:
                FatigueValue += value;
                if (FatigueValue > MaxWellbeingValue)
                    FatigueValue = MaxWellbeingValue;
                if (FatigueValue < 0)
                    FatigueValue = 0;
                break;
            case Wellbeing.Stress:
                StressValue += value;
                if (StressValue > MaxWellbeingValue)
                    StressValue = MaxWellbeingValue;
                if (StressValue < 0)
                    StressValue = 0;
                break;
            default:
                break;
        }
    }

}
