using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BathroomIssues : MonoBehaviour{
    public GameObject TextEntryBox;
    public GameObject TitleText;
    //public GameObject SubmitButton;
    //public GameObject NoIssuesButton;
    public GameObject PopupBackground;
    public GameObject ConfirmPopup;
    private static readonly List<string> locations = new List<string>
    {
        "5NM",
        "5NW",
        "5SM",
        "5SW",
        "5StaffL",
        "5StaffR",
        "4NM",
        "4NW",
        "4SM",
        "4SW",
        "4StaffL",
        "4StaffR",
        "3NM",
        "3NW",
        "3SM",
        "3SW",
        "3StaffL",
        "3StaffR",
        "2NM",
        "2NW",
        "2SM",
        "2SW",
        "2StaffL",
        "2StaffR",
        "1NM",
        "1NW",
        "1SM",
        "1SW",
        "1StaffL",
        "1StaffR"
    };

    private void Start()
    {
        TitleText.GetComponent<Text>().text = "Enter any issues for " + QRDecode.GetRestroomLocation();
    }

    public void UpdateArray()
    {
        string issue = TextEntryBox.GetComponent<Text>().text;
        Variables.IssueArray.Add(QRDecode.GetRestroomLocation() + ": " + issue);
    }

    public void UpdateArray(string issues)
    {
        Variables.IssueArray.Add(issues);
    }


    public void SubmitArray()
    {
        List<string> Unchecked = new List<string>();
        Unchecked = locations.Except(Variables.IssueArray).ToList();
        string message = "";
        for (int i = 0; i < Variables.IssueArray.Count; i++)
        {
            if (Variables.IssueArray[i] != "NO ISSUES")
                message += (Variables.IssueArray[i] + "\n");
        }
        if (Unchecked.Count > 0)
        {
            message += "The following restrooms were unchecked:\n";
            for (int i = 0; i < Unchecked.Count; i++)
            {
                message += Unchecked[i];
            }
        }
        StartCoroutine(PostToSlack.Post(message, 1));
    }
}