using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public HealthBar healthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 20f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }
}
