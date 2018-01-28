using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour {

    private string keyName = "highestScore";
    public Text highscoreText;
    public PlayerProgress playerProgress;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        
        
	}

    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }
	
	// Update is called once per frame
	void Update () {
        highscoreText.text = "Score: " + playerProgress.highestScore;
	}

    public int GetPlayerHighScore()
    {
        return playerProgress.highestScore;
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey(keyName))
            playerProgress.highestScore = PlayerPrefs.GetInt(keyName);
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt(keyName, playerProgress.highestScore);
        PlayerPrefs.Save();
    }
}
