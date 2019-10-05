 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Vector2 goal;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        goal = new Vector2(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        MoveWith(Seek());
    }

    Vector2 Seek()
    {
        Vector2 distanceVector = ((Vector2)this.transform.position - this.goal).normalized;
        print(this.transform.position);
        print(distanceVector);
        return distanceVector * speed ;
          
    }

    void MoveWith(Vector2 vector)
    {
        this.transform.position += new Vector3(vector.x, vector.y, 0);
    }
}
