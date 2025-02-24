using UnityEngine;

public class GridElement : MonoBehaviour, IBoardElement
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public void Init(int x, int y)
    {
        SetCoordinates(x, y);
        SetPosition(x, y);
        name = $"Grid[{x},{y}]";
    }

    public void SetCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetPosition(int x, int y, float animationDuration = 0)
    {
        transform.localPosition = new Vector2(x, y);
    }
}
