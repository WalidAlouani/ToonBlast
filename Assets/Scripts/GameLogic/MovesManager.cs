using System;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    private int moves = 0;

    public Action<int> OnMovesChanged;
    public Action OnOutOfMoves;

    public void Init(int maxMoves)
    {
        moves = maxMoves;
        OnMovesChanged?.Invoke(moves);
    }

    public void DecreaseMoves()
    {
        if (moves <= 0)
            return;

        moves--;
        OnMovesChanged?.Invoke(moves);
        if (moves == 0)
            OnOutOfMoves?.Invoke();
    }
}
