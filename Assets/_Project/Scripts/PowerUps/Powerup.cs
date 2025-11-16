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
    [SerializeField] float lifeTime = 20f; // Lifetime before disappearing
    float currentLifeTime;
    [SerializeField] AudioClip[] powerUpSoundEffects;

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
        public float duration;  // Duration in seconds
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
        
        currentLifeTime = lifeTime;

    }

    void Update()
    {
        onUpdate();
        checkLifeTime();
    }

    void checkLifeTime()
    {
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0f)
        {
            currentLifeTime = lifeTime;
            gameObject.SetActive(false);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPickUp();
            PowerupManager.instance.checkPowerup(effect);
            SoundEffectsManager.instance.PlayRandomSoundEffect(powerUpSoundEffects, PlayerStateMachine.instance.transform,1f);
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
    
    // Not working
    protected void PulsePowerUp()
    {
        float scale = _initialScale.x + 0.1f * Mathf.Sin(Time.time * 2f);
        symbolObject.transform.localScale = new Vector3(scale, scale, scale);
    }
}
