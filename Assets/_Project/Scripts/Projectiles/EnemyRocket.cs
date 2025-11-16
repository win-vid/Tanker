using System.IO;
using UnityEngine;

public class EnemyRocket : Projectile
{
    PlayerStateMachine target;
    Rigidbody targetRb;
    float currentSpeed;
    Vector3 finalDirection = Vector3.zero;
    [Tooltip("Time the Rocket takes to search for the player.")]
    [SerializeField] float flyTime = 2f;
    float currentFlyTime;
    public override void onSpawn()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
        targetRb = target.GetComponent<Rigidbody>();
        currentFlyTime = flyTime;
        currentSpeed = speed;
    }

    protected override void Move()
    {
        if (target == null || targetRb == null)
        {
            Debug.LogWarning(this.name + " Target or TargetRigidbody is null in EnemyRocket Move()");
            return;
        }

        if(currentFlyTime > 0)
        {
            PursueTarget();
            currentFlyTime -= Time.fixedDeltaTime;
        }
        else
        {
            if (finalDirection == Vector3.zero)
            {
                Vector3 dir = SteeringBehaviour.Pursue(target.transform.position, target.getCurrentVelocity(), transform.position, 3f);
                if (dir.sqrMagnitude > 0.001f)
                    finalDirection = dir;
                else
                    finalDirection = transform.forward; // fallback to current facing direction
            }

            RotateTowardsTarget(finalDirection);
            currentSpeed += 20 * Time.deltaTime; // use += instead of *= for smooth acceleration
            transform.position += finalDirection * currentSpeed * Time.deltaTime;
        }

    }

    void RotateTowardsTarget(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void PursueTarget()
    {
        Vector3 direction = SteeringBehaviour.Pursue(target.transform.position, target.getCurrentVelocity(), transform.position, .8f);
        transform.position += Vector3.Scale(direction, new Vector3(1, 1, 1)) * currentSpeed * Time.deltaTime;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
    }

    protected override void ResetProjectile()
    {
        currentFlyTime = flyTime;
        finalDirection = Vector3.zero;
    }
}
