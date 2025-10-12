using UnityEngine;

public class JagdPanzerManager : AIStateMachine
{
    Wander wanderState = new Wander();

    void Awake()
    {
        currentState = wanderState;
    }
}
