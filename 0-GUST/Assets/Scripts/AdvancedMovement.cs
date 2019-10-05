using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedMovement : MonoBehaviour
{
    public float maxMovementSpeed;
    public float acceleration;
    public float deceleration;

    private float currentSpeed;

    enum MovementType
    {
        DECELERATION = 0,
        ACCELERATION = 1
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Not moving
        if (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            ComputeCurrentSpeed(MovementType.DECELERATION);
        }else
        {// Moving
            ComputeCurrentSpeed(MovementType.ACCELERATION);

            if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.S)) // forward
                transform.position += transform.TransformDirection(Vector3.up) * currentSpeed;
            
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Z)) // backward
                transform.position += transform.TransformDirection(Vector3.down) * currentSpeed;
            
            if (Input.GetKey(KeyCode.D) && !Input.GetKey("q")) // right
                transform.position += transform.TransformDirection(Vector3.right) * currentSpeed;
             
            if (Input.GetKey(KeyCode.Q) && !Input.GetKey("d")) // left
                transform.position += transform.TransformDirection(Vector3.left) * currentSpeed;
        }
    }

    void ComputeCurrentSpeed(MovementType mt)
    {
        if(mt == MovementType.DECELERATION)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0.0f) currentSpeed = 0.0f;
        }
        else if(mt == MovementType.ACCELERATION)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxMovementSpeed) currentSpeed = maxMovementSpeed;
        }
    }
}
