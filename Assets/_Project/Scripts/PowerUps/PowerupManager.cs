using UnityEngine;

// A "list" of possible power-ups in the game
public class PowerupManager : MonoBehaviour
{
    public static PowerupManager instance;

    PlayerStateMachine player;
    Turret turret;
    void Start()
    {
        instance = this;
        player = FindFirstObjectByType<PlayerStateMachine>();
        turret = FindFirstObjectByType<Turret>();

        if (player == null)
        {
            Debug.LogError("PowerupManager: No PlayerStateMachine instance found in the scene.");
        }
    }
    public void checkPowerup(Powerup.Effect effect)
    {
        switch (effect.effectType)
        {
            case Powerup.EffectType.HEAL:
                player.Heal(effect.amount);
                break;
            case Powerup.EffectType.INVINCIBLE:
                player.setInvincible(true);
                Invoke("RemoveInvincibility", effect.duration);
                break;
            case Powerup.EffectType.POWERSHOT:
                // Implement PowerShot effect
                turret.shootingSpeed /= effect.amount; 
                Invoke("removePowerShot", effect.duration);
                break;
            default:
                Debug.LogWarning("PowerupManager: Unknown effect type " + effect.effectType);
                break;
        }
    }

    private void RemoveInvincibility()
    {
        player.setInvincible(false);
    }

    private void removePowerShot()
    {
        turret.shootingSpeed = turret.initialShootingSpeed;
    }
}
