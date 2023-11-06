using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_TG : Block_TG, Interactive_TG
{
    private bool is_closed = true;
    private Transform rotate_point_tr;
    private void OnEnable()
    {
        is_closed = true;
        transform.rotation = Quaternion.identity;
        rotate_point_tr = transform.GetChild(0);
    }
    public void react()
    {
        //Debug.Log("react");

        transform.RotateAround(rotate_point_tr.position, rotate_point_tr.up, 90f * (is_closed? -1:1));
        is_closed = !is_closed;

    }
}
