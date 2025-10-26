using UnityEngine;

public class ScoreManager : MonoBehaviour
{
 
    private int[] runDeaths = new int[5];
    private string[] runTimes = new string[5];
    private int currentRunIndex = 0;

    private const string DeathsKey = "RunDeaths";
    private const string TimesKey = "RunTimes";
    private const string RunIndexKey = "RunIndex";

    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        LoadData();
    }

    private void LoadData()
    {
        for (int i = 0; i < 5; i++)
        {
            runDeaths[i] = PlayerPrefs.GetInt(DeathsKey + i, 99);  
            runTimes[i] = PlayerPrefs.GetString(TimesKey + i, "99:99:99");  
        }

        currentRunIndex = PlayerPrefs.GetInt(RunIndexKey, 0); 
    }

    private void SaveData()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(DeathsKey + i, runDeaths[i]);
            PlayerPrefs.SetString(TimesKey + i, runTimes[i]);
        }

        PlayerPrefs.SetInt(RunIndexKey, currentRunIndex);
        PlayerPrefs.Save();
    }

    public void EndRun(int deaths, string timeTaken)
    {
        runDeaths[currentRunIndex] = deaths;
        runTimes[currentRunIndex] = timeTaken;

        currentRunIndex = (currentRunIndex + 1) % 5;

        SaveData();

        Debug.Log("Run " + (currentRunIndex) + ": Deaths = " + deaths + ", Time = " + timeTaken);
    }

    public int[] GetRunDeaths() => runDeaths;
    public string[] GetRunTimes() => runTimes;
}
