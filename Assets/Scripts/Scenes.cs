/*
 * Copyright (C) 2018 Ryan Long: ryanlong@nevada.unr.edu 
 * Made for the Mathewson IGT Knowledge Center Building Operations app
 * 
 * m_manager.cs
 * This class is a simple class to add a functionality to go back to the home screen when the back button (android only) or the back icon (in the GUI) is pressed.
 * This class will also load scenes from the home screen based on which button is pressed
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {

    public GameObject GridView;
    public GameObject ListView;

    //takes a string from the buttons used to switch scenes to determine which scene to switch to
    public void Sceneswitch(string SceneInput)
    {
        SceneManager.LoadScene(SceneInput);
    }

    private void Update()
    {
        //if the user is on android and presses the back button, go home. Won't do anything if you're on the firststartup screen (fixes graphical glitch that was present before)
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "FirstStartup")
        {
            Sceneswitch("Home");
        }
    }

    //runs before the scene loads
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private void Start()
    {
        
        //if the user has never run the app before, it will make them enter their name before continuing as a first run step
        int firstStart = PlayerPrefs.GetInt("firstStart", 0);
        if (firstStart == 0)
        {
            //loads the first start screen if youre not already there but you should be 
            if(SceneManager.GetActiveScene().name != "FirstStartup")
                SceneManager.LoadScene("FirstStartup");
        }
        //PlayerPrefs.Save();

        //If the user is at home, load their chosen view, either list or grid
        if (SceneManager.GetActiveScene().name == "Home")
        {
            if (Variables.HomeScreenMode == 1)
            {
                ListView.SetActive(false);
                GridView.SetActive(true);
            }
            else
            {
                ListView.SetActive(true);
                GridView.SetActive(false);
            }
        }
    }

    //sets the homescreen mode to grid
    public void SwithcHomeGrid()
    {
        Variables.HomeScreenMode = 1;
    }
    
    //sets the mode to list
    public void SwitchHomeList()
    {
       Variables.HomeScreenMode = 0;
    }
}
