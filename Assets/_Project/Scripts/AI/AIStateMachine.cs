using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateMachine : MonoBehaviour
{
    public BaseAIState currentState;

    [Header("Stats")]
    public float health;            // hp
    public float currentHealth;
    public float speed;             // movement speed
    public float rotationSpeed;     // rotation speed
    public float acceleration;      // acceleration
    public float deceleration;      // deceleration
    public float shootSpeed;        // time between shots
    public float aimBias;           // aim randomness
    bool active = false;

    [Header("References")]
    [SerializeField] public ObjectPool.PoolType projectilePrefab;
    [SerializeField] protected AITurret turret;     // reference to turret, if any

    [HideInInspector] public PlayerStateMachine player;
    [HideInInspector] public Wander wanderState = new Wander();
    [HideInInspector] public DeadAIState deadState = new DeadAIState();
    List<Motor> motors = new List<Motor>();

    [Header("Steering")]
    [Range(0, 1)] public double seekWeight = 1.0; // weight for separation behavior
    public double flockingRadius = 5.0;    // radius for flocking behavior
    public float wanderRadius = 10f;      // radius for wandering behavior
    [HideInInspector] public float flockingRadiusSqr;

    [Header("Wrack Settings")]
    public ObjectPool.PoolType wrackPrefab;

    public void SwitchState(BaseAIState newState)
    {
        currentState.onExit(this);
        currentState = newState;
        currentState.onEnter(this);
    }

    void Start()
    {
        currentState.onEnter(this);
        turret = GetComponentInChildren<AITurret>();                    // get turret if any is attached to the GameObject
        player = FindFirstObjectByType<PlayerStateMachine>();           // find player in scene
        flockingRadiusSqr = (float)(flockingRadius * flockingRadius);   // precompute squared radius for performance
        currentHealth = health;                                         // set current health to max health
        motors.AddRange(GetComponentsInChildren<Motor>());              // get all motors in children

        if (WaveManager.instance == null) Debug.LogWarning(this.name + " WaveManager instance not found!");
    }

    void Update()
    {
        checkHealth();
        currentState.onUpdate(this);
    }

    void FixedUpdate()
    {
        currentState.onFixedUpdate(this);
    }

    public float getDistanceWeight(Vector3 flee)
    {
        // weight of distance between point and ai, returns a value between 0 and 1, where 1 is very close and 0 is far away
        float distance = Vector3.Distance(flee, this.transform.position);
        float distanceWeight = 1f - Mathf.Clamp01(distance / 10f) + 0.01f;
        return distanceWeight;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            currentHealth -= other.GetComponent<Projectile>().damage;
            other.GetComponent<Projectile>().onImpact();
        }
    }

    // Shoot projectile from object pool by type
    public void shootProjectile(ObjectPool.PoolType type)
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject(type);
        if (bullet != null)
        {
            if (turret != null)  // if turret exists, shoot from turret position
            {
                Quaternion randomJitter = Quaternion.Euler(0, Random.Range(-aimBias, aimBias), 0);
                bullet.transform.position = turret.transform.position + turret.transform.forward * 2f + new Vector3(0, 1f, 0);
                bullet.transform.rotation = turret.transform.rotation * randomJitter;
            }
            else
            {
                bullet.transform.position = transform.position + transform.forward * 2f + new Vector3(0, 1f, 0);
                bullet.transform.rotation = transform.rotation;
            }
            bullet.SetActive(true);
        }
    }

    // Checks current health and deactivates the AI if health is 0, spawns a wrack if applicable
    void checkHealth()
    {
        if (currentHealth <= 0 && this.gameObject.activeSelf)
        {
            currentHealth = health;
            RemoveFromGame();
        }
    }

    public void setCurrentHealth(float health)
    {
        currentHealth = health;
    }

    // Spawns a wrack prefab from the object pool
    public void spawnWrack()
    {
        if (wrackPrefab == ObjectPool.PoolType.NONE) return;
        GameObject wrackObject = ObjectPool.instance.GetPooledObject(wrackPrefab);
        wrackObject.transform.position = this.transform.position;
        wrackObject.transform.rotation = this.transform.rotation;
        wrackObject.SetActive(true);
        wrackObject.GetComponent<Wrack>().playParticleSystem();
    }

    protected void RemoveFromGame()
    {
        if (WaveManager.instance != null) WaveManager.instance.enemiesSpawned--;
        else Debug.LogWarning(this.name + " WaveManager instance not found!");

        foreach (Motor motor in motors) motor.gameObject.SetActive(true);   // reactivate motors for next spawn

        if (wrackPrefab != ObjectPool.PoolType.NONE) spawnWrack();
        this.gameObject.SetActive(false);
    }
}
