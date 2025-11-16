using UnityEngine;

// this singleton manages restarting the current level if "R" is pressed and also processes which game mode is currently active

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager instance;
    public GameMode gameMode = GameMode.TOPDOWN;    // current game mode
    [Header("Top Down Camera")]
    public Camera cam1;     // Top Down Camera
    [Header("FPS Camera")]
    public Camera cam2;     // FPS Camera

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        CheckInput();
    }

    // Check Players Key-Input
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchGameMode();
        }
    }

    // Switch between diffrent game mode
    // TODO: Scaling problem
    void SwitchGameMode()
    {
        switch (gameMode)
        {
            case GameMode.TOPDOWN:
                gameMode = GameMode.FPS;
                cam1.gameObject.SetActive(false);
                cam2.gameObject.SetActive(true);
                break;
            case GameMode.FPS:
                gameMode = GameMode.TOPDOWN;
                cam2.gameObject.SetActive(false);
                cam1.gameObject.SetActive(true);
                break;
        }
    }
}
