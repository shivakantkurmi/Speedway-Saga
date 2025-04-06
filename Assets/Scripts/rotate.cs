using UnityEngine;

public class DiamondRotator : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        transform.RotateAround(GetComponent<Renderer>().bounds.center, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
