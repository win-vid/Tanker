using Mono.Cecil.Cil;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

/* 
* Base class for all projectiles in the game.
* Handles movement, lifetime, and collision with obstacles.
* Inherit from this class to create specific projectile types (e.g., PlayerProjectile, EnemyProjectile).
* Each subclass must implement the Move() and onSpawn() methods.
*/

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime;
    [HideInInspector] public float currentLifeTime;
    [SerializeField] public int damage;
    public bool hurtPlayer;
    protected Material projectileMaterial;
    [SerializeField] protected Color projectileColor;

    void Awake()
    {
        currentLifeTime = lifeTime;
        this.projectileMaterial = GetComponent<Renderer>()!.material;
        if (this.projectileMaterial != null) setMaterialColor(projectileColor);
        onSpawn();
    }

    void FixedUpdate()
    {
        Move();     // Move the projectile
        currentLifeTime -= Time.fixedDeltaTime; // decrease lifetime
        if (currentLifeTime <= 0)
        {
            onImpact();
        }

        
    }

    // called when the projectile is spawned
    public abstract void onSpawn();

    // called every fixed update
    protected abstract void Move();

    // on collision with obstacle destroy the projectile
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            onImpact();
        }
    }

    // sets the color of the projectile material to a given color
    protected void setMaterialColor(Color color)
    {
        if (this.projectileMaterial != null)
        {
            this.projectileMaterial.color = color;
            this.projectileMaterial.SetColor("_EmissionColor", color);

        }
    }

    // handles what happens on impact (currently just deactivates the projectile for object pooling)
    public void onImpact()
    {
        ResetProjectile();
        currentLifeTime = lifeTime; // reset lifetime for object pooling
        gameObject.SetActive(false);

    }

    protected virtual void ResetProjectile()
    {
        // Override in subclasses if additional reset logic is needed
    }
}
