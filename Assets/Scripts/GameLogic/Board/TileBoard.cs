using UnityEngine;

public class TileBoard : Board<TileItem>
{
    public TileBoard(int width, int height) : base(width, height)
    {
    }

    public virtual bool ElementIsNull(int x, int y)
    {
        return IsEqualTo(x, y, null);
    }

    public virtual void SwapElements(int x1, int y1, int x2, int y2)
    {
        if (!IsWithinBounds(x1, y1) || !IsWithinBounds(x2, y2))
        {
            Debug.LogError("SwapElements out of bounds.");
            return;
        }

        (this[x1, y1], this[x2, y2]) = (this[x2, y2], this[x1, y1]);
        this[x1, y1]?.SetCoordinates(x1, y1);
        this[x2, y2]?.SetCoordinates(x2, y2);
    }

    public virtual bool IsColumnClearAbove(int x, int y)
    {
        for (int checkY = y + 1; checkY < Height; checkY++)
        {
            if (!ElementIsNull(x, checkY))
                return false;
        }
        return true;
    }
}
