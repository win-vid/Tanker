using UnityEngine;

// A projectile fired by enemies, which can rotate as it moves

public class EnemyProjectile : Projectile
{
    public override void Move()
    {
        // Move the projectile forward based on its speed
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
        transform.Rotate(0, 0, 10);

        // Decrease lifetime
        lifeTime -= Time.fixedDeltaTime;
    }

    public override void onSpawn()
    {
    }
}
