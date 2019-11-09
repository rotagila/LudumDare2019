using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCollision : MonoBehaviour
{
    private bool isColliding = false;
    private bool isCollidingDestroy = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("onCollisionEnter");

        if (collision.gameObject.tag == "Grabbable")
        {
            Debug.Log("Colliding Grabbable");
            isColliding = true;
        }
            
        else if(collision.gameObject.tag != "Player")
            isCollidingDestroy = true;  
    }

    public bool GetIsColliding()
    {
        return isColliding;
    }

    public bool GetIsCollidingDestroy()
    {
        return isCollidingDestroy;
    }
}
