using UnityEngine;

public class PowerUpGameManager : MonoBehaviour
{
    public static PowerUpGameManager instance; 
    [SerializeField] PowerUpManagerPrefab[] powerUpPrefab;
    float spawnInterval = 10f;

    [System.Serializable]
    class PowerUpManagerPrefab
    {
        public ObjectPool.PoolType poolType;
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        /*
        // Legacy System
        if (Time.time % spawnInterval < Time.deltaTime)
        {
            SpawnPowerUp();
        }
        */
    }

    Vector3 getRandomPosition() // Legacy
    {
        Vector3 position = Vector3.zero;
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera not found!");
            return position;
        }

        float spawnDistance = 30f; // distance from the camera to spawn enemies
        float angle = Random.Range(0, 360);
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
        position = mainCamera.transform.position + direction * spawnDistance;
        position.y = 0; // keep on ground level
        return position;
    }

    void SpawnPowerUp()     // Legacy
    {
        if (powerUpPrefab.Length == 0)
        {
            Debug.LogWarning("No power-up prefabs assigned!");
            return;
        }

        int randomIndex = Random.Range(0, powerUpPrefab.Length);
        PowerUpManagerPrefab selectedPowerUp = powerUpPrefab[randomIndex];

        Vector3 spawnPosition = getRandomPosition();
        GameObject powerUpInstance = ObjectPool.instance.GetPooledObject(selectedPowerUp.poolType);
        powerUpInstance.transform.position = spawnPosition;
        powerUpInstance.transform.rotation = Quaternion.identity;
        powerUpInstance.SetActive(true);
    }

    // Refrenced by AIStateMAchine
    public void DropRandomPowerUp(Vector3 position, Quaternion rotation)
    {
        if (powerUpPrefab.Length == 0)
        {
            Debug.LogWarning("No power-up prefabs assigned!");
            return;
        }

        int randomIndex = Random.Range(0, powerUpPrefab.Length);
        PowerUpManagerPrefab selectedPowerUp = powerUpPrefab[randomIndex];

        GameObject powerUpInstance = ObjectPool.instance.GetPooledObject(selectedPowerUp.poolType);
        powerUpInstance.transform.position = position;
        powerUpInstance.transform.rotation = rotation;
        powerUpInstance.SetActive(true);
    }
}
