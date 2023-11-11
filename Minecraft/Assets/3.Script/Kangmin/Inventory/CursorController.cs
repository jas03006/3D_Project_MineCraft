using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    [Header("transform")]
    [SerializeField] private RectTransform _transform;
    [SerializeField] private RectTransform info_transform;

    [Header("Info")]
    [SerializeField] private ID2Datalist_YG id2data;
    public Image info_image;
    public Text info_text;
    public Text info_sub;

    //[SerializeField] private Image image;
    private Vector2 mousePos;

    void Start()
    {
        _transform = GetComponent<RectTransform>();
        Init_Cursor();
    }

    void Update()
    {
        Update_MousePosition();
    }
    private void Init_Cursor()
    {
        if (transform.GetComponent<Graphic>())
            transform.GetComponent<Graphic>().raycastTarget = false;
    }

    private void Update_MousePosition()
    {
        mousePos = Input.mousePosition;
        transform.position = mousePos;
        info_transform.position = mousePos;
    }

    public void Text_Update(Item_ID_TG item_id)
    {
        if (item_id == Item_ID_TG.None)
        {
            Reset_info();
        }
        else
        {
            //none 아닐경우 이미지, info_text 활성화
            //이미지
            info_image.enabled = true;
            Color tem_color = info_image.color;
            tem_color.a = 0.75f;
            info_image.color = tem_color;

            //info_text
            info_text.enabled = true;
            info_text.text = $"{id2data.Get_data(item_id).ItemName}\n <Color=#0069FF>{id2data.Get_data(item_id).Classname}</Color>";

            //inventorydata가 wear,useful일 경우 info_sub 활성화
            //info_sub
            InventoryData tmp = id2data.Get_data(item_id);
            if (tmp is Wear)
            {
                Wear wear = tmp as Wear;
                info_sub.enabled = true;
                info_sub.text = wear.sub_info;
            }
            else if (tmp is Useful)
            {
                Useful useful = tmp as Useful;
                info_sub.enabled = true;
                info_sub.text = useful.sub_info;
            }
            else
            {
                info_sub.text = " ";
                info_sub.enabled = false;
            }
        }
    }
    public void Reset_info()
    {
        //모두 비활성화
        info_image.enabled = false;
        info_text.text = " ";
        info_sub.text = " ";
        info_text.enabled = false;
        info_sub.enabled = false;
    }
}
