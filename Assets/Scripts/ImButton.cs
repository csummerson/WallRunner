using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImButton : MonoBehaviour, IPointerEnterHandler
{
    Button button;

    public bool bigBoiButton = false;
    
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }
    
    void OnButtonClicked() {
        if (bigBoiButton) {
            AudioManager.instance.PlaySFX(AudioManager.instance.bigBoiButton, 0.8f);
        } else {
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClicks, 0.9f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonHovers, 0.3f);
    }

}
