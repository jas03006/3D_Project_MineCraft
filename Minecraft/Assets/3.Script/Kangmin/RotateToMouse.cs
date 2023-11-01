using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField] private float rotCamXAxisSpeed = 5f; // x�� ȸ�� �ӵ�
    [SerializeField] private float rotCamYAxisSpeed = 5f; // y�� ȸ�� �ӵ�

    private float limitMinX = -80; // ī�޶� x�� ȸ�� ����(�ּ�)
    private float limitMaxX = 50; // ī�޶� x�� ȸ�� ����(�ִ�)

    private float eulerAngleX; // ���콺 ��/�� �̵����� ī�޶� y�� ȸ��
    private float eulerAngleY; // ���콺 ��/�� �̵����� ī�޶� x�� ȸ��

    public void CalculateRotation(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotCamXAxisSpeed;
        eulerAngleX -= mouseY * rotCamYAxisSpeed;
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        //if( angle < 360)
        //{
        //    angle += 360;
        //}

        //if( angle > 360)
        //{
        //    angle -= 360;
        //}

        return Mathf.Clamp(angle, min, max);
    }

}
