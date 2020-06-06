using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    //private
    //private HealthBar healthBar;
    //private PlayerHealth playerHealth;
    //public float health = 200f;

    public AudioClip gainSFX;

    // Start is called before the first frame update
    void Start()
    {
       // playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && PlayerHealth.ph.currentHealth < PlayerHealth.ph.maxHealth)
        {
            Debug.Log("collide");

            //play audio
            if (gainSFX)
            {
                AudioSource.PlayClipAtPoint(gainSFX, this.gameObject.transform.position);
            }

            PlayerHealth.ph.SetHealth();
            Destroy(gameObject);
        }
    }
}
