using UnityEngine;

public class EnemyTankManager : AIStateMachine
{
    DeadAIState dead = new DeadAIState();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentState = wanderState;
    }
}
