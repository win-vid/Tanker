using MilkShake;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class HealthLightIndicator : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] GameObject alarmSound;

    [Header("Indicators")]
    public static HealthLightIndicator instance;
    Light light;
    Color healthyColor;
    [SerializeField]ParticleSystem ps;
    [SerializeField]ParticleSystem hurtSparks;
    [SerializeField]GameObject hurtVolume;
    [SerializeField] Shaker cameraShaker;
    [SerializeField] ShakePreset hurtShake;

    void Start()
    {
        instance = this;
        light = GetComponent<Light>();
        healthyColor = light.color;
    }

    // checks light color and plays particle effects as well as sound effects upon hit
    public void checkColor()
    {
        float currentHealth = PlayerStateMachine.instance.currentHealth;
        if (currentHealth <= PlayerStateMachine.instance.maxHealth * 0.25f)
        {
            light.color = Color.red;
            ps.Play();
            alarmSound.SetActive(true);
            hurtVolume.SetActive(true);
        }
        else {
            light.color = healthyColor;
            ps.Stop();
            alarmSound.SetActive(false);
            hurtVolume.SetActive(false);
        }
        if(currentHealth > 0) {
            hurtSparks.Play();
            cameraShaker.Shake(hurtShake);
        }
    }

}
