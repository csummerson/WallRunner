using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Player Modifiers")]

    public float playerSpeedModifier = 1;
    public float playerShootModifier = 1, playerHealthModifier = 1;
    
    
    public float playerLevel = 1;


    
    [Header("Enemy Modifiers")]

    public float enemySpeedModifier = 1;
    public float enemyShootModifier = 1, enemyHealthModifier = 1;
    public float enemeyLevel = 1, enemeyShields = 0;

    [Header("Fracture Modifiers")]

    public float fractureSpeedModifer = 1;


    [HideInInspector]
    public int currentRoom = 0, maxRooms, deathRoom = 0;

    GameObject player;

    public float runTime = 0f;

    public enum GameState {
        Play,
        LevelUp,
        Pause,
        Dead,
        Reload,
        Win
    }

    public ModifierScriptableObject multipliers;

    public List<ModifierObject> enemeyModifiers;

    public GameState gameState { get; private set; } // everything else has to use the ChangeGameState function

    public List<Health> enemeyHealths;

    [HideInInspector]
    public float xpCurrent = 0, xpMax = 3, deaths = 0;
    

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            ///Debug.LogWarning("Two Game Managers in Scence!");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        enemeyHealths = new List<Health>();
    }

    void Start()
    {
        ChangeGameState(GameState.Play); 
    }

    public bool playing = false;

    public bool goober = false;

    void Update()
    {
        if (goober == false && deaths >= 100)
        {
            SceneManager.LoadScene("Maxwell");
        }

        if (gameState == GameState.Play && playing)
        {
            runTime += Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(runTime / 60);
        int seconds = Mathf.FloorToInt(runTime % 60);
        int milliseconds = Mathf.FloorToInt((runTime * 100) % 100);

        runTimeText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }


    public string runTimeText;


    public event Action levelUp;

    public void ChangeGameState(GameState state) {
        gameState = state;
        
        switch (state) {
            case GameState.Play:
                Time.timeScale = 1;
                break;
            case GameState.LevelUp:
                Time.timeScale = 0;
                LevelUpFinished = false;
                playerLevel++;
                levelUp?.Invoke();
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.Dead:
                deaths++;
                break;
            case GameState.Reload:
                SceneManager.LoadScene("Dungeon Generator");
                ChangeGameState(GameState.Play);
                break;
            case GameState.Win:
                //Time.timeScale = 0;
                ScoreManager.instance.EndRun((int)deaths, runTimeText);
                playing = false;
                endRun = true;
                fader.fadeOut = true;
                StartCoroutine(WinGame());
                break;
        }
    }

    public ManagerFader fader;

    IEnumerator WinGame() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
        yield return new WaitForSeconds(1);
        fader.fadeOut = false;
    }

    //[HideInInspector]
    public bool boss2Dead = false;

    public IEnumerator StartGame() {
        yield return new WaitForSeconds(0.6f);
        
        SceneManager.LoadScene("Teaching Room");

        Destroy(gameObject);
    }

    public bool endRun = false;

    public bool LevelUpFinished = true;

    public void ChangeEnemeyHealth(float num) {
        foreach (var enemy in enemeyHealths) {
            if (enemy != null) {
                enemy.ChangeMaxHealth(num);
            }
        }
    }

    public void ChangePlayerHealth(float num) {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Health>().ChangeMaxHealth(num);   
    }
    
    public IEnumerator QuitGame(float i) {
        yield return new WaitForSeconds(i);
        Debug.Log("Quit the Game");
        Application.Quit();
    }

    public void JustQuit() {
        Application.Quit();
    }

    public IEnumerator LoadDelayed(float i) {
        yield return new WaitForSeconds(i);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator Reload(float i) {
        yield return new WaitForSeconds(i);
        ChangeGameState(GameState.Reload);
    }


}
