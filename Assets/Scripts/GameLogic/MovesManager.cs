using System;
using System.Collections.Generic;

public class MovesManager : IDisposable
{
    private int moves = 0;

    public Action<int> OnMovesChanged;
    public Action OnOutOfMoves;

    private EventManager eventManager;

    public MovesManager()
    {
        ServiceLocator.Register(this);
        eventManager = ServiceLocator.Get<EventManager>();
        eventManager.OnTilesDestroyed.Subscribe(DecreaseMoves);
    }

    public void Init(int maxMoves)
    {
        moves = maxMoves;
        OnMovesChanged?.Invoke(moves);
    }

    public void DecreaseMoves(List<TileItem> tiles)
    {
        if (moves <= 0)
            return;

        moves--;
        OnMovesChanged?.Invoke(moves);
        if (moves == 0)
            OnOutOfMoves?.Invoke();
    }

    public void Dispose()
    {
        ServiceLocator.Deregister(this);
        eventManager?.OnTilesDestroyed.Unsubscribe(DecreaseMoves);
    }
}
