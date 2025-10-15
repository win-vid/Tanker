using UnityEngine;

public abstract class AIStateMachine : MonoBehaviour
{
    public BaseAIState currentState;

    // Variables
    [Header("Stats")]
    public float health;            // hp
    public float speed;             // movement speed
    public float rotationSpeed;     // rotation speed
    public float acceleration;      // acceleration
    public float deceleration;      // deceleration
    public float shootSpeed;        // time between shots
    public float aimBias;           // aim randomness
    [SerializeField] public ObjectPool.PoolType projectilePrefab;
    [SerializeField] protected AITurret turret;     // reference to turret, if any

    [HideInInspector] public PlayerStateMachine player;
    [HideInInspector] public Wander wanderState = new Wander();

    [Header("Steering")]
    [Range(0, 1)] public double seekWeight = 1.0; // weight for separation behavior
    public double flockingRadius = 5.0;    // radius for flocking behavior
    public float wanderRadius = 10f;      // radius for wandering behavior
    [HideInInspector] public float flockingRadiusSqr;

    public void SwitchState(BaseAIState newState)
    {
        currentState.onExit(this);
        currentState = newState;
        currentState.onEnter(this);
    }

    void Start()
    {
        currentState.onEnter(this);
        turret = GetComponentInChildren<AITurret>();      // get turret if any is attached to the GameObject
        player = FindFirstObjectByType<PlayerStateMachine>();
        flockingRadiusSqr = (float)(flockingRadius * flockingRadius); // precompute squared radius for performance
    }

    void Update()
    {
                // check health
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
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
            health -= other.GetComponent<Projectile>().damage;
            other.GetComponent<Projectile>().onImpact();
        }
    }

    // Shoot projectile from object pool by type
    public void shootProjectile(ObjectPool.PoolType type)
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject(type);
        if (bullet != null)
        {
            if(turret != null)  // if turret exists, shoot from turret position
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
}
