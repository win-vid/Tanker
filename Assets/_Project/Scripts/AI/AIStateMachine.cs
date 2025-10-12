using UnityEngine;

public abstract class AIStateMachine : MonoBehaviour
{
    public BaseAIState currentState;

    // Variables
    public float health;
    public float speed;
    public float rotationSpeed;
    public float acceleration;
    public float deceleration;

    public void SwitchState(BaseAIState newState)
    {
        currentState.onExit(this);
        currentState = newState;
        currentState.onEnter(this);
    }

    void Start()
    {
        currentState.onEnter(this);
    }

    void Update()
    {
        currentState.onUpdate(this);
    }

    void FixedUpdate()
    {
        currentState.onFixedUpdate(this);
    }
}
