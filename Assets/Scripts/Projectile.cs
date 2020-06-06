using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageToPlayer = 0f;

    //for destroying itself
    public float destroyTime = 0f;

    private float currentDestroyTime;
    private float saveTime;

    private void Start()
    {
        saveTime = Time.time;
        currentDestroyTime = destroyTime;
    }

    private void Update()
    {
        DestroyProjectile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageToPlayer);
            Destroy(gameObject);
        }
    }

    private void DestroyProjectile()
    {
        if(Time.time - saveTime >= currentDestroyTime)
        {
            Destroy(gameObject);
            saveTime = Time.time;
            currentDestroyTime = destroyTime;
        }
    }
}
