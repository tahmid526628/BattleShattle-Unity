using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public
    public GameObject pauseMenuUI;

    public static bool isPaused = false;
    public string mainMenuScene;

    //private

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;

        //mouse cursor should be disabled
        GameManager.gm.unlockMouseCursor = false;
        //also enable mouse rotation
        GameManager.gm.mouseRotation = true;
    }

    void Pause()
    {
        //mouse cursor should be enabled
        GameManager.gm.unlockMouseCursor = true;
        //also disable mouse rotation
        GameManager.gm.mouseRotation = false;
        // have to freeze the game
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
