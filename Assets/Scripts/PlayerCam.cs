using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField]
    float sensX;
    [SerializeField]
    float sensY;

   
    public Transform orientation;

    float xRot;
    float yRot;

    float rotationSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (orientation != null)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            xRot -= mouseY;

            if (GameManager.gm.player.GetComponent<PlayerMove>().turnLeft)
            {
                yRot += -90;
                transform.rotation = Quaternion.Lerp(transform.rotation, (Quaternion.Euler(45, yRot, 0)), rotationSpeed);
            }
            else if (GameManager.gm.player.GetComponent<PlayerMove>().turnRight)
            {
                yRot += 90;
                transform.rotation = Quaternion.Lerp(transform.rotation, (Quaternion.Euler(45, yRot, 0)), rotationSpeed);
            }

            yRot += mouseX;


            xRot = Mathf.Clamp(xRot, -90f, 90f);
            transform.rotation = Quaternion.Euler(45, yRot, 0);
            orientation.rotation = Quaternion.Euler(0, yRot, 0);
        }
        
    }
}
