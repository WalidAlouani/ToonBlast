using UnityEngine;

public class GridElement : MonoBehaviour
{
    public int X {  get; private set; }
    public int Y { get; private set; }

    public void Init(int x, int y)
    {
        X = x;
        Y = y;
        transform.localPosition = new Vector3(x, y);
        name = $"Grid[{x},{y}]";
    }
}
