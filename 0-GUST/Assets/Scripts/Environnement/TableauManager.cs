using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class TableauManager : MonoBehaviour
{
    public int currentTableau = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            Transform newpos = collision.GetComponentInChildren<Door>().OpenDoor(currentTableau);
            transform.position = newpos.position;
            Debug.Log("at " + currentTableau);

            string tmp = Regex.Match(newpos.name, @"\d+").Value;
            currentTableau = Int32.Parse(tmp);

            Debug.Log("go to " + currentTableau);

        }
    }
}
