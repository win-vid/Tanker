using UnityEngine;

public class PlayerProjectile : Projectile
{
    public override void Move()
    {
        // Move the projectile forward based on its speed
        transform.position += transform.forward * speed * Time.fixedDeltaTime;

        // Decrease lifetime
        lifeTime -= Time.fixedDeltaTime;
    }

    public override void onSpawn()
    {
    }
}

