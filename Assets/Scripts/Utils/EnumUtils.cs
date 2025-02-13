using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class EnumUtils
{
    public static T GetRandomValue<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        int randomIndex = Random.Range(0, values.Length);
        return (T)values.GetValue(randomIndex);
    }

    public static List<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
}