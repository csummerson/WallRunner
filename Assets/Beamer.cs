using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Beamer : MonoBehaviour
{
    public GameObject bulletEffect;
    
    void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null) {
            health.ChangeHealth(-2 * Time.deltaTime * 10);
            Instantiate(bulletEffect, collision.transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX(hits, 0.1f);
        }
    }

    public AudioClip[] hits;
}
