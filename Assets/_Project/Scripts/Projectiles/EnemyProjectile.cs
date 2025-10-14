using UnityEngine;

// A projectile fired by enemies, which can rotate as it moves

public class EnemyProjectile : Projectile
{
    protected override void Move()
    {
        // Move the projectile forward based on its speed
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
        transform.Rotate(0, 0, 10);

        // Decrease lifetime
        currentLifeTime -= Time.fixedDeltaTime;
    }

    protected override void onSpawn()
    {
        setMaterialColor(Color.red);
    }
}
