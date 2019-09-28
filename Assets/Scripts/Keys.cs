/*
 * Copyright (C) 2018 Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledge Center Building Operations app
 * 
 * Keys.cs:
 * This class is designed to replace the keys google sheet. The user selects which key they are going to use (key1, key2, or key3), then submits that they are checking out the key set
 * The programme then takes the users name, the key set and the in/out status and submits that information into a google form, without the user having access to the form url itself
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Keys : MonoBehaviour {

    public GameObject key1;
    public Button Key1Button;
    public GameObject key2;
    public Button Key2Button;
    public GameObject key3;
    public Button Key3Button;
    public GameObject InButton;
    public GameObject OutButton;
    public GameObject SelectKeyText;
    public GameObject CurrentKeyText;
    public GameObject checkedOutText;
    public GameObject confirmedText;
    public GameObject ConfirmationPopup;
    public GameObject SucessPopup;
    public GameObject PopupBackground;
    public GameObject ErrorPopup;
    public GameObject SelectPanel1;
    public GameObject SelectPanel2;
    public GameObject SelectPanel3;

    private int keySet;
    private readonly string REAL_SURVEY_URL =    "https://docs.google.com/forms/d/e/1FAIpQLSeJ-jMnBYzlrIdOSJaJvnBX-Z2i8SRxAxcTG9fzW8uVfzpYfA/formResponse";
    void Start () {
        //loads the saved key set and, if necessary, changes the layout
        keySet = Variables.KeySetNumber;
        LayoutChange();
    }

    void colour(int keys)
    {
        switch (keys){
            case 1:
                SelectPanel1.SetActive(true);
                SelectPanel2.SetActive(false);
                SelectPanel3.SetActive(false);
                break;
            case 2:
                SelectPanel1.SetActive(false);
                SelectPanel2.SetActive(true);
                SelectPanel3.SetActive(false);
                break;
            case 3:
                SelectPanel1.SetActive(false);
                SelectPanel2.SetActive(false);
                SelectPanel3.SetActive(true);
                break;
        }
    }

    public void setKeySet(string inputText)
    {
        int.TryParse(inputText, out keySet);
        Debug.Log(keySet);
        checkedOutText.GetComponent<Text>().text = "Key Set " + keySet;
        confirmedText.GetComponent<Text>().text = "Key Set " + keySet;
        colour(keySet);
    }

    public void submit(string InStatus)
    {
        //PlayerPrefs.SetString("InOut", InStatus);
        Variables.InOut = InStatus;
        Debug.Log(InStatus);

        string keys = "Key Set " + keySet.ToString();
        Debug.Log(keys);

        string name = Variables.Name;
        Debug.Log(name);

        StartCoroutine(RealPost(name, keys, InStatus));

        if (InStatus == "In")
        {
            keySet = Variables.KeySetNumber = 0;
        }
        else Variables.KeySetNumber = keySet;
    }

    IEnumerator RealPost(string name, string keyNumber, string checkedIn)
    {
        WWWForm Keys = new WWWForm();
        //Real Data
        Keys.AddField("entry.673874653", name);
        Keys.AddField("entry.734337030", keyNumber);
        Keys.AddField("entry.606492357", checkedIn);
        byte[] RAW_Data = Keys.data;
        var www = UnityWebRequest.Post(REAL_SURVEY_URL, Keys);
        //UnityWebRequest www = new UnityWebRequest();
        yield return www.SendWebRequest();
    }

    public void LayoutChange()
    {
        if (keySet != 0)
        {
            switch (keySet)
            {
                case 1:
                    key2.SetActive(false);
                    key3.SetActive(false);
                    Key1Button.interactable = false;
                    break;
                case 2:
                    key1.SetActive(false);
                    key3.SetActive(false);
                    Key2Button.interactable = false;
                    break;
                case 3:
                    key1.SetActive(false);
                    key2.SetActive(false);
                    Key3Button.interactable = false;
                    break;
            }
            OutButton.SetActive(false);
            InButton.SetActive(true);
            SelectKeyText.SetActive(false);
            CurrentKeyText.SetActive(true);
            SelectPanel1.SetActive(false);
            SelectPanel2.SetActive(false);
            SelectPanel3.SetActive(false);
        }
        else
        {
            key1.SetActive(true);
            key2.SetActive(true);
            key3.SetActive(true);
            Key1Button.interactable = true;
            Key2Button.interactable = true;
            Key3Button.interactable = true;
            OutButton.SetActive(true);
            InButton.SetActive(false);
            SelectKeyText.SetActive(true);
            CurrentKeyText.SetActive(false);
        }
    }

    public void LaunchSubmitScreen()
    {
        if (keySet != 0)
        {
            PopupBackground.SetActive(true);
            ConfirmationPopup.SetActive(true);
            ErrorPopup.SetActive(false);
        }
        else
        {
            showError();
            Debug.Log("you better choose a key set there buckeroo");
        }
    }

    public void LaunchSuccessScreen()
    {
        ConfirmationPopup.SetActive(false);
        SucessPopup.SetActive(true);
    }

    public void ClosePopups()
    {
        ConfirmationPopup.SetActive(false);
        SucessPopup.SetActive(false);
        PopupBackground.SetActive(false);
        ErrorPopup.SetActive(false);
    }

    void showError()
    {
        PopupBackground.SetActive(true);
        ConfirmationPopup.SetActive(false);
        SucessPopup.SetActive(false);
        ErrorPopup.SetActive(true);
    }
}