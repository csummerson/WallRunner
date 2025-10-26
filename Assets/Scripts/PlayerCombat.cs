using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject bulletPrefab;
    public LayerMask layerMask;
    public float coolDown;
    bool canAttack = true;
    
    public int damage;
    public AudioClip shootSFX;

    public Transform pointer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canAttack && Time.timeScale != 0) {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack() {
        GameObject bullet = Instantiate(bulletPrefab, pointer.position, transform.rotation);
        BulletController bulletCon = bullet.GetComponent<BulletController>();
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletCon.damageValue = damage;

        AudioManager.instance.PlaySFX(shootSFX);

        canAttack = false;

        float modifier = 3 * Mathf.Log10(Mathf.Pow(GameManager.instance.playerShootModifier, 2) + 1);
        
        yield return new WaitForSeconds(coolDown / modifier);

        canAttack = true;
    }
}
