using UnityEngine;

// This is the heart of the player controller. It manages the different states the player can be in.
// It starts in the IdleState and can switch to other states as needed.

public class PlayerStateMachine : MonoBehaviour
{
    // Variables
    public float speed = 10f;
    public float reverseSpeed = 5f;
    public float rotationSpeed = 100f;
    public float acceleration = 5f;
    public float deceleration = 6f;

    // Health
    public int maxHealth = 100;

    // Current State
    public float currentSpeed = 0f;
    public float inputVertical;            // W/S or Up/Down Arrow
    public float inputHorizontal;          // A/D or Left/Right Arrow
    public int currentHealth;


    // References
    public Turret turret;

    // States
    BaseState currentState;
    public IdleState idleState = new IdleState();
    public MovementState movementState = new MovementState();
    public DeadState deadState = new DeadState();

    void Start()
    {
        currentHealth = maxHealth;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            currentHealth -= other.GetComponent<Projectile>().damage;
            other.GetComponent<Projectile>().onImpact();
            Debug.Log("Player hit, health: " + currentHealth);

            // TODO: later add dead state here
        }      
    }
}
