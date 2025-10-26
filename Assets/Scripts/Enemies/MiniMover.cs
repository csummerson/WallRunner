using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMover : Enemy
{
    bool canAttack = true;

    public float followDistance;

    public float spawnForce = 10;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitASecond());
        float added = Random.Range(5f, 100f);
        rb.AddForce(transform.up * (spawnForce + added), ForceMode2D.Impulse);
    }

    bool startAttack = false;

    IEnumerator WaitASecond() {
        yield return new WaitForSeconds(1);
        startAttack = true;
    }

    protected override void Update()
    {
        base.Update();

        if (canAttack && inRange) {
            StartCoroutine(Attack());
        }
    }

    public AudioClip sound;

    public void ReturnHome(Vector3 home) {
        startAttack = false;
        goHome = true;
        target = home;
        GetComponent<Health>().ignoreColissions = true;
    }

    Vector3 target;

    bool goHome = false;

    public bool hasReturnedHome = false;

    void FixedUpdate()
    {
        if (goHome) {
            Vector2 moveDir = (target - transform.position).normalized;

            rb.velocity = moveDir * 5;

            Vector2 ddirection = target - transform.position;
            float ttargetAngle = Mathf.Atan2(ddirection.y, ddirection.x) * Mathf.Rad2Deg - 90f;

            float ssmoothAngle = Mathf.LerpAngle(rb.rotation, ttargetAngle, 10 * Time.fixedDeltaTime);
        
            rb.MoveRotation(ssmoothAngle);

            if (Vector3.Distance(target, transform.position) <= 0.2f) {
                hasReturnedHome = true;
                Destroy(gameObject);
            }
        }
        
        
        
        if (!inRange || !startAttack) {
            return;
        }

        Vector2 direction = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, 10 * Time.fixedDeltaTime);
        
        rb.MoveRotation(smoothAngle);
    }

    IEnumerator Attack() {
        canAttack = false;
        
        float modifier = 3 * Mathf.Log10(Mathf.Pow(GameManager.instance.enemyShootModifier, 2) + 1);

        yield return new WaitForSeconds(shootCoolDown / modifier);

        canAttack = true;

        if (startAttack) {

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            BulletController bulletCon = bullet.GetComponent<BulletController>();
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bulletCon.damageValue = damage;

            AudioManager.instance.PlaySFX(sound, 0.5f);
        }
    }
}
