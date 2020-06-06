using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public string firstStageScene;
    public string settingsScene;
    public string instructionScene;
    public string mainMenuScene;



    public void OnStartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // this line load scene from the build settings
        SceneManager.LoadScene(firstStageScene);
        Debug.Log(firstStageScene);
    }

    public void OnSettings()
    {
        SceneManager.LoadScene(settingsScene);
        Debug.Log("Settings");
    }

    public void OnInstructions()
    {
        SceneManager.LoadScene(instructionScene);
        Debug.Log("Instructions");
    }

    // amra chaile jeshob button er jonno onno scene load dite hobe na shegulo code sarai active korte paari, inspector theke(button action e)

    public void OnExit()
    {
        Application.Quit();
        Debug.Log("Exit. Thank You!");
    }

    public void OnBack()
    {
        SceneManager.LoadScene(mainMenuScene);
        Debug.Log("Main Menu");
    }
}
