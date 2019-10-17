/*
 * Copyright (C) 2018 Ben Penwell
 * Made for the Deal-Breaker app
 * 
 * DatabaseContact.cs
 * Class that contacts database for deal query and/or submital
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseContact
{

    //private readonly string RECEIVE_DATABASE_URL => "https://www.bpenwell.com";
    //private readonly string UPLOAD_DATABASE_URL => "https://www.bpenwell.com";

    //IEnumerator UploadDeal(DealForm data, ProfileData profile)
    //{
    //    WWWForm form = new WWWForm();

    //    form.AddField("Author", profile.Username);
    //    form.AddField("BusinessName", data.BusinessName);
    //    form.AddField("BusinessAddress", data.BusinessAddress);
    //    form.AddField("City", data.City);
    //    form.AddField("State", data.State);
    //    form.AddField("DealType", data.DealType);
    //    form.AddField("DealTags", data.DealTags);
    //    form.AddField("ShortDescription", data.ShortDescription);
    //    form.AddField("Frequency", data.Frequency);
    //    form.AddField("AllDay", data.AllDay.ToString());
    //    form.AddField("StartTime", data.StartTime);
    //    form.AddField("EndTime", data.EndTime);

    //    //UnityWebRequest www = UnityWebRequest.Put(DATABASE_URL, form);
    //    //yield return www.SendWebRequest();

    //    //if (www.isNetworkError || www.isHttpError)
    //    //{
    //    //    Debug.Log(www.error);
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("Upload complete!");
    //    //}
    //}

    //https://answers.unity.com/questions/344770/how-to-get-gps-coordinates-in-unity-3d.html?_ga=2.55515179.1058483353.1571279312-1615045507.1533450659
    //^ Everything you need to get location data
    //private IEnumerator ReceiveDeals(Action<string> callback = null)
    //{
    //    UnityWebRequest www = UnityWebRequest.get(RECEIVE_DATABASE_URL);
    //    yield return request.SendWebRequest();
    //    var data = request.downloadHandler.text;

    //    // This isn't required, but I prefer to pass in a callback so that I can
    //    // act on the response data outside of this function
    //    if (callback != null)
    //        callback(data);

    //}

    //// Callback to act on our response data
    //private void ResponseCallback(LocationInfo userLocationData, string data)
    //{
    //    var lat = userLocationData.latitude;
    //    var long = userLocationData.longitude;
    //    Debug.Log(data);
    //    recentData = data;

    //    //Send back data
    //    //return ...
    //}
}
