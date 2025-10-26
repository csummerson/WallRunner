using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHealth : Health
{
    new public event Action OnDeath;
    
    protected override void Start()
    {
        base.Start();
    }

    public override void ChangeHealth(float amount) {
        if (ignoreColissions) {
            return;
        }

        StartCoroutine(DamageFlash());
        
        currentHealth += amount;

        if (currentHealth < 1) {
            
            currentHealth = 0;
            OnDeath?.Invoke();
        }
    }

    public override void ChangeMaxHealth(float amount)
    {
        base.ChangeMaxHealth(amount);
    }
}
