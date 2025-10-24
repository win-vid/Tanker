using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]GameObject pauseMenuUI;
    bool isPaused = false;

    // Pause game when escape is pressed
    // resume game when left mouse button is clicked
    // exit to menu if escape is pressed again
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) Pause();
        else if (Input.GetMouseButtonDown(0) && isPaused) Resume();
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) ExitToMenu();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void ExitToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        isPaused = false;
        pauseMenuUI.SetActive(false);
    }
}
