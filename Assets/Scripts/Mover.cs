using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 startingPos;

    Vector3 targetPos;

    public Vector3 endingPos = Vector3.zero;

    public bool inPlace = false;

    void Start()
    {
        if (startingPos == Vector3.zero) {
            startingPos = transform.localPosition;
        }
        targetPos = transform.localPosition;
    }

    Vector3 referenceVel = Vector3.zero;

    public float speed = 0.3f;

    // Update is called once per frame
    void Update()
    {
        if (inPlace) {
            //Debug.Log("In PLace");
            targetPos = endingPos;
        } else {
            //Debug.Log("Not in place");
            targetPos = startingPos;
        }
        
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref referenceVel, speed, Mathf.Infinity, Time.unscaledDeltaTime);
    }
}
