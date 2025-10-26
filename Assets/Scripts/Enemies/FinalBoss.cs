using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinalBoss : Enemy
{
    bool canAttack = true;

    public AudioClip sound;

    public GameObject miniMe;

    public float spinCooldown = 2f;
    public float spinDuration = 2f;
    public float spinSpeed = 1f;

    public float deathTime = 5f;
    public float healthDivider = 10;
    public float enemySpawnSpacer = 0.2f;
    
    FinalBossHealth fakeHealth;

    public Collider2D myCollider;
    public SpriteRenderer myRenderer;

    List<GameObject> miniMes;

    public AudioClip[] beamFire;

    protected override void Start()
    {
        base.Start();
        fakeHealth = GetComponent<FinalBossHealth>();
        fakeHealth.OnDeath += MainBodyDied;
        
        //active = false;
        //StartCoroutine(Attack());
    }

    public bool splitUp = false;

    void MainBodyDied() {
        Debug.Log("Reached Parent");
        fakeHealth.ignoreColissions = true;
        StartCoroutine(DeathSquence());
    }

    public ParticleSystem summonSystem, explodingSystem, smallSystem;

    IEnumerator DeathSquence() {
        Debug.Log("Spawning started");
        
        splitUp = true;
        miniMes = new List<GameObject>();
        chargeUp.Stop();


        StopCoroutine(Attack());
        targetBeam.SetActive(false);
        beam.SetActive(false);

        Vector3 originalScale = transform.localScale;

        float healthMax = fakeHealth.maxHealth;

        int enemiesToSpawn = (int) (healthMax / healthDivider);
        Vector3 step = originalScale / enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++) {
            Vector3 newScale = transform.localScale - step;
            newScale.x = Mathf.Max(newScale.x, 0);
            newScale.y = Mathf.Max(newScale.y, 0);
            newScale.z = Mathf.Max(newScale.z, 0);
            transform.localScale = newScale;
            
            smallSystem.Play();

            miniMes.Add(Instantiate(miniMe, transform.position, Quaternion.Euler(0,0,Random.Range(0, 360))));
            
            Physics2D.IgnoreCollision(miniMes[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());            
            
            yield return new WaitForSeconds(enemySpawnSpacer);
        }
        active = false;

        yield return new WaitUntil(() => miniMes.TrueForAll(mini => mini == null));
        //yield return new WaitForSeconds(deathTime);

        // summonSystem.Play();

        // fakeHealth.currentHealth = 0;

        // // RESPAWNING
        // active = true;
        // int remaining = 0;
        // foreach (var mini in miniMes) {
        //     if (mini != null) {
        //         remaining++;

        //         MiniMover mover = mini.GetComponent<MiniMover>();
        //         mover.ReturnHome(transform.position);
                
        //         yield return new WaitUntil(() => mover.hasReturnedHome);

        //         Vector3 newScale = transform.localScale + step;
        //         newScale.x = Mathf.Min(newScale.x, originalScale.x);
        //         newScale.y = Mathf.Min(newScale.y, originalScale.y);
        //         newScale.z = Mathf.Min(newScale.z, originalScale.z);
        //         transform.localScale = newScale;

        //         fakeHealth.currentHealth += healthDivider;

                
        //         //yield return new WaitForSeconds(enemySpawnSpacer + 0.3f);
        //     }
        // }

        // summonSystem.Stop();

        explodingSystem.Play();

        // if (remaining == 0) {
        //     Destroy(gameObject);
        // }

        Destroy(gameObject);

        // fakeHealth.maxHealth = fakeHealth.currentHealth;

        // fakeHealth.ignoreColissions = false;

        // yield return new WaitForSeconds(0.2f);

        // splitUp = false;
        // StartCoroutine(Attack());
    }

    protected override void Update()
    {
        base.Update();

        if (canAttack && GameManager.instance.boss2Dead) {
            StartCoroutine(Attack());
        } 
    }

    bool spin = false;

    void FixedUpdate()
        {
        //     if (!inRange) {
        //     return;
        // }

        

        if (attacking || splitUp) {
            return;
        }

        Vector2 direction = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Smoothly rotate towards the target angle over time
        float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, 10 * Time.fixedDeltaTime * GameManager.instance.enemySpeedModifier / 4);
        
        rb.MoveRotation(smoothAngle);
    
    }

    bool alternate = true;

    public ParticleSystem chargeUp;

    public 

    bool attacking;

    public GameObject targetBeam, beam;

    public float chargeTime = 3f, pauseTime = 1f, attackTime = 2f, fadeTime = 1f, extraTime = 1f;

    public AudioClip[] chargeFire;

    IEnumerator Attack() {
        SpriteRenderer targetRenderer = targetBeam.GetComponent<SpriteRenderer>();
        SpriteRenderer beamRenderer = beam.GetComponent<SpriteRenderer>();



        canAttack = false;

        
            yield return new WaitForSeconds(extraTime);
            
        
            if (splitUp) {
                targetBeam.SetActive(false);
                beam.SetActive(false);
                yield break;
            }

            
            yield return new WaitForSeconds(0.2f);
            
            targetBeam.SetActive(true);
            AudioManager.instance.PlaySFX(chargeFire, 0.5f);
            StartCoroutine(FadeSprite(targetRenderer, 0f, 1f, chargeTime)); // Fade in target
            chargeUp.Play();
            
            yield return new WaitForSeconds(chargeTime);
            //if (splitUp) break;

            chargeUp.Stop();
            attacking = true;

            StartCoroutine(FadeSprite(targetRenderer, 1f, 0f, pauseTime)); // Fade out target
            yield return new WaitForSeconds(pauseTime);
            //if (splitUp) break;

            targetBeam.SetActive(false);

            AudioManager.instance.PlaySFX(beamFire, 0.6f);
            beam.SetActive(true);
            StartCoroutine(FadeSprite(beamRenderer, 0f, 1f, attackTime / 20)); // Fade in beam

            yield return new WaitForSeconds(attackTime);
            //if (splitUp) break;

            StartCoroutine(FadeSprite(beamRenderer, 1f, 0f, fadeTime)); // Fade out beam
            yield return new WaitForSeconds(fadeTime);
            //if (splitUp) break;

            beam.SetActive(false);
            attacking = false;

            //yield return new WaitForSeconds(extraTime);
            //if (splitUp) break;

            canAttack = true;
        
    }

    IEnumerator FadeSprite(SpriteRenderer sprite, float startAlpha, float endAlpha, float duration) {
        float time = 0f;
        Color color = sprite.color;
        while (time < duration) {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            sprite.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }
}
