using System;
using UnityEngine;

// Base class for all power-ups
// Children have access to the player state machine
// Disables upon pickup

// Every Powerup needs an effect class that defines what the powerup does, this effect is called in the player state manager upon pickup

[RequireComponent(typeof(Collider))]
public abstract class Powerup : MonoBehaviour
{
    protected PlayerStateMachine player;
    [SerializeField] protected GameObject symbolObject;  // The Object that represents the power-up visually
    [SerializeField] private float rotationSpeed = 50f;
    Vector3 _initialScale;

    public enum EffectType
    {
        HEAL,
        INVINCIBLE,
        POWERSHOT,
    }

    [SerializeField] Effect effect;

    // Will be applied in player class upon pickup
    [System.Serializable]
    public class Effect
    {
        public EffectType effectType;
        public float duration;  // Duration in seconds, 0 for instant effects
        public int amount;    // Amount for effects like HEAL

        public Effect(EffectType effectType, float duration)
        {
            this.effectType = effectType;
            this.duration = duration;
        }

        public Effect(EffectType effectType, float duration, int amount)
        {
            this.effectType = effectType;
            this.duration = duration;
            this.amount = amount;
        }
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerStateMachine>();
        symbolObject = transform.GetChild(0).gameObject;  // Assumes the symbol is the first child

        if (player == null || symbolObject == null)
        {
            Debug.LogError("Powerup: Missing references in " + gameObject.name);
        }

        if (PowerupManager.instance == null)
        {
            Debug.LogError("Powerup: No PowerupManager instance found in the scene.");
        }
        
        _initialScale = symbolObject.transform.localScale;

    }

    void Update()
    {
        onUpdate();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPickUp();
            PowerupManager.instance.checkPowerup(effect);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    protected abstract void onPickUp();

    protected abstract void onUpdate();

    protected void RotatePowerUp()
    {

        symbolObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    
    protected void PulsePowerUp()
    {
        /*
        float scale = _initialScale.x + 0.1f * Mathf.Sin(Time.time * 5f);
        symbolObject.transform.localScale = new Vector3(scale, scale, scale);
        */
    }
}
