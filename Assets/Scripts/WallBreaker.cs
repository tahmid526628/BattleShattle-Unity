using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreaker : MonoBehaviour
{
    public int breakableObjectHealth = 0;
    public GameObject breakEffect;

    //audio
    public AudioClip breakSFX;

    private Transform tempTransform;

    public void GetDamaged()
    {
        breakableObjectHealth -= 1;
        if (breakableObjectHealth <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        //this.gameObject.GetComponent<AudioSource>().Play();

        tempTransform = this.gameObject.transform;

        Destroy(gameObject);
        if (breakSFX)
        {
            AudioSource.PlayClipAtPoint(breakSFX, tempTransform.position, 100f);
        }
        GameObject effect = Instantiate(breakEffect, transform.position, transform.rotation);
        
        Destroy(effect, 1.5f);
    }
}
