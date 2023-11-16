using UnityEngine;
using UnityEngine.UI;

public class UISlot_Y : MonoBehaviour
{
    public Item_ID_TG _item_id; //아이템 id
    public int _value
    {
        get
        {
            return _value_private;
        }
        set
        {
            _value_private = value;
            if (_value < 0)
            {
                _value = 0;
            }
            if (_value <= 0 && _item_id != Item_ID_TG.None)
            {
                clone_slot.ResetItem();
                _value = 0;
            }
            else if (_item_id != Item_ID_TG.None)
            {
                if (clone_slot.item_id == _item_id && clone_slot.number == _value)
                {
                    return;
                }
                clone_slot.GetItem(_item_id, _value_private);
            }
        }
    }
    private int _value_private;
    public ID2Datalist_YG id2data; //id -> 데이터 파일
    public Image _image;
    public Image highlight;
    public Text text;
    public Slot_Y clone_slot;
    public bool is_active;

    void Awake()
    {
        _image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }
   /* private void Update()
    {
        Test();
    }*/

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
    }

    public void NotActive()
    {
        highlight.enabled = false;
    }

    public void value_minus()
    {
        if (is_active)
        {
            _value--;
        }
    }

    public void Test()
    {
        if (Input.GetMouseButtonDown(1)) //마우스 오른쪽
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
        }

        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽
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

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Input.GetKeyDown(KeyCode.B)");
            value_minus();
            Debug.Log($"{_value}");
        }
    }
}
