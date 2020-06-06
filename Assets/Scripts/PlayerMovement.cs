using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public float enemyAlertDistance = 0f;
    public LayerMask alertLayer;

    public static PlayerMovement pm;

    private float gravity = 9.81f;
    private float verticalVelocity = 0f;
    //private bool isRun = false;
    
    private CharacterController myCharacterController;

    // Start is called before the first frame update
    void Start()
    {
        if (pm == null)
            pm = this.gameObject.GetComponent<PlayerMovement>();

        myCharacterController = gameObject.GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        Controller();
        ManualCheckWhenDrop();
    }

    private void Controller()
    {
        if (myCharacterController.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }

        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveAlongZ = Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime;
        Vector3 moveAlongX = Input.GetAxis("Horizontal") * Vector3.right * moveSpeed * Time.deltaTime;
        Vector3 moveAlongY = new Vector3(0, verticalVelocity * Time.deltaTime, 0);

        Vector3 movement = transform.TransformDirection(moveAlongX + moveAlongZ + moveAlongY);
        //Debug.Log("Movement = " + movement);

        //movement.y -= gravity * Time.deltaTime;

        myCharacterController.Move(movement);
    }


    public void EnableCharacterController(bool cond)
    {
        myCharacterController.enabled = cond;
    }

    public void ManualCheckWhenDrop()
    {
        if( this.gameObject.transform.position.y < -1.2f)
        {
            this.gameObject.GetComponent<PlayerHealth>().DeathZoneTakeDamage(100f);
        }
    }
}
