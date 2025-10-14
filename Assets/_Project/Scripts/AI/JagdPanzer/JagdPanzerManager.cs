using UnityEngine;

public class JagdPanzerManager : AIStateMachine
{
    public Wander wanderState = new Wander();
    public Shoot shootState = new Shoot();

    void Awake()
    {
        currentState = wanderState;
    }
}
