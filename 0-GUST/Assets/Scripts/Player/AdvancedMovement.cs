using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedMovement : MonoBehaviour
{
    public float maxMovementSpeed;
    public float accelerationFactor;
    public float decelerationFactor;
    public Sprite sprite_north;
    public Sprite sprite_south;
    public Sprite sprite_west;
    public Sprite sprite_east;

    public float angle;

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
        HandleMovement();
        HandleSpriteOrientation();
    }

    void HandleMovement()
    {
        // Not moving
        if (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            ComputeCurrentSpeed(MovementType.DECELERATION);
        }
        else
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
            currentSpeed -= decelerationFactor * maxMovementSpeed * Time.deltaTime;
            if (currentSpeed < 0.0f) currentSpeed = 0.0f;
        }
        else if(mt == MovementType.ACCELERATION)
        {
            currentSpeed += accelerationFactor * maxMovementSpeed * Time.deltaTime;
            if (currentSpeed > maxMovementSpeed) currentSpeed = maxMovementSpeed;
        }
    }

    void HandleSpriteOrientation()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Debug.Log(angle);

        if (angle > 45 && angle <= 135)
            GetComponent<SpriteRenderer>().sprite = sprite_north;
        
        else if (angle > -135 && angle <= -45)
            GetComponent<SpriteRenderer>().sprite = sprite_south;
        
        else if (Mathf.Abs(angle) > 135 && Mathf.Abs(angle) <= 180)
            GetComponent<SpriteRenderer>().sprite = sprite_west;
        
        else if (Mathf.Abs(angle) >= 0 && Mathf.Abs(angle) <= 45)
                GetComponent<SpriteRenderer>().sprite = sprite_east;

    }

}
