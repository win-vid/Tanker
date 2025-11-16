using TMPro;
using UnityEngine;

// sets and displays current wave text
public class UIWaveManager : MonoBehaviour
{
    [SerializeField] GameObject waveUICanvas;
    TextMeshProUGUI waveText;
    int currentWave;
    [SerializeField] float displayTime = 3f;
    float currentDisplayTime;

    void Start()
    {
        if (waveUICanvas == null)
        {
            Debug.LogError("Wave UI Canvas is not assigned in the inspector.");
            return;
        }

        waveText = waveUICanvas.GetComponentInChildren<TextMeshProUGUI>();
        if (waveText == null)
        {
            Debug.LogError("TextMeshPro component not found in children of the Wave UI Canvas.");
            return;
        }

        currentDisplayTime = displayTime;
    }

    void Update()
    {
        UpdateCanvas();
    }

    public void Display()
    {
        waveUICanvas.SetActive(true);
    }

    public void Hide()
    {
        waveUICanvas.SetActive(false);
    }

    public void setCurrentWave(int wave)
    {
        currentWave = wave;
        waveText.text = currentWave.ToString() + ". WAVE";
    }

    void UpdateCanvas()
    {
        if (waveUICanvas != null && waveText != null)
        {
            if (waveUICanvas.activeSelf)
            {
                currentDisplayTime -= Time.deltaTime;
                if (currentDisplayTime <= 0f)
                {
                    Hide();
                    currentDisplayTime = displayTime;
                }
            }
        }
    }
    
    public float getDisplayTime()
    {
        return displayTime;
    }
}