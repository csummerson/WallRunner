using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    bool canAttack = true;

    public AudioClip sound;

    public Transform[] cardinals;
    public Transform[] interCardinals;

    public float spinCooldown = 2f;
    public float spinDuration = 2f;
    public float spinSpeed = 1f;

    Health hhealth;
    

    protected override void Start()
    {
        base.Start();
        //active = false;
        StartCoroutine(SpinRoutine());
        hhealth = GetComponent<Health>();
        hhealth.OnDeath += TellDeath;
        GameManager.instance.boss2Dead = false;
    }

    private void TellDeath() {
        GameManager.instance.boss2Dead = true;
    }

    protected override void Update()
    {
        base.Update();

        if (canAttack && inRange) {
            StartCoroutine(Attack());
        }
    }

    bool spin = false;

    void FixedUpdate()
        {
            if (!inRange) {
            return;
        }

        if (!spin) {

            Vector2 direction = player.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, 5.5f * Time.fixedDeltaTime);
            
            rb.MoveRotation(smoothAngle);
        } else {
            float newAngle;
            
            if (spinDir == 1) {
                newAngle = rb.rotation + spinSpeed * Time.fixedDeltaTime * 50;
            } else {
                newAngle = rb.rotation - spinSpeed * Time.fixedDeltaTime * 50;
            }
            
            
            
            rb.MoveRotation(newAngle);
            return;
        }
    }

    int spinDir = 1;

    IEnumerator SpinRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spinCooldown, spinCooldown + 3f)); // Adds randomness to the spin intervals
            spinDir = Random.Range(0, 2) * 2 - 1;
            cards = Random.value > 0.5f;
            spin = true;
            yield return new WaitForSeconds(spinDuration);
            spin = false;
        }
    }

    bool alternate = true;
    bool cards = false;

    IEnumerator Attack() {
        canAttack = false;
        
        float modifier = 3 * Mathf.Log10(Mathf.Pow(GameManager.instance.enemyShootModifier, 2) + 1);

        if (spin) {
            modifier *= 2;
        }

        yield return new WaitForSeconds(shootCoolDown / modifier);

        canAttack = true;
        
        //Debug.Log("I was called");

        if (spin) {
            //bool cards = Random.value > 0.5f;
            if (cards) {
                foreach (var t in  cardinals) {
                    GameObject bullet = Instantiate(bulletPrefab, t.position, t.rotation);
                    BulletController bulletCon = bullet.GetComponent<BulletController>();
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    bulletCon.damageValue = damage;
                    alternate = false;
                }
            } else {
                foreach (var t in interCardinals) {
                    GameObject bullet = Instantiate(bulletPrefab, t.position, t.rotation);
                    BulletController bulletCon = bullet.GetComponent<BulletController>();
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    bulletCon.damageValue = damage;
                    alternate = true;
                }
            }
        } else if (alternate) {
            foreach (var t in  cardinals) {
                GameObject bullet = Instantiate(bulletPrefab, t.position, t.rotation);
                BulletController bulletCon = bullet.GetComponent<BulletController>();
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                bulletCon.damageValue = damage;
                alternate = false;
            }
        } else {
            foreach (var t in interCardinals) {
                GameObject bullet = Instantiate(bulletPrefab, t.position, t.rotation);
                BulletController bulletCon = bullet.GetComponent<BulletController>();
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                bulletCon.damageValue = damage;
                alternate = true;
            }
        }
        
        AudioManager.instance.PlaySFX(sound, 0.5f);
    }
}
