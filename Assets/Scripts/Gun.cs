using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //for public use
    public Camera gunCam;
    public ParticleSystem gunFlare;
    public GameObject hitEffect;
    public GameObject hitEffectForGiftBox;

    public float shootRange = 50f;
    public float damageAmount = 10f;
    //continuous fire
    public float fireRate = 5f;
    public float nextTimeToFire = 0f;
    public float numberOfBullets = 6f;

    public static Gun gun;
    public bool playerShooting = false;

    //gun bullet show
    public Text countText;
    public int numberOfBulletPack = 0;
    public GameObject reloadText;

    //audio
    public AudioClip shootSFX;
    public AudioClip emptyGunSFX;
    public AudioClip reloadSFX;

    //private
    private float bullets;
    private int currentBulletPack;
    private bool doReload = true;

    // Start is called before the first frame update
    void Start()
    {
        //initiating for public use
        gun = this.gameObject.GetComponent<Gun>();

        //initial bullets
        SetBullet(numberOfBulletPack);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && bullets > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            bullets -= 1f;

            UpdateBulletText();
        }
        else if(bullets <= 0)
        {
            doReload = true;
            reloadText.SetActive(true);
            //Debug.Log("Reload");

            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.R))
            {
                if(currentBulletPack >0 && doReload)
                {
                    currentBulletPack -= 1;
                    Reload();
                }                      
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (emptyGunSFX)
                {
                    AudioSource.PlayClipAtPoint(emptyGunSFX, this.gameObject.transform.position);
                }
            }
        }
        else
        {
            doReload = false;
        }

        //if press right mouse button without need of reload
        if (doReload == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.R))
            {
                if (currentBulletPack > 0)
                {
                    currentBulletPack -= 1;
                    Reload();
                }
            }
        }
    }

    private void Shoot()
    {
        //play the Gun Flare particle system
        gunFlare.Play();
        if (shootSFX)
        {
            if (this.gameObject.GetComponent<AudioSource>()) //if the game object has its own audio source
            {
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(shootSFX);
            }
            else
            {
                // dynamically create a new gameObject with an AudioSource
                // this automatically destroys itself once the audio is done
                AudioSource.PlayClipAtPoint(shootSFX, this.gameObject.transform.position);
            }
        }

        RaycastHit hit;

        if (Physics.Raycast(gunCam.transform.position, gunCam.transform.forward, out hit, shootRange))
        {
            //Debug.Log("Object :" + hit.transform.name);
            EnemyDamage target = hit.transform.GetComponent<EnemyDamage>();
            //EnemySpawn spawnTarget = hit.transform.GetComponent<EnemySpawn>();
            GiftDamage giftBox = hit.transform.GetComponent<GiftDamage>();
            WallBreaker breakableWall = hit.transform.GetComponent<WallBreaker>();

            if (target != null)
            {
                target.GetDamage(damageAmount);

                GameObject hitIm = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                hitIm.SetActive(true);
                Destroy(hitIm, 2f);

                //enemy alert
                //EnemyBehaviour.em.playerShooting = true;
            }

            if (giftBox) 
            {
                GameObject hitef = Instantiate(hitEffectForGiftBox, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitef, 1.5f);
                giftBox.TakeDamage(damageAmount);
            }

            //wall breaker
            if (breakableWall != null)
            {
                breakableWall.GetDamaged();
            }

            // destroy spawn
            

            /*if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }*/
        }
    }

    private void Reload()
    {
        if (reloadSFX)
        {
            AudioSource.PlayClipAtPoint(reloadSFX, this.gameObject.transform.position);
        }
        //set the reload text false
        reloadText.SetActive(false);

        //set bullet after relad
        SetBullet(currentBulletPack);
    }

    public void SetBullet(int bulletPack)
    {
        bullets = numberOfBullets;
        currentBulletPack = bulletPack;
        countText.text = bulletPack.ToString() + "/" + numberOfBullets.ToString();
    }
    public void UpdateBulletText()
    {
        countText.text = currentBulletPack.ToString() + "/" + bullets.ToString();
    }

    public int GetNumberBulletPack()
    {
        return currentBulletPack;
    }
}
