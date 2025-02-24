public interface ITileFactory
{
    TileItem CreateTile(int x, int y, ItemType tileType);
}