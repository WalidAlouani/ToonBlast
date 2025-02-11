using UnityEngine;

public abstract class InputSource: MonoBehaviour
{
    public abstract bool TapPosition(out Vector2 position);
}
