using UnityEngine;

/*
*   Mainly used for rotating object.
*   F.E. the rotors of the TVP drone.
*/

public class Rotor : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1000f;

    // Update is called once per frame
    void Update()
    {
        // rotate around the y axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
