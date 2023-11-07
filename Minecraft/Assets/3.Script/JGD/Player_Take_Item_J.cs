using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Take_Item_J : MonoBehaviour
{
    // 테스트용 플레이어 아이템 먹는 스크립트 
    //나중에 합치거나 버리면 될듯?????

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
