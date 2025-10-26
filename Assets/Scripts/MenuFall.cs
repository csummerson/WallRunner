using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFall : MonoBehaviour
{
    public float speed = 1;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
