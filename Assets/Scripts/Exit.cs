/*
 * Copyright (C) Ryan Long 2019
 * Made for the Building operations mobile app for the Mathewson IGT Knowledge center
 * 
 * exit.cs
 * This file contains the instructions to save the variables when the app is quit
 */

using UnityEngine;

public class Exit : MonoBehaviour {
    private void OnApplicationQuit()
    {
        Debug.Log("Saving Variables to file...");
        PlayerPrefs.SetInt("ColourIndex", Variables.ColorIndex);
        PlayerPrefs.SetString("ColourProfile", Variables.ColourProfile);
        PlayerPrefs.SetString("ColourValues", Variables.ColourValues);
        PlayerPrefs.SetString("Name", Variables.Name);
        PlayerPrefs.SetInt("HomeScreenMode", Variables.HomeScreenMode);
        PlayerPrefs.SetInt("KeySetNumber", Variables.KeySetNumber);
        PlayerPrefs.SetString("InOut", Variables.InOut);
        PlayerPrefs.SetString("HourlyAssignment", "None");
        PlayerPrefs.SetInt("OnRounds", Variables.OnRounds);
        for (int floor = 0; floor < 5; floor++)
        {
            PlayerPrefs.SetInt("m_Floor" + (floor + 1), Variables.FloorCount[floor]);
        }
        PlayerPrefs.Save();
        Debug.Log("Exit");
    }

    //saves on pause since that gets called before quit and also because it doesnt actually do the quit part...
    private void OnApplicationPause(bool pause)
    {
        OnApplicationQuit();
    }
}
