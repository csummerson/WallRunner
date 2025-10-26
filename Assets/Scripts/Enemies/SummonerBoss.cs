using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBoss : Enemy
{
    bool canAttack = true;

    public AudioClip sound;

    public Transform[] shooters;
    public Transform[] summoners;

    public GameObject summoned;

    public float summonCoolDown;

    //public float spinSpeed = 1;

    protected override void Start()
    {
        base.Start();
        active = false;
        StartCoroutine(Summon());
    }

    protected override void Update()
    {
        base.Update();

        if (canAttack && inRange) {
            StartCoroutine(Attack());
        }
    }

    //bool spin = false;

    void FixedUpdate() {
        if (!inRange) {
            // float newAngle = rb.rotation + spinSpeed * Time.fixedDeltaTime * 50;
            // rb.MoveRotation(newAngle);
            return;
        } else if (!summoning) {
            Vector2 direction = player.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, 3f * Time.fixedDeltaTime);
            
            rb.MoveRotation(smoothAngle);
        } else {
            Vector2 direction = transform.position - player.transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, 3f * Time.fixedDeltaTime);
            
            rb.MoveRotation(smoothAngle);
        }
    }


    IEnumerator Attack() {
        canAttack = false;

        float modifier = 3 * Mathf.Log10(Mathf.Pow(GameManager.instance.enemyShootModifier, 2) + 1);

        yield return new WaitForSeconds(shootCoolDown / modifier);

        canAttack = true;
        
        if (!summoning) {
            foreach (var t in  shooters) {
                GameObject bullet = Instantiate(bulletPrefab, t.position, t.rotation);
                BulletController bulletCon = bullet.GetComponent<BulletController>();
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                bulletCon.damageValue = damage;
            }   
            
                AudioManager.instance.PlaySFX(sound, 0.5f);
        }
    }

    bool summoning = false;

    IEnumerator Summon() {
        while (true) {
            float modifier = 3 * Mathf.Log10(Mathf.Pow(GameManager.instance.enemyShootModifier, 2) + 1);
            yield return new WaitForSeconds(0.5f * summonCoolDown / modifier);
            summoning = true;
            yield return new WaitForSeconds(0.2f * summonCoolDown / modifier);
            if (active) {
               
                    
                    GameObject summon = Instantiate(summoned, summoners[0].position, Quaternion.Inverse(summoners[0].rotation));
                    summon.GetComponent<Rigidbody2D>().AddForce(summon.transform.up * 15, ForceMode2D.Impulse);
                
                    summon = Instantiate(summoned, summoners[1].position, Quaternion.Inverse(summoners[1].rotation));
                    summon.GetComponent<Rigidbody2D>().AddForce(summon.transform.up * 15, ForceMode2D.Impulse);
                
            }
            yield return new WaitForSeconds(0.3f * summonCoolDown / modifier);
            summoning = false;
            yield return new WaitForSeconds(0.5f * summonCoolDown / modifier);
        }
    }
}
