using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniDasher : Enemy
{
    bool canAttack = true;

    public float followDistance, chargeDistance, secondCooldown;
    public float dashForce;

    public ParticleSystem charge;

    public AudioClip dash, chargeUp;

    protected override void Start()
    {
        base.Start();
        active = true;
        StartCoroutine(WaitASecond());
    }
    
    bool move = false;

    IEnumerator WaitASecond() {
        yield return new WaitForSeconds(1);
        move = true;
    }

    protected override void Update()
    {
        base.Update();

        if (canAttack && Vector3.Distance(player.transform.position, transform.position) <= chargeDistance) {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
        {
            if (!inRange || !move) {
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Health>().ChangeHealth(-damage);
        }
    }
}
