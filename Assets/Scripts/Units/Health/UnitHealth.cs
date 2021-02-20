using System;

public class UnitHealth : IUnitHealth
{
    public event Action<float> OnDamaged;
    public event Action<int> OnDeath;
    private readonly Unit _unit;

    public UnitHealth(Unit unit)
    {
        _unit = unit;
    }

    public void Damage(float damage)
    {
        _unit.HealthPoints.Current -= damage;
        if (_unit.HealthPoints.Current <= 0)
        {
            _unit.HealthPoints.Current = 0;
            LaunchDeathEvent();
            return;
        }
        LaunchDamagedEvent();
    }

    private void LaunchDamagedEvent()
    {
        OnDamaged?.Invoke(_unit.HealthPoints.GetCurrentHealthPercentage());
    }

    private void LaunchDeathEvent()
    {
        OnDeath?.Invoke(_unit.InstanceId);
    }
}