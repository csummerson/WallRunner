using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMaker : MonoBehaviour
{
    ParticleSystem system;
    Vector3 initialScale;

    public Collider2D bCollider;

    void Start()
    {
        system = GetComponentInChildren<ParticleSystem>();
        initialScale = transform.localScale;
        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        //bCollider = GetComponent<Collider2D>();
        bCollider.enabled = false;
    }

    public void CreateMe()
    {
        // system.Play();  
        StartCoroutine(MakeAfterParticles()); 
    }

    IEnumerator MakeAfterParticles()
    {
        // bCollider.enabled = true;
        // float duration = system.main.duration;
        // float elapsedTime = 0f;

        // while (elapsedTime < duration) {
        //     elapsedTime += Time.deltaTime;
        //     float percentage = elapsedTime / duration;

        //     transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(0f, initialScale.y, percentage), transform.localScale.z);

             yield return null;
        // }
    }

    public void DestroyMe()
    {
        // system.Play();
        // initialScale = transform.localScale;  
        // StartCoroutine(DestroyAfterParticles()); 
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

        Destroy(GetComponent<BoxCollider2D>());
    }
}
