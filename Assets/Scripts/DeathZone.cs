using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public float damage = 0f;
    public bool isTrigger = true;

    public bool isContinuousDamage = false;
    public float timeDelayToDamage = 0f;

    public GameObject lifeDecreasingEffect;

    //private
    private float saveTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject.GetComponent<PlayerHealth>() != null)
                {
                    other.gameObject.GetComponent<PlayerHealth>().DeathZoneTakeDamage(damage);
                    isTrigger = false;

                    Debug.Log("Collide");
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isContinuousDamage)
        {
            if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerHealth>() != null)
            {
                if (Time.time - saveTime >= timeDelayToDamage)
                {
                    saveTime = Time.time;
                    other.gameObject.GetComponent<PlayerHealth>().DeathZoneTakeDamage(damage);
                    //isTrigger = false;
                    GameObject lifeDecEf = Instantiate(lifeDecreasingEffect, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    Destroy(lifeDecEf, 1.5f);
                    Debug.Log("Still Collide");
                }
            }
        }
    }

}
