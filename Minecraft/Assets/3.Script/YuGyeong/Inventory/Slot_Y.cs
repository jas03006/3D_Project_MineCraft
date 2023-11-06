using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Y : MonoBehaviour
{
    // Start is called before the first frame update
    public Item_ID_TG item_id; //아이템 id
    public int value; //갯수
    public ID2Datalist_YG id2data; //id -> 데이터 파일
    private Image image;
    [SerializeField]private bool havedata;
    [SerializeField]private bool is_cursor_slot;
    [SerializeField] private Slot_Y cursor_slot;
    private Button button;

    private void Start()
    {
        image = GetComponentInChildren<Image>();
        ResetItem();
        if (!is_cursor_slot)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => PushSlot());
        }
    }

    public void PushSlot()
    {
        if (!is_cursor_slot)
        {
            if (!havedata) //데이터 없음
            {
                if (cursor_slot.item_id != Item_ID_TG.None)
                {
                    GetItem(cursor_slot.item_id, cursor_slot.value);
                    cursor_slot.ResetItem();
                }
            }
            else //데이터 있음
            {
                cursor_slot.GetItem(item_id, value);
                ResetItem();
            }
        }
    }

    public void GetItem(Item_ID_TG itemID, int num)
    {
        item_id = itemID;
        value = num;
        havedata = true;
        image.enabled = true;
        image.sprite = id2data.get_data(itemID).itemSprite;
        Color tem_color = image.color;
        tem_color.a = 255f;
        image.color = tem_color;
    }

    public void ResetItem()
    {
        item_id = Item_ID_TG.None;
        value = 0;
        havedata = false;
        image.sprite = id2data.get_data(item_id).itemSprite;
        image.enabled = true;
        Color tem_color = image.color;
        tem_color.a = 0f;
        image.color = tem_color;
    }
}
