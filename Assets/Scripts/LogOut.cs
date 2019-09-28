/*
 * Copyright (C) Ryan Long: ryanlong@nevada.unr.edu 
 * Made For the Mathewson IGT Knowledge Center Building Operations Department
 * 
 * logOut.cs
 * This class provides log out funtionality to clear the name preference and allow users to use the tablet
 */

using UnityEngine;

public class LogOut : MonoBehaviour {
    public GameObject popupBackground;
    public GameObject confirmPopup;
    public GameObject successPopup;

    public void LogOutNow()
    {
        PlayerPrefs.SetInt("firstStart", 0);
        Variables.Name = null;
        PlayerPrefs.SetInt("ValidUser", 0);
    }

    public void Confirm()
    {
        popupBackground.SetActive(true);
        confirmPopup.SetActive(true);
    }

    public void Success()
    {
        successPopup.SetActive(true);
        confirmPopup.SetActive(false);
    }

    public void ClosePopups()
    {
        popupBackground.SetActive(false);
        confirmPopup.SetActive(false);
        successPopup.SetActive(false);
    }
}
