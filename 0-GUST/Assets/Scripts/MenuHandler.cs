using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject skillSelectionMenu;

    public GameObject[] activeSkillsPanels;
    

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
    }
}
