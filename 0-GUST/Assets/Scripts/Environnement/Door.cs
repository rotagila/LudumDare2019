using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Ancor_Follow _camera;

    private int _in;
    private int _out;

    [SerializeField]
    private Transform _inTrans;
    [SerializeField]
    private Transform _outTrans;

    // Start is called before the first frame update
    void Start()
    {
        string tmp;

        tmp = Regex.Match(_inTrans.name, @"\d+").Value;
        _in = Int32.Parse(tmp);
        tmp = Regex.Match(_outTrans.name, @"\d+").Value;
        _out = Int32.Parse(tmp);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Transform OpenDoor(int i)
    {
        if (i == _in)
        {
            _camera.SwitchAncor(i);
            return _outTrans;
        }
        else
        {
            _camera.SwitchAncor(i - 2);
            return _inTrans;
        }
    }

}
