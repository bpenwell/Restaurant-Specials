/*
 * Copyright (C) 2018 Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledge Center Building Operations app
 * 
 * FirstStart.cs
 * This class is for the first start screen. It takes in the user's name, which is stored as a PlayerPref, which is then read throughout the app and used for submitting
 * The app will verify that the user is a member of the team by querying a google sheet that contains the names of all of the employees
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//TODO::ADD A PASSCODE FUNCTIONALITY FOR AN EXTRA LAYER OF SECURITY
public class FirstStart : MonoBehaviour {
    //Object Declaration
    public GameObject nameText;
    public GameObject errorText;
    public GameObject errorPopup;

    //calls the async IEnumerator when the button is pressed
    public void firstStartup()
    {
        StartCoroutine(Check());
    }

    //checks the manifest for whether the user is valid or not
    public IEnumerator Check()
    {
        string input = nameText.GetComponent<Text>().text;
        Debug.Log(input);
        //The user cannot continue without entering something for their name
        if (input == "")
        {
            Debug.Log("You have to enter a name");
            errorText.SetActive(true);
            yield return null;
        }
        else
        {
            Variables.Name = input;
            yield return StartCoroutine(ValidateUser.CheckName(Variables.Name));

            if (!Variables.ValidUser)
            {
                dispError();
            }
            else
            {
                PlayerPrefs.SetString("Name", Variables.Name);
                SceneManager.LoadScene("Home");
            }
        }
    }

    public void clearErrors()
    {
        errorPopup.SetActive(false);
    }

    public void dispError()
    {
        errorPopup.SetActive(true);
    }
}
