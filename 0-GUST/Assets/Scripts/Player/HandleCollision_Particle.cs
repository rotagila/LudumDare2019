using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision_Particle : MonoBehaviour
{

    public Sprite burntSprite;

    public GameObject subParticleEffect;
    GameObject iceCubesShatterEffect;

    private void OnParticleCollision(GameObject other)
    {
        if(other.name.Equals("ParticleSystem_Flamethrower(Clone)"))
        {
            Debug.Log("hot !");
            GetComponent<SpriteRenderer>().sprite = burntSprite;
        }
        else if (other.name.Equals("ParticleSystem_Emp(Clone)"))
        {
            Debug.Log("emp'd");
        }
        else if (other.name.Equals("ParticleSystem_IceCubesLauncher(Clone)"))
        {
            Debug.Log("freezzze");

            iceCubesShatterEffect = Instantiate(subParticleEffect, transform.position, Quaternion.identity);

            iceCubesShatterEffect.GetComponent<ParticleSystem>().Play();

            Destroy(iceCubesShatterEffect, iceCubesShatterEffect.GetComponent<ParticleSystem>().duration);
        }
    }
}
