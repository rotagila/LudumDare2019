using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ancor_Follow : MonoBehaviour
{
    [SerializeField]
    Transform[] AncorList;

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchAncor(int i)
    {
        Vector3 newPos;

        newPos.x = AncorList[i].transform.position.x;
        newPos.y = AncorList[i].transform.position.y;
        newPos.z = -10;
        transform.position = newPos;
    }
}
