using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Y : MonoBehaviour
{

    [Header("Slot")]
    [SerializeField] Slot_Y slot_1;
    [SerializeField] Slot_Y slot_2;
    [SerializeField] Slot_Y result_slot;

    [Header("Recipe")]
    [SerializeField] public Button_Y button;
    [SerializeField] Slot_Y cursor_slot;



    //public void click_button()
    //{
    //    //인벤토리에 item 들고있는지 확인
    //    List<Slot_Y> inven = Inventory.instance.playerItemList;

    //    for (int i = 0; i < inven.Count; i++)
    //    {
    //        if (inven[i].item_id == item_id_1 && inven[i].number >= num_1)
    //        {
    //            for (int j = 0; j < inven.Count; j++)
    //            {
    //                if (inven[i].item_id == item_id_2 && inven[i].number >= num_2)
    //                {
    //                    //인벤에서 꺼내서 거래 슬롯으로 옮기기
    //                    slot_1.GetItem(inven[i].item_id, inven[i].number);
    //                    inven[i].ResetItem();

    //                    slot_2.GetItem(inven[j].item_id, inven[j].number);
    //                    inven[i].ResetItem();

    //                    //result 슬롯에 결과물 띄우기
    //                    result_slot.GetItem(result_item, num_result);
    //                }
    //            }
    //        }
    //    }
    //    return;
    //}
}
