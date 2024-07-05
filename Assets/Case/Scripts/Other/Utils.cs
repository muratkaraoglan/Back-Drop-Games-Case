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

}
