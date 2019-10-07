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

    private List<String> unlockedSkills = new List<string>(new string[] 
    {
        "Basic_Vision",
      "Flamethrower", "EMP"
    });

    private List<String> allSkills = new List<string>(new string[]
    {
        "Basic_Vision", "Vision_180", "Vision_360", "Advanced_Movement", "Movement_Rollers",
      "Flamethrower", "Ice_Cubes_Launcher", "Tazzer", "Telescopic_Arms", "Benn_Camo", "EMP"
    }); 


    private void Start()
    {
        EnableSkills();
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

        ResetPanelColors();

        clickedPanel.GetComponent<Image>().color = new Color(0, 255, 0, 150);
        

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

                if (unlockedSkills.Contains(s))
                    b.interactable = true;
                else
                    b.interactable = false;
            }
        }
    }
}
