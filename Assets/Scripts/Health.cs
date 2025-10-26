using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;

    public bool disableOnDeath = false;

    public float currentHealth;

    public event Action OnDeath;

    public bool ignoreColissions = false;

    
    public GameObject deathVFX;

    public SpriteRenderer[] sprites;
    public Color hitColor;
    List<Color> initialColors = new List<Color>();

    protected virtual void Start() {
        foreach (var image in sprites) {
            initialColors.Add(image.color);
        }
        
        
        if (transform.gameObject.tag == "Player") {
            maxHealth += GameManager.instance.playerHealthModifier;
            currentHealth = maxHealth;
        } else {
            maxHealth += GameManager.instance.enemyHealthModifier;
            currentHealth = maxHealth;
        }
    
    }

    public virtual void ChangeHealth(float amount) {
        if (ignoreColissions) {
            return;
        }
        
        currentHealth += amount;

        StartCoroutine(DamageFlash());

        if (currentHealth < 1) {
            currentHealth = 0;
            
            if (deathVFX != null) {
                Instantiate(deathVFX, transform.position, Quaternion.identity);
            }
            
            OnDeath?.Invoke();
            if (!disableOnDeath) {
                Destroy(gameObject);
            }
        }
    }

    public virtual void ChangeMaxHealth(float amount) {
        float percentage = currentHealth / maxHealth;
        maxHealth += amount;
        currentHealth = maxHealth * percentage;
    }

    protected IEnumerator DamageFlash() {
        foreach (var image in sprites) {
            image.color = hitColor;
        }

        yield return new WaitForSeconds(0.10f);

        for (int i = 0; i < initialColors.Count; i++) {
            sprites[i].color = initialColors[i];
        }
    }
}
