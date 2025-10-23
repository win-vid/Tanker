using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Wander : BaseAIState
{
    Vector3 direction;
    Vector3 randomOffset;

    public override void onEnter(AIStateMachine stateMachine)
    {
        // Pick random point on a circle around the player
        float angle = Random.Range(0f, 2f * Mathf.PI);
        randomOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * stateMachine.wanderRadius;
    }

    public override void onExit(AIStateMachine stateMachine)
    {

    }

    public override void onFixedUpdate(AIStateMachine stateMachine)
    {

    }

    public override void onUpdate(AIStateMachine stateMachine)
    {
        Vector3 playerPos = stateMachine.player.transform.position;
        Vector3 AIpos = stateMachine.transform.position;
        Vector3 pointOfInterest = playerPos + randomOffset;
        // flee from player
        Vector3 flee = SteeringBehaviour.Flee(playerPos, AIpos);
        // move to point
        Vector3 seek = SteeringBehaviour.Seek(pointOfInterest, AIpos);

        // combination of move towards point and flee from player
        direction = ((float)stateMachine.seekWeight * seek.normalized + stateMachine.getDistanceWeight(playerPos) * flee.normalized + getSeperationVector(stateMachine)).normalized;

        // Move the AI into direction mutliplied with speed and deltaTime
        stateMachine.transform.position += Vector3.Scale(direction, new Vector3(1, 0, 1)) * stateMachine.speed * Time.deltaTime;

        // Look where you are going
        if (direction.sqrMagnitude > 0.001f) // prevent NaN rotation when direction is zero
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            stateMachine.transform.rotation = Quaternion.Slerp(
                stateMachine.transform.rotation,
                targetRotation,
                stateMachine.rotationSpeed * Time.deltaTime
            );

            stateMachine.transform.rotation = Quaternion.Euler(0, stateMachine.transform.rotation.eulerAngles.y, 0); // lock x and z rotation
        }

        // TODO: this is a very ugly implementation, fix later
        if (Vector3.Distance(stateMachine.transform.position, pointOfInterest) < 5f)
        {
            if (stateMachine is JagdPanzerManager panzer)
            {
                panzer.SwitchState(panzer.shootState);
            }
            else if (stateMachine is TVP tvp)
            {
                tvp.SwitchState(tvp.flyAtPlayerState);
            }
            else stateMachine.SwitchState(stateMachine.wanderState); // pick new point
        }

        // draw line to point of interest for debugging
        // Debug.DrawLine(stateMachine.transform.position, pointOfInterest, Color.green);
    }

    // TODO: This is a performance issue
    // checks the EnemyManager for other enemies and calculates a seperation vector
    Vector3 getSeperationVector(AIStateMachine stateMachine)
    {
        Vector3 seperation = Vector3.zero;
        foreach (AIStateMachine other in EnemyManager.instance.enemies)
        {
            if (other == stateMachine || other == null) continue;
            
            Vector3 diff = stateMachine.transform.position - other.transform.position;
            float distSqr = diff.sqrMagnitude;
            
            if (distSqr < stateMachine.flockingRadiusSqr)
            {
                seperation += diff.normalized;
            }
        }
        return seperation.normalized;
    }
}
