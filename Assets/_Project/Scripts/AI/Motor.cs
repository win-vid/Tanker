using UnityEngine;

// Hitting the motor will provide critical damage to the AI.
// The Motor is a child of the AI GameObject and has a trigger collider.

public class Motor : MonoBehaviour
{
    AIStateMachine ai;

    void Awake()
    {
        ai = GetComponentInParent<AIStateMachine>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            ai.setCurrentHealth(-100);
            other.gameObject.SetActive(false);
        }
    }
}
