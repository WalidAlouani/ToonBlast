using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform gridContainer;
    [SerializeField] private GridElement gridElementPrefab;

    private GridBoard gridBoard;

    public void Init(int width, int height)
    {
        gridBoard = new GridBoard(width, height);

        gridBoard.ForEach((x, y) =>
        {
            var gridElement = Instantiate(gridElementPrefab, gridContainer);
            gridBoard.SetElement(gridElement);
            gridElement.Init(x, y);
        });
    }
}
