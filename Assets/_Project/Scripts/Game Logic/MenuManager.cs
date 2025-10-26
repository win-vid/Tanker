using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // load main scene
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Playground");
    }
}
