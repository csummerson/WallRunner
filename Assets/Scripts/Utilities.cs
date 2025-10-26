using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities 
{
    public static float Damp(float source, float target, float smoothing, float dt)
    {
        return Mathf.Lerp(source, target, 1 - Mathf.Pow(smoothing, dt));
    }
}
