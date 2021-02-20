public class DamageUnitCommand : ICommand
{
    private readonly IUnitHealth _unitHealth;
    private readonly float _damageToUnit;

    public DamageUnitCommand(IUnitHealth unitHealth, float damage)
    {
        _unitHealth = unitHealth;
        _damageToUnit = damage;
    }

    public void Execute()
    {
        _unitHealth.Damage(_damageToUnit);
    }
}