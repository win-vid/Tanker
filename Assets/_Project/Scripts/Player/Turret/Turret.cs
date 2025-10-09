using UnityEngine;
using UnityEngine.InputSystem;

// This is the Turret Script.
// The Turret will always rotate towards the mouse current x, y position.
// The Turret will rotate only on the y axis.
// The Turret shoots with the left mouse button. After each shot a cool down timer will take effect.

public class Turret : MonoBehaviour
{
    public GameObject turret;
    [Range(0f, 10f)] public float rotationSpeed = 20f;
    public float shootingSpeed = 1.5f;
    float coolDown = 0f;
    bool alive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            RotateTurret();
            ReceiveInput();
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
        if (coolDown >= 0) coolDown -= Time.deltaTime;
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
        // Shoot Logic Here ...
        Debug.Log("Pew Pew");
    }
}
