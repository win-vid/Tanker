using UnityEngine;

/*
* JagdPanzer AI Manager
* Does not need a turret
*/

public class TVP : AIStateMachine
{
    public float explosionTimer = 2f;
    [SerializeField, Range(1,360)] int explosionProjectileDegree = 20;   // amount of degrees between each projectile spawned on explosion
    public Wander wanderState = new Wander();
    public FlyAtPlayer flyAtPlayerState = new FlyAtPlayer();

    void Awake()
    {
        currentState = wanderState;
    }

    public void Explode()
    {
        // spawn a barrage of projectiles in all directions
        for (int i = 0; i < 360; i += explosionProjectileDegree)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            GameObject bullet = ObjectPool.instance.GetPooledObject(projectilePrefab);
            bullet.transform.position = transform.position + new Vector3(0, 1f, 0);     // slightly above ground
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.SetActive(true);
        }

        RemoveFromGame();
        SwitchState(wanderState);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            setCurrentHealth(0);
            Explode();
        }
    }
}
