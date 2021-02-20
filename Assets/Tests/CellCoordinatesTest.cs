using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CellCoordinatesTest
    {
        [Test]
        public void WhenCoordinateIsWithinCoordinateRange_ReturnTrue()
        {
            var coords1 = TestMocksFactory.GetCellCoordinates(0, 0);
            var coords2 = TestMocksFactory.GetCellCoordinates(2, 0);
            var range = 2;

            var isWithinRange = coords1.AreCoordinatesInRange(coords2, range);

            Assert.IsTrue(isWithinRange);
        }

        [Test]
        public void WhenCoordinateIsNotWithinCoordinateRange_ReturnFalse()
        {
            var coords1 = TestMocksFactory.GetCellCoordinates(3, 3);
            var coords2 = TestMocksFactory.GetCellCoordinates(2, 2);
            var range = 1;

            var isWithinRange = coords1.AreCoordinatesInRange(coords2, range);

            Assert.IsFalse(isWithinRange);
        }
    }
}
