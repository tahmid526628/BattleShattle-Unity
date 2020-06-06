using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject objectToRePosition;
    private bool isTrigger = true;

    private void OnTriggerEnter(Collider collision)
    {
        if (isTrigger)
        {
            if (collision.gameObject.GetComponent<PlayerHealth>() != null)
            {
                collision.gameObject.GetComponent<PlayerHealth>().UpdatePosition(objectToRePosition.transform.position, objectToRePosition.transform.rotation);
                Debug.Log("Done");
                isTrigger = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = true;
    }
}
