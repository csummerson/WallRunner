using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathProgressBar : MonoBehaviour
{
    Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
    }

    void Update() {
        slider.maxValue = GameManager.instance.maxRooms;
        
        slider.value = Utilities.Damp(slider.value, GameManager.instance.deathRoom, 0.25f, Time.deltaTime);
    }
}
