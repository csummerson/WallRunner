using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIController : MonoBehaviour
{
    public static PauseUIController instance;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        pauseUI.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public GameObject pauseUI;

    bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            if (paused) {
                paused = false;
                pauseUI.SetActive(false);
                GameManager.instance.ChangeGameState(GameManager.GameState.Play);
            } else {
                paused = true;
                pauseUI.SetActive(true);
                GameManager.instance.ChangeGameState(GameManager.GameState.Pause);
            }
        }
    }

    public void QuitTheGame() {
        Application.Quit();
    }
}
