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
    //    //�κ��丮�� item ����ִ��� Ȯ��
    //    List<Slot_Y> inven = Inventory.instance.playerItemList;

    //    for (int i = 0; i < inven.Count; i++)
    //    {
    //        if (inven[i].item_id == item_id_1 && inven[i].number >= num_1)
    //        {
    //            for (int j = 0; j < inven.Count; j++)
    //            {
    //                if (inven[i].item_id == item_id_2 && inven[i].number >= num_2)
    //                {
    //                    //�κ����� ������ �ŷ� �������� �ű��
    //                    slot_1.GetItem(inven[i].item_id, inven[i].number);
    //                    inven[i].ResetItem();

    //                    slot_2.GetItem(inven[j].item_id, inven[j].number);
    //                    inven[i].ResetItem();

    //                    //result ���Կ� ����� ����
    //                    result_slot.GetItem(result_item, num_result);
    //                }
    //            }
    //        }
    //    }
    //    return;
    //}
}
