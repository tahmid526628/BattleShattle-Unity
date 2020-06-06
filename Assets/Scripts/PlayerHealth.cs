using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public HealthBar healthBar;
    //public LifeCounter lifeCounter;
    public int lifeCount = 1;
    public int currentLifeCount;
    public static PlayerHealth ph;

    //audio
    public AudioClip hurtClip;

    //private
    private Vector3 newPosition;
    private Quaternion newRotation;


    private void Start()
    {
        ph = gameObject.GetComponent<PlayerHealth>();
      
        SetHealth();
        SetLifeCountOnStart(lifeCount);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (hurtClip)
        {
            AudioSource.PlayClipAtPoint(hurtClip, this.gameObject.transform.position);
        }
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            //die();
            UpdateLifeCountDecrement(1);
            if (currentLifeCount > 0) // in: > 0 the life remain extra 1, don't know why 
            { 
                SetHealth();
                //GameManager.gm.unlockMouseCursor = true;

            }
            else
            {
                //
                Die();
            }
        }
    }

    //for death zone and others
    public void DeathZoneTakeDamage(float damage)
    {
        currentHealth -= damage;
        if (hurtClip)
        {
            AudioSource.PlayClipAtPoint(hurtClip, this.gameObject.transform.position);
        }
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            //die();
            UpdateLifeCountDecrement(1);
            if (currentLifeCount > 0) // in: > 0 the life remain extra 1, don't know why 
            {
                SetHealth();

                //
                PlayerMovement.pm.EnableCharacterController(false);
                //Debug.Log(PlayerMovement.pm);
                transform.position = newPosition;
                transform.rotation = newRotation;
                PlayerMovement.pm.EnableCharacterController(true); // character controller jodi enable thaake tahole kono transform kaaj korbe na. r tai eta k disable kore nite hobe
                //Debug.Log("transformed");
            }
            else
            {
                Die();
            }
        }
    }

    private void Die()
    {
        //Destroy(gameObject);

        //load GameOver scene
        // but for now The main menu scene
        if (GameManager.gm != null)
        {
            
            GameManager.gm.GameOverSceneLoad();
            //Debug.Log(GameManager.gm.unlockMouseCursor);
        }
    }

    public void SetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //Debug.Log(currentHealth);
    }


    //life count related
    public void SetLifeCountOnStart(int lCount)
    {
        currentLifeCount = lCount;
        LifeCounter.lc.SetLifeCount(lCount);
    }

    public void SetLifeCount(int lCount)
    {
        LifeCounter.lc.SetLifeCount(lCount);
    }

    public void UpdateLifeCountDecrement(int decLife)
    {
        currentLifeCount -= decLife;
        LifeCounter.lc.SetLifeCount(currentLifeCount);
    }

    public void UpdateLifeCountIncrement(int incLife)
    {
        currentLifeCount += incLife;
        SetLifeCount(currentLifeCount);
    }

    public void UpdatePosition(Vector3 position, Quaternion rotation)
    {
        newPosition = position;
        newRotation = rotation;
    }
}
