using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class BaseStat
{
    private readonly float baseValue;
    private readonly float statGrowth;
    public virtual float Value 
    {
        get
        {
            if (isDirty || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                lastCalculatedValue = CalculateFinalValue();
                isDirty = false;
            }
            return lastCalculatedValue;
        }
    }
    public virtual float StatGrowth 
    {
        get { return statGrowth; } 
    }
    protected bool isDirty = true;
    protected float lastCalculatedValue;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public BaseStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }
    public BaseStat(float baseValue) : this()
    {
        this.baseValue = baseValue;
        statGrowth = 0;
    }
    public BaseStat(float baseValue, float _statGrowth) : this()
    {
        this.baseValue = baseValue;
        statGrowth = _statGrowth;
    }
    public virtual void AddModifier(StatModifier modifier)
    {
        isDirty = true;
        statModifiers.Add(modifier);
        statModifiers.Sort(CompareModifierOrder);
    }
    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if(a.Order > b.Order)
            return 1;
        return 0; //if "a" and "b" have the same Order value
    }
    public virtual bool RemoveModifier(StatModifier modifier)
    {
        if (statModifiers.Remove(modifier))
        {
            isDirty = true;
            return true;
        }
        return false;
    }
    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool DidRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                DidRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return DidRemove;
    }
    public virtual bool RemoveAllModifiersFromOrder(int order)
    {
        bool DidRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Order == order)
            {
                isDirty = true;
                DidRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return DidRemove;
    }
    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            if (mod.Type == StatModifierType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModifierType.PercentAdd)
            {
                sumPercentAdd += mod.Value;
                if (i + 1 >= statModifiers.Count || statModifiers[i+1].Type != StatModifierType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.Type == StatModifierType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        return (float)Math.Round(finalValue, 4);
    }
}
