using UnityEngine;

/*
*   The Aim Assists represents a little "UI" Element. Its a little white circle that always points into the direction where the turret is shooting at.
*   The script uses a line renderer to calculate the distance from the turret to the max distance of the aim assist.
*   if an object with a collider box intercepts the line, the aim assist will relocate to the hit position.
*/

public class AimAssist : MonoBehaviour
{
    [SerializeField] Transform aimBaseTransform;
    [SerializeField] Vector3 Offset = Vector3.zero;
    [SerializeField] Transform targetTransform;     // transform of the target
    [SerializeField] LayerMask aimLayerMask;
    [SerializeField] float aimAssistLength = 5f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (aimBaseTransform == null)
        {
            Debug.LogError("AimAssist: Aim Base Transform is not assigned.");
            return;
        }
    }
    
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (aimBaseTransform != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(aimBaseTransform.position, aimBaseTransform.forward, out hit, aimAssistLength, 72))
            {
                targetTransform.position = hit.point;
            }
            else
            {
                targetTransform.position = aimBaseTransform.position + aimBaseTransform.forward * aimAssistLength;
            }
        }
    }
}
