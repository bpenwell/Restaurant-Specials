/*
 * Copyright (C) 2019 Ryan Long
 * Made for the MIKC Building Ops Mobile App
 * 
 * startup.cs
 * initialises variables for use with the rest of the app so that the app doesnt have to constantly read from the file the whole time. 
 */ 


using UnityEngine;

public class Startup : MonoBehaviour {

    //Runs before anything is loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private void Awake()
    {
        //Makes the nav bar show up on android
        Screen.fullScreen = false;
        //Initialise variables for use in the rest of the app
        Debug.Log("Reading variables from file...");
        //colour things
        Variables.ColorIndex = PlayerPrefs.GetInt("ColourIndex", 0);
        Variables.ColourProfile = PlayerPrefs.GetString("ColourProfile");
        Variables.ColourValues = PlayerPrefs.GetString("ColourValues", "66, 116, 255");
        if (Variables.ColourValues == "") Variables.ColourValues = "66, 116, 255";
        //name
        Variables.Name = PlayerPrefs.GetString("Name", "NULL");
        //settings
        Variables.HomeScreenMode = PlayerPrefs.GetInt("HomeScreenMode", 0);
        Variables.Vibration = PlayerPrefs.GetInt("Vibration", 1);
        //keys
        Variables.KeySetNumber = PlayerPrefs.GetInt("KeySetNumber", 0);
        Variables.InOut = PlayerPrefs.GetString("InOut", "out");
        //headcounts
        Variables.FloorCount = new int[5];
        for (int floor = 0; floor < 5; floor++)
        {
            Variables.FloorCount[floor] = PlayerPrefs.GetInt("m_Floor" + (floor + 1), 0);
        }
        //setting up the colour codes
        Variables.ColourCodes = new byte[3];
        string colourCode = Variables.ColourValues;
        string[] rgb = colourCode.Split(',');
        for (int i = 0; i < 3; i++)
        {
            byte.TryParse(rgb[i], out Variables.ColourCodes[i]);
        }
        //hourly assignments
        Variables.HourlyAssignment = PlayerPrefs.GetString("HourlyAssignment", "None");
        Variables.OnRounds = PlayerPrefs.GetInt("OnRounds", 0);
        //Bathrooms
        Variables.IssueArray = new System.Collections.Generic.List<string>();
        Variables.ScannedQRCodes = new System.Collections.Generic.List<string>();
        //DONE
        Debug.Log("Variables Initialised!");
        Debug.Log("Checking user status...");
        //checks the users status
        StartCoroutine(ValidateUser.CheckName(Variables.Name));    
    }
}