using System;

public static class ArrayUtils
{
    public static T GetRandomValue<T>(T[] array)
    {
        if (array == null || array.Length == 0)
            throw new ArgumentException("Array cannot be null or empty.", nameof(array));

        int randomIndex = UnityEngine.Random.Range(0, array.Length);
        return array[randomIndex];
    }
}