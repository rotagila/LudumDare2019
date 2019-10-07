using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public BennaorSceneHandler bennaorSceneHandler;

    public int composantCount = 0;

    public Text countText;

    public int currentLevel = 0;   

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void InitGame()
    {
       bennaorSceneHandler = gameObject.GetComponent<BennaorSceneHandler>();

        if (countText == null)
        {
            // S'il y a une erreur ici, c'est que le HUD des composants est désactivé mais c'est pas grave
            countText = GameObject.Find("ComposantCount").GetComponentInChildren<Text>();
        }

        switch(currentLevel)
        {
            case 0:
                bennaorSceneHandler.HandleDialogs(bennaorSceneHandler.nextDialogToShow);
                break;
        }
        
    }

    public void PickUp()
    {
        this.composantCount += 1;
        countText.text = composantCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
