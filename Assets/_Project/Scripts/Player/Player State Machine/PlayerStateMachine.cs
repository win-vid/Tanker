using Unity.VisualScripting;
using UnityEngine;

// This is the heart of the player controller. It manages the different states the player can be in.
// It starts in the IdleState and can switch to other states as needed.

public class PlayerStateMachine : StateMachine
{
    // References
    public Turret turret;

    // States
    BaseState currentState;
    public IdleState idleState = new IdleState();
    public MovementState movementState = new MovementState();
    public DeadState deadState = new DeadState();

    void Start()
    {
        currentState = idleState;
        currentState.onEnter(this);
    }

    void Update()
    {
        currentState.onUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.onFixedUpdate(this);

    }

    public void SwitchState(BaseState state)
    {
        currentState.onExit(this);
        currentState = state;
        currentState.onEnter(this);                 //führt vom neuen State onEnter aus 

    }
}
