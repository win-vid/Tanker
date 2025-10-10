using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public bool hurtPlayer;

    void FixedUpdate()
    {
        // Move the projectile
        if (lifeTime <= 0)
        {
            onImpact();
        }
    }

    // on collision with obstacle destroy the projectile
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            onImpact();
        }
    }

    public void onImpact()
    {
        Destroy(gameObject);
    }
}
