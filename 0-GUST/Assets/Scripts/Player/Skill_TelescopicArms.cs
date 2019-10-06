using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_TelescopicArms : MonoBehaviour
{
    public float moveSpeed;

    private bool effectActive;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            effectActive = true;
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("ini: " + targetPosition);
        }

        if(effectActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            Debug.Log("pos: " + transform.position + " -- going to "+ targetPosition);
            if (transform.position == targetPosition) 
                effectActive = false;
        }
    }

}
