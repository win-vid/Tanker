using UnityEngine;

// LightFlicker simulates a flickering candlelight effect by smoothly varying the light intensity over time.

public class LightFlicker : MonoBehaviour
{
    private Light candleLight;

    [Header("Flicker Settings")]
    [SerializeField] float flickerSpeed = 2f;
    [SerializeField] float intensityChangeInterval = 0.5f;
    [SerializeField] float minFlickerMultiplier = 0.5f;
    [SerializeField] float maxFlickerMultiplier = 1.5f;

    private float baseIntensity;
    private float targetIntensity;
    private float nextTargetTime;

    void Start()
    {
        candleLight = GetComponentInChildren<Light>();
        baseIntensity = candleLight.intensity;
        SetNewTargetIntensity();
    }

    void Update()
    {
        // Smoothly lerp toward the target intensity
        candleLight.intensity = Mathf.Lerp(candleLight.intensity, targetIntensity, Time.deltaTime * flickerSpeed);

        // Set a new target intensity every interval
        if (Time.time >= nextTargetTime)
        {
            SetNewTargetIntensity();
        }
    }

    void SetNewTargetIntensity()
    {
        targetIntensity = baseIntensity * Random.Range(minFlickerMultiplier, maxFlickerMultiplier);
        nextTargetTime = Time.time + intensityChangeInterval;
    }
}
