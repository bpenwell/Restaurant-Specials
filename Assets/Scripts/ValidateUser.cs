using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public static class ValidateUser {
    static public IEnumerator CheckName (string name) 
    {
        //Variables.doneLoading = false;
        //name must be sent with an underscore 
        Debug.Log("Checking User Status");
        name = name.Replace(' ', '_');
        Debug.Log(name);
        //string Realurl = "https://script.google.com/macros/s/AKfycbwhmWZWh5rSUtLX0ckFci-fAlwr13kXdE_CuuWnhXXLXDJ1_qQ/exec" + "?name=" + name;
        string url = "https://script.google.com/macros/s/AKfycbwouI50V1zDIJu_QGZPM9n_ThGb4hWqYexene1-1ARjolth6SRc/exec" + "?name=" + name;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string[] deliniators = { "<title>", "</title>" };
        string[] body = www.downloadHandler.text.Split(deliniators, StringSplitOptions.RemoveEmptyEntries);

        if (body[1] == "Success")
        {
            Debug.Log("it worked");
            PlayerPrefs.SetInt("ValidUser", 1);
            PlayerPrefs.SetString("Name", name.Replace('_', ' '));
            PlayerPrefs.SetInt("firstStart", 1);
            PlayerPrefs.SetInt("ValidUser", 1);
            PlayerPrefs.Save();
            Variables.ValidUser = true;
            yield return true;
        }
        else
        {
            Debug.Log("it didnt work");
            PlayerPrefs.SetInt("ValidUser", 0);
            Variables.ValidUser = false;
            yield return false;
        }

        //if youre not already on the first startup screen, move there
        if (!Variables.ValidUser && SceneManager.GetActiveScene().name != "FirstStartup")
        {
            SceneManager.LoadScene("FirstStartup");
        }
        if (Variables.ValidUser && SceneManager.GetActiveScene().name == "validate") SceneManager.LoadScene("Home");
        Debug.Log("User Status Checked!");
    }
}
