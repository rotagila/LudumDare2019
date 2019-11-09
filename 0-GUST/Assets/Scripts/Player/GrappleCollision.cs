using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCollision : MonoBehaviour
{
    private bool isColliding = false;
    private bool isCollidingDestroy = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grabbable")
            isColliding = true;
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
