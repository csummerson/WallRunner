using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;

    public LayerMask layerMask;

    public float moveSpeed;

    public float damageValue;

    public GameObject impactEffect;

    public AudioClip impactSFX;
    public AudioClip[] hitSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.excludeLayers = layerMask;
        rb.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
    }

    public float audioMultiplier;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(impactEffect, transform.position, Quaternion.identity);

        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null) {
            health.ChangeHealth(-damageValue);

            
            AudioManager.instance.PlaySFX(hitSFX, audioMultiplier);
        } else {
            
            AudioManager.instance.PlaySFX(impactSFX, audioMultiplier);
        }


        Destroy(gameObject);
    }

    public void BarrierHit() {
        AudioManager.instance.PlaySFX(impactSFX, audioMultiplier);
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
