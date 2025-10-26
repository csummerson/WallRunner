using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;

public class LeaderBoardController : MonoBehaviour
{
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> deaths;
    public List<TextMeshProUGUI> times;

    string publicLeaderBoardKey = "068332ffd6d5bc77642816f282dfa42f3722ea67b91c1f73c3bfac29f282bdde";

    public GameObject playButton, submitButton, inputArea;

    public TextMeshProUGUI inputText, roundText;

    void Start()
    {
        GetLeaderBoard();

        if (GameManager.instance.gameState == GameManager.GameState.Win)
        {
            playButton.SetActive(false);
            submitButton.SetActive(true);
            inputArea.SetActive(true);
            roundText.text = GameManager.instance.deaths + " Deaths in " + GameManager.instance.runTimeText;
        }
        else
        {
            playButton.SetActive(true);
            submitButton.SetActive(false);
            inputArea.SetActive(false);
            roundText.text = "";
        }
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;

                int integerTime = msg[i].Score;

                int minutes = integerTime / 10000;
                int seconds = (integerTime / 100) % 100;
                int milliseconds = integerTime % 100;

                string runTimeText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

                times[i].text = runTimeText;
                deaths[i].text = msg[i].Extra;

                if (msg[i].IsMine())
                {
                    names[i].color = Color.yellow;
                    times[i].color = Color.yellow;
                    deaths[i].color = Color.yellow;
                }
                else
                {
                    names[i].color = Color.white;
                    times[i].color = Color.white;
                    deaths[i].color = Color.white;
                }
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score, string time)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey, username, score, time, ((msg) =>
        {
            GetLeaderBoard();
        }));
    }

    public TMP_InputField inputName;

    public void NamePass()
    {
        string text = inputName.text;

        float runTime = GameManager.instance.runTime;

        int minutes = Mathf.FloorToInt(runTime / 60);
        int seconds = Mathf.FloorToInt(runTime % 60);
        int milliseconds = Mathf.FloorToInt((runTime * 100) % 100);

        int integerTime = milliseconds + seconds * 100 + minutes * 10000;

        SetLeaderboardEntry(text, integerTime, GameManager.instance.deaths + "");

        DisableEntry();
    }

    public void DisableEntry()
    {
        playButton.SetActive(true);
        submitButton.SetActive(false);
        inputArea.SetActive(false);
    }


    // void Start()
    // {
    //     runDeaths = ScoreManager.instance.GetRunDeaths();
    //     runTimes = ScoreManager.instance.GetRunTimes();

    //     List<RunData> runDatas = new List<RunData>();

    //     for (int i = 0; i < runDeaths.Length; i++) {
    //         if (runDeaths[i] == 99) {
    //             runDatas.Add(new RunData(0, runDeaths[i], runTimes[i]));
    //         } else {
    //             runDatas.Add(new RunData(i+1, runDeaths[i], runTimes[i]));
    //         }   
    //     }

    //     runDatas.Sort((a, b) =>
    //     {
    //         int deathComparison = a.deaths.CompareTo(b.deaths);
    //         if (deathComparison == 0)
    //         {
    //             return a.time.CompareTo(b.time);
    //         }
    //         return deathComparison;
    //     });

    //     for (int i = 0; i < 5; i++)
    //     {
    //         if (i < runDatas.Count)  // Ensure we don't go out of bounds
    //         {
    //             var run = runDatas[i];
    //             texts[i].text = $"{run.runIndex}  -  {run.deaths}  -  {run.time}";
    //         } else {
    //             texts[i].text = $"?  -  ??  -  ??:??:??";
    //         }
    //     }
    // }

    // private class RunData {
    //     public int runIndex;
    //     public int deaths;
    //     public string time;

    //     public RunData(int runIndex, int deaths, string time) {
    //         this.runIndex = runIndex;
    //         this.deaths = deaths;
    //         this.time = time;
    //     }
    // }
}

