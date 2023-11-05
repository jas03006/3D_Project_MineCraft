using UnityEngine;

public class Cam_3_Y : MonoBehaviour
{
    [Header("공용")]
    private float mouseX;
    private float mouseY;
    private float cameraVer;
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

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        Cam3();
    }

    private void Cam3()
    {
        // 마우스 입력을 받아 카메라 회전 각도를 조절
        currentX += mouseX * XSpeed;
        currentY -= mouseY * YSpeed;

        // 카메라의 위치 계산 (플레이어 주위로 회전하면서 일정 거리를 유지)
        Vector3 rot_offset = new Vector3(0, distance, -distance);
        Vector3 pos_offset = new Vector3(0, currentY * zoom_distance, 0);
        //currentY = Mathf.Clamp(currentY, minY, maxY);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = cam_pos.position + rotation * rot_offset + pos_offset;

        // 카메라가 플레이어를 바라보게 설정
        transform.LookAt(cam_pos.position);
    }
}
