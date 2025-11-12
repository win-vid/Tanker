using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletCase : MonoBehaviour
{
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SpawnCase()
    {
        rb.linearVelocity += (PlayerStateMachine.instance.transform.up * 10) + (PlayerStateMachine.instance.transform.right * 3);
    }
}
