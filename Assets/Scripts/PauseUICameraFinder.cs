using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUICameraFinder : MonoBehaviour
{
    public Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.worldCamera == null) {
            canvas.worldCamera = Camera.main;
        }
    }
}
