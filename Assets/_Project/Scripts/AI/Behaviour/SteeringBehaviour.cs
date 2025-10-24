using UnityEngine;

// This is a collection of steering behaviours for AI agents.
public class SteeringBehaviour : MonoBehaviour
{
    // Move towards point
    public static Vector3 Seek(Vector3 targetPosition, Vector3 currentPosition)
    {
        return (targetPosition - currentPosition).normalized;
    }

    // Move away from point
    public static Vector3 Flee(Vector3 fleeFromPosition, Vector3 currentPosition)
    {
        return (currentPosition - fleeFromPosition).normalized;
    }

    // Pursue a moving target with prediction
    public static Vector3 Pursue(Vector3 targetPosition, Vector3 targetVelocity, Vector3 currentPosition, float predictor)
    {
        return Seek(targetPosition + targetVelocity * predictor, currentPosition);
    }
}
