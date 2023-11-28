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
    [SerializeField] bool is_hide;

    private void OnEnable()
    {
        send_data();
    }
    public void click_button()
    {
        //인벤토리에 item 들고있는지 확인
        List<Slot_Y> inven = Inventory.instance.playerItemList;

        //변수 할당
        Item_ID_TG item_id_1 = selected_button.item_id_list[0];
        Item_ID_TG item_id_2 = selected_button.item_id_list[1];
        Item_ID_TG item_result = selected_button.item_id_list[2];

        int num_1 = selected_button.num_list[0];
        int num_2 = selected_button.num_list[1];
        int num_result = selected_button.num_list[2];

        if (result_slot.item_id != Item_ID_TG.None)
        {
            return;
        }

        for (int i = 0; i < inven.Count; i++)
        {
            if (inven[i].item_id == item_id_1 && inven[i].number >= num_1)
            {
                for (int j = 0; j < inven.Count; j++)
                {
                    if (inven[j].item_id == item_id_2 && inven[j].number >= num_2)
                    {
                        //인벤에서 꺼내서 거래 슬롯으로 옮기기
                        slot_1.GetItem(inven[i].item_id, num_1);
                        inven[i].number -= num_1;

                        slot_2.GetItem(inven[j].item_id, num_2);
                        inven[j].number -= num_2;

                        //result 슬롯에 결과물 띄우기
                        result_slot.GetItem(item_result, num_result);

                        if ((inven[i].number < num_1 || inven[j].number < num_2))
                        {
                            is_hide = true;
                            Debug.Log($"is_hide {is_hide}");
                        }
                        else
                        {
                            is_hide = false;
                            Debug.Log($"is_hide {is_hide}");
                        }
                        break;
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
            //버튼에 레시피 보내기
            buttons[i].get_recipe(recipe2NPC_Y.get_recipe(recipe_num[i]));
            buttons[i].update_Button();
        }
    }

    public void set_index(int[] index)
    {
        recipe_num = index;
    }

    public void reset_data()
    {
        if (is_hide)
        {
            slot_1.ResetItem();
            slot_2.ResetItem();
        }
    }

}
