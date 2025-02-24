using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public Action<Vector2Int> OnGridClicked;
    private InputSource inputSource;

    private void Start()
    {
        InitializeCamera();
        InitializeInputSource();
    }

    private void InitializeCamera()
    {
        if (_camera == null)
            _camera = Camera.main;

        if (_camera == null)
        {
            Debug.LogError("Camera Not Found");
            return;
        }
    }

    private void InitializeInputSource()
    {
        switch (PlatformUtils.GetCurrentPlatform())
        {
            case SupportedPlatform.PC:
                inputSource = gameObject.AddComponent<MouseInputSource>();
                break;
            case SupportedPlatform.Mobile:
                inputSource = gameObject.AddComponent<TouchInputSource>();
                break;
            default:
                Debug.LogError("Unsupported Platform.");
                return;
        }
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

        OnGridClicked?.Invoke(new Vector2Int(gridElement.X, gridElement.Y));
        //Debug.Log("Tile clicked: " + gridElement.X + " " + gridElement.Y);
    }
}
