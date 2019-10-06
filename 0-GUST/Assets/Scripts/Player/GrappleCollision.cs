using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCollision : MonoBehaviour
{
    private bool isColliding = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Grabbable")
            isColliding = true;
    }

    public bool GetIsColliding()
    {
        return isColliding;
    }
}
