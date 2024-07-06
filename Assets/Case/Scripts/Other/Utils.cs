using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class Utils
{
    public static float ClampAngle(float current,float min,float max)
    {
        if (current < -360f) current += 360f;
        if (current > 360f) current -= 360f;
        return Mathf.Clamp(current, min, max);
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rnd = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void DestroyAllChildren(this Transform target)
    {
        for (int i = target.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(target.GetChild(i).gameObject);
        }
    }

}
