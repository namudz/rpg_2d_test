using UnityEngine;

namespace Tests
{
    public class UnitStatsConfigBuilder : TestDataBuilder<UnitStatsConfig> 
    {
        private readonly UnitStatsConfig _unitStats;

        public UnitStatsConfigBuilder()
        {
            _unitStats = ScriptableObject.CreateInstance<UnitStatsConfig>();
        }
        public override UnitStatsConfig Build()
        {
            return _unitStats;
        }

        public UnitStatsConfigBuilder WithHealthPoints(float hp)
        {
            _unitStats.HealthPoints = hp;
            return this;
        }

        public UnitStatsConfigBuilder WithMovementRange(int range)
        {
            _unitStats.MovementRange = range;
            return this;
        }

        public UnitStatsConfigBuilder WithAttackRange(int range)
        {
            _unitStats.AttackRange = range;
            return this;
        }

        public UnitStatsConfigBuilder WithAttackPoints(float damage)
        {
            _unitStats.AttackPoints = damage;
            return this;
        }
    }
}