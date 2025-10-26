using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    public Health healthRef;

    public TextMeshProUGUI txt;

    void Start() {
        slider = GetComponent<Slider>();
        slider.maxValue = healthRef.maxHealth;
        slider.value = healthRef.maxHealth;
    }

    void Update() {
        slider.maxValue = healthRef.maxHealth;
        slider.value = Utilities.Damp(slider.value, healthRef.currentHealth, .15f, Time.unscaledDeltaTime);

        txt.text = $"{(int)healthRef.currentHealth}/{healthRef.maxHealth}";
    }
}
