public interface IBoardElement
{
    public int X { get; }
    public int Y { get; }

    void SetCoordinates(int x, int y);
}
