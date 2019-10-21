/*
 * Copyright (C) 2018 Ben Penwell
 * Made for the Deal-Breaker app
 * 
 * DatabaseContact.cs
 * Class that contacts database for deal query and/or submital
 */

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseContact : MonoBehaviour
{
    public TextMeshProUGUI output;
    private string RECEIVE_DATABASE_URL => "http://bpenwell.com/printData.php?dbusername=ben10002_admin&dbpassword=admin&dbname=ben10002_VirtualPantryProfiles";
    private string UPLOAD_DATABASE_URL => "https://www.bpenwell.com";

    public void FindDeals()
    {
        StartCoroutine(ReceiveDeals());
    }

    private IEnumerator UploadDeal(DealForm data, ProfileData profile)
    {
        WWWForm form = new WWWForm();

        form.AddField("Author", profile.Username);
        form.AddField("BusinessName", data.BusinessName);
        form.AddField("BusinessAddress", data.BusinessAddress);
        form.AddField("City", data.City);
        form.AddField("State", data.State);
        form.AddField("DealType", data.DealType);
        form.AddField("DealTags", data.DealTags);
        form.AddField("ShortDescription", data.ShortDescription);
        form.AddField("Frequency", data.Frequency);
        form.AddField("AllDay", data.AllDay.ToString());
        form.AddField("StartTime", data.StartTime);
        form.AddField("EndTime", data.EndTime);

        UnityWebRequest www = UnityWebRequest.Put(UPLOAD_DATABASE_URL, form.data);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
        yield return null;
    }


    //https://answers.unity.com/questions/344770/how-to-get-gps-coordinates-in-unity-3d.html?_ga=2.55515179.1058483353.1571279312-1615045507.1533450659
    //^ Everything you need to get location data
    private IEnumerator ReceiveDeals()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(RECEIVE_DATABASE_URL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                output.text = webRequest.downloadHandler.text;
            }

            //// This isn't required, but I prefer to pass in a callback so that I can
            //// act on the response data outside of this function
            //if (callback != null)
            //    callback(webRequest.downloadHandler.data);

            yield return null;
        }
    }

    // Callback to act on our response data
    private void ResponseCallback(LocationInfo userLocationData, string data)
    {
        var latitude = userLocationData.latitude;
        var longitude = userLocationData.longitude;
        Debug.Log(data);

        //Send back data
        //return ...
    }
}
