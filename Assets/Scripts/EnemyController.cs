using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1f, coolDown = 2, xpValue = 1;

    public GameObject player, bulletPrefab, xpSquarePrefab;

    public int damage, detectionRadius = 10;

    

    bool canAttack = true, inRange = false;

    Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<Health>();
        health.OnDeath += EnemyDeath;
    }

    void Update()
    {
        inRange = Vector3.Distance(player.transform.position, transform.position) < detectionRadius;
        
        if (canAttack && inRange) {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        if (!inRange) {
            return;
        }
        
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }

    IEnumerator Attack() {
        canAttack = false;
        
        yield return new WaitForSeconds(coolDown / GameManager.instance.enemyShootModifier);

        canAttack = true;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        BulletController bulletCon = bullet.GetComponent<BulletController>();
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletCon.damageValue = damage;
    }

    void EnemyDeath() {
        GameObject xp = Instantiate(xpSquarePrefab, transform.position, Quaternion.identity);
        xp.GetComponent<XPSquare>().xpValue = xpValue;
    }
}
