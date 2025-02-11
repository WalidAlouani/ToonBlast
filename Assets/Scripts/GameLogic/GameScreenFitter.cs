using UnityEngine;

public class GameScreenFitter : MonoBehaviour
{
    public void Init(int width, int height)
    {
        transform.position = new Vector3((1 - width) / 2.0f, -height / 2.0f);
    }
}
