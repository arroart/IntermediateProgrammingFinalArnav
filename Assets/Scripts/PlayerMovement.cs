using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 10f;

    [SerializeField] float jumpSpeed= 10f;
    [SerializeField] float playerName;
    float ySpeed;
    float originalStep;
    CharacterController myCh;

    int jumpCounter;
    int jumpMax=1;

    [SerializeField] float gravityStrength = 2f;

    [SerializeField]
    Transform orientation;

    int laneIn;

    [SerializeField]
    float distanceMove = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        myCh = GetComponent<CharacterController>();
        originalStep = myCh.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            float magnitude = movementDirection.magnitude;
            magnitude = Mathf.Clamp01(magnitude);
            movementDirection.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime * gravityStrength * 1f;

            if (myCh.isGrounded)
            {
                myCh.stepOffset = originalStep;

                ySpeed = -0.4f;

                if (Input.GetButtonDown("Jump"))
                {
                    ySpeed = jumpSpeed;
                }
            }
            else
            {
                myCh.stepOffset = 0f;
            }


            Vector3 velocity = movementDirection * magnitude * moveSpeed * 1;
            velocity.y = ySpeed;

        if (Input.GetKeyDown(KeyCode.I))
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }
        //{
        //transform.posi new Vector3(transform.position.x -distanceMove, transform.position.y, transform.position.z);
        //laneIn--;
        //}
        //if (Input.GetKeyDown(KeyCode.D) && laneIn < 1)
        //{
        //transform.position = new Vector3(transform.position.x + distanceMove, transform.position.y, transform.position.z);
        //laneIn--;
        //}

        
        
        myCh.Move(velocity * Time.deltaTime);

            //if (movementDirection != Vector3.zero)
            //{
                //Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
            //}

           
        //if (Input.GetKeyDown(KeyCode.I))
        //{
            //transform.rotation = Quaternion.Euler(0, 10, 0);
        //}
    }

    
}
