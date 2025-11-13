using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] PlayerStateMachine player;

    void Awake()
    {
        player = FindFirstObjectByType<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }
}
