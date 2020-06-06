using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GiftDamage : MonoBehaviour
{
    //public
    public HealthBar healthBar;
    public float maxHealth;
    public GameObject destroyingEffect;
    public float currentHealth;

    //object that appears after braking the gift box
    public GameObject lifeHealBox;
    public GameObject lifeUpgrade;
    public GameObject gunPack;
    public enum listOfGift { lifeHealBox, lifeUpgrade, gunPack };
    public listOfGift chooseGift = listOfGift.lifeHealBox;

    //audio
    public AudioClip breakSFX;

    //private
    private GameObject innerObject;

    private Transform tempTransform;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Break();
        }
    }

    public void Break()
    {
        //this.gameObject.GetComponent<AudioSource>().Play();
        //store transform when destry
        tempTransform = this.gameObject.transform;

        Destroy(gameObject);

        if (breakSFX)
        {
            AudioSource.PlayClipAtPoint(breakSFX, tempTransform.position);
        }
        GameObject destroyingEf = Instantiate(destroyingEffect, transform.position, transform.rotation);
        destroyingEf.SetActive(true);
        
        Destroy(destroyingEf, 2f);

        GiftChooser();
        GameObject innerOb = Instantiate(innerObject, transform.position, transform.rotation);
        innerOb.SetActive(true);
    }

    /*private void RandomChoose()
    {
        System.Random rand = new System.Random();
        int r;
    }*/

    private void GiftChooser()
    {
        switch (chooseGift)
        {
            case listOfGift.lifeHealBox:
                innerObject = lifeHealBox;
                break;
            case listOfGift.lifeUpgrade:
                innerObject = lifeUpgrade;
                break;
            case listOfGift.gunPack:
                innerObject = gunPack;
                break;
        }
    }
}
