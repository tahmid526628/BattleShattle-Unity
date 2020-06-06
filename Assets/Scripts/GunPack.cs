using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPack : MonoBehaviour
{
    public bool isTrigger = true;

    public int minNumberOfBulletPack;
    public int maxNumberOfBulletPack;

    public AudioClip gainSFX;

    private int numberOfBulletPack;



    private void Start()
    {
        numberOfBulletPack = Random.Range(minNumberOfBulletPack, maxNumberOfBulletPack);
    }

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
                int packNum = Gun.gun.GetNumberBulletPack() + numberOfBulletPack;
                Gun.gun.SetBullet(packNum);
                isTrigger = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = true;
    }
}
