using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Wave Manager
* Manages enemy waves and spawning.
* Randomly selects enemies to spawn based on wave points.
* UIWaveManager handles wave display and must be assigned as a component.
*/

public class WaveManager : MonoBehaviour
{
    int currentWave;
    [SerializeField] int currentWavePoints;
    public int enemiesSpawned = 0;
    [SerializeField] int wavePointIncrement = 2;

    public static WaveManager instance;
    [SerializeField] List<Enemy> enemies;
    [SerializeField] int bossWaveInterval = 10;
    UIWaveManager uiWaveManager;

    [SerializeField] float minDistanceFromCamera = 25f; // outside camera view
    [SerializeField] float maxDistanceFromCamera = 60f; // not too far either

    [SerializeField] Collider _worldColliderBox;

    [System.Serializable]

    // Enemy class to hold type and cost
    public class Enemy
    {
        public ObjectPool.PoolType type;
        public int cost;
    }
    void Start()
    {
        instance = this;
        uiWaveManager = GetComponent<UIWaveManager>();
    }

    // Update is called once per frame
    void Update()
    { 
        // Start new wave if no more enemies spawned and the wave points are depleted
        if (enemiesSpawned <= 0 && currentWavePoints <= 0)
        {
            StartCoroutine(StartNewWave());
        }
    }

    Enemy GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }

    IEnumerator StartNewWave()
    {
        currentWave++;
        currentWavePoints = currentWave * wavePointIncrement;
        uiWaveManager.setCurrentWave(currentWave);
        PlayerUIManager.instance.SetWaveText(currentWave);
        uiWaveManager.Display();
        
        // wait for display time
        yield return new WaitForSeconds(uiWaveManager.getDisplayTime());
        
        Debug.Log("Starting Wave: " + currentWave + " with " + currentWavePoints + " points.");
        enemiesSpawned = 0;

        // Boss Wave every 10 waves
        if (currentWave % bossWaveInterval == 0 && currentWave != 0)
        {
            Enemy boss = new Enemy();
            boss.type = ObjectPool.PoolType.TRX;
            spawnEnemy(boss);
        }

        while (currentWavePoints > 0)
        {
            Enemy enemy = GetRandomEnemy();
            spawnEnemy(enemy);
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

        if (_worldColliderBox == null)
        {
            Debug.LogError("No Wolrd Box Set");
            return position;
        }

        // Get world bounds
        Bounds bounds = _worldColliderBox.bounds;

        for (int i = 0; i < 20; i++) // try multiple times to find a valid spot
        {
            // Pick random position within world box
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 candidate = new Vector3(x, 0, z);

            // Check distance and visibility
            float dist = Vector3.Distance(candidate, mainCamera.transform.position);

            if (dist < minDistanceFromCamera || dist > maxDistanceFromCamera)
                continue; // too close or too far

            // Convert world pos to viewport space (0–1 range)
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(candidate);

            // If it's inside the camera’s view, skip it
            if (viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1)
                continue;

            // Otherwise it's outside view and within world bounds — valid!
            return candidate;
        }

        Debug.LogWarning("Could not find valid spawn position after several attempts!");
        return new Vector3(0,0,0);  // spawn at world position 0
    }

    void spawnEnemy(Enemy enemy)
    {
        if (enemy.cost <= currentWavePoints)
            {
                GameObject spawnedEnemy = ObjectPool.instance.GetPooledObject(enemy.type);
                if (spawnedEnemy == null)
                {
                    // POSSIBLE ISSUE: No more enemies of this type available in pool
                    Debug.LogWarning("No more enemies of type " + enemy.type + " available in pool.");
                    return; // exit if no more enemies of this type are available
                }
                spawnedEnemy.transform.position = getRandomPosition();
                spawnedEnemy.transform.rotation = Quaternion.identity;
                enemiesSpawned++;
                spawnedEnemy.SetActive(true);
                currentWavePoints -= enemy.cost;
            }
    }
}
