using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_TelescopicArms : MonoBehaviour
{
    public float grappleSpeed;
    public float playerSpeed;
    public float maxDistance;

    public GameObject grapple;

    private bool grappleLaunched;
    private bool hooked;

    private GameObject grappleInstance;

    private Vector3 targetPosition;
    private Quaternion q; 


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !grappleLaunched && !hooked)
        {
            grappleLaunched = true;
            hooked = false;

            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;

            Vector3 direction = GetDirection();

            q = Quaternion.LookRotation(direction);

            grappleInstance = Instantiate(grapple, transform.position, Quaternion.identity);
        
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            grappleInstance.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);

        }

        if(grappleLaunched && (grappleInstance != null) && !hooked)
        {
            grappleInstance.transform.position = Vector3.MoveTowards(grappleInstance.transform.position, targetPosition, grappleSpeed * Time.deltaTime);

            if (grappleInstance.GetComponent<GrappleCollision>().GetIsColliding())
            {
                hooked = true;
            }

            if (grappleInstance.GetComponent<GrappleCollision>().GetIsCollidingDestroy())
            {
                ResetSkill();
            }

            // if maxDistance reached OR target reached and nothing hooked
            if ((Vector3.Distance(transform.position, grappleInstance.transform.position) >= maxDistance)
                || ((grappleInstance.transform.position == targetPosition) && !hooked)
                )
                ResetSkill();
        }

        if(hooked && (grappleInstance != null))
        {
            transform.position = Vector3.MoveTowards(transform.position, grappleInstance.transform.position, playerSpeed * Time.deltaTime);

            float dist = Vector3.Distance(transform.position, grappleInstance.transform.position);

            //if(transform.position == grappleInstance.transform.position)
            if(dist < 1.0f) ResetSkill();  
        }
    }

    Vector3 GetDirection()
    {
        return Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }

    void ResetSkill()
    {
        grappleLaunched = false;
        hooked = false;
        Destroy(grappleInstance);
    }

    public void FoundGrabbable()
    {
        Debug.Log("found grabbable object !");
    }

}
