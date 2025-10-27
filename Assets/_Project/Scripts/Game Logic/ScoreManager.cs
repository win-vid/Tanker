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
        UIScoreBoard.SetActive(false);
    }

    public void AddScore(int points)
    {
        _score += points;
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
        UIScoreBoard.SetActive(true);
        highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore").ToString();
        currentScoreText.text = "SCORE: " + _score.ToString();
    }
}
