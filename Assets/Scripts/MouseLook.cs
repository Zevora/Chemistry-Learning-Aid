using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 160f;   //Default Mouse sensitiivity

    public Transform playerBody;        //setting the transform for the playerBody

    float xRotation = 0f;               //set our rotation to 0
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;     //Added this
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined; //Added this
        Cursor.visible = true;                      //Added this
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;    //Taking in the input for mouse on X axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;    //Taking in the input for mouse on Y axis

        xRotation -= mouseY;                                                            //to look up and down           += is inverted axis
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);                                 //Clamping rotation so that when you look up and down it doesn't go past 180 degrees
        

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);          
        playerBody.Rotate(Vector3.up * mouseX);                                         //To rotate the body

        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed primary button.");
    }
}
