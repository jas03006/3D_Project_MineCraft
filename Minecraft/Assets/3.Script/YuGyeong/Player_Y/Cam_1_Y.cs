using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_1_Y : MonoBehaviour
{
    [Header("공용")]
    private float mouseX;
    private float mouseY;
    private float cameraVer;
    private float cameraHor;
    private float r_speed;

    [Header("3인칭")]
    public Transform cam_pos;
    public float distance = 8.0f; // 카메라와 플레이어 사이의 거리
    public float XSpeed = 3.0f; // 카메라 회전 속도
    private float currentX = 0.0f;

    private float currentY = 0.0f;
    public float maxY = -80; // 카메라의 최소 높이
    public float minY = 80; // 카메라의 최대 높이
    public float YSpeed = 3.0f; // 카메라 줌인 속도
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
