using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTurret : Enemy
{
    bool canAttack = true;

    public float followDistance;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (canAttack && inRange) {
            StartCoroutine(Attack());
        }
    }

    public AudioClip sound;

    void FixedUpdate()
        {
            if (!inRange) {
            return;
        }

        Vector2 direction = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, 10 * Time.fixedDeltaTime);
        
        rb.MoveRotation(smoothAngle);

        if (Vector3.Distance(player.transform.position, transform.position) >= followDistance) {
            Vector2 moveDir = (player.transform.position - transform.position).normalized;

            rb.velocity = moveDir * speed * GameManager.instance.enemySpeedModifier;
        }
    }

    IEnumerator Attack() {
        canAttack = false;

        float modifier = 3 * Mathf.Log10(Mathf.Pow(GameManager.instance.enemyShootModifier, 2) + 1);

        yield return new WaitForSeconds(shootCoolDown / modifier);

        canAttack = true;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        BulletController bulletCon = bullet.GetComponent<BulletController>();
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletCon.damageValue = damage;

        AudioManager.instance.PlaySFX(sound, 0.5f);
    }
}
