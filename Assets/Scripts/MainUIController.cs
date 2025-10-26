using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    bool otherUIInplace = false;

    public float transitionSpeed = 2f;

    public Mover mainMenu;

    public Mover leaderBoard;
    
    public void MoveMeIn(Mover mover) { 
        mainMenu.inPlace = !mainMenu.inPlace;
        mover.inPlace = !mover.inPlace;
        otherUIInplace = !otherUIInplace;
    }

    public Image background;

    void Start()
    {
        if (GameManager.instance.endRun) {
            ShowLeaderBoard();
        }
    }

    void Update()
    {
        if (otherUIInplace) {
            Color color = background.color;
            color.a = Mathf.Lerp(color.a, 0.9f, transitionSpeed * Time.deltaTime);
            background.color = color;
        } else {
            Color color = background.color;
            color.a = Mathf.Lerp(color.a, 0, transitionSpeed * Time.deltaTime);
            background.color = color;
        }

        if (quitGame) {
            Color color = background.color;
            color.a += transitionSpeed * Time.deltaTime * 2;
            background.color = color;
            if (color.a >= 1) {
                StartCoroutine(GameManager.instance.QuitGame(1));
            }
        }

        if (playGame) {
            Color color = background.color;
            color.a += 1 * Time.deltaTime * 2;
            background.color = color;
            if (color.a >= 1) {
                StartCoroutine(GameManager.instance.StartGame());
            }
        }
    }

    bool quitGame = false, playGame = false;

    public void QuitGame() {
        quitGame = true;
    }

    public void PlayGame() {
        playGame = true;
        leaderBoard.inPlace = false;
    }

    public void ShowLeaderBoard() {
        mainMenu.inPlace = false;
        leaderBoard.inPlace = true;
        otherUIInplace = true;
    }

    public void GoBack() {
        mainMenu.inPlace = true;
        leaderBoard.inPlace = false;
        otherUIInplace = false;
    }
    
}
