using System.Collections.Generic;
using UnityEngine;

// This is the heart of the player controller. It manages the different states the player can be in.
// It starts in the IdleState and can switch to other states as needed.

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine instance;

    // Variables
    public float speed = 10f;
    public float reverseSpeed = 5f;
    public float rotationSpeed = 100f;
    public float acceleration = 5f;
    public float deceleration = 6f;

    // Health
    public int maxHealth = 100;
    bool invincible = false;

    // Current State
    public float currentSpeed = 0f;
    public float inputVertical;            // W/S or Up/Down Arrow
    public float inputHorizontal;          // A/D or Left/Right Arrow
    public int currentHealth;


    // References
    public Turret turret;
    [SerializeField] GameObject shield;

    // States
    BaseState currentState;
    public IdleState idleState = new IdleState();
    public MovementState movementState = new MovementState();
    public DeadState deadState = new DeadState();

    // Physics
    Vector3 currentVelocity;
    Vector3 lastPosition;

    void Start()
    {
        instance = this;
        currentHealth = maxHealth;
        currentState = idleState;
        currentState.onEnter(this);
        shield.SetActive(false);
    }

    void Update()
    {
        currentState.onUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.onFixedUpdate(this);
        updateVelocity();

    }

    public void SwitchState(BaseState state)
    {
        currentState.onExit(this);
        currentState = state;
        currentState.onEnter(this);                 //führt vom neuen State onEnter aus 

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile") && !invincible)
        {
            currentHealth -= other.GetComponent<Projectile>().damage;
            other.GetComponent<Projectile>().onImpact();
            Debug.Log("Player hit, health: " + currentHealth);

            // TODO: later add dead state here
        }
    }

    public void Heal(int amount)
    {
        this.currentHealth = Mathf.Min(this.currentHealth + amount, this.maxHealth);
    }

    public void setInvincible(bool value)
    {
        this.invincible = value;
        shield.SetActive(value);
    }

    void updateVelocity()
    {
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    public Vector3 getCurrentVelocity()
    {
        return currentVelocity;
    }
}
