using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDestroyer : MonoBehaviour
{
    ParticleSystem system;
    Vector3 initialScale;

    void Start()
    {
        system = GetComponentInChildren<ParticleSystem>();
    }

    public bool done = false;

    public void DestroyMe()
    {
        system.Play();
        initialScale = transform.localScale;  
        StartCoroutine(DestroyAfterParticles()); 
    }

    IEnumerator DestroyAfterParticles()
    {
        float duration = system.main.duration;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / duration;

            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(initialScale.y, 0f, percentage), transform.localScale.z);

            yield return null;
        }
        done = true;
        Destroy(GetComponent<BoxCollider2D>());
    }
}
