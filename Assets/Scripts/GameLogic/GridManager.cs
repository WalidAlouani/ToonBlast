using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform gridContainer;
    [SerializeField] private GridElement gridElement;

    private GridElement[,] gridElements;

    public void Init(int width, int height)
    {
        // center grid position depending on the width
        gridContainer.position = new Vector3((width - 1) * -0.5f, gridContainer.position.y, gridContainer.position.z);

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

    public Vector3 GetGridPosition(int x, int y) 
    {
        return gridElements[x, y].transform.position;
    }
}
