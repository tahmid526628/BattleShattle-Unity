using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //public GameObject laserPrefab;
    //public GameObject firingObject;
    public ParticleSystem fireFlash;
    public float fireRange = 0f;
    public float playerHitDamage = 0f;
    public GameObject playerHitEffect;

    //audio
    public AudioClip fireSFX;

    //private
    private float nextTimeToFire;


    public void Fire()
    {
        //laser.SetActive(true);
        if (Time.time > nextTimeToFire)
        {
            fireFlash.Play();
            if (fireSFX)
            {
                //have no audio source
                // that's why create a audio source object dynamically
                //and it will destroy automatically when the audio is done
                AudioSource.PlayClipAtPoint(fireSFX, this.gameObject.transform.position);
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, fireRange))
            {
                //Debug.Log("Shooting");
                PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
                //Debug.Log(target);
                if (target != null)
                {
                    //Debug.Log("Hitted");
                    target.TakeDamage(playerHitDamage);
                    GameObject hitEf = Instantiate(playerHitEffect, hit.point , Quaternion.LookRotation(hit.normal));
                    //hit point theke minus korlam camera theke ektu pisone effect ta deyar jonno
                    Destroy(hitEf, 2f);

                }
            }
            float fireRate = 0.8f;
            nextTimeToFire = Time.time + fireRate;
        }
    }

    private void NotFire()
    {
        //laser.SetActive(false);
    }
}
