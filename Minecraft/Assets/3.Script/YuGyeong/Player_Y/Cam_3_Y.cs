using UnityEngine;

public class Cam_3_Y : MonoBehaviour
{
    [Header("����")]
    private float mouseX;
    private float mouseY;
    private float cameraVer;
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

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        Cam3();
    }

    private void Cam3()
    {
        // ���콺 �Է��� �޾� ī�޶� ȸ�� ������ ����
        currentX += mouseX * XSpeed;
        currentY -= mouseY * YSpeed;

        // ī�޶��� ��ġ ��� (�÷��̾� ������ ȸ���ϸ鼭 ���� �Ÿ��� ����)
        Vector3 rot_offset = new Vector3(0, distance, -distance);
        Vector3 pos_offset = new Vector3(0, currentY * zoom_distance, 0);
        //currentY = Mathf.Clamp(currentY, minY, maxY);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = cam_pos.position + rotation * rot_offset + pos_offset;

        // ī�޶� �÷��̾ �ٶ󺸰� ����
        transform.LookAt(cam_pos.position);
    }
}
