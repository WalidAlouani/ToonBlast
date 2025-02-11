using TMPro;
using UnityEngine;

public class UI_MovesManager : MonoBehaviour
{
    [SerializeField] private MovesManager movesManager;
    [SerializeField] private TMP_Text moves;

    void Awake()
    {
        movesManager.OnMovesChanged += OnMovesChanged;
    }

    void OnDestroy()
    {
        movesManager.OnMovesChanged -= OnMovesChanged;
    }

    private void OnMovesChanged(int moves)
    {
        this.moves.text = moves.ToString();
    }
}
