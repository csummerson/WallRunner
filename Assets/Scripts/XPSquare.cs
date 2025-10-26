using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSquare : MonoBehaviour
{
    public float xpValue = 1;
    // float pulseSpeed, maxEmissionIntensity;

    Material mat;

    Color colorStart;

    public GameObject xpEffect;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        colorStart = mat.color;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 animation = new Vector3(0, 0, xpValue * Time.deltaTime * 50);
        transform.Rotate(animation);

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 5) {
            float moveSpeed = Mathf.Clamp(3f / distance, 0, 4f);  

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public AudioClip[] sounds;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Instantiate(xpEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
            XPSystem xPSystem = collision.gameObject.GetComponent<XPSystem>();
            xPSystem.AddXPPoints(xpValue);

            
            AudioManager.instance.PlaySFX(sounds);

        }   
    }

    
}
