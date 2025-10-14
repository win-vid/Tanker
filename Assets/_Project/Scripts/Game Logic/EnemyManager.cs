using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<AIStateMachine> enemies = new List<AIStateMachine>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        getAllEnemies();
    }

    void getAllEnemies()
    {
        enemies.Clear();
        AIStateMachine[] foundEnemies = FindObjectsByType<AIStateMachine>(FindObjectsSortMode.None);
        enemies.AddRange(foundEnemies);
    }
}
