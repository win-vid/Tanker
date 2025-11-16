using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

// This is the heart of the player controller. It manages the different states the player can be in.
// It starts in the IdleState and can switch to other states as needed.

[RequireComponent(typeof(Rigidbody))]
public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine instance;

    // Variables
    public float speed = 10f;
    public float reverseSpeed = 5f;
    public float rotationSpeed = 100f;
    public float acceleration = 5f;
    public float deceleration = 6f;
    public bool isAlive = true;

    // Health
    public int maxHealth = 100;
    [SerializeField] bool invincible = false;
    [SerializeField] Healthbar healthbar;

    // Current State
    public float currentSpeed = 0f;
    public float inputVertical;            // W/S or Up/Down Arrow
    public float inputHorizontal;          // A/D or Left/Right Arrow
    public int currentHealth;


    // References
    public Turret turret;
    [SerializeField] GameObject shield;
    [HideInInspector] public Rigidbody rb;

    // States
    BaseState currentState;
    public IdleState idleState = new IdleState();
    public MovementState movementState = new MovementState();
    public DeadState deadState = new DeadState();

    // Physics
    Vector3 currentVelocity;
    Vector3 lastPosition;

    [Header("Sound Effects")]
    [SerializeField] AudioClip[] hitSounds;

    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
        healthbar = FindFirstObjectByType<Healthbar>();

        // set maxHealth of healthbar to players maxHealth
        if (healthbar != null)
        {
            healthbar.setMaxHealth(maxHealth);
            healthbar.setHealth(maxHealth);
        }
        else Debug.LogError("No Healthbar UI set or created");
        
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
        checkHealth();

    }

    public void SwitchState(BaseState state)
    {
        currentState.onExit(this);
        currentState = state;
        currentState.onEnter(this);                 //führt vom neuen State onEnter aus 

    }

    // Collide with enemy projectile
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile") && !invincible)
        {
            currentHealth -= other.GetComponent<Projectile>().damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, 100);
            other.GetComponent<Projectile>().onImpact();
            SoundEffectsManager.instance.PlayRandomSoundEffect(hitSounds, transform, 1f);

            // Update Healthbar
            healthbar.setHealth(currentHealth);
            HealthLightIndicator.instance.checkColor();
            // TODO: later add dead state here
        }
    }

    public void Heal(int amount)
    {
        this.currentHealth = Mathf.Min(this.currentHealth + amount, this.maxHealth);
        healthbar.setHealth(currentHealth);
        HealthLightIndicator.instance.checkColor();
        HealthLightIndicator.instance.checkColor();
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

    void checkHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0 && currentState != deadState && !invincible)
        {
            SwitchState(deadState);
        }
    }

    [ContextMenu("Die")]
    public void Die()
    {
        currentHealth = 0;
    }
}
