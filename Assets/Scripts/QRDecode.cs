/*
 * Copyright (C) 2018 Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledge Center Building Operations app
 *
 * QRDecode.cs
 * This script makes a camera object in the scene, then reads any qr values found. 
 * TODO: parse the information and submit to google forms based on the result.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Linq;
using ZXing;

/*
 * @brief the class that handles all of the QR Decoding for the app
 */ 
public class QRDecode : MonoBehaviour {
    //variables 
    public RawImage QRWindow;
    private WebCamTexture camTexture;
    public GameObject ResultText;
    public GameObject CameraOnButton;
    public GameObject EndButton;
    public GameObject CameraOffButton;
    public GameObject popupBacground;
    public GameObject endConfirmPopup;
    public GameObject endSuccessPopup;
    public GameObject confirmText;
    private readonly List<string> Locations = new List<string>
    {
        "1 Back Stairs",
        "1 Main Gate",
        "1 N Restroom",
        "1 Garden",
        "1 Stairs B",
        "1 Viewing Room",
        "2 Back Stairs",
        "Loading Docks",
        "2 Stairs F",
        "2 N Entrance",
        "2 S Entrance",
        "2 N Restroom",
        "2 S Restroom",
        "Popular Reading",
        "Reference Area",
        "3 Back Stairs",
        "3 Basque Lobby",
        "3 Basque Back",
        "3 N Overlook",
        "3 N Restroom",
        "3 S Restroom",
        "3 S Stacks",
        "3 Tower",
        "4 Back Stairs",
        "4 E Stacks",
        "4 N Stacks",
        "4 S Stacks",
        "4 N Restroom",
        "4 S Restroom",
        "4 Carrels",
        "4 Tower",
        "4 Rotunda",
        "5 Roof Access",
        "5 E Stacks",
        "5 S Stacks",
        "5 N W Corner",
        "5 N Restroom",
        "5 S Restroom",
        "5 Tower"
    };
    private List<string> UnscannedCodes = new List<string>();
    private bool isDecoding = false;
    IBarcodeReader barcodeReader = new BarcodeReader();
    private static string RestroomLocation;

    //returns the resteroom location so it can be used with other scripts 
    public static string GetRestroomLocation()
    {
        return RestroomLocation;
    }

    //Runs at startup of the scene
    public void Start () {
        ClearPopups();
        EndButton.SetActive(true);
        CameraOffButton.SetActive(true);
        CameraOnButton.SetActive(false);
        camTexture = new WebCamTexture
        {
            requestedHeight = 800,
            requestedWidth = 600
        };
        QRWindow.texture = camTexture;
        QRWindow.material.mainTexture = camTexture;
        if (camTexture != null) camTexture.Play();
        Debug.Log("Cam Is On");
        ResultText.GetComponent<Text>().text = "";
    }

    //releases popups and asks the user to confirm their choices
    public void ConfirmStopRounds()
    {
        popupBacground.SetActive(true);
        endConfirmPopup.SetActive(true);
        confirmText.GetComponent<Text>().text = "You are about to submit " + Variables.ScannedQRCodes.Count + "/" + Locations.Count + " QR Codes!";
        StopCamera();
    }

    //submits the current array to the server
    public void SubmitRounds()
    {
        Variables.OnRounds = 0;
        endConfirmPopup.SetActive(false);
        endSuccessPopup.SetActive(true);
        StartCoroutine(Post(0, "FINISH"));
        Debug.Log("There are " + Variables.ScannedQRCodes.Count + " Scanned codes");
        if (Variables.ScannedQRCodes.Count > 0)
        {
            UnscannedCodes = Locations.Except(Variables.ScannedQRCodes).ToList();
        }
        else
        {
            UnscannedCodes = Variables.ScannedQRCodes;
        }
        int numScanned = Variables.ScannedQRCodes.Count;
        string message = Variables.Name + " scanned " + numScanned + " codes while on property rounds!";
        StartCoroutine(PostToSlack.Post(message, 2));
        Variables.ScannedQRCodes.Clear();
        Debug.Log(message + " Sent to slack ;)");
    }

