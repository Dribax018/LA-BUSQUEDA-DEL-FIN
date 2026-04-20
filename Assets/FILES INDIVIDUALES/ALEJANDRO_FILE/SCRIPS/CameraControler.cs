using UnityEngine;

public class CameraControler : MonoBehaviour
{

    public Transform player;

    void Start() { }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}

