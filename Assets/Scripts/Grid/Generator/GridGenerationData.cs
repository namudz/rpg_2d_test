using System.Collections.Generic;

public class GridGenerationData
{
    public int Rows;
    public int Columns;
    public Dictionary<int, Cell> CellsById;
    public Cell[,] CellsByCoordinates;
    public int CounterCharacters;
    public int CounterEnemies;

    public GridGenerationData(
        int rows, 
        int columns, 
        Dictionary<int, Cell> cellsById, 
        Cell[,] cellsByCoordinates, 
        int counterCharacters, 
        int counterEnemies)
    {
        Rows = rows;
        Columns = columns;
        CellsById = cellsById;
        CellsByCoordinates = cellsByCoordinates;
        CounterCharacters = counterCharacters;
        CounterEnemies = counterEnemies;
    }
}