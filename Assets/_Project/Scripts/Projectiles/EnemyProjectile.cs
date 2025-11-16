using UnityEngine;

// A projectile fired by enemies, which can rotate as it moves

public class EnemyProjectile : Projectile
{
    Vector3 _correctSize;
    protected override void Move()
    {
        // Move the projectile forward based on its speed
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
        transform.Rotate(0, 0, 10);

        /*
        // Scale up the projectile to its correct size shortly after spawning
        if (transform.localScale.x < _correctSize.x)
        {
            transform.localScale += _correctSize * 1f * Time.fixedDeltaTime;
            if (transform.localScale.x > _correctSize.x)
            {
                transform.localScale = _correctSize;
            }
        }
        */
        // Decrease lifetime
        currentLifeTime -= Time.fixedDeltaTime;
    }

    public override void onSpawn()
    {
    }
}
