using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 2;
    void Update()
    {
        Vector3 animation = new Vector3(0, 0, rotationSpeed * Time.deltaTime * 50);
        transform.Rotate(animation);
    }
}
