using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_1_Y : MonoBehaviour
{
    [Header("����")]
    private float mouseX;
    private float mouseY;
    private float cameraVer;
    private float cameraHor;
    private float r_speed;

    [Header("3��Ī")]
    public Transform cam_pos;
    public float distance = 8.0f; // ī�޶�� �÷��̾� ������ �Ÿ�
    public float XSpeed = 3.0f; // ī�޶� ȸ�� �ӵ�
    private float currentX = 0.0f;

    private float currentY = 0.0f;
    public float maxY = -80; // ī�޶��� �ּ� ����
    public float minY = 80; // ī�޶��� �ִ� ����
    public float YSpeed = 3.0f; // ī�޶� ���� �ӵ�
    public float zoom_distance = 1f;

    private void Start()
    {
        transform.position = new Vector3(0, 1.23f, 0.18f);
    }
    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        Cam1();
    }

    private void Cam1()
    {
        cameraVer -= mouseY;
        cameraHor += mouseX;
        cameraVer = Mathf.Clamp(cameraVer, -90f, 90f);
        transform.localEulerAngles = new Vector3(cameraVer, cameraHor, 0);

        transform.Rotate(Vector3.up, mouseX * r_speed);
    }
}
