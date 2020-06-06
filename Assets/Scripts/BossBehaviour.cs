using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    //================public================
    public Transform target;

    public LayerMask wallLayer;
    public float maxDistanceFromWall = 0f;
    public LayerMask floorLayer;
    public float maxDistanceFromFloor = 0f;
    public LayerMask playerLayer;
    public float maxDistanceFromPlayer = 0f;

    public GameObject floorDetector;

    public GameObject bossHand1;
    public GameObject bossHand2;   
    public float handRotationSpeed = 0f;

    public GameObject bossEye1;
    public GameObject bossEye2;

    public GameObject powerBox;

    public float moveSpeed = 0f;
    public float chaseSpeed = 0f;
    public float minDistanceToChase = 0f;
    public bool randomWalk = false;

    //related to Fire
    public float maxDistanceToFire = 0f;
    public GameObject fireEffect;
    public GameObject extraEffect;
    public GameObject playerHitEffect;
    public ParticleSystem fireFlash1;
    public ParticleSystem fireFlash2;
    public float damageToPlayer = 0f;
    public float minTimeToPower;
    public float maxTimeToPower;
    public float maxDistanceWhenPlayerShoot = 0f;

    //audio
    public AudioClip fireSFX;
    public AudioClip extraPowerSFX;
    

    //related to extra power
    
    public GameObject projectile;
    public float projectileForce = 0f;

    //================private=============== 
    private Rigidbody rb;
    private Vector3 moveDirection;

    //fire ralted
    private float nextTimeToFire;
    private float timeToExtraPower = 0f;
    private float saveTime;

    public bool followingPlayer = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        moveDirection = ChooseDirection();
        transform.rotation = Quaternion.LookRotation(moveDirection);

        saveTime = Time.time;
        timeToExtraPower = Random.Range(minTimeToPower, maxTimeToPower);
    }

    // Update is called once per frame
    void Update()
    {
        //in every update, boss hand rotation will update and from that it
        // will sends a raycast to detect more perfectly
        bossHand1.transform.Rotate(Vector3.up * handRotationSpeed * Time.deltaTime);
        bossHand2.transform.Rotate(Vector3.up * handRotationSpeed * Time.deltaTime);
        
        // searching the player
        FindPlayer();
    }

    private void FixedUpdate()
    {
        // if randomWalk true than walk randomly
        if (randomWalk == true)
        {
            RandomWalk();
            //Debug.Log(randomWalk);
        }
    }

    void RandomWalk()
    {
        rb.velocity = moveDirection * moveSpeed;

        if (Physics.Raycast(transform.position, transform.forward, maxDistanceFromWall, wallLayer)
            || Physics.Raycast(bossHand1.transform.position, bossHand1.transform.forward, maxDistanceFromWall, wallLayer)
            || Physics.Raycast(bossHand2.transform.position, bossHand2.transform.forward, maxDistanceFromWall, wallLayer)
            || Physics.Raycast(floorDetector.transform.position, floorDetector.transform.forward, maxDistanceFromFloor, floorLayer)
            )
        {
            moveDirection = ChooseDirection();
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    private void FindPlayer()
    {
        float playerDistance = Vector3.Distance(transform.position, target.position);

        if (target == null)
        {
            return;
        }

        if(Physics.Raycast(transform.position, transform.forward, maxDistanceFromPlayer, playerLayer)
            || Physics.Raycast(powerBox.transform.position, powerBox.transform.forward, maxDistanceFromPlayer, playerLayer)
            || Physics.Raycast(bossEye1.transform.position, bossEye1.transform.forward, maxDistanceFromPlayer, playerLayer)
            || Physics.Raycast(bossEye2.transform.position, bossEye2.transform.forward, maxDistanceFromPlayer, playerLayer)
            || playerDistance <= maxDistanceWhenPlayerShoot
            )

        {
            if (followingPlayer == true)
            {
                FollowPlayer();
            }
        }
        else
        {
            randomWalk = true;
            //Debug.Log("Random Walking");
        }
    }

    private void FollowPlayer()
    {
        randomWalk = false;
        //Debug.Log("Following the Player");

        //face the target
        transform.LookAt(target, Vector3.up);

        //until distance between target and the boss is not touching the minismum distance to chase
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance > minDistanceToChase)
        {
            transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        }
        //checking again for fire
        distance = Vector3.Distance(transform.position, target.position);
        if (distance <= maxDistanceToFire)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (Time.time > nextTimeToFire)
        {
            //Debug.Log("Firing");
            fireFlash1.Play();
            fireFlash2.Play();
            if (fireSFX)
            {
                //have no audio source
                // that's why create a audio source object dynamically
                //and it will destroy automatically when the audio is done
                AudioSource.PlayClipAtPoint(fireSFX, this.gameObject.transform.position);
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistanceToFire))
            {
                PlayerHealth targetHealth = hit.transform.GetComponent<PlayerHealth>();

                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damageToPlayer);
                    GameObject hitEf = Instantiate(playerHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(hitEf, 1.5f);
                }
            }
            float fireRate = 0.7f;
            nextTimeToFire = Time.time + fireRate;
        }
        if(Time.time - saveTime >= timeToExtraPower)
        {
            if (extraPowerSFX)
            {
                AudioSource.PlayClipAtPoint(extraPowerSFX, this.gameObject.transform.position);
            }
            ExtraPower();
            saveTime = Time.time;
            timeToExtraPower = Random.Range(minTimeToPower, maxTimeToPower);
        }
    }

    private void ExtraPower()
    {
        if (projectile)
        {
            GameObject newProjectile = Instantiate(projectile, powerBox.transform.position, powerBox.transform.rotation) as GameObject;

            //if it has not rigidbody
            if (!newProjectile.GetComponent<Rigidbody>())
            {
                newProjectile.AddComponent<Rigidbody>();
            }
            newProjectile.GetComponent<Rigidbody>().AddForce(powerBox.transform.forward * projectileForce, ForceMode.VelocityChange);
        }

        //now the audio
    }

    private Vector3 ChooseDirection()
    {
        System.Random rand = new System.Random();
        int i = rand.Next(0, 3);

        Vector3 newDirection = new Vector3();
        switch (i)
        {
            case 0:
                newDirection = transform.forward;
                break;
            case 1:
                newDirection = -transform.forward;
                break;
            case 2:
                newDirection = transform.right;
                break;
            case 3:
                newDirection = -transform.right;
                break;
        }

        return newDirection;
    }
}
