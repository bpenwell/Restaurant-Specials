/*
 * Copyright (C) 2018 Ben Penwell, Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledge Center Building Operations app
 * 
 * FloorManager.cs
 * This class is the driving force behind how the app handles headcounts. It takes in input from the various buttons on the headcounts scene and manipulates the data accordingly
 */

using UnityEngine;
using UnityEngine.UI;

public class Headcounts : MonoBehaviour {

    //variable declaration
    public InputField floor1;
    public InputField floor2;
    public InputField floor3;
    public InputField floor4;
    public InputField floor5;
    public GameObject ConfirmPopup;
    public GameObject ConfirmText;
    public GameObject popupBackground;
    public GameObject time;
    public GameObject errorPopUp;
    public GameObject successPopUp;
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject PlusButton;
    //colors for each floor
    public GameObject backdrop1;
    public GameObject backdrop2;
    public GameObject backdrop3;
    public GameObject backdrop4;
    public GameObject backdrop5;

    private int currentFloorIndex;

    // Use this for initialization
    void Start () {

        //set values of text elements to the saved values if they exist
        floor1.text = Variables.FloorCount[0].ToString();
        floor2.text = Variables.FloorCount[1].ToString();
        floor3.text = Variables.FloorCount[2].ToString();
        floor4.text = Variables.FloorCount[3].ToString();
        floor5.text = Variables.FloorCount[4].ToString();
        //sets the default current index
        currentFloorIndex = 4;
        ColourChange();
    }

    //A function to increase the count on the specified floor 
    public void increase()
    {
        Variables.FloorCount[currentFloorIndex]++;
        if (Variables.Vibration == 1) Handheld.Vibrate();
        UpdateText();
        Debug.Log("floor #" + (currentFloorIndex + 1) + ": " + Variables.FloorCount[currentFloorIndex]);
    }

    //a function to decrease the count on the specified floor 
    public void decrease()
    {
        if(Variables.FloorCount[currentFloorIndex] > 0)
            Variables.FloorCount[currentFloorIndex]--;
        UpdateText();
        Debug.Log("floor #" + (currentFloorIndex + 1) + ": " + Variables.FloorCount[currentFloorIndex]);
    }

    //a function to increase the floor number
    public void increaseFloor()
    {
        Debug.Log("run increase");
        currentFloorIndex = ++currentFloorIndex % 5;
        ColourChange();
    }
    
    //a function to decrease the number of the floor 
    public void decreaseFloor()
    {
        Debug.Log("run decrease");
        currentFloorIndex = (--currentFloorIndex + 5) % 5;
        ColourChange();
    }

    //a function to being the submission process
    public void moveToSubmitScreen()
    {

        Debug.Log("Yo" + "'" + Variables.Name + "'");

        //if they have submitted all of the required information, display a confirmation window, otherwise display an error window
        if (time.GetComponent<Text>().text != "Choose a Time" && time.GetComponent<Text>().text != "" && floor1.text != "" && floor2.text != "" && floor3.text != "" && floor5.text != "")
        {
            ConfirmPopup.SetActive(true);
            popupBackground.SetActive(true);
            ConfirmText.GetComponent<Text>().text = "1st: " + Variables.FloorCount[0] + " | 2nd: " + Variables.FloorCount[1] + " | 3rd: " + Variables.FloorCount[2] + " | 4th: " + Variables.FloorCount[3] + " | 5th: " + Variables.FloorCount[4];
        }
        else
        {
            moveToErrorScreen();
        }
    }

    //exit button
    public void exitSubmitScreen()
    {
        ConfirmPopup.SetActive(false);
        popupBackground.SetActive(false);
    }

    //display error window
    public void moveToErrorScreen()
    {
        errorPopUp.SetActive(true);
        popupBackground.SetActive(true);
        ConfirmPopup.SetActive(false);
        successPopUp.SetActive(false);
    }

    //exit button
    public void exitErrorScreen()
    {
        errorPopUp.SetActive(false);
        popupBackground.SetActive(false);
    }

    //display success window
    public void moveToSuccessScreen()
    {
        successPopUp.SetActive(true);
        popupBackground.SetActive(true);
        ConfirmPopup.SetActive(false);
    }

    //change the colour of the backdrop
    //TODO::make this mroe efficient
    void ColourChange()
    {
        PlusButton.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
        //MenuBar.GetComponent<Image>().color = new Color32((byte)r, (byte)g, (byte)b, 255);
        if (currentFloorIndex == 0)
        {
            backdrop1.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
            backdrop2.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop3.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop4.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop5.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
        }
        if (currentFloorIndex == 1)
        {
            backdrop2.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
            backdrop1.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop3.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop4.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop5.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
        }
        if (currentFloorIndex == 2)
        {
            backdrop3.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
            backdrop2.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop1.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop4.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop5.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
        }
        if (currentFloorIndex == 3)
        {
            backdrop4.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
            backdrop2.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop3.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop1.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop5.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
        }
        if (currentFloorIndex == 4)
        {
            backdrop5.GetComponent<Image>().color = new Color32(Variables.ColourCodes[0], Variables.ColourCodes[1], Variables.ColourCodes[2], 255);
            backdrop2.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop3.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop4.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
            backdrop1.GetComponent<Image>().color = new Color32(53, 53, 53, 255);
        }
    }

    //update the text in the box to match the number of the headcount
    void UpdateText()
    {
        switch (currentFloorIndex)
        {
            case 0:
                floor1.text = Variables.FloorCount[currentFloorIndex].ToString();
                break;
            case 1:
                floor2.text = Variables.FloorCount[currentFloorIndex].ToString();
                break;
            case 2:
                floor3.text = Variables.FloorCount[currentFloorIndex].ToString();
                break;
            case 3:
                floor4.text = Variables.FloorCount[currentFloorIndex].ToString();
                break;
            case 4:
                floor5.text = Variables.FloorCount[currentFloorIndex].ToString();
                break;
        }
    }

    //take in input from the box and set the value accordinly
    public void setNumFromInput(int floor)
    {
        switch (floor)
        {
            case 0:
                int.TryParse(floor1.text, out Variables.FloorCount[floor]);
                break;
            case 1:
                int.TryParse(floor2.text, out Variables.FloorCount[floor]);
                break;
            case 2:
                int.TryParse(floor3.text, out Variables.FloorCount[floor]);
                break;
            case 3:
                int.TryParse(floor4.text, out Variables.FloorCount[floor]);
                //PlayerPrefs.SetInt("m_Floor" + (floor + 1), Variables.FloorCount[floor]);
                break;
            case 4:
                int.TryParse(floor5.text, out Variables.FloorCount[floor]);
                break;
        }
    }

    //change the floor when inputs are recieved
    public void SetFloor(int floor)
    {
        currentFloorIndex = floor;
        ColourChange();
    }
}
