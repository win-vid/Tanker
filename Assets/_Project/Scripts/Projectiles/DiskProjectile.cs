using UnityEngine;

// A projectile fired by enemies, spawns 4 bullets in cardinal directions when its lifetime ends

public class DiskProjectile : Projectile
{
    protected override void Move()
    {
        // Move the projectile forward based on its speed
        transform.position += transform.forward * speed * Time.fixedDeltaTime;

        // Decrease lifetime
        currentLifeTime -= Time.fixedDeltaTime;

        if (currentLifeTime <= 0)
        {
            spawnProjectiles();
        }
    }

    public override void onSpawn()
    {
        
    }

    void spawnProjectiles()
    {
        float baseYRotation = transform.rotation.eulerAngles.y;
        for (int i = 0; i <= 3; i++)
        {
            GameObject projectile = ObjectPool.instance.GetPooledObject(ObjectPool.PoolType.EnemyBulletEasy);
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.Euler(
                0f,
                baseYRotation + (i * 90f),
                0f
                );
                projectile.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile") || other.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
            spawnProjectiles();
            onImpact();
        }
    }
}
