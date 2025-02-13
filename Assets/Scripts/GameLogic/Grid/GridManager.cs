using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform gridContainer;
    [SerializeField] private GridElement gridElement;

    private GridElement[,] gridElements;

    public void Init(int width, int height)
    {
        gridElements = new GridElement[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                gridElements[x, y] = Instantiate(gridElement, gridContainer);
                gridElements[x, y].Init(x, y);
            }
        }
    }
}
