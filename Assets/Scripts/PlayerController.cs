using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1f;
    Camera mainCamera;

    Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        health = GetComponent<Health>();
        health.OnDeath += OnPlayerDeath;
    }

    float x, y;
    Vector3 mousePosition;

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector2 moveDir = new Vector2(x, y).normalized;

        rb.velocity = moveDir * speed * GameManager.instance.playerSpeedModifier;

        Vector3 direction = mousePosition - transform.position;
    
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }

    void OnDrawGizmos()
    {
        if (mainCamera == null) return;
        
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, mousePosition);
    }

    public Fader fader;
    public ParticleSystem playerDeath;

    public AudioClip playerded;

    void OnPlayerDeath() {
        if (!IDIEDALREADYWHYDOCOROUTINESWORKLIKETHISIMSTRATINGTOLOOSEMYMINDAHHHHTHISGAMETAKESSOLONGTOMAKECAUSEIMBADSTILLRAHHH) {
            
            IDIEDALREADYWHYDOCOROUTINESWORKLIKETHISIMSTRATINGTOLOOSEMYMINDAHHHHTHISGAMETAKESSOLONGTOMAKECAUSEIMBADSTILLRAHHH = true;
            Instantiate(playerDeath, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX(playerded);
            
            
            fader.LetThemThink1();
            
            gameObject.SetActive(false);
        }

    }

    bool IDIEDALREADYWHYDOCOROUTINESWORKLIKETHISIMSTRATINGTOLOOSEMYMINDAHHHHTHISGAMETAKESSOLONGTOMAKECAUSEIMBADSTILLRAHHH = false;
}
