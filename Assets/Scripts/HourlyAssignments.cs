using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HourlyAssignments : MonoBehaviour {

    public GameObject confirmPopup;
    public GameObject successPopup;
    public GameObject popupBackground;
    public Button propertyRounds;
    public Button headcounts;
    public Button kiosk;
    public GameObject currentText;
    public GameObject confirmText;
    public GameObject propertyRoundsBackground;
    public GameObject headcountsBackground;
    public GameObject kioskBackground;

    string savedAssignment;

    public void Post()
    {
        StartCoroutine(PostToSlack.Post(savedAssignment, 0));
        Variables.HourlyAssignment = savedAssignment;
        confirmPopup.SetActive(false);
        successPopup.SetActive(true);
        SwitchColours(savedAssignment);
        currentText.GetComponent<Text>().text = "Current Position: " + Variables.HourlyAssignment;
    }

    public void ConfirmAssignment(string assignment)
    {
        Debug.Log("Do you want to go on to " + assignment + "?");
        popupBackground.SetActive(true);
        confirmText.GetComponent<Text>().text = assignment;
        savedAssignment = assignment;
        confirmPopup.SetActive(true);
        successPopup.SetActive(false);
    }

    public void ClearPopups()
    {
        popupBackground.SetActive(false);
        confirmPopup.SetActive(false);
        successPopup.SetActive(false);
    }

    public void Start()
    {
        currentText.GetComponent<Text>().text = "Current Position: " + Variables.HourlyAssignment;
        SwitchColours(Variables.HourlyAssignment);
        ClearPopups();
    }

    void SwitchColours(string assignment)
    {
        switch (assignment)
        {
            case "Property Rounds":
                propertyRoundsBackground.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
                kioskBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                headcountsBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                break;
            case "Headcounts":
                headcounts.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
                kioskBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                propertyRoundsBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                break;
            case "Kiosk":
                kioskBackground.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
                propertyRoundsBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                headcountsBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                break;
            default:
                kioskBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                propertyRoundsBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                headcountsBackground.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
                break;
        }
    }
}
