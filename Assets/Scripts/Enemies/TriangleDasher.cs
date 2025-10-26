using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriangleDasher : Enemy
{
    bool canAttack = true;

    public float followDistance, chargeDistance, secondCooldown;
    public float dashForce;

    public ParticleSystem charge;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // if (!active || !inRange) {
        //     return;
        // }

        if (canAttack && Vector3.Distance(player.transform.position, transform.position) <= chargeDistance) {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
        {
            if (!inRange || !active) {
            return;
        }

        Vector2 direction = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, 10 * Time.fixedDeltaTime);
        
        rb.MoveRotation(smoothAngle);

        if (!canAttack) {
            return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) >= followDistance) {
            Vector2 moveDir = (player.transform.position - transform.position).normalized;

            rb.velocity = moveDir * speed * GameManager.instance.enemySpeedModifier;
        } 
    }

    public AudioClip dash, chargeUp;

    IEnumerator Attack() {
        canAttack = false;

        charge.Play();

        AudioManager.instance.PlaySFX(chargeUp);

        float modifier = 3 * Mathf.Log10(Mathf.Pow(GameManager.instance.enemyShootModifier, 2) + 1);

        yield return new WaitForSeconds(shootCoolDown / modifier);

        charge.Stop();

        rb.AddForce(transform.up * dashForce, ForceMode2D.Impulse);

        AudioManager.instance.PlaySFX(dash);
        
        yield return new WaitForSeconds(secondCooldown / modifier);
        
        canAttack = true;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Health>().ChangeHealth(-damage);
        }
    }
}
