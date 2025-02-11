using UnityEngine;

public class TouchInputSource : InputSource
{
    public override bool TapPosition(out Vector2 position)
    {
        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                position = touch.position;
                return true;
        }

        position = Vector2.positiveInfinity;
        return false;
    }
}
