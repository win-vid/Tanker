using UnityEngine;
using UnityEngine.Animations;

public class FlyAtPlayer : BaseAIState
{
    Vector3 targetDirection;
    float shootTimer;
    Quaternion randomJitter;
    float detonateTimer = 1f; // time until detonation after reaching player
    float currentDetonateTime; // time until detonation after reaching player
    public override void onEnter(AIStateMachine stateMachine)
    {
        shootTimer = stateMachine.shootSpeed; // time between shots
        randomJitter = Quaternion.Euler(0, Random.Range(-stateMachine.aimBias, stateMachine.aimBias), 0);
        currentDetonateTime = detonateTimer;
    }

    public override void onExit(AIStateMachine stateMachine)
    {
        if(stateMachine is TVP tvp) currentDetonateTime = tvp.explosionTimer;
    }

    public override void onFixedUpdate(AIStateMachine stateMachine)
    {

    }

    public override void onUpdate(AIStateMachine stateMachine)
    {
        // Aim at player with a little randomness
        targetDirection = stateMachine.player.transform.position - stateMachine.transform.position;

        if (targetDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            stateMachine.transform.rotation = Quaternion.Slerp(
                stateMachine.transform.rotation,
                targetRotation * randomJitter,
                stateMachine.rotationSpeed * Time.deltaTime
            );
        }

        // shoot at player
        if (shootTimer <= 0f)
        {
            {
                LaunchAtPlayer(stateMachine);

                CheckDetonation(stateMachine);
            }
        }
        else shootTimer -= Time.deltaTime;
    }

    void LaunchAtPlayer(AIStateMachine stateMachine)
    {
        Vector3 launchDirection = (stateMachine.transform.forward).normalized;
        Vector3 direction = ((float)stateMachine.seekWeight * launchDirection + stateMachine.getDistanceWeight(stateMachine.player.transform.position) * launchDirection).normalized;

        // Move the AI into direction mutliplied with speed and deltaTime
        stateMachine.transform.position += Vector3.Scale(direction, new Vector3(1, 0, 1)) * stateMachine.speed * Time.deltaTime;
    }
    // calls the Projectile from the object pool
    
    void CheckDetonation(AIStateMachine stateMachine)
    {
        if(currentDetonateTime <= 0f)
        {
            // Detonate
            // For now, just log to console
            if (stateMachine is TVP tvp) tvp.Explode();
        }
        else
        {
            currentDetonateTime -= Time.deltaTime;
        }
    }

}
