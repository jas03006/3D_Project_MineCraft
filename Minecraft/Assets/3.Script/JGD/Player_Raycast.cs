using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Raycast : MonoBehaviour
{
    Block_Test blockTest;

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Player_ray();
    }

    private void Player_ray()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
               // Debug.Log(hit.transform.name);
                checkTarget(hit);
            }
        }

    }

    private void checkTarget(RaycastHit hit)
    {

        switch (hit.transform.tag)
        {
            case "TestBlock":
                blockTest = hit.collider.gameObject.GetComponent<Block_Test>();
                blockTest.blockHp -= 10f *Time.deltaTime;
                blockTest.Destroy_Block();
                break;
            default:
                break;
        }
    }


}
