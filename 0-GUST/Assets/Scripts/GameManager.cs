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

    public int nextDialogToShow = 0;

    // skill, cost, isUnlocked
    private Dictionary<string, System.Tuple<int, bool>> skills = new Dictionary<string, System.Tuple<int, bool>>()
    {
        {"Advanced_Movement", new System.Tuple<int, bool>(5, false)},
        {"Movement_Rollers", new System.Tuple<int, bool>(5, false)},
        {"Vision_180", new System.Tuple<int, bool>(5, false)},
        {"Vision_360", new System.Tuple<int, bool>(5, false)},
        {"Telescopic_Arms", new System.Tuple<int, bool>(9, false)},
        {"Ice_Cubes_Launcher", new System.Tuple<int, bool>(0, false)},
        {"EMP", new System.Tuple<int, bool>(0, false)},
        {"Flamethrower", new System.Tuple<int, bool>(2, false)},
    };


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

        bennaorSceneHandler.numberOfComponents = composantCount;
        bennaorSceneHandler.requiredNumberForUpgrade = GetRequiredNumberForUpgrade();
        bennaorSceneHandler.upgradeName = GetNextUpgradeName();

        switch (currentLevel)
        {
            case 0:
                bennaorSceneHandler.HandleDialogs(nextDialogToShow);
                break;
        }
    }

    public void PickUp()
    {
        this.composantCount += 1;
        countText.text = composantCount.ToString();
    }

    int GetRequiredNumberForUpgrade()
    {
        int cost = composantCount + 1;

        foreach (KeyValuePair<string, System.Tuple<int, bool>> entry in skills)
        {
            // if not unlocked
            if (!entry.Value.Item2) return entry.Value.Item1;
        }

        return cost;
    }

    string GetNextUpgradeName()
    {
        string name = "";

        foreach (KeyValuePair<string, System.Tuple<int, bool>> entry in skills)
        {
            if (!entry.Value.Item2) return entry.Key;
        }

        return name;
    }

    public bool UnlockSkill(string name)
    {
        if (skills.ContainsKey(name))
        {
            composantCount -= skills[name].Item1;

            skills[name] = new System.Tuple<int, bool>(skills[name].Item1, skills[name].Item2);
            return true;
        }

        return false;
    }

    public void SetNextDialogToShow(int i)
    {
        nextDialogToShow = i;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
