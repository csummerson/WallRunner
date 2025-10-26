using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public float sfxVolume = 1, musicVolume = 1;

    public AudioClip[] buttonHovers, buttonClicks, bigBoiButton;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        musicSource.clip = music;
        musicSource.Play();
    }

    public float deadSpeed = 0.5f;
    

    void Update() {
        sfxSource.volume = sfxVolume;
        musicSource.volume = musicVolume;

        if (GameManager.instance.gameState == GameManager.GameState.Dead) {
            musicSource.pitch = Utilities.Damp(musicSource.pitch, deadSpeed, .15f, Time.unscaledDeltaTime);
        } else {
            musicSource.pitch = Utilities.Damp(musicSource.pitch, 1, .15f, Time.unscaledDeltaTime);
        }
    }

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip music;

    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float f) {
        sfxSource.PlayOneShot(clip, f * sfxVolume);
    }

    public void PlaySFX(AudioClip[] clips) {
        var chosen = clips[Random.Range(0, clips.Length)];
        sfxSource.PlayOneShot(chosen);
    }

    public void PlaySFX(AudioClip[] clips, float f) {
        var chosen = clips[Random.Range(0, clips.Length)];
        sfxSource.PlayOneShot(chosen, f * sfxVolume);
    }
}
