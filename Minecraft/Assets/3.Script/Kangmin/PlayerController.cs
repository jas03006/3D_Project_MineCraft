using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private RotateToMouse rotateToMouse; // 마우스 이동으로 카메라 회전

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotateToMouse = GetComponent<RotateToMouse>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
    }

    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CalculateRotation(mouseX, mouseY);
    }

    void UpdateMove()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(xAxis, 0, zAxis);

        rb.velocity = inputDirection * moveSpeed;
    }

}
