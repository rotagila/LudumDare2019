using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Flamethrower : MonoBehaviour
{
    public GameObject particleEffect;
    private bool effectActive = false;
    GameObject flamethrowerEffect;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 direction = GetDirection();
            var targetDistance = direction.normalized * 0.3f;

            Quaternion q = Quaternion.LookRotation(direction);

            flamethrowerEffect = Instantiate(particleEffect, transform.position, q);
            flamethrowerEffect.transform.parent = transform;

            // deprecated, could be great to find something else
            flamethrowerEffect.GetComponent<ParticleSystem>().startLifetime = Vector3.Distance(transform.position, transform.position + targetDistance);

           
            effectActive = true;
            flamethrowerEffect.GetComponent<ParticleSystem>().Play();
        }

        if(effectActive && Input.GetMouseButtonUp(0))
        {
            effectActive = false;
            Destroy(flamethrowerEffect, flamethrowerEffect.GetComponent<ParticleSystem>().duration);
            
        }

        if(effectActive && Input.GetMouseButton(0))
        {
            flamethrowerEffect.transform.rotation = Quaternion.LookRotation(GetDirection());
        }
    }

    Vector3 GetDirection()
    {
        return Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }
}
