using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Y : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    public Item_ID_TG item_id; //아이템 id
    public int number
    {
        get
        {
            return number_private;
        }

        set
        {
            number_private = value;
            text.text = number_private.ToString();
        }
    }
    private int number_private;
    public ID2Datalist_YG id2data; //id -> 데이터 파일
    public Text text;
    [SerializeField] private Image image;
    [SerializeField] private bool havedata;
    [SerializeField] private bool is_cursor_slot;
    [SerializeField] private bool have_UIClone;
    [SerializeField] private bool is_result_slot;
    [SerializeField] private Slot_Y cursor_slot;
    [SerializeField] private UISlot_Y uISlot;
    private Button button;

    [SerializeField] private Image info_image;
    [SerializeField] private Text info_text;
    [SerializeField] private bool is_craft_slot = false;
    [SerializeField] private bool is_equipment;
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
            if (!havedata) //데이터 없음
            {
                if (is_result_slot)
                {
                    return;
                }
                if (cursor_slot.item_id != Item_ID_TG.None)
                {
                    GetItem(cursor_slot.item_id, cursor_slot.number);
                    cursor_slot.ResetItem();
                }
            }
            else //데이터 있음
            {
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
        number = _num;
        havedata = true;
        text.enabled = true;
        image.sprite = id2data.Get_data(itemID).Itemsprite;
        image.enabled = true;

        Color tem_color = image.color;
        tem_color.a = 255f;
        image.color = tem_color;

        if (have_UIClone)
        {
            uISlot.GetItem(item_id, number, image.color);
        }
    }

    public virtual void ResetItem()
    {
        item_id = Item_ID_TG.None;
        number = 0;
        text.enabled = false;
        havedata = false;
        image.sprite = id2data.Get_data(item_id).Itemsprite;
        image.enabled = true;
        Color tem_color = image.color;
        tem_color.a = 0f;
        image.color = tem_color;

        if (have_UIClone)
        {
            uISlot.GetItem(item_id, number, image.color);
        }
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (is_cursor_slot)
        {
            return;
        }

        //Info_text
        if (item_id == Item_ID_TG.None)
        {
            hide_info();
        }

        else
        {
            info_text.text = $"{id2data.Get_data(item_id).ItemName}\n <Color=#0069FF>{id2data.Get_data(item_id).Classname}</Color>";
            info_text.enabled = true;
            info_image.enabled = true;
            Color tem_color = info_image.color;
            tem_color.a = 0.75f;
            info_image.color = tem_color;
        }
    }

    public void hide_info() {
        info_text.text = " ";
        info_text.enabled = false;
        info_image.enabled = false;
    }

    //mouse left click -> device
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (is_result_slot)
        {
            return;
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (cursor_slot.item_id == Item_ID_TG.None)
            {
                cursor_slot.GetItem(item_id, 1);
                number--;
            }
            else if (cursor_slot.item_id == item_id)
            {
                int tmp = number;
                cursor_slot.number += number;
                number -= tmp;
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
