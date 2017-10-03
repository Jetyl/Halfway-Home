using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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


    public int GetTrueSocialStat(Social stat)
    {
        switch (stat)
        {
            case Social.Awareness:
                return AwarenessTier;
            case Social.Expression:
                return ExpressionTier;
            case Social.Grace:
                return GraceTier;
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
                socialstat = AwarenessTier - stressReduction;
                break;
            case Social.Expression:
                socialstat = ExpressionTier - stressReduction;
                break;
            case Social.Grace:
                socialstat = GraceTier - stressReduction;
                break;
            default:
                return 0;
        }

        if (socialstat < 0)
            socialstat = 0;

        return socialstat;
    }

    public void IncrementSocialStat(Social stat, int addition)
    {
        if (addition < 0)
            addition = 0;

        switch (stat)
        {
            case Social.Awareness:
                if (AwarenessTier >= MaxSocialGrowth)
                    break;

                AwarenessValue += addition;
                if(AwarenessValue >= SocialThreshold)
                {
                    AwarenessValue = 0;
                    IncrementSocialTier(stat);
                }
                break;
            case Social.Expression:
                if (ExpressionTier >= MaxSocialGrowth)
                    break;

                ExpressionValue += addition;
                if (ExpressionValue >= SocialThreshold)
                {
                    ExpressionValue = 0;
                    IncrementSocialTier(stat);
                }
                break;
            case Social.Grace:
                if (GraceTier >= MaxSocialGrowth)
                    break;

                GraceValue += addition;
                if (GraceValue >= SocialThreshold)
                {
                    GraceValue = 0;
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
                if (AwarenessTier > 5)
                    AwarenessTier = 5;
                break;
            case Social.Expression:
                ExpressionTier += 1;
                if (ExpressionTier > 5)
                    ExpressionTier = 5;
                break;
            case Social.Grace:
                GraceTier += 1;
                if (GraceTier > 5)
                    GraceTier = 5;
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
