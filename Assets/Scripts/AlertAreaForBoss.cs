using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertAreaForBoss : MonoBehaviour
{
    public bool isTrigger = true;


    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("its Boss");
                other.gameObject.GetComponent<BossBehaviour>().followingPlayer = false; 
                other.gameObject.GetComponent<BossBehaviour>().randomWalk = true;


                isTrigger = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isTrigger = true;
            other.gameObject.GetComponent<BossBehaviour>().followingPlayer = true;
            other.gameObject.GetComponent<BossBehaviour>().randomWalk = false;
        }
    }
}
