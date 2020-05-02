using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    float speed = 12f;
    public float morphSpeed = 6f;
    public Vector3 velocity;
    float gravityBase = -19.62f;
        // ground, water, wall jumping, and checking for the roof all use
        // the same style of checking, but with different sized areas to 
        // check in.
    public bool isGrounded;
    Transform groundCheck;
    float groundDistance = 0.2f;
    public LayerMask groundMask;
    public bool isRoof;
    Transform roofChecker;
    public float roofDistance;
    public LayerMask roofMask;
    float jumpHeight = 3f;
        //maximum jumps and dashes can be modified, but are currently set
        //to 2 of each until upgrades are worked on
    public int jumpsMax;
    public int jumpsLeft;
    GameObject mainCamera;
    bool isMorph;

    public GameObject BriefCase;
    public GameObject crosshair;
    bool usingbriefCase = false;


    void Awake(){
            //setting all check objects to their proper place
        groundCheck = GameObject.Find("GroundCheck").transform;
        roofChecker = GameObject.Find("RoofCheck").transform;
        mainCamera = GameObject.Find("Main Camera");
        isMorph = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isGrounded)
        {
            if(usingbriefCase)
            {
                usingbriefCase = false;
                mainCamera.SetActive(true);
                BriefCase.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                crosshair.SetActive(true);
            }
            else
            {
                usingbriefCase = true;
                mainCamera.SetActive(false);
                BriefCase.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                crosshair.SetActive(false);
            }
        }

        if(!usingbriefCase)
        {
            Movement();
        }

    }

    void Movement()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (isMorph)
            {
                if (!isRoof)
                {
                    //!morphball is just setting scale of player to .5 
                    //and scaling back to full when needed
                    isMorph = false;
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                isMorph = true;
                transform.localScale = new Vector3(1, 0.5f, 1);
            }
        }
        //checking if physics checks are active or not
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isRoof = Physics.CheckSphere(roofChecker.position, roofDistance, roofMask);

        if (isGrounded && velocity.y < 0)
        {    //y velocity while grounded is slightly lower than 0 to keep the player grounded
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");  //setting wasd to modifiers for later use
        float z = Input.GetAxis("Vertical");    //also works on controller for movement but camera doesn't
        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 verticalMove = transform.right * x + transform.up * z;

        if (isGrounded)
        {     //all jumps and dashes regen while grounded, jumps are instant
            jumpsLeft = jumpsMax;
        }

        if (Input.GetButton("Run"))
        {
            controller.Move(move * speed * Time.deltaTime * 2);
        }
        if (Input.GetButtonDown("Jump") && jumpsLeft != 0 && !isMorph)
        {   //jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityBase);
            jumpsLeft--;
        }
        if (isRoof)
        {         //hit a roof and you fall down
            velocity.y = (0);
        }
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravityBase * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}