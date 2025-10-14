using UnityEngine;

public abstract class AIStateMachine : MonoBehaviour
{
    public BaseAIState currentState;

    // Variables
    public float health;            // hp
    public float speed;             // movement speed
    public float rotationSpeed;     // rotation speed
    public float acceleration;      // acceleration
    public float deceleration;      // deceleration
    public float shootSpeed;        // time between shots
    public float aimBias;           // aim randomness
    public GameObject projectilePrefab;

    public PlayerStateMachine player;

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
