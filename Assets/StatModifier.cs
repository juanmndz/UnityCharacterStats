public enum StatModType
{
    Flat,
    Percent,
}

public class StatModifier
{
    public readonly float Value;
    public readonly StatModType Type;
    public readonly int Order;

    // Change the existing constructor to look like this
    public StatModifier(float value, StatModType type, int order)
    {
        Value = value;
        Type = type;
        Order = order;
    }

    public StatModifier(float value, StatModType type) : this(value, type, (int)type) { }