using System.Collections.Generic;
using System;

public class CharacterStat
{
    /*
     * Create a class that represent stats
     * A variable for the base of the stat value
     * A list to store all the individual modifiers that applied to the stat
     * We also need a class to represent stat modifiers
     * Storage for Character stats
     * */
    public float BaseValue;

    // Array like List (readonly like Const)
    private readonly List<StatModifier> statModifiers;
    // Add these variables
    private bool isDirty = true;
    private float _value;



    // The constructor for CharacterStat (Initialization?)
    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        // We update the array list by initializing it
        statModifiers = new List<StatModifier>();
        // By Default does not know how to have our modify sorted
        statModifiers.Sort(CompareModifierOrder);
    }

    // Add this method to the CharacterStat class
    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }

    // Change the Value property to this
    public float Value
    {
        get
        {
            if (isDirty)
            {
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    // Change the AddModifier method
    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
    }

    // And change the RemoveModifier method
    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return statModifiers.Remove(mod);
    }
    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.Percent)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        // Rounding gets around dumb float calculation errors (like getting 12.0001f, instead of 12f)
        // 4 significant digits is usually precise enough, but feel free to change this to fit your needs
        return (float)Math.Round(finalValue, 4);
    }

}