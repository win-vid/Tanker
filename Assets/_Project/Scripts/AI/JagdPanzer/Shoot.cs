using UnityEngine;

public class Shoot : BaseAIState
{
    Vector3 targetDirection;
    float shootTimer;
    Quaternion randomJitter;
    public override void onEnter(AIStateMachine stateMachine)
    {
        shootTimer = stateMachine.shootSpeed; // time between shots
        randomJitter = Quaternion.Euler(0, Random.Range(-stateMachine.aimBias, stateMachine.aimBias), 0);
    }

    public override void onExit(AIStateMachine stateMachine)
    {

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
                // Fire projectile logic here
                GameObject projectile = GameObject.Instantiate(stateMachine.projectilePrefab, stateMachine.transform.position + stateMachine.transform.forward * 2f + new Vector3(0, 1f, 0), stateMachine.transform.rotation);
                if (stateMachine is JagdPanzerManager panzer)
                {
                    panzer.SwitchState(panzer.wanderState);
                }
            }
        }
            shootTimer -= Time.deltaTime;
        }
}
