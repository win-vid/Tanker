using UnityEngine;
using System.Collections.Generic;
using System;

// code from https://learn.unity.com/tutorial/introduction-to-object-pooling
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public Dictionary<PoolType, List<GameObject>> poolDictionary;
    public List<Pool> pools;

    public enum PoolType
    {
        EnemyBulletEasy,
        PlayerBullet,
        EnemyDisk,
        T1000,
        T1WRACK,
        T2000,
        T2WRACK,
        T3000,
        T3WRACK,
        TVP,
        NONE,
        T4000,
        EnemyRocket,
        TRX,
        TVPWRACK,

    }

    [System.Serializable]
    public class Pool
    {
        public PoolType type;
        public GameObject prefab;
        public int size;
    }

    void Awake()
    {
        poolDictionary = new Dictionary<PoolType, List<GameObject>>();
        instance = this;
    }

    // Initialize the pools
    void Start()
    {
        foreach (Pool pool in pools)
        {
            List<GameObject> pooledObjects = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
            poolDictionary.Add(pool.type, pooledObjects);
        }
    }

    // Get a pooled object by tag
    public GameObject GetPooledObject(PoolType type)
    {
        if (poolDictionary.ContainsKey(type))
        {
            foreach (GameObject obj in poolDictionary[type])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }
        }
        Debug.LogWarning("No inactive objects of type " + type + " available in pool.");
        return null;
    }
}
