using System;
using Random = UnityEngine.Random;

public static class EnumUtils
{
    public static T GetRandomValue<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        int randomIndex = Random.Range(1, values.Length);
        return (T)values.GetValue(randomIndex);
    }
}