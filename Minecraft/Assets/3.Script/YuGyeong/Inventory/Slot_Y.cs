using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Y : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    public Item_ID_TG item_id; //아이템 id
    public int number;
    //{
    //    get
    //    {
    //        return number_private;
    //    }

    //    set
    //    {
    //        number_private = number;
    //        text.text = number_private.ToString();
    //        if (number <= 0)
    //        {
    //            ResetItem();
    //        }
    //    }
    //}
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
    //private Vector2 mousePos;

    private void Start()
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

    public void PushSlot()
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
                cursor_slot.GetItem(item_id, number);
                ResetItem();
            }
        }
    }

    public void GetItem(Item_ID_TG itemID, int _num)
    {
        item_id = itemID;
        number = _num;
        havedata = true;
        text.text = $"{number}";
        text.enabled = true;
        image.sprite = id2data.Get_data(itemID).itemSprite;
        image.enabled = true;

        Color tem_color = image.color;
        tem_color.a = 255f;
        image.color = tem_color;

        if (have_UIClone)
        {
            uISlot.GetItem(item_id, number, image.color);
        }
    }

    public void ResetItem()
    {
        item_id = Item_ID_TG.None;
        number = 0;
        text.enabled = false;
        havedata = false;
        image.sprite = id2data.Get_data(item_id).itemSprite;
        image.enabled = true;
        Color tem_color = image.color;
        tem_color.a = 0f;
        image.color = tem_color;

        if (have_UIClone)
        {
            uISlot.GetItem(item_id, number, image.color);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (is_cursor_slot)
        {
            return;
        }

        //Info_text
        if (item_id == Item_ID_TG.None)
        {
            info_text.text = " ";
            info_text.enabled = false;
            info_image.enabled = false;
        }

        else
        {
            info_text.text = $"{id2data.Get_data(item_id).ItemName}\n <Color=#0069FF>{id2data.Get_data(item_id).classname}</Color>";
            info_text.enabled = true;
            info_image.enabled = true;
            Color tem_color = info_image.color;
            tem_color.a = 0.75f;
            info_image.color = tem_color;
        }
    }

    //mouse left click -> device
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (cursor_slot.item_id == item_id)
            {
                number = cursor_slot.number + number;
            }

            if (cursor_slot.item_id == Item_ID_TG.None)
            {
                cursor_slot.GetItem(item_id, 1);
                number--;
            }
        }

        if (number == 0)
        {
            ResetItem();
        }
    }
}
