using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntellectualScript : MonoBehaviour
{
    public AudioSource funkyTown;

    public float rotationSpeed = 60f;
    public float insanitySpeed = 0.01f;
    float direction;

    private void Start()
    {
        
        
        Destroy(AudioManager.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        
        direction = Random.Range(0, 1);

        if (direction < 0.5f)
        {
            direction = -1f;
        } else
        {
            direction = 1f;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * funkyTown.pitch * Mathf.Abs(funkyTown.pitch)) * Time.deltaTime);
        funkyTown.pitch += insanitySpeed * direction * Time.deltaTime;
    }
}
