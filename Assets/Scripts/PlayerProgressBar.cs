using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressBar : MonoBehaviour
{
    Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
    }

    void Update() {
        slider.maxValue = GameManager.instance.maxRooms;

        
        slider.value = Utilities.Damp(slider.value, GameManager.instance.currentRoom, 0.25f, Time.deltaTime);
    }
}
