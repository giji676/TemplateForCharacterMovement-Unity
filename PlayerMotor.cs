using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    float xMov;
    float zMov;
    float yRot;
    float xRot;

    float walkSpeed = 7f;
    float sensitivity = 1.6f;

    Vector3 moveHorizontal;
    Vector3 moveVertical;

    Vector3 velocity;
    Vector3 rotation;
    Vector3 camRotation;

    public Camera cam;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        getClientInput();
        applyClientInput();
    }

    void getClientInput()
    {
        xMov = Input.GetAxis("Horizontal");
        zMov = Input.GetAxis("Vertical");
        yRot = Input.GetAxisRaw("Mouse X");
        xRot = Input.GetAxisRaw("Mouse Y");

        moveHorizontal = transform.right * xMov;
        moveVertical = transform.forward * zMov;

        velocity = (moveHorizontal + moveVertical).normalized * walkSpeed;

        rotation = new Vector3(0f, yRot, 0f) * sensitivity;
        camRotation = new Vector3(xRot, 0f, 0f) * sensitivity;
    }
    
    void applyClientInput()
    {
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        cam.transform.Rotate(-camRotation);
    }
}
