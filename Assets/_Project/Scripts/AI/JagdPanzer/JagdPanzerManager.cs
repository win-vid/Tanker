using UnityEngine;

/*
* JagdPanzer AI Manager
* Does not need a turret
*/

public class JagdPanzerManager : AIStateMachine
{
    public Wander wanderState = new Wander();
    public Shoot shootState = new Shoot();

    void Awake()
    {
        currentState = wanderState;
    }
}
