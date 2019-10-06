using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision_Flamethrower : MonoBehaviour
{

    public Sprite burntSprite;

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("collision");
        GetComponent<SpriteRenderer>().sprite = burntSprite;
    }
}
