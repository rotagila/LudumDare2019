using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Flamethrower : MonoBehaviour
{
    public GameObject particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var targetDistance = direction.normalized * 0.3f;

            Quaternion q = Quaternion.LookRotation(direction);

            GameObject flamethrowerEffect = Instantiate(particleEffect, transform.position, q);

            // deprecated, could be great to find something else
            flamethrowerEffect.GetComponent<ParticleSystem>().startLifetime = Vector3.Distance(transform.position, transform.position + targetDistance);




            GetComponent<AdvancedMovement>().gameObject.SetActive(false);

            flamethrowerEffect.GetComponent<ParticleSystem>().Play();
            Destroy(flamethrowerEffect, flamethrowerEffect.GetComponent<ParticleSystem>().duration);

            GetComponent<AdvancedMovement>().gameObject.SetActive(true);

        }
    }
}
