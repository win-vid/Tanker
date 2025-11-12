using UnityEngine;

[RequireComponent(typeof(Light))]
public class HealthLightIndicator : MonoBehaviour
{
    public static HealthLightIndicator instance;
    Light light;

    void Start()
    {
        instance = this;
        light = GetComponent<Light>();
    }

    public void checkColor()
    {
        float currentHealth = PlayerStateMachine.instance.currentHealth;
        if (PlayerStateMachine.instance.currentHealth <= currentHealth * 0.25f) light.color = Color.red;
        else light.color = new Color(0, 0,11f, 1);
    }

}
