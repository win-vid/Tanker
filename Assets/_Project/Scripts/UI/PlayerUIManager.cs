using TMPro;
using UnityEngine;

// A Singleton to set Text in the players ui
public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _waveText;
    [SerializeField] TextMeshProUGUI _healthText;

    void Awake()
    {
        instance = this;
    }

    public void SetWaveText(int wave)
    {
        if(_waveText == null) return;
        _waveText.text = wave.ToString() + ". WAVE";
    } 

    public void SetScoreText(int score)
    {
        if(_scoreText == null) return;
        _scoreText.text = score.ToString();
    } 

    public void SetHealthText(int hp)
    {
        if(_scoreText == null) return;
        _healthText.text = hp.ToString() + " %";
    } 
}
