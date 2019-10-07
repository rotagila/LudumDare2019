using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public BennaorSceneHandler bennaorSceneHandler;

    public GameObject basicCharacter;

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

    private List<string> activeSkills = new List<string>(new string[] 
    {
        "Telescopic_Arms", "Ice_Cubes_Launcher", "EMP", "Flamethrower"
    });

    public List<string> unlockedSkills = new List<string>(new string[] { });



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

        // S'il y a une erreur ici, c'est que le HUD des composants est désactivé mais c'est pas grave




        //switch (currentLevel)
        //{
        //    case 0:
        //        bennaorSceneHandler.numberOfComponents = composantCount;
        //        bennaorSceneHandler.requiredNumberForUpgrade = GetRequiredNumberForUpgrade();
        //        bennaorSceneHandler.upgradeName = GetNextUpgradeName();
        //        bennaorSceneHandler.HandleDialogs(nextDialogToShow);
        //        break;
        //}
    }

    public void PickUp()
    {
        this.composantCount += 1;
        countText = GameObject.Find("ComposantCount").GetComponentInChildren<Text>();
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

    public Dictionary<string, System.Tuple<int, bool>> GetSkills(bool onlyActiveSkills)
    {
        if (!onlyActiveSkills) return skills;
        else
        {
            Dictionary<string, System.Tuple<int, bool>> activeSkills = new Dictionary<string, System.Tuple<int, bool>>()
            {
                {"Telescopic_Arms", new System.Tuple<int, bool>(9, false)},
                {"Ice_Cubes_Launcher", new System.Tuple<int, bool>(0, false)},
                {"EMP", new System.Tuple<int, bool>(0, false)},
                {"Flamethrower", new System.Tuple<int, bool>(2, false)},
            };

            return activeSkills;
        }
    }


    public Dictionary<string, System.Tuple<int, bool>> GetPassiveSkills()
    {

        Dictionary<string, System.Tuple<int, bool>> passiveSkills = new Dictionary<string, System.Tuple<int, bool>>()
        {
            {"Advanced_Movement", new System.Tuple<int, bool>(10, false)},
            {"Vision_180", new System.Tuple<int, bool>(5, false)},
            {"Vision_360", new System.Tuple<int, bool>(5, false)}
        };

        return passiveSkills;

    }

    public void LoadLevels()
    {
        SceneManager.LoadScene("Levels");
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 16;
    }

    //public List<System.Tuple<string, Component>> GetSkillsScripts(bool onlyActiveSkills)
    //{
    //    List<System.Tuple<string, Component>> list = new List<System.Tuple<string, Component>>();

    //    foreach (KeyValuePair<string, System.Tuple<int, bool>> entry in skills)
    //    {
    //        if(onlyActiveSkills)
    //        {
    //           if(activeSkills.Contains(entry.Key))
    //            {
    //                Component c = basicCharacter.GetComponent(entry.Key);
    //                list.Add(new System.Tuple<string, Component>(entry.Key, c));
    //            }
    //        }else
    //        {
    //            Component c = basicCharacter.GetComponent(entry.Key);
    //            list.Add(new System.Tuple<string, Component>(entry.Key, c));
    //        }
            
    //    }

    //    return list;
    //}


    // Update is called once per frame
    void Update()
    {
        
    }
}
