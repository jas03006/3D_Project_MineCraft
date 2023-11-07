using UnityEngine;
using UnityEngine.UI;

public class UISlot_Y : MonoBehaviour
{
    public Item_ID_TG _item_id; //아이템 id
    public int _value; //갯수
    public ID2Datalist_YG id2data; //id -> 데이터 파일
    public Image _image;
    public Image highlight;
    public bool is_active;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (is_active)
        {
            Active();
        }
        else
        {
            NotActive();
        }
    }

    public void GetItem(Item_ID_TG item_id, int value, Color tem_color)
    {
        _item_id = item_id;
        _value = value;
        InventoryData idt = id2data.Get_data(_item_id);
        _image.sprite = idt.itemSprite;
        _image.color = tem_color;
    }

    public void Active()
    {
        highlight.enabled = true;
        if (_item_id == Item_ID_TG.None)
        {
            return;
        }
        Debug.Log($"{_item_id}");
    }

    public void NotActive()
    {
        highlight.enabled = false;
    }
}
