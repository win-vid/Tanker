using UnityEngine;

public class Rotor : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // rotate around the y axis
        transform.Rotate(0, 1000 * Time.deltaTime, 0);
    }
}
