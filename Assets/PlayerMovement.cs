using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask grounndMask;

    public  Transform world;
    Vector3 velocity;
    bool isGrounded;

    public float rotateSpeed = 0.5f;
    float currentRotation = 0;
    Quaternion wantedRotation = Quaternion.Euler(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, grounndMask);
        

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move *  speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        if(Input.GetButtonDown("Submit"))
        {
             currentRotation+=90;
             wantedRotation = Quaternion.Euler(0,0,currentRotation); 
        }

        world.rotation = Quaternion.RotateTowards(world.rotation, wantedRotation, Time.deltaTime * rotateSpeed);
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
