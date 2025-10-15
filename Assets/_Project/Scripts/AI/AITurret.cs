using UnityEngine;

public class AITurret : MonoBehaviour
{
    PlayerStateMachine player;
    AIStateMachine ai;
    float currentShootTime;
    [SerializeField] ObjectPool.PoolType bullet;

    void Start()
    {
        player = FindFirstObjectByType<PlayerStateMachine>();
        ai = GetComponentInParent<AIStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTurret();
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
        ai.shootProjectile(ai.projectilePrefab);
        currentShootTime = ai.shootSpeed;
    }
}
