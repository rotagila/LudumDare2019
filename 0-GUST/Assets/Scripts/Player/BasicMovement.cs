using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.Z)) // forward
        {
            // transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            transform.position += transform.TransformDirection(Vector3.up) * Time.deltaTime * movementSpeed;
        }
        else if(Input.GetKey(KeyCode.S)) // backward
        {
            //transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            transform.position += transform.TransformDirection(Vector3.down) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey("q")) // right
        {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.Q) && !Input.GetKey("d")) // left
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
    }
}

