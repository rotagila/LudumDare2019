using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision_Particle : MonoBehaviour
{

    public Sprite burntSprite;

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

        
    }
}
