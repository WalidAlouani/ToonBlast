using UnityEngine;

public class MouseInputSource : InputSource
{
    public override bool TapPosition(out Vector2 position)
    {
        if (Input.GetMouseButtonUp(0))
        {
            position = Input.mousePosition;
            return true;
        }

        position = Vector2.positiveInfinity;
        return false;
    }
}
