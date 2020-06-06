using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLife : MonoBehaviour
{
    public int lifeIncrement;

    public AudioClip gainSFX;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //play audio
            if (gainSFX)
            {
                AudioSource.PlayClipAtPoint(gainSFX, this.gameObject.transform.position);
            }
            Debug.Log("Upgraded");
            PlayerHealth.ph.UpdateLifeCountIncrement(lifeIncrement);
            Destroy(gameObject);
        }
    }
}
