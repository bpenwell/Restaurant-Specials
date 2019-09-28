/*
 * Copyright (C) 2018 Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledge Center App
 * 
 * settings.cs
 * This class handles the settings page and sets all of the required playerprefs values based on what the user wants
 */
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Dropdown ColourPicker;
    private readonly string  LightBlue =  "66,116,255";
    private readonly string DarkBlue =   "8,0,175";
    private readonly string UNRBlue =    "0,46,98";
    private readonly string LightGreen = "95,230,80";
    private readonly string DarkGreen =  "40,120,0";
    private readonly string Red =        "255,40,35";
    private readonly string Orange =     "255,130,30";
    private readonly string Yellow =     "255,220,45";
    private readonly string Purple =     "70,0,140";
    private readonly string Pink =       "255,60,215";
    private int profileIndex;

    public GameObject GridButton;
    public GameObject ListButton;
    public Toggle vibration;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    void Start () {
        ColourPicker.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("ColourIndex", 0);
        SwitchButton();
        if (Variables.Vibration == 0) vibration.GetComponent<Toggle>().isOn = false;
        else vibration.GetComponent<Toggle>().isOn = true;
    }

    public void MenuColours()
    {
        profileIndex = ColourPicker.GetComponent<Dropdown>().value;
        Debug.Log(profileIndex);
        string profile = ColourPicker.options[profileIndex].text;
        Debug.Log(profile);
        Variables.ColourProfile = profile;
        switch (profile)
        {
            case "Light Blue (default)":
                Variables.ColourValues = LightBlue;
                Variables.ColorIndex = 0 ;
                break;
            case "Dark Blue":
                Variables.ColourValues = DarkBlue;
                Variables.ColorIndex = 1;
                break;
            case "UNR Blue":
                Variables.ColourValues = UNRBlue;
                Variables.ColorIndex = 2;
                break;
            case "Light Green":
                Variables.ColourValues = LightGreen;
               Variables.ColorIndex = 3;
                break;
            case "Dark Green":
                Variables.ColourValues = DarkGreen;
                Variables.ColorIndex = 4;
                break;
            case "Red":
                Variables.ColourValues = Red;
                Variables.ColorIndex = 5;
                break;
            case "Orange":
                Variables.ColourValues = Orange;
                Variables.ColorIndex = 6;
                break;
            case "Yellow":
                Variables.ColourValues = Yellow;
                Variables.ColorIndex = 7;
                break;
            case "Purple":
                Variables.ColourValues = Purple;
                Variables.ColorIndex = 9;
                break;
            case "Pink":
                Variables.ColourValues = Pink;
                Variables.ColorIndex = 8;
                break;
        }
        string colourCode = Variables.ColourValues;
        string[] rgb = colourCode.Split(',');
        for (int i = 0; i < 3; i++)
        {
            byte.TryParse(rgb[i], out Variables.ColourCodes[i]);
        }
    }

    public void SwitchButton()
    {
        if(Variables.HomeScreenMode == 0)
        {
            GridButton.SetActive(true);
            ListButton.SetActive(false);
        }
        else
        {
            GridButton.SetActive(false);
            ListButton.SetActive(true);
        }
    }

    public void vibrate()
    {
        if (vibration.isOn) Variables.Vibration = 1;
        else Variables.Vibration = 0;
        Debug.Log("vibration status changed");
    }
}