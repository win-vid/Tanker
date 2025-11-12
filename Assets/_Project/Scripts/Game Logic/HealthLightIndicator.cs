using UnityEngine;

[RequireComponent(typeof(Light))]
public class HealthLightIndicator : MonoBehaviour
{
    public static HealthLightIndicator instance;
    Light light;
    Color healthyColor;
    [SerializeField]ParticleSystem ps;

    void Start()
    {
        instance = this;
        light = GetComponent<Light>();
        healthyColor = light.color;
    }

    public void checkColor()
    {
        float currentHealth = PlayerStateMachine.instance.currentHealth;
        if (currentHealth <= PlayerStateMachine.instance.maxHealth * 0.25f)
        {
            light.color = Color.red;
            ps.Play();
        }
        else {
            light.color = healthyColor;
            ps.Stop();
        }
    }

}
