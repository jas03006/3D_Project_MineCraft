using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Take_Item_J : MonoBehaviour
{
    // �׽�Ʈ�� �÷��̾� ������ �Դ� ��ũ��Ʈ 
    //���߿� ��ġ�ų� ������ �ɵ�?????

    Break_Block_Item break_item;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stepable_Block")
        {
            break_item = FindObjectOfType<Break_Block_Item>();

            break_item.gameObject.SetActive(false);        

        }
    }
}
