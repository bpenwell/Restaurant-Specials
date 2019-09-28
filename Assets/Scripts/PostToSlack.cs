using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * @brief posts to the appropriate slack channel depending on the input recieved. appropriately formats the content so it can be parsed by the javascript parsing system
 */ 
public static class PostToSlack {
    /*
     * @brief handles the posting of items to slack
     * 
     * @param[in] message: a string containing the message to be posted
     * @param[in] option: an integer representing the option of the slack post 
     */ 
    static public IEnumerator Post (string message, int option)
    {
        //WE ONLY NEED ONE URL NOW!! YAY
        string URL = "https://script.google.com/macros/s/AKfycbz6kZCmtwYK7s-8snWYvM2L8ufwAhWI0Tb6RiXRbRWG-0Axm98/exec";
        string[] name = Variables.Name.Split(' ');
        //replacing spaces with _ so that it will load properly into the page
        message = message.Replace(' ', '_');
        switch (option)
        {
            //Hourly Assignments
            case 0:
                Debug.Log(name[0] + "_is_on_" + message);
                string url = URL  + "?body=" + name[0] + "_is_on_" + message + "&channel=0";
                UnityWebRequest hourly = new UnityWebRequest(url);
                yield return hourly.SendWebRequest();
                Debug.Log("Message sent to Hourly Assignments!");
                break;
            //Bathrooms 
            case 1:
                string fullName = Variables.Name.Replace(" ", "_");
                Debug.Log("This is a bathroom post");
                string restroomURL = URL + "?body=" + message + "&channel=1";
                UnityWebRequest bathrooms = new UnityWebRequest(restroomURL);
                yield return bathrooms.SendWebRequest();
                Debug.Log("Bathroom posted");
                break;
            //Property Rounds
            case 2:
                Debug.Log("Prop Rounds Post");
                string prUrl = URL + "?body=" + message + "&channel=2";
                UnityWebRequest prop = new UnityWebRequest(prUrl);
                yield return prop.SendWebRequest();
                Debug.Log("prop rounds posted");
                break;
        }
        yield return 0;
    }
}
