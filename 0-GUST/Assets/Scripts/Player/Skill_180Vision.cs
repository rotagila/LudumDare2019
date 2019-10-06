using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_180Vision : MonoBehaviour
{
    public GameObject vision180;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vision180.transform.position = this.transform.position;
        float angle = this.GetComponent<AdvancedMovement>().angle;
        vision180.transform.eulerAngles = new Vector3(0, 0, angle+90);
    }

    private void OnEnable()
    {
        vision180 = Instantiate(vision180, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void OnDisable()
    {
        Destroy(vision180);
    }
}
