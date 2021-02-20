using UnityEngine;

namespace Tests
{
    public class TestMocksFactory
    {
        public static IGrid GetMockGrid()
        {
            var gridConfig = ScriptableObject.CreateInstance<GridConfig>();
            var generator = new GridGenerator(gridConfig, 0,0);
            return generator.GenerateGrid();
        }

        public static Cell GetMockCell(int cellId, CellCoordinates coordinates, int[] adjacentCells)
        {
            return new Cell(
                cellId,
                coordinates,
                new UnityEngine.Vector2(coordinates.X, coordinates.Y),
                adjacentCells
            );
        }


        public static CellCoordinates GetCellCoordinates(int x, int y)
        {
            return new CellCoordinates(x,y);
        }


        public static TurnDealer GetMockTurnDealer()
        {
            return new TurnDealer(
                new PlayerTurnHandler(),
                new CPUTurnHandler()
            );
        }

        public static CharacterUnitController GetMockCharacterController(Cell spawnCell, IGrid grid)
        {
            var gameobject = new GameObject();
            gameobject.AddComponent<UnitSpriteController>();
            gameobject.AddComponent<UnitCanvasView>();
            gameobject.AddComponent<UnitFxController>();
            var unitController = gameobject.AddComponent<CharacterUnitController>();

            var unit = new TestUnitBuilder().Build();
            unit.CurrentCell = spawnCell;

            unitController.InjectDependencies(
                unit,
                GetMockTurnDealer(),
                grid,
                GetMockUnitHealth(unit),
                GetMockUnitMovement(unit, grid),
                null
            );

            return unitController;
        }

        public static IUnitHealth GetMockUnitHealth(Unit unit)
        {
            return new UnitHealth(unit);
        }

        public static IUnitMovement GetMockUnitMovement(Unit unit, IGrid grid)
        {
            return new UnitMovement(
                new GameObject().transform,
                unit,
                grid,
                GetMockUnitAttack(unit, grid)
            );
        }

        public static IUnitAttack GetMockUnitAttack(Unit unit, IGrid grid)
        {
            return new CharacterAttack(unit, grid);
        }
    }
}