using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // public
    public GameObject spawnPrefab;
    public Transform target;
    public float minTimeToSpawning;
    public float maxTimeToSpawning;
    //public float rateOfCreatingObject = 0f;
    public ParticleSystem spawningFlash;


    //private
    private float timeBetweenSpawning;
    private float savedTime;

    private void Start()
    {
        savedTime = Time.time;
        timeBetweenSpawning = Random.Range(minTimeToSpawning, maxTimeToSpawning);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - savedTime > timeBetweenSpawning)
        {
            MakeSpawn();
            savedTime = Time.time;
            timeBetweenSpawning = Random.Range(minTimeToSpawning, maxTimeToSpawning);
        }
    }

    void MakeSpawn()
    {
        spawningFlash.Play();
        GameObject spawn = Instantiate(spawnPrefab, transform.position, transform.rotation);
        if(target != null)
        {
            spawn.gameObject.GetComponent<EnemyBehaviour>().SetTarget(target);
        }
    }
}
