using UnityEngine;

// Dissolves the dissolve Shader

public class EffectDissolve : MonoBehaviour
{
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private float dissolveDuration = 2.0f;

    private float dissolveAmount = 0.0f;
    private bool isDissolving = true;

    void Awake()
    {
        dissolveMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount += Time.deltaTime / dissolveDuration;
            dissolveMaterial.SetFloat("DissolveStrength", dissolveAmount);

            if (dissolveAmount >= 1.0f)
            {
                isDissolving = false;
            }
        }
    }

    public void StartDissolve()
    {
        dissolveAmount = 0.0f;
        isDissolving = true;
    }
}
