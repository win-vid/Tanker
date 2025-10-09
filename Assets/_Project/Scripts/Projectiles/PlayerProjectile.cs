using UnityEngine;

public class PlayerProjectile : Projectile
{

    void FixedUpdate()
    {
        // Move the projectile forward based on its speed
        transform.position += transform.forward * speed * Time.fixedDeltaTime;

        // Decrease lifetime
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0)
        {
            onImpact();
        }
    }
}

