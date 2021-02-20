using UnityEngine;

namespace Tests
{
    public class TestUnitBuilder : TestDataBuilder<Unit> 
    {
        private UnitStatsConfig _stats;
        private int _id;

        public TestUnitBuilder()
        {
            _id = 0;
            _stats = ScriptableObject.CreateInstance<UnitStatsConfig>();
        }
        public override Unit Build()
        {
            return new Unit(_id, _stats);
        }

        public TestUnitBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public TestUnitBuilder WithStats(UnitStatsConfig stats)
        {
            _stats = stats;
            return this;
        }
    }
}