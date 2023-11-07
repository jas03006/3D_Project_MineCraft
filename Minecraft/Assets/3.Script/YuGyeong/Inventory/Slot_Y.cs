using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Y : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public Item_ID_TG item_id; //아이템 id
    public int value; //갯수
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

    [SerializeField] private RectTransform info_transform;
    [SerializeField] private Image info;
    [SerializeField] private Text info_text;
    private Vector2 mousePos;

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
        //Debug.Log("PushSlot");
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
        //Debug.Log("GetItem");
        item_id = itemID;
        value = num;
        havedata = true;
        text.text = $"{value}";
        text.enabled = true;
        image.sprite = id2data.Get_data(itemID).itemSprite;
        image.enabled = true;

        Color tem_color = image.color;
        tem_color.a = 255f;
        image.color = tem_color;

        if (have_UIClone)
        {
            uISlot.GetItem(item_id, value, image.color);
        }
    }

    public void ResetItem()
    {
        //Debug.Log("ResetItem");
        item_id = Item_ID_TG.None;
        value = 0;
        text.enabled = false;
        havedata = false;
        image.sprite = id2data.Get_data(item_id).itemSprite;
        image.enabled = true;
        Color tem_color = image.color;
        tem_color.a = 0f;
        image.color = tem_color;

        if (have_UIClone)
        {
            uISlot.GetItem(item_id, value, image.color);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        GameObject enteredObject = eventData.pointerEnter;
        Slot_Y slot_data = enteredObject.GetComponent<Slot_Y>();
        if (slot_data.item_id == Item_ID_TG.None)
        {
            return;
        }
        else
        {
            info_transform.position = mousePos + (Vector2.right * 100f);
            info_text.text = $"{slot_data.id2data.Get_data(slot_data.item_id).ItemName}\n <Color=blue>{slot_data.id2data.Get_data(slot_data.item_id).classname}</Color>";
            info.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        info.enabled = false;
    }
}
