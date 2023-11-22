using System.Collections.Generic;
using UnityEngine;


public class NPC_UI_Y : MonoBehaviour
{
    [Header("Slot")]
    [SerializeField] Slot_Y slot_1;
    [SerializeField] Slot_Y slot_2;
    [SerializeField] Slot_Y result_slot;

    [Header("Recipe")]
    [SerializeField] public Button_Y selected_button;
    [SerializeField] public Button_Y[] buttons;
    [SerializeField] public recipe2NPC_Y recipe2NPC_Y;
    [SerializeField] Slot_Y cursor_slot;
    [SerializeField] int[] recipe_num;


    private void OnEnable()
    {
        send_data();
    }
    public void click_button()
    {
        //�κ��丮�� item ����ִ��� Ȯ��
        List<Slot_Y> inven = Inventory.instance.playerItemList;

        //���� �Ҵ�
        Item_ID_TG item_id_1 = selected_button.item_id_list[0];
        Item_ID_TG item_id_2 = selected_button.item_id_list[1];
        Item_ID_TG item_result = selected_button.item_id_list[2];

        int num_1 = selected_button.num_list[0];
        int num_2 = selected_button.num_list[1];
        int num_result = selected_button.num_list[2];

        for (int i = 0; i < inven.Count; i++)
        {
            if (inven[i].item_id == item_id_1 && inven[i].number >= num_1)
            {
                for (int j = 0; j < inven.Count; j++)
                {
                    if (inven[j].item_id == item_id_2 && inven[j].number >= num_2)
                    {
                        //�κ����� ������ �ŷ� �������� �ű��
                        slot_1.GetItem(inven[i].item_id, slot_1.number + 1);
                        inven[i].number--;

                        slot_2.GetItem(inven[j].item_id, slot_2.number + 1);
                        inven[j].number--;

                        //���� ������ ������ ���� ����
                        if (inven[i].number == 0 && inven[j].number == 0)
                        {
                            slot_1.ResetItem();
                            slot_2.ResetItem();
                            result_slot.ResetItem();
                        }
                        //result ���Կ� ����� ����
                        result_slot.GetItem(item_result,num_result);
                    }
                }
            }
        }
    }

    public void send_data()
    {
        //Debug.Log("send_data");
        for (int i = 0; i < buttons.Length; i++)
        {
            //��ư�� ������ ������
            buttons[i].get_recipe(recipe2NPC_Y.get_recipe(recipe_num[i]));
            buttons[i].update_Button();
        }
    }

    public void set_index(int[] index)
    {
        recipe_num = index;
    }
    

}