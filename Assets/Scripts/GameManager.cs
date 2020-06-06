using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //for use in other script
    public static GameManager gm;

    //main menu
    public string mainMenuToLoad;

    //next level
    public string nextLevelToLoad;
    public bool isNextLevel = false;

    //pause menu
    public string pauseMenuToLoad;

    //game over scene
    public string gameOverSceneToLoad;
    //public GameObject gameOverToLoad;

    //unlocking the mouse cursor which is locked while playing game
    public bool unlockMouseCursor = false;
    public bool mouseRotation = true;

    //store checkpoint
    //public GameObject playerLastCheckpoint;


    //private var


    // Start is called before the first frame update
    void Start()
    {
        if (gm == null)
        {
            gm = this.gameObject.GetComponent<GameManager>();
        }
    }

    public void PauseMenuSceneLoad()
    {
        SceneManager.LoadScene(pauseMenuToLoad);
    }

    public void NextLevelSceneLoad()
    {
        SceneManager.LoadScene(nextLevelToLoad);
    }

    public void GameOverSceneLoad()
    {
        unlockMouseCursor = true;
        SceneManager.LoadScene(gameOverSceneToLoad);
        
        Debug.Log("Called");
        Debug.Log(unlockMouseCursor);

        /*******manually unlock again the mouse cursor. if the function in the 
         mouse looker script not work then it will insha Allah*/
        // make the mouse pointer visible
        Cursor.visible = true;

        // unlock the mouse pointer so player can click on other windows
        Cursor.lockState = CursorLockMode.None;
        /*************************/
    }

    public void MainMenuToLoad()
    {
        unlockMouseCursor = true;
        SceneManager.LoadScene(mainMenuToLoad);

    }
}
