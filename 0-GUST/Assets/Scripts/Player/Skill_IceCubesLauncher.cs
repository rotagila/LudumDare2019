using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_IceCubesLauncher : MonoBehaviour
{
    public GameObject particleEffect;
    GameObject iceCubesLauncherEffect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 direction = GetDirection();
            var targetDistance = direction.normalized * 0.3f;

            Quaternion q = Quaternion.LookRotation(direction);

            iceCubesLauncherEffect = Instantiate(particleEffect, transform.position, q);
            iceCubesLauncherEffect.transform.parent = transform;

            iceCubesLauncherEffect.GetComponent<ParticleSystem>().Play();

            Destroy(iceCubesLauncherEffect, iceCubesLauncherEffect.GetComponent<ParticleSystem>().duration * 1.75f);
        }

        if(iceCubesLauncherEffect != null)
            iceCubesLauncherEffect.transform.rotation = Quaternion.LookRotation(GetDirection());
    }

    Vector3 GetDirection()
    {
        return Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }
}
