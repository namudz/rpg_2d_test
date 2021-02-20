using System.Collections.Generic;

public interface IGrid
{
    int Rows { get; }
    int Columns { get; }
    Cell GetCellById(int cellId);
    Cell GetRandomFreeCell();

    /// <summary>
    /// Return given Cell if not occupied, free random otherwise.
    /// </summary>
    Cell TryGetSpawnCell(int x, int y);
    Cell[] GetRowCells(int rowId);
    bool IsCellFree(int cellId);

    List<int> GetAdjacentCellsWithinRange(int cellId, int range);
    
    void AddCharacter(AUnitController unitController);
    void RemoveCharacter(AUnitController unitController);
    void AddEnemy(AUnitController unitController);
    void RemoveEnemy(AUnitController unitController);

    AUnitController GetCharacterOnCell(int cellId);
    AUnitController GetEnemyOnCell(int cellId);

    List<AUnitController> GetAllCharacters();
}