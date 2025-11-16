using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletCase : MonoBehaviour
{
    Rigidbody rb;
    float despawnTime = 3f, currentDespawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentDespawnTime = despawnTime;
    }

    public void SpawnCase()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero; // reset just in case
        rb.angularVelocity = Vector3.zero;

        // Add a little random ejection force for realism
        Vector3 ejectDirection = (Vector3.up + Turret.instance.turret.transform.right * 3);
        rb.AddForce(ejectDirection * 3f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
    }


    void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            
            currentDespawnTime -= Time.deltaTime;

            if (currentDespawnTime<=0)
            {
                this.gameObject.SetActive(false);
                currentDespawnTime = despawnTime;
            }
        }
    }
}
