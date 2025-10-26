using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerFader : MonoBehaviour
{
    public float transitionSpeed = 2;
    public Image background;

    public bool fadeOut = false;

    void Start()
    {
        // Color color = background.color;
        // color.a = 1;
        // background.color = color;
    }

    void Update()
    {
        Color color = background.color;

        if (!fadeOut && color.a >= 0) {
            color.a -= transitionSpeed * Time.unscaledDeltaTime;
            background.color = color;
        } else if (fadeOut && color.a <= 1) {
            color.a += transitionSpeed * Time.unscaledDeltaTime * 2;
            background.color = color;
        } 
    }

}
