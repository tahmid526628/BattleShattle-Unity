using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void OnReturnToMainMenu()
    {
        //this.gameObject.SetActive(false);
        GameManager.gm.MainMenuToLoad();
    }
}
