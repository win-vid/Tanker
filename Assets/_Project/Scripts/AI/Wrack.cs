using UnityEngine;

public class Wrack : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    public ObjectPool.PoolType wrackPoolType;
    [SerializeField] GameObject turret;
    float despawnTime = 5f;
    float currentDespawnTime;

    void Awake()
    {
        ps.Stop();
        // repeat the particle system only once
        var main = ps.main;
        main.loop = false;
        currentDespawnTime = despawnTime;
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

}
