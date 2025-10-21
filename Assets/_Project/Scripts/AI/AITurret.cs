using UnityEngine;

public class AITurret : MonoBehaviour
{
    PlayerStateMachine player;
    AIStateMachine ai;
    [SerializeField]float shootSpeed;
    float currentShootTime;
    [SerializeField] ObjectPool.PoolType bullet;
    [SerializeField] bool followPlayer;

    void Start()
    {

        player = FindFirstObjectByType<PlayerStateMachine>();
        ai = GetComponentInParent<AIStateMachine>();
        currentShootTime = shootSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer) RotateTurret();
        checkShoot();
    }

    void RotateTurret()
    {
        if (player == null) return;

        // Get direction from turret to player (ignore Y to keep rotation flat)
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f; // prevent tilting up/down

        if (direction.sqrMagnitude < 0.001f)
            return; // prevent NaN rotation if too close

        // Calculate desired rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate toward the player
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            ai.rotationSpeed * Time.deltaTime
        );
    }

    void checkShoot()
    {
        if (currentShootTime > 0)
        {
            currentShootTime -= Time.deltaTime;
            return;
        }
        shootProjectile(bullet);
        currentShootTime = shootSpeed;
    }

    
    // Shoot projectile from object pool by type
    public void shootProjectile(ObjectPool.PoolType type)
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject(type);
        if (bullet != null)
        {

                Quaternion randomJitter = Quaternion.Euler(0, Random.Range(-ai.aimBias, ai.aimBias), 0);
                bullet.transform.position = transform.position + transform.forward * 2f + new Vector3(0, 1f, 0);
                bullet.transform.rotation = transform.rotation * randomJitter;
            bullet.SetActive(true);
            bullet.GetComponent<Projectile>().onSpawn();
        }
    }
}
