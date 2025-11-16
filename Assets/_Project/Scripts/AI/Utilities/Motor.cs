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
            ai.currentHealth -= 100; // critical damage
            other.gameObject.SetActive(false);
            gameObject.SetActive(false); // disable motor on hit
        }
    }
}
