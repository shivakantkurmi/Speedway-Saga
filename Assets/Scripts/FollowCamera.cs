using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;  

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, player.position.z + offset.z);
        transform.position = newPosition;
    }
}
