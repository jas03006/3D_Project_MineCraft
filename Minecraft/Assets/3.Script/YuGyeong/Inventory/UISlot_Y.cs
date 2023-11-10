using UnityEngine;
using UnityEngine.UI;

public class UISlot_Y : MonoBehaviour
{
    public Item_ID_TG _item_id; //������ id
    public int _value; //����
    public ID2Datalist_YG id2data; //id -> ������ ����
    public Image _image;
    public Image highlight;
    public Text text;
    public bool is_active;

    void Awake()
    {
        _image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }

    public void GetItem(Item_ID_TG item_id, int value, Color tem_color)
    {
        _item_id = item_id;
        _value = value;
        text.text = $"{_value}";
        InventoryData idt = id2data.Get_data(_item_id);
        _image.sprite = idt.Itemsprite;
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

    public void Test()
    {
        if (Input.GetMouseButtonDown(1)) //���콺 ������
        {
            InventoryData tmp = id2data.Get_data(_item_id);
            if (tmp is Food)
            {
                Food food = tmp as Food;
                if (food != null)
                {
                    food.R_Eat();
                }
            }
            //else if(tmp is )
        }

        if (Input.GetMouseButtonDown(0)) //���콺 ����
        {
            InventoryData tmp = id2data.Get_data(_item_id);
            if (tmp is Useful)
            {
                Useful useful = tmp as Useful;
                if (useful != null)
                {
                    useful.L_Swing();
                }
            }
        }
    }
}
