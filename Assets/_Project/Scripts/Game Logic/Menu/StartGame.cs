using UnityEngine;

// Starts the wave manager when the player projectile collides with this object
public class StartGame : MonoBehaviour
{
    WaveManager waveManager;

    [Header("Debug")]
    public bool isRequired = true;      // deactivating will start the game immediately

    void Awake()
    {
        if (!isRequired)
        {
            startGame();
            return;
        }
        
        waveManager = FindFirstObjectByType<WaveManager>();
        waveManager.gameObject.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            other.gameObject.GetComponent<Projectile>().onImpact();
            startGame();
        }
    }
    
    void startGame()
    {
        waveManager.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
