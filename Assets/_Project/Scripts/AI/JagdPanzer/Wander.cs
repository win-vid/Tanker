using UnityEngine;

public class Wander : BaseAIState
{
    float radius = 10f;
    Vector3 direction;
    Vector3 randomOffset;

    public override void onEnter(AIStateMachine stateMachine)
    {
        // Pick random point on a circle around the player
        float angle = Random.Range(0f, 2f * Mathf.PI);
        randomOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
    }

    public override void onExit(AIStateMachine stateMachine)
    {

    }

    public override void onFixedUpdate(AIStateMachine stateMachine)
    {

    }

    public override void onUpdate(AIStateMachine stateMachine)
    {
        Vector3 pointOfInterest = stateMachine.player.transform.position + randomOffset;
        // flee from player
        Vector3 flee = stateMachine.transform.position - stateMachine.player.transform.position;
        // move to point
        Vector3 seek = pointOfInterest - stateMachine.transform.position;

        // combination of move towards point and flee from player
        direction = (0.2f * seek.normalized + 0.1f * flee.normalized).normalized;

        stateMachine.transform.position += direction * stateMachine.speed * Time.deltaTime;

        if (direction.sqrMagnitude > 0.001f) // prevent NaN rotation when direction is zero
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            stateMachine.transform.rotation = Quaternion.Slerp(
                stateMachine.transform.rotation,
                targetRotation,
                stateMachine.rotationSpeed * Time.deltaTime
            );
        }

        // TODO: this is a very ugly implementation, fix later
        if (Vector3.Distance(stateMachine.transform.position, pointOfInterest) < 5f)
        {
            if (stateMachine is JagdPanzerManager panzer)
            {
                panzer.SwitchState(panzer.shootState);
            }
        }

        // draw line to point of interest for debugging
        Debug.DrawLine(stateMachine.transform.position, pointOfInterest, Color.green);
    }
}
