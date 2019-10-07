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

    public GameObject TableauIn;
    public GameObject TableauOut;

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
            if (TableauIn != null)
            {
                TableauOut.SetActive(true);
                TableauIn.SetActive(false);
            }

            _camera.SwitchAncor(_out);
            return _outTrans;
        }
        else
        {
            if (TableauIn != null)
            {
                TableauIn.SetActive(true);
                TableauOut.SetActive(false);
            }
            //DisableTableauOut
            //EnableTableauIn
            _camera.SwitchAncor(_in);
            return _inTrans;
        }
    }

}
