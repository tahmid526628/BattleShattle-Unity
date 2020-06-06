using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertAreaForEnemy : MonoBehaviour
{
    public bool isTrigger = true;


    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("its Enemy");
                other.gameObject.GetComponent<EnemyBehaviour>().followingPlayer = false; 
                other.gameObject.GetComponent<EnemyBehaviour>().randomWalk = true;


                isTrigger = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isTrigger = true;
            other.gameObject.GetComponent<EnemyBehaviour>().followingPlayer = true;
            other.gameObject.GetComponent<EnemyBehaviour>().randomWalk = false;
        }
    }
}
