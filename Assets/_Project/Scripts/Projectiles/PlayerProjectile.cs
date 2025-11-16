using UnityEngine;

public class PlayerProjectile : Projectile
{
    protected override void Move()
    {
        // Move the projectile forward based on its speed
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    public override void onSpawn()
    {
    }
}

