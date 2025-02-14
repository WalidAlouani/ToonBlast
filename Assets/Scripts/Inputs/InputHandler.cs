using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputSource inputSource;

    public Action<GridElement> OnTileClicked;

    private void Start()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    private void Update()
    {
        if (!inputSource.TapPosition(out var position))
            return;

        var worldPoint = _camera.ScreenToWorldPoint(position);
        var hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider == null)
            return;

        var gridElement = hit.collider.GetComponent<GridElement>();
        if (gridElement == null)
            return;

        OnTileClicked?.Invoke(gridElement);
        //Debug.Log("Tile clicked: " + gridElement.X + " " + gridElement.Y);
    }
}
