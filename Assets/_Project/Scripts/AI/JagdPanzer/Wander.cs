using UnityEngine;

public class Wander : BaseAIState
{
 private Vector3 targetDirection;
    private float changeDirectionTimer;
    private float timeToChange;

    public override void onEnter(AIStateMachine stateMachine)
    {
        // Pick initial random direction
        PickNewDirection(stateMachine);
    }

    public override void onExit(AIStateMachine stateMachine)
    {
        // No special cleanup needed here
    }

    public override void onFixedUpdate(AIStateMachine stateMachine)
    {
        Transform t = stateMachine.transform;

        // --- Rotate smoothly toward target direction ---
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            t.rotation = Quaternion.Lerp(
                t.rotation,
                targetRotation,
                stateMachine.rotationSpeed * Time.fixedDeltaTime
            );
        }

        // --- Move forward ---
        t.position += t.forward * stateMachine.speed * Time.fixedDeltaTime;

        // --- Decrease timer and pick new direction if time is up ---
        changeDirectionTimer -= Time.fixedDeltaTime;
        if (changeDirectionTimer <= 0f)
        {
            PickNewDirection(stateMachine);
        }
    }

    public override void onUpdate(AIStateMachine stateMachine)
    {
        // We handle all movement in FixedUpdate for physics consistency
    }

    private void PickNewDirection(AIStateMachine stateMachine)
    {
        // Pick a random direction within a sphere on the XZ plane
        Vector3 randomDir = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;

        targetDirection = randomDir;

        // Random interval before changing again
        timeToChange = Random.Range(2f, 5f);
        changeDirectionTimer = timeToChange;
    }
}
