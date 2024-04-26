using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool turnRight, turnLeft;
    [SerializeField]
    float moveSpeed;

    float ySpeed;
    float originalStep;
    [SerializeField] float jumpSpeed = 10f;
    int jumpCounter;

    [SerializeField] float gravityStrength = 2f;

    CharacterController myCh;

    Transform playerBody;
    

    // Start is called before the first frame update
    void Start()
    {
        myCh = GetComponent<CharacterController>();
        originalStep = myCh.stepOffset;

        playerBody = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        turnLeft = Input.GetKeyDown(KeyCode.A);
        turnRight =   Input.GetKeyDown(KeyCode.D);

        
        if (turnLeft)
        {
            
            transform.Rotate(new Vector3(0,-90,0));
            
            
        }
        else if (turnRight)
        {
            
            transform.Rotate(new Vector3(0, 90, 0));
        }
        float horizontalInput = Input.GetKey(KeyCode.LeftArrow)? -1 : Input.GetKey(KeyCode.RightArrow) ?1 :0;
        //Vector3 movementDirection = new Vector3(transform.right.x*horizontalInput, 0, 1);
        Vector3 movementDirection = transform.forward+transform.right * horizontalInput;
        //float magnitude = movementDirection.magnitude;
        //magnitude = Mathf.Clamp01(magnitude);
        //movementDirection.Normalize();
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
        Vector3 velocity=movementDirection *moveSpeed * 1;
        Vector3 bodyVelocity= movementDirection * moveSpeed * 1;
       
        
        velocity.y = ySpeed;
        myCh.Move(velocity * Time.deltaTime);

    }
}
