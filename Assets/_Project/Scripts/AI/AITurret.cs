using UnityEngine;

public class AITurret : MonoBehaviour
{
    PlayerStateMachine player;
    AIStateMachine ai;
    [SerializeField, Range(0.1f, 10f)] float rotationSpeed;
    [SerializeField, Range(0.1f, 10f)] float shootSpeed;
    [SerializeField] Transform _projectileSpawnPoint;
    float currentShootTime;
    [SerializeField] ObjectPool.PoolType projectile;
    [SerializeField] bool followPlayer;
    [SerializeField, Tooltip("If this is set to false the turret will not shoot")] bool shootAtPlayer = true;

    void Start()
    {

        player = FindFirstObjectByType<PlayerStateMachine>();
        ai = GetComponentInParent<AIStateMachine>();
        if (_projectileSpawnPoint = null) _projectileSpawnPoint = this.gameObject.transform;
        currentShootTime = shootSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer) RotateTurret();
        if (shootAtPlayer) checkShoot();
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
            rotationSpeed * Time.deltaTime
        );
    }

    void checkShoot()
    {
        if (currentShootTime > 0)
        {
            currentShootTime -= Time.deltaTime;
            return;
        }
        shootProjectile(projectile);
        currentShootTime = shootSpeed;
    }

    
    // Shoot projectile from object pool by type
    public void shootProjectile(ObjectPool.PoolType type)
    {
        GameObject projectile = ObjectPool.instance.GetPooledObject(type);
        if (projectile != null)
        {

            Quaternion randomJitter = Quaternion.Euler(0, Random.Range(-ai.aimBias, ai.aimBias), 0);
            projectile.transform.position = transform.position + transform.forward * 2f;
            projectile.transform.rotation = transform.rotation * randomJitter;
            projectile.SetActive(true);
            SoundEffectsManager.instance.PlayRandomSoundEffect(ai.shootSounds, transform, 1f);

            // Check if Projectile is Bullet or AI
            Projectile bullet = projectile.GetComponent<Projectile>();
            if (bullet != null)
            {
                bullet.onSpawn();
                return;
            }

            AIStateMachine stateMachine = projectile.GetComponent<AIStateMachine>();
            if (stateMachine != null)
            {
                WaveManager.instance.enemiesSpawned++;
                return;
            }
        }
    }
}
