using NUnit.Framework;

namespace Tests
{
    public class DamageUnitCommandTest
    {
        [Test]
        public void WhenDamageIsApplied_DamageIsSubstractedFromHealthOk()
        {
            var unitStats = new UnitStatsConfigBuilder().WithHealthPoints(100).Build();
            var unit = new TestUnitBuilder().WithStats(unitStats).Build();

            var unitHealth = TestMocksFactory.GetMockUnitHealth(unit);
            var damage = 1;
            var damageUnitCommand = new DamageUnitCommand(unitHealth, damage);

            var expectedHealth = unit.HealthPoints.Current - damage;

            damageUnitCommand.Execute();

            Assert.AreEqual(unit.HealthPoints.Current, expectedHealth);
        }
    }
}
