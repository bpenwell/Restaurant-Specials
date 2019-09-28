/*
 * Copyright (C) 2018 Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledege Center Building Operations App
 * 
 * MenuBarColours.cs
 * This class switches the menu bar colour so that it matches the users choice of accent colour
 */
using UnityEngine;
using UnityEngine.UI;

public class MenuBarColours : MonoBehaviour {
    public GameObject MenuBar;
    void Start () {
        UpdateColour();
    }
	
	public void UpdateColour () {
        MenuBar.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
    }
}
