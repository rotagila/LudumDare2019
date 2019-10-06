using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{

    public Vector2 goal = new Vector2(5, 5);
    public float speed = 1f;
    public float attractionRange = 8f;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        this.goal = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (this.goal - (Vector2)this.transform.position);
        float distance = direction.magnitude;
        if ( distance < attractionRange)
        {
            if (distance > 0.5f)
            {
                direction = direction.normalized;
                this.transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime * attractionRange / distance;
            } else
            {
                this.PickUp();
            }  
        } 
    }

    void PickUp()
    {
        // TODO
        Destroy(gameObject);
    }
}
