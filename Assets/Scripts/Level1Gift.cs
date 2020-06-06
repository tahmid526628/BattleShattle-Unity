using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Gift : MonoBehaviour
{
    public bool isTrigger = true;

    public AudioClip gainSFX;


    private float takeTimeToLoadLevel;
    private float saveTime;

    private void Start()
    {
        saveTime = Time.time;
        takeTimeToLoadLevel = 3f;
    }

    //public GameObject blockWall;
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.gameObject.tag == "Player")
            {
                //play audio
                if (gainSFX)
                {
                    AudioSource.PlayClipAtPoint(gainSFX, this.gameObject.transform.position);
                }
                //blockWall.SetActive(false);
                GameManager.gm.isNextLevel = true;
                isTrigger = false;
                Destroy(gameObject);

                //next level load
                GameManager.gm.NextLevelSceneLoad();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = true;
    }
}
