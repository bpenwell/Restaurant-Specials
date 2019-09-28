/*
 * Copyright (C) 2018 Ben Penwell, Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledge Center Building Operations app
 * 
 * SendToGoogle.cs
 * This class is used to submit the headcount data collected and parsed by the FloorManager to the Headcounts google form, without the user having to interact with the form url directly
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToGoogle : MonoBehaviour {

    public GameObject time;
    public InputField floor1;
    public InputField floor2;
    public InputField floor3;
    public InputField floor4;
    public InputField floor5;

    public Transform dropdownMenu;

    private string Time;
    private string Floor1;
    private string Floor2;
    private string Floor3;
    private string Floor4;
    private string Floor5;
    private string Initials;

    [SerializeField]
    //private readonly string BASE_URL =           "https://docs.google.com/forms/d/e/1FAIpQLSeOuOzNxD73BN13VDM_kcHX2pD38vkxuAT_0RavZn7JT0dlgQ/formResponse";
    private readonly string REAL_SURVEY_URL =    "https://docs.google.com/forms/d/e/1FAIpQLSfHv_-XW7farh8-vZZy2oTgTLLurnba0tZxslGV443mlEiqDQ/formResponse";
    /*
    IEnumerator Post(string time, string floor1, string floor2, string floor3, string floor4, string floor5, string initials)
    {
        WWWForm form = new WWWForm();
        //data for my backend
        form.AddField("entry.146606296", time);
        form.AddField("entry.178080618", floor1);
        form.AddField("entry.629089262", floor2);
        form.AddField("entry.1148679554", floor3);
        form.AddField("entry.729847420", floor4);
        form.AddField("entry.1645371889", floor5);
        form.AddField("entry.1499993869", initials);

        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }
    */
    IEnumerator Real_Post(string time, string floor1, string floor2, string floor3, string floor4, string floor5, string initials)
    {
        WWWForm real_form = new WWWForm();
        //actual data
        real_form.AddField("entry.419120641", time);
        real_form.AddField("entry.2092834832", floor1);
        real_form.AddField("entry.2112837311", floor2);
        real_form.AddField("entry.1249192221", floor3);
        real_form.AddField("entry.928473257", floor4);
        real_form.AddField("entry.1397058879", floor5);
        real_form.AddField("entry.1401688371", initials);

        //byte[] rawData = real_form.data;
        UnityWebRequest www = UnityWebRequest.Post(REAL_SURVEY_URL, real_form);
        yield return www.SendWebRequest();
    }

    public void Send()
    {
        //Preparing the Data for submission
        int menuIndex = time.GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions = time.GetComponent<Dropdown>().options;
        Time = menuOptions[menuIndex].text;
        Floor1 = floor1.text;
        Floor2 = floor2.text;
        Floor3 = floor3.text;
        Floor4 = floor4.text;
        Floor5 = floor5.text;
        Initials = Variables.Name;
        Debug.Log(Initials);

        //If the user has forgotten some information, make sure they fill it all out 
        if (Time == "" || Floor1 == null || Floor2 == null || Floor3 == null || Floor4 == null || Floor5 == null || Initials == "" || Time == "Choose a Time")
        {
            Debug.Log("Value Missing");
            return;
        }

        //submitting the data from the IEnumerator coroutines
        //StartCoroutine(Post(Time, Floor1, Floor2, Floor3, Floor4, Floor5, Initials));
        StartCoroutine(Real_Post(Time, Floor1, Floor2, Floor3, Floor4, Floor5, Initials));
        
        //resetting playerprefs
        for (int i = 0; i < 5; i++)
        {
            Variables.FloorCount[i] = 0;
        }
        Debug.Log("Player Prefs Reset");
    }
}
