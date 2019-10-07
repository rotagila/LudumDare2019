using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Emp : MonoBehaviour
{
    public GameObject particleEffect;
    GameObject empEffect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            empEffect = Instantiate(particleEffect, transform.position, Quaternion.identity);
            empEffect.transform.parent = transform;
            empEffect.GetComponent<ParticleSystem>().Play();
            Destroy(empEffect, empEffect.GetComponent<ParticleSystem>().duration);
        }
    }
}
