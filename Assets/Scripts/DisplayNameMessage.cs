/*
 * Copyright (C) 2018 Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledge Center
 * 
 * displayNameMessage.cs
 * This class displays a message at the bottom of the screen that changes based on the time of day
 */

using UnityEngine;
using UnityEngine.UI;

public class DisplayNameMessage : MonoBehaviour {
    public GameObject textBox;
	// Use this for initialization
	void Start () {
        var time = System.DateTime.Now;
        int hour = time.Hour;
        string modifier;
        if (hour >= 12 && hour < 18)
        {
            modifier = "Afternoon";
        }
        else if (hour < 12)
        {
            modifier = "Morning";
        }
        else
        {
            modifier = "Evening";
        }
        string[] SeparatedNames = Variables.Name.Split(' ');
        textBox.GetComponent<Text>().text = "Good " + modifier + ", " + SeparatedNames[0];
	}
}
