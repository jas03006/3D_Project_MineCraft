using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Y : MonoBehaviour
{
    [SerializeField] Item_ID_TG item_id_1;
    [SerializeField] Item_ID_TG item_id_2;
    [SerializeField] Item_ID_TG result_item;
    [SerializeField] int num_1;
    [SerializeField] int num_2;
    [SerializeField] int num_result;
    [SerializeField] ID2Datalist_YG id2data;
    [SerializeField] Image image_1;
    [SerializeField] Image image_2;

    [SerializeField] NPC_Y npc;

    private void OnEnable()
    {
        image_1.sprite = id2data.Get_data(item_id_1).Itemsprite;
        image_2.sprite = id2data.Get_data(item_id_2).Itemsprite;
        image_2.sprite = id2data.Get_data(result_item).Itemsprite;
    }

    public void click_button()
    {
        npc.button = this;
    }
}
