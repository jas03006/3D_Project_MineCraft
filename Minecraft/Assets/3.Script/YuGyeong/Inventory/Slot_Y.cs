using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Y : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Item_ID_TG item_id; 
    public int number
    {
        get
        {
            if (number_private <= 0 && item_id != Item_ID_TG.None)
            {
                ResetItem();
                number_private = 0;
            }
            return number_private;
        }
        set
        {
            number_private = value;
            if (number_private <= 0)
            {
                if (item_id == Item_ID_TG.None)
                {
                    number_private = 0;
                    text.enabled = false;
                    text.text = " ";
                }

                else
                {
                    ResetItem();
                    Debug.Log($"{gameObject.name}");
                }
            }
            else
            {
                text.enabled = true;
                text.text = number_private.ToString();
            }
        }
    }
    private int number_private;
    public ID2Datalist_YG id2data; 
    public Text text;
    [SerializeField] private Image image;
    [SerializeField] private bool havedata;
    [SerializeField] private bool is_cursor_slot;
    [SerializeField] private bool have_UIClone;
    [SerializeField] private bool is_result_slot;
    [SerializeField] private bool is_furnace_result_slot;
    [SerializeField] private Slot_Y cursor_slot;
    [SerializeField] private UISlot_Y uISlot;
    private Button button;

    [SerializeField] private bool is_craft_slot = false;
    [SerializeField] private bool is_equipment;
    [SerializeField] private armor_type armor_Type;
    public virtual void Start()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();

        ResetItem();
        if (!is_cursor_slot)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => PushSlot());
        }
    }
    public virtual void PushSlot()
    {
        if (!is_cursor_slot)
        {
            if (!havedata)
            {
                if (is_result_slot)
                {
                    return;
                }

                if (is_equipment)
                {
                    InventoryData tmp = cursor_slot.id2data.Get_data(cursor_slot.item_id);
                    if (tmp is Wear)
                    {
                        Wear wear = tmp as Wear;
                        if (wear.type == armor_Type)
                        {
                            Debug.Log($"{wear.type}/{armor_Type}");
                            GetItem(cursor_slot.item_id, cursor_slot.number);
                            cursor_slot.ResetItem();
                            Make_Player_princess.instance.update_equipment(wear.type, item_id);
                            wear.defense_up();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                if (cursor_slot.item_id != Item_ID_TG.None)
                {
                    GetItem(cursor_slot.item_id, cursor_slot.number);
                    cursor_slot.ResetItem();
                }
            }
            else //havedata = true;
            {
                if (is_result_slot)
                {
                    if (item_id != Item_ID_TG.None && cursor_slot.item_id != Item_ID_TG.None)
                    {
                        return;
                    }
                }
                if (is_equipment && cursor_slot.item_id == Item_ID_TG.None)
                {
                    InventoryData tmp = cursor_slot.id2data.Get_data(item_id);
                    if (tmp is Wear)
                    {
                        Wear wear = tmp as Wear;
                        if (wear.type == armor_Type)
                        {
                            wear.defense_down();
                            Make_Player_princess.instance.update_equipment(wear.type, Item_ID_TG.None);
                            cursor_slot.GetItem(item_id, number);
                            ResetItem();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                if (is_furnace_result_slot)
                {
                    Exp_pooling.instance.generate_exp(1*number, Furnace_Y.furnace_tg.transform.position);
                }

                if (cursor_slot.item_id == Item_ID_TG.None)
                {
                    cursor_slot.GetItem(item_id, number);
                    ResetItem();
                }
                else if (cursor_slot.item_id == item_id)
                {
                    GetItem(item_id, number + cursor_slot.number);
                    cursor_slot.ResetItem();
                }

                if (is_result_slot)
                {
                    Inventory.instance.use_recipe(this);
                    Inventory.instance.check_recipe(this);                    
                }
            }
            if (is_craft_slot)
            {
                Inventory.instance.check_recipe(this);
            }
        }
    }

    public virtual void GetItem(Item_ID_TG itemID, int _num)
    {
        if (itemID == Item_ID_TG.None || itemID == Item_ID_TG.Fill)
        {
            return;
        }

        item_id = itemID;

        if (number >= id2data.Get_data(item_id).MaxValue)
        {
            return;
        }
        number = _num;
        havedata = true;
        image.sprite = id2data.Get_data(itemID).Itemsprite;
        image.enabled = true;

        Color tem_color = image.color;
        tem_color.a = 255f;
        image.color = tem_color;

        if (have_UIClone)
        {
            uISlot.GetItem(item_id, number, image.color);
            if (uISlot.is_active) {
                Inventory.instance.weapon_position.Equip_Weapon(item_id);
            }
        }
    }

    public virtual void ResetItem()
    {
        item_id = Item_ID_TG.None;
        number = 0;
        havedata = false;
        image.sprite = id2data.Get_data(item_id).Itemsprite;
        image.enabled = true;

        Color tem_color = image.color;
        tem_color.a = 0f;
        image.color = tem_color;

        if (have_UIClone)
        {
            uISlot.GetItem(item_id, number, image.color);
            if (uISlot.is_active)
            {
                Inventory.instance.weapon_position.Equip_Weapon(item_id);
            }
        }
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (is_cursor_slot)
        {
            return;
        }

        //Info_text
        Inventory.instance.cursorController.Text_Update(item_id);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.instance.cursorController.Reset_info();
    }

    //mouse left click -> device
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (is_result_slot)
        {
            return;
        }
        //오른쪽 키 눌렀을때
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (cursor_slot.item_id == Item_ID_TG.None)
            {
                if (number == 1)
                {
                    cursor_slot.GetItem(item_id, 1);
                    number--;
                }
                else
                {
                    int tmp = number / 2;
                    cursor_slot.GetItem(item_id, tmp);
                    number -= tmp;
                }

            }
            else if (cursor_slot.item_id == item_id)
            {
                cursor_slot.number--;
                number++;

                if (number == 0)
                {
                    ResetItem();
                }
            }
            else if (item_id == Item_ID_TG.None)
            {
                GetItem(cursor_slot.item_id, 1);
                cursor_slot.number -= 1;
                if (cursor_slot.number == 0)
                {
                    cursor_slot.ResetItem();
                }
            }
            if (is_craft_slot)
            {
                Inventory.instance.check_recipe(this);
            }


        }

        if (number == 0)
        {
            ResetItem();
        }
    }
}
