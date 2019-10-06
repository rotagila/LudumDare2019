using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComposantCount : MonoBehaviour
{
    public int composantCount = 0;

    public Text countText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp()
    {
        this.composantCount += 1;
        countText.text = composantCount.ToString();
    }
}
