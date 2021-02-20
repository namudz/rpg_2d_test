public struct HealthPoints
{
    private float Base;
    public float Current;

    public HealthPoints(float baseHp)
    {
        Base = Current = baseHp;
    }

    public float GetCurrentHealthPercentage()
    {
        return Current / Base;
    }
}