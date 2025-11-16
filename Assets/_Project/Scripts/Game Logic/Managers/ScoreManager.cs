using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

// Singleton Score Manager to handle player scores
public class ScoreManager : MonoBehaviour
    {
    public static ScoreManager instance;
    int _score;
    [SerializeField] GameObject UIScoreBoard;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;

    void Awake()
    {
        instance = this;

        if(UIScoreBoard == null)
        {
            Debug.LogError("ScoreManager: UIScoreBoard reference is missing!");
            return;
        }

        UIScoreBoard.SetActive(false);
    }

    // Adds points to score and updates the score in the players ui
    public void AddScore(int points)
    {
        _score += points;
        PlayerUIManager.instance.SetScoreText(_score);
    }

    void compareScore()
    {

        if (PlayerPrefs.GetInt("HighScore") < _score)
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }
    }
    
    public void updateScore()
    {
        compareScore();
        if (UIScoreBoard == null || currentScoreText == null || highScoreText == null) { Debug.LogError("ScoreManager: One or more UI references are missing!"); return; }
        UIScoreBoard.SetActive(true);
        highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore").ToString();
        currentScoreText.text = "SCORE: " + _score.ToString();
    }
}