    //turns the camera off to conserve on battery
    public void StopCamera()
    {
        camTexture.Stop();
        Debug.Log("Camera is Off");
        CameraOnButton.SetActive(true);
        CameraOffButton.SetActive(false);
    }
    
    //turns the camera back on so codes can be scanned 
    public void ResumeCamera()
    {
        if(!camTexture.isPlaying) camTexture.Play();
        Debug.Log("Camera is on");
        CameraOffButton.SetActive(true);
        CameraOnButton.SetActive(false);
    }
 
    //checks the camera for something that may resemble a QR code and handles it appropriately
    void OnGUI()
    {
        if (!isDecoding && camTexture.isPlaying)
        {
            try
            {
                var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
                if (result != null)
                {
                    isDecoding = true;
                    Debug.Log("DECODED TEXT FROM QR: " + result.Text);
                    if (Variables.Vibration == 1) Handheld.Vibrate();
                    ParseInput(result.Text);
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
            }
        }
    }

    //parses the input depending on what the content of the QR code is
    void ParseInput(string url)
    {
        //Different outcomes depending on how the decoded text is split up
        string[] arguments = url.Split(':');
        switch (arguments[0])
        {
            case "PR":
                if (Variables.ScannedQRCodes.Count == 0)
                {
                    Variables.OnRounds = 1;
                    StartCoroutine(Post(0, "START"));
                }
                Debug.Log("Property Rounds");
                ResultText.GetComponent<Text>().text = arguments[1] + " Logged";
                Variables.ScannedQRCodes.Add(arguments[1]);
                StartCoroutine(Post(0, arguments[1]));
                break;
            case "Print":
                Debug.Log("Printer");
                StartCoroutine(Post(1, arguments[1]));
                break;
            case "BR":
                Debug.Log("Restrooms");
                RestroomLocation = arguments[1];
                StartCoroutine(Post(2, RestroomLocation));
                SceneManager.LoadScene("Bathrooms");
                break;
            case "http":
            case "https":
                Application.OpenURL(url);
                isDecoding = false;
                break;
            default:
                ResultText.GetComponent<Text>().text = url;
                isDecoding = false;
                break;
        }
    }

    //posts the input to the appropriate form 
    IEnumerator Post(int option, string argument)
    {
        string[] url =
        {
            "https://docs.google.com/forms/d/e/1FAIpQLScoS2yV9TGX2vw88iv_xo0vjjPo5XGAjQU1vB7b0OtDWHn-vQ/formResponse",
            "https://docs.google.com/forms/d/e/1FAIpQLSeMm8-TyRxixOyV8TccSuHfTIhhdEM4BYaiNvu6UoxNekYgVg/formResponse",
            "https://docs.google.com/forms/d/e/1FAIpQLSdhFmOHIyWw6PDNN7jEK37JjXVG4jiubGwk1B55gVNooYuthQ/formResponse"
        };

        WWWForm www = new WWWForm();
        switch (option){
            case 0:
                //entry.1021794339=loc&entry.1063995315=name
                www.AddField("entry.1021794339", argument);
                www.AddField("entry.1063995315", Variables.Name);
                break;
            case 1:
                //entry.2054084277=loc&entry.2060021607=name
                www.AddField("entry.2054084277", argument);
                www.AddField("entry.2060021607", Variables.Name);
                break;
            case 2:
                //&entry.499353568=loc&entry.199588605=name
                www.AddField("entry.499353568", argument);
                www.AddField("entry.199588605", Variables.Name);
                break;
        }

        var request = UnityWebRequest.Post(url[option], www);
        yield return request.SendWebRequest();
        yield return new WaitForSeconds(3);
        isDecoding = false;
    }

    //clears popups from the screen 
    public void ClearPopups()
    {
        popupBacground.SetActive(false);
        endConfirmPopup.SetActive(false);
        endSuccessPopup.SetActive(false);
    }
}
