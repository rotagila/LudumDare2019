using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnjInteraction : MonoBehaviour
{
    private bool _canOpenShop = false;

    [SerializeField]
    private GameObject _talkToast;
    private CharacterController2D _playerMouvement;
    [SerializeField]
    private GameObject _shop;

    // Start is called before the first frame update
    void Start()
    {
        _canOpenShop = false;
        Cursor.lockState = CursorLockMode.Locked;
        _playerMouvement = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && _canOpenShop)
        {
            Debug.Log("openshop");
            _talkToast.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            _playerMouvement.enabled = false;
            _shop.SetActive(true);
        }

        if (Input.GetKey(KeyCode.Escape) && _shop.activeInHierarchy)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            _shop.GetComponent<Shop>().QuitShop();
            _shop.SetActive(false);
            _playerMouvement.enabled = true;
            _talkToast.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PNJ")
        {
            _talkToast.SetActive(true);
            _canOpenShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PNJ")
        {
            _talkToast.SetActive(false);
            _canOpenShop = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }


}
