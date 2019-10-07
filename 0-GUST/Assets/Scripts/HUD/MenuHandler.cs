using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuHandler : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject skillSelectionMenu;

    public GameObject[] activeSkillsPanels;
    public GameObject GameHandler;

    //private List<string> unlockedSkills = new List<string>(new string[] 
    //{
    //    "Basic_Vision",
    //  "Flamethrower", "EMP"
    //});

    private List<String> allSkills = new List<string>(new string[]
    {
        "Basic_Vision", "Vision_180", "Vision_360", "Advanced_Movement",
      "Flamethrower", "Ice_Cubes_Launcher", "Tazzer", "Telescopic_Arms", "Benn_Camo", "EMP"
    }); 


    private void Start()
    {
        EnableSkills();
        DisableScripts("");
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (isGamePaused) Resume(skillSelectionMenu);
            else Pause(skillSelectionMenu);
        }
    }

    public void ActivateSkill(GameObject clickedPanel)
    {
        string t = clickedPanel.GetComponentInChildren<Text>().text;

        DisablePassiveScripts();
        DisableScripts(t);
        ResetPanelColors();

        Debug.Log("activate "+t);
        //EnableScript()

        clickedPanel.GetComponent<Image>().color = new Color(0, 255, 0, 150);
    }

    void DisablePassiveScripts()
    {
        if (GameHandler != null)
        {
            Dictionary<string, System.Tuple<int, bool>> passiveSkills = GameHandler.GetComponent<GameManager>().GetPassiveSkills();


            foreach (KeyValuePair<string, System.Tuple<int, bool>> entry in passiveSkills)
            {
                if((entry.Key == "Advanced_Movement") && entry.Value.Item2)
                {
                    GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("BasicMovement").gameObject.SetActive(false);
                    GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("AdvancedMovement").gameObject.SetActive(true);
                }
                if ((entry.Key == "Advanced_Movement") && !entry.Value.Item2)
                {
                    GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("BasicMovement").gameObject.SetActive(true);
                    GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("AdvancedMovement").gameObject.SetActive(false);
                }

                if (entry.Key == "Vision_180")
                {
                    if (!entry.Value.Item2)
                        GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("Skill_180Vision").gameObject.SetActive(true);
                    else
                        GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("Skill_BasicVision").gameObject.SetActive(false);
                }

                if (entry.Key == "Vision_360")
                {
                    if (!entry.Value.Item2)
                        GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("Skill_180Vision").gameObject.SetActive(false);
                    else
                        GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("Skill_BasicVision").gameObject.SetActive(false);
                }
            }
        }
    }

    void DisableScripts(string dontDisable)
    {
        Debug.Log("disable scripts");
        // disable all active skills except the one selected
        if (GameHandler != null)
        {
            Dictionary<string, System.Tuple<int, bool>> activeSkills = GameHandler.GetComponent<GameManager>().GetSkills(true);

            foreach (KeyValuePair<string, System.Tuple<int, bool>> entry in activeSkills)
            {

                if (!dontDisable.Equals(entry.Key))
                {
                    Debug.Log("disable " + entry.Key);
                    GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("Skill_" + entry.Key).gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("enable " + entry.Key);
                    GameHandler.GetComponent<GameManager>().basicCharacter.GetComponent("Skill_" + entry.Key).gameObject.SetActive(true);
                }
            }
        }
    }

    void ResetPanelColors()
    {
        if(activeSkillsPanels.Length > 0)
        {
            foreach (GameObject go in activeSkillsPanels)
            {
                go.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
        
    }

    void Resume(GameObject menu)
    {
        menu.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    void Pause(GameObject menu)
    {
        menu.SetActive(true);
        Time.timeScale = 0.0f;
        isGamePaused = true;
        EnableSkills();
    }

    void EnableSkills()
    {
        foreach (string s in allSkills)
        {
            GameObject go = GameObject.Find("Button_" + s);
            if (go != null)
            {
                Button b = go.GetComponent<Button>();
                
                if (GameHandler.GetComponent<GameManager>().unlockedSkills.Contains(s))
                    b.interactable = true;
                else
                    b.interactable = false;
            }
        }
    }
}
