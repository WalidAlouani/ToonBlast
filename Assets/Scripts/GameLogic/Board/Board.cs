using System;
using UnityEngine;

public abstract class Board<T> where T : class, IBoardElement
{
    public T[,] Elements => elements;
    public int Width => elements?.GetLength(0) ?? 0;
    public int Height => elements?.GetLength(1) ?? 0;

    protected T[,] elements;

    protected Board(int width, int height)
    {
        elements = new T[width, height];
    }

    public virtual T GetElement(int x, int y)
    {
        if (!IsWithinBounds(x, y))
            return null;

        return this[x, y];
    }

    public virtual T GetElementFromWorldPosition(Vector2 position)
    {
        var x = (int)(position.x + 0.5f);
        var y = (int)(position.y + 0.5f);
        Debug.Log("GetElemenet : " + x + " " + y);
        return GetElement(x, y);
    }

    public virtual void SetElement(T element)
    {
        if (!IsWithinBounds(element.X, element.Y))
        {
            Debug.LogError($"SetElement out of bounds: ({element.X}, {element.Y})");
            return;
        }
        this[element.X, element.Y] = element;
    }

    public virtual void RemoveElement(int x, int y)
    {
        if (!IsWithinBounds(x, y))
        {
            Debug.LogError($"RemoveElement out of bounds: ({x}, {y})");
            return;
        }
        this[x, y] = null;
    }

    public virtual void ForEach(Action<int, int> action)
    {
        if (action == null)
            return;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                action.Invoke(x, y);
            }
        }
    }

    public virtual bool IsEqualTo(int x, int y, T element)
    {
        return GetElement(x, y) == element;
    }

    public T this[int x, int y]
    {
        get
        {
            if (!IsWithinBounds(x, y))
            {
                Debug.LogError($"Index out of bounds: ({x}, {y})");
                return null;
            }
            return elements[x, y];
        }
        set
        {
            if (!IsWithinBounds(x, y))
            {
                Debug.LogError($"Index out of bounds: ({x}, {y})");
                return;
            }
            elements[x, y] = value;
        }
    }

    public bool IsWithinBounds(int x, int y)
    {
        return elements != null && x >= 0 && x < Width && y >= 0 && y < Height;
    }
}