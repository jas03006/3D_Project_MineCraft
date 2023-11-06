using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Raycast : MonoBehaviour
{
    Block_Break blockTest;

    private void Start()
    {
        blockTest = GetComponent<Block_Break>();

    }

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
        else if (Input.GetMouseButtonUp(0))
        {
            blockTest.blockHp = blockTest.Max_Block_Hp;
        }

    }

    private void checkTarget(RaycastHit hit)
    {
        switch (hit.transform.tag)
        {
            case "TestBlock":
                blockTest = hit.collider.gameObject.GetComponent<Block_Break>();
                blockTest.blockHp -= 10f *Time.deltaTime;     //10f 나중에 무기얻으면 더 올라가게 변수로 바꾸기
                blockTest.Destroy_Block();
                break;
            default:
                break;
        }
    }

}
