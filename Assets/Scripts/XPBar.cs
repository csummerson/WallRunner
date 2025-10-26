using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    Slider slider;
    public XPSystem xp;

    public TextMeshProUGUI txt;

    void Start() {
        slider = GetComponent<Slider>();
    }

    void Update() {
        slider.maxValue = GameManager.instance.xpMax;
        slider.value = Utilities.Damp(slider.value, GameManager.instance.xpCurrent, .15f, Time.unscaledDeltaTime);

        txt.text = $"{GameManager.instance.xpCurrent}/{GameManager.instance.xpMax}";
    }
}
