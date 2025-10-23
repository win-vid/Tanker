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
}
