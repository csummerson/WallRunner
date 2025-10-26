using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestoryer : MonoBehaviour
{
    ParticleSystem thisSystem;

    void Start()
    {
        thisSystem = GetComponent<ParticleSystem>();

        Destroy(gameObject, thisSystem.main.duration);   
    }
}
