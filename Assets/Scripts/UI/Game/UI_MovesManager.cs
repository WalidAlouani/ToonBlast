using TMPro;
using UnityEngine;

public class UI_MovesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moves;

    private MovesManager movesManager;
    
    void OnEnable()
    {
        movesManager = ServiceLocator.Get<MovesManager>();
        movesManager.OnMovesChanged += OnMovesChanged;
    }

    void OnDisable()
    {
        movesManager.OnMovesChanged -= OnMovesChanged;
    }

    private void OnMovesChanged(int moves)
    {
        this.moves.text = moves.ToString();
    }
}
