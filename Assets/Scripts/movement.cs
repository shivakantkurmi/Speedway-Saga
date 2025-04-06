using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRunner : MonoBehaviour
{
    public float forwardSpeed = 10f;  
    public float horizontalSpeed = 5f; 
    public float leftTurnSpeed = 3f; // Less turning speed to the left
    public float rightTurnSpeed = 5f; // Normal turning speed to the right
    public float speedIncreaseRate = 0.1f; 

    void Update()
    {
        MoveForward();
        MoveLeftRight();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        forwardSpeed += speedIncreaseRate * Time.deltaTime;
    }

    void MoveLeftRight()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        
        float turnSpeed = 0f;
        if (moveHorizontal < 0) // Turning left
        {
            turnSpeed = leftTurnSpeed;
        }
        else if (moveHorizontal > 0) // Turning right
        {
            turnSpeed = rightTurnSpeed;
        }

        Vector3 horizontalMovement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.Translate(horizontalMovement * turnSpeed * Time.deltaTime);
    }
}
