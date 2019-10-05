using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{ 
    private string _objName;
    private int _objValue;

    [SerializeField]
    private GameObject _buyButton;
    
    /*[SerializeField]
    private PlayerStat _playerStat*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitShop()
    {
        _buyButton.SetActive(false);
    }

    public void SelectItem(ISkill skill)
    {
        _objName = skill.Name;
        _objValue = skill.Value;
        _buyButton.SetActive(true);
        _buyButton.GetComponentInChildren<Text>().text = "Buy " + _objName + " for " + _objValue + " PO ?";
    }

    public void ConfirmPayment()
    {
        //playerStat.money -= _objValue;
        //GameObject.Find(_objName);
    }
}
