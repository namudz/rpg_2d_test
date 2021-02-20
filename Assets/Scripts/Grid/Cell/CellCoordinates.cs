using UnityEngine;

public class CellCoordinates
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public CellCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        var text = string.Format(
            "({0}, {1})",
            X,
            Y
        );
        return text;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        var coords = (CellCoordinates)obj;
        return X == coords.X && Y == coords.Y;
    }

    public bool AreCoordinatesInRange(CellCoordinates target, int range)
    {
        var distance = GetDistanceToCoordinates(target);
        return distance <= range;
    }

    public int GetDistanceToCoordinates(CellCoordinates target)
    {
        var vectorDistance = new Vector2Int(
            Mathf.Abs(target.X - X),
            Mathf.Abs(target.Y - Y)
        );
        return vectorDistance.x + vectorDistance.y;
    }
}