public interface IUnitHealth
{
    event System.Action<float> OnDamaged;
    event System.Action<int> OnDeath;
    void Damage (float damage);
}