using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour {

    private PlayerProgress playerProgress;
    private string keyName = "highestScore";
    public Text highScoreText;


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        LoadPlayerProgress();
        highScoreText.text = playerProgress.highestScore.ToString();
    }



    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

 
    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey(keyName))
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt(keyName, playerProgress.highestScore);
    }
}
