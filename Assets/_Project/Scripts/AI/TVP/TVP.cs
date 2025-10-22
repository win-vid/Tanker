using UnityEngine;

/*
* JagdPanzer AI Manager
* Does not need a turret
*/

public class TVP : AIStateMachine
{
    public Wander wanderState = new Wander();
    public FlyAtPlayer flyAtPlayerState = new FlyAtPlayer();

    void Awake()
    {
        currentState = wanderState;
    }

    public void Explode()
    {
        // spawn a barrage of projectiles in all directions
        for (int i = 0; i < 360; i += 20)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            GameObject bullet = ObjectPool.instance.GetPooledObject(projectilePrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(direction);
            bullet.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    void OggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerProjectile"))
        {
            Explode();
        }
    }
}
