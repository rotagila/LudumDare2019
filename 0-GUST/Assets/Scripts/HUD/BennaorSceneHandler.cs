using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class BennaorSceneHandler : MonoBehaviour
{
    public Image nextDialog;
    public int nextDialogToShow;
    public Text text_zone;
    public Button button_play;
    public Button button_yes;
    public Button button_no;
    public string nextLevelName;

    public GameObject BennaorPanel;

    public int numberOfComponents;

    // a changer
    public int requiredNumberForUpgrade = 42;
    public string upgradeName = "Flamethrower";

    private enum DialogName:int
    {
        START_PART1, START_PART2, GOT_CAUGHT, CAN_GIVE_IF, ENOUGH, UPGRADE_INSTALLED, REFUSED_UPGRADE
    };

    private Dictionary<DialogName, string> dialogFilenames = new Dictionary<DialogName, string>()
    {
        {DialogName.START_PART1, "starting_dialog_0.txt" },
        {DialogName.START_PART2, "starting_dialog_1.txt" },
        {DialogName.GOT_CAUGHT, "got_caught.txt" },
        {DialogName.CAN_GIVE_IF, "can_give_you_this.txt" },
        {DialogName.ENOUGH, "enough_interested.txt" },
        {DialogName.UPGRADE_INSTALLED, "upgrade_installed.txt" },
        {DialogName.REFUSED_UPGRADE, "refused_upgrade.txt" }
    };


    private bool nextDialogWaiting = true;
    private bool waitingForAnswer = false;
    private bool alreadyAnswered = false;

    private Button button_yes_inst;
    private Button button_no_inst;

    private void Start()
    {
        HandleDialogs(nextDialogToShow);
    }

    // Update is called once per frame
    void Update()
    {
        if(nextDialogWaiting)
        {
            nextDialog.transform.RotateAround(nextDialog.transform.position, Vector3.forward, 180 * Time.deltaTime);
        }

        // a la fin du clic
        if (Input.GetMouseButtonUp(0) && !waitingForAnswer)
        {
            HandleDialogs(nextDialogToShow);
        }

    }

    void ShowDialog(DialogName dn)
    {
        StringBuilder sb_filepath = new StringBuilder("Assets/dialogs/");
        sb_filepath.Append(dialogFilenames[dn]);

        string dialog_text = ParseDialogFile(sb_filepath.ToString(), dn);

        text_zone.text = dialog_text;
    }

    string ParseDialogFile(string filepath, DialogName dn)
    {
        StreamReader reader = new StreamReader(filepath);
        string dialog_text = reader.ReadToEnd();

        if (dn == DialogName.CAN_GIVE_IF)
        {
            dialog_text = ReplaceTag(dialog_text, "%x", numberOfComponents.ToString());

            int required = requiredNumberForUpgrade - numberOfComponents;
            dialog_text = ReplaceTag(dialog_text, "%y", required.ToString());

            dialog_text = ReplaceTag(dialog_text, "%z", upgradeName);
        }
        else if (dn == DialogName.ENOUGH)
        {
            dialog_text = ReplaceTag(dialog_text, "%z", upgradeName);
        }

        return dialog_text;
    }

    string ReplaceTag(string s, string tag, string replaceWith)
    {
        StringBuilder sb = new StringBuilder(s);
        
        string startTag = "<" + tag + ">";
        string endTag = "</" + tag + ">";

        int startIndex = s.IndexOf(startTag);
        int endIndex = s.IndexOf(endTag, startIndex + startTag.Length) + endTag.Length;

        sb.Remove(startIndex, endIndex - startIndex);

        sb.Insert(startIndex, replaceWith);

        return sb.ToString();
    }

    private void ShowPlayButton()
    {
        nextDialogWaiting = false;

        Vector3 buttonPos = BennaorPanel.transform.position;
        buttonPos.y += 50;

        Button button_play_inst = Instantiate(button_play, buttonPos, Quaternion.identity);
        //button_play_inst.transform.parent = BennaorPanel.transform;
        button_play_inst.transform.SetParent(BennaorPanel.transform);

        button_play_inst.onClick.AddListener(OnClickPlay);
    }

    public void OnClickPlay()
    {
        // use nextLevelId
        //SceneManager.LoadScene("Basic2DScene");
        SceneManager.LoadScene(nextLevelName);
    }

    private void ShowYesNoButtons()
    {
        waitingForAnswer = true;

        Vector3 buttonYesPos = BennaorPanel.transform.position;
        Vector3 buttonNoPos = BennaorPanel.transform.position;

        buttonYesPos.y += 75;
        
        button_yes_inst = Instantiate(button_yes, buttonYesPos, Quaternion.identity);
        button_yes_inst.transform.SetParent(BennaorPanel.transform);

        button_no_inst = Instantiate(button_no, buttonNoPos, Quaternion.identity);
        button_no_inst.transform.SetParent(BennaorPanel.transform);

        button_yes_inst.onClick.AddListener(OnClickYes);
        button_no_inst.onClick.AddListener(OnClickNo);
    }

    //public void OnClickYes(Button yes, Button no)
    public void OnClickYes()
    {
        numberOfComponents -= requiredNumberForUpgrade;
        nextDialogToShow = (int)DialogName.UPGRADE_INSTALLED;
        Answered();
        
    }

    //public void OnClickNo(Button yes, Button no)
    public void OnClickNo()
    {
        nextDialogToShow = (int)DialogName.REFUSED_UPGRADE;
        Answered();
    }

    private void Answered()
    {
        Destroy(button_yes_inst.gameObject);
        Destroy(button_no_inst.gameObject);
        waitingForAnswer = false;
        alreadyAnswered = true;
    }

    private void HandleDialogs(int nextDialog)
    {
        if ((int)DialogName.START_PART1 == nextDialog)
        {
            ShowDialog(DialogName.START_PART2);

            nextDialogToShow = (int)DialogName.START_PART2;

            ShowPlayButton();
        }
        else if ((int)DialogName.GOT_CAUGHT == nextDialog)
        {
            ShowDialog(DialogName.GOT_CAUGHT);

            nextDialogToShow = (int)DialogName.CAN_GIVE_IF;

            // if the player has enough components
            // the "numberOfComponents" elseif will be executed and not the "CanGiveIf"
        }
        else if ((numberOfComponents >= requiredNumberForUpgrade) && !alreadyAnswered)
        {
            ShowDialog(DialogName.ENOUGH);
            ShowYesNoButtons();

        }
        else if ((int)DialogName.CAN_GIVE_IF == nextDialog)
        {
            ShowDialog(DialogName.CAN_GIVE_IF);
            nextDialogToShow = (int)DialogName.CAN_GIVE_IF;

            ShowPlayButton();
        }
        else if ((int)DialogName.UPGRADE_INSTALLED == nextDialog)
        {
            ShowDialog(DialogName.UPGRADE_INSTALLED);

            ShowPlayButton();
        }
        else if ((int)DialogName.REFUSED_UPGRADE == nextDialog)
        {
            ShowDialog(DialogName.REFUSED_UPGRADE);

            ShowPlayButton();
        }
    }
}
