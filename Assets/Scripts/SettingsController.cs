using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider sfxSlider, musicSlider;
    public TextMeshProUGUI sfxText, musicText;

    public float sfxVolume, musicVolume;

    void Start()
    {
        sfxVolume = AudioManager.instance.sfxVolume * sfxSlider.maxValue;
        musicVolume = AudioManager.instance.musicVolume  * musicSlider.maxValue;


        sfxSlider.value = sfxVolume;
        sfxText.text = "" + sfxSlider.value;

        musicSlider.value = musicVolume;
        musicText.text = "" + musicSlider.value;
    }

    // Update is called once per frame

    public void SFXSliderUpdate() {
        sfxVolume = sfxSlider.value / sfxSlider.maxValue;
        sfxText.text = "" + sfxSlider.value;
        AudioManager.instance.sfxVolume = sfxVolume;
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonHovers, 0.3f);
    }

    public void MusicSliderUpdate() {
        musicVolume = musicSlider.value / musicSlider.maxValue;
        musicText.text = "" + musicSlider.value;
        AudioManager.instance.musicVolume = musicVolume;
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonHovers, 0.3f * musicVolume);
    }
}
