using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    Ancor_Follow _camera;

    // Start is called before the first frame update
    void Start()
    {
        if (_camera == null)
            _camera = GameObject.Find("Main Camera").GetComponent<Ancor_Follow>();

        _camera.GetAnchors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
