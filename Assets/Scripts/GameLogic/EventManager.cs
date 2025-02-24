using System.Collections.Generic;

public class EventManager
{
    public PropertyEvent<List<TileItem>> OnTilesDestroyed = new PropertyEvent<List<TileItem>>();
    public PropertyEvent<GameState> OnGameStateChanged = new PropertyEvent<GameState>();
}
