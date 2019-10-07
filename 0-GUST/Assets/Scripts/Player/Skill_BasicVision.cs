using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BasicVision : MonoBehaviour
{

    public GameObject basicVision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        basicVision.transform.position = this.transform.position;
    }

    private void OnEnable()
    {
        basicVision = Instantiate(basicVision, new Vector3(0, 0, 0), Quaternion.identity);
        basicVision.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    private void OnDisable()
    {
        Destroy(basicVision);
    }
}
