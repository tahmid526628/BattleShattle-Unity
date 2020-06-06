using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float currentEnemyHealth;
    public float maxEnemyHealth = 50f;
    public HealthBar enemyHealthBar;

    public enum enemyLevel { beginningEnemy, Boss};
    public enemyLevel enmLevel = enemyLevel.beginningEnemy;

    //for level1 boss
    public GameObject level1Gift;
    public GameObject congratulationCanvas;
    public GameObject blockWallToNextLevel;
    public AudioClip gainSFX;
    public GameObject target; // need this for play audio at player point
    // otherwise the sound will not loud

    //public TextMeshPro giftText;

    private void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        enemyHealthBar.SetMaxHealth(maxEnemyHealth);
    }
    public void GetDamage(float damageAmount)
    {
        currentEnemyHealth -= damageAmount;
        enemyHealthBar.SetHealth(currentEnemyHealth);
        if(currentEnemyHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        switch (enmLevel)
        {
            case enemyLevel.Boss:
                congratulationCanvas.SetActive(true);
                //play audio
                if (gainSFX)
                {
                    AudioSource.PlayClipAtPoint(gainSFX, target.transform.position);
                }
                //giftText.text = "You've got Online Classes";
                GameObject gift = Instantiate(level1Gift, transform.position + new Vector3(0,1.4f,0), transform.rotation);              
                break;
        }
    }
}
