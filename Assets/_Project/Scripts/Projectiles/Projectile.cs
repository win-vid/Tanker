using UnityEngine;

/* 
* Base class for all projectiles in the game.
* Handles movement, lifetime, and collision with obstacles.
* Inherit from this class to create specific projectile types (e.g., PlayerProjectile, EnemyProjectile).
* Each subclass must implement the Move() and onSpawn() methods.
*/

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    [HideInInspector] public float currentLifeTime;
    public int damage;
    public bool hurtPlayer;

    void Awake()
    {
        currentLifeTime = lifeTime;
        onSpawn();
    }

    void FixedUpdate()
    {
        Move();     // Move the projectile
        if (currentLifeTime <= 0)
        {
            onImpact();
        }
    }

    public abstract void onSpawn();
    public abstract void Move();

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
        gameObject.SetActive(false);
        currentLifeTime = lifeTime; // reset lifetime for object pooling
    }
}
