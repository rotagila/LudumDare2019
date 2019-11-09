using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedMovement : MonoBehaviour
{
    public float maxMovementSpeed;
    public float accelerationFactor;
    public Sprite sprite_north;
    public Sprite sprite_south;
    public Sprite sprite_west;
    public Sprite sprite_east;

    public float angle;

    public Rigidbody2D body2D;


    private Vector2 movement;
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

        body2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleSpriteOrientation();
    }

    void HandleMovement()
    { 
        movement.x = Input.GetAxisRaw("Horizontal");

        movement.y = Input.GetAxisRaw("Vertical");


        if (movement == new Vector2(0, 0))
        {
            ComputeCurrentSpeed(MovementType.DECELERATION);
        }
        else
        {
            ComputeCurrentSpeed(MovementType.ACCELERATION);
        }

        Debug.Log(movement * currentSpeed);


        body2D.MovePosition(body2D.position + movement * currentSpeed * Time.deltaTime);

    }

    void ComputeCurrentSpeed(MovementType mt)
    {
        if(mt == MovementType.DECELERATION)
        {
            currentSpeed = 0;
        }
        else if(mt == MovementType.ACCELERATION)
        {
            currentSpeed += accelerationFactor * maxMovementSpeed;
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
