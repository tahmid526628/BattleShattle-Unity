using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedArea : MonoBehaviour
{
    public bool isTrigger = false;
    public GameObject boundedWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTrigger = true;
            if (isTrigger)
            {
                if (boundedWall != null)
                {
                    boundedWall.SetActive(true);
                    isTrigger = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTrigger = false;
            //boundedWall.SetActive(false);
        }
    }
}
