using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 0;
    public int currentWavePoints;
    public int enemiesSpawned = 0;
    [SerializeField] int wavePointIncrement = 2;

    public static WaveManager instance;
    [SerializeField] List<Enemy> enemies;

    [System.Serializable]
    public class Enemy
    {
        public ObjectPool.PoolType type;
        public int cost;
    }
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned <= 0)
        {
            StartNewWave();
        }
    }

    Enemy GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }

    void StartNewWave()
    {
        currentWave++;
        currentWavePoints = currentWave * wavePointIncrement;
        Debug.Log("Starting Wave: " + currentWave + " with " + currentWavePoints + " points.");
        enemiesSpawned = 0;

        while (currentWavePoints > 0)
        {
            Enemy enemy = GetRandomEnemy();
            if (enemy.cost <= currentWavePoints)
            {
                GameObject spawnedEnemy = ObjectPool.instance.GetPooledObject(enemy.type);
                if (spawnedEnemy == null)
                {
                    Debug.LogWarning("No more enemies of type " + enemy.type + " available in pool.");
                    break; // exit if no more enemies of this type are available
                }
                spawnedEnemy.transform.position = getRandomPosition();
                spawnedEnemy.transform.rotation = Quaternion.identity;
                spawnedEnemy.SetActive(true);
                enemiesSpawned++;
                currentWavePoints -= enemy.cost;
            }
        }
        Debug.Log("Wave Points Left: " + currentWavePoints);
    }
    
    // get a random position outside the camera view
    Vector3 getRandomPosition()
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
}
