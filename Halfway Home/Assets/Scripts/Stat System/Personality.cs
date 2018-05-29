/******************************************************************************/
/*!
File:   Personality.cs
Author: Jesse Lozano
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
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
        Depression
    }

    public enum Social
    {
        Awareness,
        Grace,
        Expression
    }

    Dictionary<Wellbeing, int> WellbeingValues;

    Dictionary<Social, int> SocialValues;
    
    Dictionary<Social, int> BasicSocialStars;
    Dictionary<Social, int> BonusSocialStars;
    
    
    int StressThreshold1 = 20; //value of stress, where it will begin decrimetting social stats by 1
    int StressThreshold2 = 50; //value of stress, where it will begin decrimetting social stats by 2
    int StressThreshold3 = 80; //value of stress, where it will begin decrimetting social stats by 3


    public int SocialThreshold = 50;
    int MaxSocialStars = 3; //maximum number of tiers the player can get normally (not counting character growths)
    int MaxBonusStars = 2;

    public int MaxWellbeingValue = 100;

    public Personality()
    {
        WellbeingValues = new Dictionary<Wellbeing, int>();

        for (var i = 0; i < Enum.GetValues(typeof(Wellbeing)).Length; ++i)
        {
            WellbeingValues.Add((Wellbeing)i, 0);

        }

        SocialValues = new Dictionary<Social, int>();
        BasicSocialStars = new Dictionary<Social, int>();
        BonusSocialStars = new Dictionary<Social, int>();

        for (var i = 0; i < Enum.GetValues(typeof(Social)).Length; ++i)
        {
            SocialValues.Add((Social)i, 0);
            BasicSocialStars.Add((Social)i, 0);
            BonusSocialStars.Add((Social)i, 0);

        }

    }

    public int GetStat(string statName)
    {
        for (var i = 0; i < Enum.GetValues(typeof(Wellbeing)).Length; ++i)
        {
            if(Enum.GetName(typeof(Wellbeing), (Wellbeing)i).ToLower() == statName.ToLower())
            {
                return GetWellbingStat((Wellbeing)i);
            }

        }

        for (var i = 0; i < Enum.GetValues(typeof(Social)).Length; ++i)
        {
            if (Enum.GetName(typeof(Social), (Social)i).ToLower() == statName.ToLower())
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
            if (Enum.GetName(typeof(Wellbeing), (Wellbeing)i).ToLower() == statName.ToLower())
            {
                SetWellbeingStat((Wellbeing)i, value);
            }

        }

        for (var i = 0; i < Enum.GetValues(typeof(Social)).Length; ++i)
        {
            if (Enum.GetName(typeof(Social), (Social)i).ToLower() == statName.ToLower())
            {
                 SetSocialStat((Social)i, value);
            }

        }
        
    }

    public int GetSocialProgress(Social stat)
    {
        return SocialValues[stat];
    }

    //returns the current number of stars collected
    public int GetBasicSocialStat(Social stat)
    {
        return BasicSocialStars[stat];
    }

    //returns the current number of stars collected
    public int GetBonusSocialStat(Social stat)
    {
        return BonusSocialStars[stat];
    }

    //returns the current number of stars collected
    public int GetTrueSocialStat(Social stat)
    {
        return BasicSocialStars[stat] + BonusSocialStars[stat];
    }

    //returns the star count, minus stress reduction
    public int GetModifiedSocialStat(Social stat)
    {
        int stressReduction = GetStressReduction();
        int socialstat = BasicSocialStars[stat] + BonusSocialStars[stat] - stressReduction;
        
        if (socialstat < 0)
            socialstat = 0;

        return socialstat;
    }

    public void SetSocialStar(Social stat, int Value)
    {

        SocialValues[stat] = Value * SocialThreshold;
        if (Value <= MaxSocialStars)
            BasicSocialStars[stat] = Value;
        else
        {
            BasicSocialStars[stat] = MaxSocialStars;
            BonusSocialStars[stat] = Value - MaxSocialStars;
        }

    }

    public void SetSocialStat(Social stat, int Value)
    {

        SocialValues[stat] = Value;
        BasicSocialStars[stat] = Value / SocialThreshold;
        
    }

    public void IncrementSocialStat(Social stat, int addition)
    {

        if (BasicSocialStars[stat] >= MaxSocialStars)
            return;

        if (addition < 0)
            addition = 0;

        SocialValues[stat] += addition;
        BasicSocialStars[stat] = SocialValues[stat] / SocialThreshold;
        
    }

    public void AddBonusSocialStar(Social stat)
    {

        if (BonusSocialStars[stat] >= MaxBonusStars)
            return;

        BonusSocialStars[stat] += 1;
        
    }

    int GetStressReduction()
    {
        var StressValue = WellbeingValues[Wellbeing.Stress];

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
        return WellbeingValues[stat];
    }

    public void SetWellbeingStat(Wellbeing stat, int value)
    {

        if (value < 0)
            value = 0;
        if (value > MaxWellbeingValue)
            value = MaxWellbeingValue;

        WellbeingValues[stat] = value;
        
    }

    public void IncrementWellbeingStat(Wellbeing stat, int value)
    {

        WellbeingValues[stat] += value;

        if (WellbeingValues[stat] > MaxWellbeingValue)
            WellbeingValues[stat] = MaxWellbeingValue;
        if (WellbeingValues[stat] < 0)
            WellbeingValues[stat] = 0;
        
    }

}
