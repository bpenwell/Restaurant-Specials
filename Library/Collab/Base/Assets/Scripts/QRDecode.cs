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
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRDecode : MonoBehaviour {
    public RawImage QRWindow;
    private WebCamTexture camTexture;
    public GameObject WindowObject;
    public GameObject ResultText;
    public GameObject startButton;
    public GameObject CameraOnButton;
    public GameObject EndButton;
    public GameObject CameraOffButton;
    public GameObject popupBacground;
    public GameObject startConfirmPopup;
    public GameObject endConfirmPopup;
    public GameObject startSuccessPopup;
    public GameObject endSuccessPopup;

    public void Start () {
        WindowObject.SetActive(false);
        startButton.SetActive(true);
        EndButton.SetActive(false);
        CameraOffButton.SetActive(false);
        CameraOnButton.SetActive(false);
        ResultText.GetComponent<Text>().text = "";
        ClearPopups();
    }

    public void ConfirmStart()
    {
        popupBacground.SetActive(true);
        startConfirmPopup.SetActive(true);
    }

    public void PopupStart()
    {
        startConfirmPopup.SetActive(false);
        startSuccessPopup.SetActive(true);
    }

    public void StartCameraDecode()
    {
        startButton.SetActive(false);
        ClearPopups();
        WindowObject.SetActive(true);
        StartCoroutine(Post(0, "START"));
        EndButton.SetActive(true);
        CameraOffButton.SetActive(true);
        camTexture = new WebCamTexture
        {
            requestedHeight = 800,
            requestedWidth = 600
        };
        QRWindow.texture = camTexture;
        QRWindow.material.mainTexture = camTexture;
        if (camTexture != null) camTexture.Play();
        Debug.Log("Cam Is On");
    }

    public void ConfirmStopRounds()
    {
        popupBacground.SetActive(true);
        endConfirmPopup.SetActive(true);
        StopCamera();
    }

    public void SubmitRounds()
    {
        endConfirmPopup.SetActive(false);
        endSuccessPopup.SetActive(true);
        StartCoroutine(Post(0, "FINISH"));

    }

    public void StopCamera()
    {
        camTexture.Stop();
        Debug.Log("Camera is Off");
        CameraOnButton.SetActive(true);
        CameraOffButton.SetActive(false);
    }
    
    public void ResumeCamera()
    {
        if(!camTexture.isPlaying) camTexture.Play();
        Debug.Log("Camera is on");
        CameraOffButton.SetActive(true);
        CameraOnButton.SetActive(false);
    }

    void OnGUI()
    { 
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
            if (result != null)
            {
                Debug.Log("DECODED TEXT FROM QR: " + result.Text);
                if (Variables.Vibration == 1) Handheld.Vibrate();
                ResultText.GetComponent<Text>().text = result + " Logged";
                ParseInput(result.Text);
            }
        }
        catch(Exception ex) { Debug.LogWarning(ex.Message); }
    }

    void ParseInput(string url)
    {
        //Different outcomes depending on how the decoded text is split up
        string[] arguments = url.Split(':');
        switch (arguments[0])
        {
            case "PR":
                Debug.Log("Property Rounds");
                StartCoroutine(Post(0, arguments[1]));
                break;
            case "Print":
                Debug.Log("Printer");
                Post(1, arguments[1]);
                break;
            case "BR":
                Debug.Log("Restrooms");
                Post(2, arguments[1]);
                break;
            case "http":
            case "https":
                Application.OpenURL(url);
                break;
            default:
                ResultText.GetComponent<Text>().text = url;
                break;
        }
    }

    IEnumerator Post(int option, string argument)
    {
        string[] url = { "https://docs.google.com/forms/d/e/1FAIpQLScoS2yV9TGX2vw88iv_xo0vjjPo5XGAjQU1vB7b0OtDWHn-vQ/formResponse", "https://docs.google.com/forms/d/e/1FAIpQLSeMm8-TyRxixOyV8TccSuHfTIhhdEM4BYaiNvu6UoxNekYgVg/formResponse", "NULL" };
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
                www.AddField("", argument);
                www.AddField("", Variables.Name);
                break;
        }

        byte[] data = www.data;
        WWW request = new WWW(url[option], data);
        yield return request;
    }

    public void ClearPopups()
    {
        popupBacground.SetActive(false);
        startConfirmPopup.SetActive(false);
        endConfirmPopup.SetActive(false); ;
        startSuccessPopup.SetActive(false);
        endSuccessPopup.SetActive(false);
    }
}
