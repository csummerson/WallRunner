using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIAnimator : MonoBehaviour
{
    public Mover[] movers;
    public Image background;

    bool inPlace = false;
    
    public float transitionSpeed = 2f;

    void Update()
    {
        Color color = background.color;
        
        if (!inPlace && color.a >= 0) {
            color.a -= transitionSpeed * Time.unscaledDeltaTime;
            background.color = color;
        } else if (inPlace && color.a <= 0.9f) {
            color.a += transitionSpeed * Time.unscaledDeltaTime * 2;
            background.color = color;
        }
    }

    public void MoveIn(bool dir) {
        inPlace = dir;
        foreach (Mover mover in movers) {
            mover.inPlace = dir;
        }
    }
}
