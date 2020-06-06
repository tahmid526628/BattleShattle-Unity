using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //public variable
    public float movingSpeed = 10f;
    public float maxDistanceFromWall = 3f;
    public float maxDistanceFromOwn = 0f;
    public float maxSideDistanceFromObstacle = 0f;
    public bool randomWalk = false;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;
    public LayerMask playerLayer;
    public Transform enemyBody;
    public Transform enemyHand1;
    public Transform enemyHand2;
    public Transform enemyLeg;
    public float enemyHandRotationSpeed = 10f;

    //Chasing related
    public Transform target;
    public float minDistanceToChase = 0f;
    public float maxDistanceToChase = 0f;
    public float maxDistanceToChaseWhenShoot = 0f;

    public float chasingSpeed = 0f;

    //public variable for enemy laser beam
    public bool shootLaserBeam = false;
    
    //variable for using this class
    public static EnemyBehaviour em;

    // firing laser beam
    public ParticleSystem fireFlash;
    EnemyShooting enemyShoot;

    //when oplayer shoot first
    //public bool playerShooting = false;


    //for detecting the floor we need another object
    public Transform objectToDetectFloor;
    public LayerMask floorLayer;
    public float maxDistanceFromFloor = 0f;

    public bool followingPlayer = true;

    //private variable
    private Vector3 movingDirection;
    private Rigidbody rb;
    
    

    // Start is called before the first frame update
    void Start()
    {
        // get a reference to the GameManager component for use by other scripts
        if (em == null)
            em = this.gameObject.GetComponent<EnemyBehaviour>();

        enemyShoot = GetComponent<EnemyShooting>();

        rb = gameObject.GetComponent<Rigidbody>();
        movingDirection = ChooseDirection();
        transform.rotation = Quaternion.LookRotation(movingDirection);
        
        //set the target of the enemy

    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 movement = movingDirection * movingSpeed * Time.deltaTime;
        gameObject.transform.Translate(movement);
        */
        enemyHand1.transform.Rotate(Vector3.up * enemyHandRotationSpeed * Time.deltaTime);
        enemyHand2.transform.Rotate(Vector3.up * enemyHandRotationSpeed * Time.deltaTime);

        //chase
        FindingPlayer();

        //if player shoot first, then start to follow and attack
    }

    private void FixedUpdate()
    {
        if (randomWalk)
        {
            RandomMove();
        }
    }



    private void RandomMove()
    {
        rb.velocity = movingDirection * movingSpeed;

        if (Physics.Raycast(transform.position, transform.forward, maxDistanceFromWall, wallLayer)
            || Physics.Raycast(transform.position, transform.forward, maxDistanceFromOwn, enemyLayer)
            || Physics.Raycast(enemyHand1.position, enemyHand1.forward, maxDistanceFromWall, wallLayer)
            || Physics.Raycast(enemyHand2.position, enemyHand2.forward, maxDistanceFromWall, wallLayer)
            || Physics.Raycast(enemyLeg.position, enemyLeg.forward, maxDistanceFromWall, wallLayer)
            || !Physics.Raycast(objectToDetectFloor.position, objectToDetectFloor.forward, maxDistanceFromFloor, floorLayer))
        {
            movingDirection = ChooseDirection();
            //Debug.Log("Change Direction = " + movingDirection);
            transform.rotation = Quaternion.LookRotation(movingDirection);
        }
    }

    Vector3 ChooseDirection()
    {
        System.Random rand = new System.Random();
        int i = rand.Next(0, 3);
        //string selectDirection = directionList[i];
        Vector3 movDir = new Vector3();
        switch (i)
        {
            case 0:
                movDir = transform.forward;
                break;
            case 1:
                movDir = -transform.forward;
                break;
            case 2:
                movDir = transform.right;
                break;
            case 3:
                movDir = -transform.right;
                break;
        }

        return movDir;
    }


    private void FindingPlayer()
    {
        float playerDistance = Vector3.Distance(transform.position, target.position);

        if (target == null)
            return;

        if (Physics.Raycast(transform.position, transform.forward, maxDistanceToChase, playerLayer) 
            || playerDistance < maxDistanceToChaseWhenShoot)
        {
            if (followingPlayer == true)
            {
                FollowPlayer();
            }
        }
        else
        {
            //enable random walk
            randomWalk = true;

            //disable laser
            //shootLaserBeam = false;
        }
        
    }

    public void FollowPlayer()
    {
        // disable the random walk
        randomWalk = false;

        // shoot laser beam beacuse enemy following the player
        //shootLaserBeam = true;
        enemyShoot.Fire();

        //face the target
        transform.LookAt(target, Vector3.up);
        //Debug.Log("Enemy Loking At");

        float distance = Vector3.Distance(transform.position, target.position);
        // if not in minimum distance, move forward
        if (distance > minDistanceToChase)
        {
            transform.position += transform.forward * chasingSpeed * Time.deltaTime;
        }

        
    }

    //new enemy target setter
    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    
}
