using System.Collections;
using MilkShake;
using UnityEngine;
using UnityEngine.InputSystem;

// This is the Turret Script.
// The Turret will always rotate towards the mouse current x, y position.
// The Turret will rotate only on the y axis.
// The Turret shoots with the left mouse button. After each shot a cool down timer will take effect.

public class Turret : MonoBehaviour
{
    public static Turret instance;
    public GameObject turret;
    [SerializeField] GameObject projectilePrefab;
    [Range(0f, 10f)] public float rotationSpeed = 20f;
    public float initialShootingSpeed = 1.5f;
    public float shootingSpeed;
    float coolDown = 0f;
    bool alive = true;
    [SerializeField] ShakePreset shootShake;

    [Header("Sounds")]
    [SerializeField] AudioClip[] shootSounds;
    [SerializeField] MouseLoadingEffect mouseLoadingEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        shootingSpeed = initialShootingSpeed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (alive)
        {
            RotateTurret();
            ReceiveInput();
            RenderLine();
            UpdateCoolDown();
        }
        else
        {
            // Dead
        }
    }

    // Update the Cool Down Timer when the Turret has shot
    void UpdateCoolDown()
    {
        if (coolDown > 0f)
        {
            coolDown -= Time.deltaTime;

            // Show loading effect if not active
            if (!mouseLoadingEffect.gameObject.activeSelf)
                mouseLoadingEffect.Show();

            // Hide slightly before cooldown finishes (0.5s early)
            if (coolDown <= 0.1f && mouseLoadingEffect.gameObject.activeSelf)
                mouseLoadingEffect.Hide();
        }
        else
        {
            // Safety check: make sure it's hidden when done
            if (mouseLoadingEffect.gameObject.activeSelf)
                mouseLoadingEffect.Hide();
        }
    }

    // Rotate the turret to face the mouse position
    void RotateTurret()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 heightCorrectedPoint = new Vector3(point.x, turret.transform.position.y, point.z);
            Quaternion targetRotation = Quaternion.LookRotation(heightCorrectedPoint - turret.transform.position);
            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        /*
        // FPS MODE

        [SerializeField] float mouseSensitivity = 2f;
        [SerializeField] float smoothTime = 0.05f;

        float yaw;
        float yawVelocity;

        void RotateTurret()
        {
            Vector2 delta = Mouse.current.delta.ReadValue();
            float mouseX = delta.x * mouseSensitivity;

            yaw = Mathf.SmoothDamp(yaw, yaw + mouseX, ref yawVelocity, smoothTime);

            turret.transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
        */
    }


    void ReceiveInput()
    {
        if (Mouse.current.leftButton.isPressed && coolDown <= 0f)
        {
            Shoot();
            coolDown = shootingSpeed;
        }
    }

    void Shoot()
    {
        GameObject projectile = ObjectPool.instance.GetPooledObject(ObjectPool.PoolType.PlayerBullet);
        if (projectile != null)
        {
            projectile.transform.position = turret.transform.position + turret.transform.forward * 2f;
            projectile.transform.rotation = turret.transform.rotation;
            projectile.SetActive(true);
            SoundEffectsManager.instance.PlayRandomSoundEffect(shootSounds, turret.transform, 1f);
            Shaker.instance.Shake(shootShake);
            StartCoroutine(spawnBulletCase());
        }

        // Spawn Bullet Case

        
    }

    IEnumerator spawnBulletCase()
    {
        GameObject bulletCase = ObjectPool.instance.GetPooledObject(ObjectPool.PoolType.BULLETCASE);
        if (bulletCase != null)
        {
            bulletCase.transform.position = turret.transform.position;
            bulletCase.transform.rotation = turret.transform.rotation;
            bulletCase.SetActive(true);
            BulletCase bc = bulletCase.GetComponent<BulletCase>();
            bc.SpawnCase();
        } 
        yield return 0;
    }

    void RenderLine()
    {
        Debug.DrawRay(turret.transform.position, turret.transform.forward * 100f, Color.red);
    }

    public void setAlive(bool state)
    {
        alive = state;
    }
}
