using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public PauseUI instance;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && GameManager.instance.gameState != GameManager.GameState.Pause) {
            GameManager.instance.ChangeGameState(GameManager.GameState.Pause);
        } else if (Input.GetKeyDown("escape")) {
            GameManager.instance.ChangeGameState(GameManager.GameState.Play);
        }
    }
}
