using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrack : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    public ObjectPool.PoolType wrackPoolType;
    [SerializeField] GameObject turret;
    float despawnTime = 5f;
    float currentDespawnTime;
    [SerializeField] Light expLight;
    List<Renderer> renderers = new List<Renderer>();
    [SerializeField] bool dissolve = false;

    [SerializeField] AudioClip[] explosionSounds;

    void Awake()
    {
        ps.Stop();
        // repeat the particle system only once
        var main = ps.main;
        main.loop = false;
        currentDespawnTime = despawnTime;
        renderers.AddRange(GetComponentsInChildren<Renderer>());
    }

    public void playParticleSystem()
    {
        ps.Play();
        if (turret != null)
        {
            popTurret();
        }
    }

    void Update()
    {
        currentDespawnTime -= Time.deltaTime;
        if (currentDespawnTime <= 0f)
        {
            this.gameObject.SetActive(false);
            currentDespawnTime = despawnTime;
        }
    }



    public void popTurret()
    {
        // reset turret position and rotation
        turret.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
        turret.transform.rotation = this.transform.rotation;
        turret.transform.rotation *= Quaternion.Euler(0, Random.Range(0, 360), 0);

        Rigidbody turretHead = turret.GetComponent<Rigidbody>();
        turretHead.isKinematic = false;
        turretHead.AddForce(Vector3.up * 7f, ForceMode.Impulse);
    }

    // activate light for 0.5 sec then deactivate
    IEnumerator LightFlash()
    {
        if (expLight == null) yield break;
        
        expLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        expLight.enabled = false;
    }

    public void Spawn()
    {
        playParticleSystem();
        StartCoroutine(LightFlash());
        SoundEffectsManager.instance.PlayRandomSoundEffect(explosionSounds, transform,1f);
        if (dissolve) StartCoroutine(DissolveOverTime());
    }

    void setDissolveStrength(float value)
    {
        if (renderers.Count == 0) return;



        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                mat.SetFloat("_DissolveStrength", value);
            }
        }
    }
    
    IEnumerator DissolveOverTime()
    {
        float duration = 3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float dissolveValue = Mathf.Lerp(0f, 1f, elapsed / duration);
            setDissolveStrength(dissolveValue);
            elapsed += Time.deltaTime;
            yield return null;
        }

        setDissolveStrength(1f);
    }


}
