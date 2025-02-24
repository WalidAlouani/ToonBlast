using System;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    private int moves = 0;

    public Action<int> OnMovesChanged;
    public Action OnOutOfMoves;

    private void OnEnable()
    {
        ServiceLocator.Get<EventManager>().OnTilesDestroyed.Subscribe(DecreaseMoves, this);
    }

    private void OnDisable()
    {
        if (ServiceLocator.TryGet<EventManager>(out var eventManager))
            eventManager.OnTilesDestroyed.Unsubscribe(DecreaseMoves);
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
}
