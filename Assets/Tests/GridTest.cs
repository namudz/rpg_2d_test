using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class GridTest
    {   
        [Test]
        public void WhenAskingForUnexistingCell_ReturnNull()
        {
            var grid = TestMocksFactory.GetMockGrid();
            Assert.IsNull(grid.GetCellById(-1));
        }

        [Test]
        public void WhenAskingForExistingCell_ReturnAskedCell()
        {
            var grid = TestMocksFactory.GetMockGrid();
            
            var expectedCellId = 1;
            var cell = grid.GetCellById(expectedCellId);

            var expectedX = 1;
            var expectedY = 0;
            var expectedCell = new Cell(
                expectedCellId,
                new CellCoordinates(expectedX, expectedY),
                new Vector2(1, 0), 
            null
            );

            Assert.AreEqual(cell.Id, expectedCell.Id);
            Assert.AreEqual(cell.Coordinates, expectedCell.Coordinates);
        }

        [Test]
        public void WhenACellIsFree_AndAskingIfItsFree_ReturnTrue()
        {
            var grid = TestMocksFactory.GetMockGrid();
            Assert.IsTrue(grid.IsCellFree(0));
        }

        [Test]
        public void WhenAnUnitIsOnACell_AndAskingIfTheCellIsFree_ReturnFalse()
        {
            var grid = TestMocksFactory.GetMockGrid();
            var unitSpawnCell = TestMocksFactory.GetMockCell(
                0, 
                TestMocksFactory.GetCellCoordinates(0,0),
                new int[0]
            );            
            var unitController = TestMocksFactory.GetMockCharacterController(unitSpawnCell, grid);

            grid.AddCharacter(unitController);

            var isCellFree = grid.IsCellFree(unitController.GetCurrentCellId());
            Assert.IsFalse(isCellFree);
        }

        [Test]
        public void WhenAskingForAdjacentCellsOfCell_ReturnCorrectAdjacentCells()
        {
            var grid = TestMocksFactory.GetMockGrid();
            var cell = grid.GetCellById(5);

            var adjacentCellIds = cell.GetAdjacentCellsIds();

            var expectedAdjacentCellIds = new int[4]
            {
                cell.Id + grid.Columns,
                cell.Id + 1,
                cell.Id - grid.Columns,
                cell.Id - 1
            };
            var expectedAdjacentCellIdsClean = expectedAdjacentCellIds.Where(cellId => cellId > -1).ToArray();

            Assert.AreEqual(adjacentCellIds, expectedAdjacentCellIdsClean);
        }
    }
}
