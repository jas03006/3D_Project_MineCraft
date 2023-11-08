using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
아이템 얻음
(오브젝트 생성(데이터), 아이템 들어갈 자리찾기, 아이템 띄우기)

네번째줄, 다섯번째 줄 동기화    
다섯번째 줄 선택 시 id 뱉어내기
 */

public class Inventory : MonoBehaviour
{


    [SerializeField] private List<Slot_Y> playerItemList = new List<Slot_Y>(36);
    [SerializeField] private List<UISlot_Y> UIItemList = new List<UISlot_Y>(9);
    [SerializeField] private List<Slot_Y> CraftList = new List<Slot_Y>(10);
    private int UIslot_index = 0;
    private bool is_scrolled = false;

    public Image inventoryImage;
    public List<Sprite> spriteImage;
    [SerializeField] private Transform[] children;
    private bool isInventoryOpen = false;

    /*
     -----------------------------일단 지금은 안쓰는거------------------------------------
     */
    Vector2 mousePos;
    [SerializeField] private Button[] button;
    [SerializeField] private Button clicked_button;
    [SerializeField] private int clicked_button_index;
    private GameObject selectedItem; // ���콺�� ������ �������� ��Ÿ���� UI ���

    private bool isMouseOver;

    private int currentIndex; // Ŭ�������� ����Ǵ� �ε��� ����
    private int maxStackCount = 64;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
        button = FindObjectsOfType<Button>();

        for (int i = 1; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < button.Length; i++)
        {
            int index = i;
        }
    }
    private void Update()
    {
        InventoryInteraction();
        UIslot_select();

        Test(); //디버그용

    }
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            GetItem(Item_ID_TG.apple, 1);
        }
    }
    private void InventoryInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInventoryOpen == false)
            {
                for (int i = 1; i < children.Length; i++)
                {
                    children[i].gameObject.SetActive(true);
                }
                isInventoryOpen = true;
                Cursor.visible = true;
            }
            else if (isInventoryOpen == true)
            {
                for (int i = 1; i < children.Length; i++)
                {
                    children[i].gameObject.SetActive(false);
                }
                isInventoryOpen = false;
                Cursor.visible = false;
            }
        }
    }

    public void GetItem(Item_ID_TG id, int num)
    {
        //같은거 있으면 갯수++
        for (int i = 0; i < playerItemList.Count; i++)
        {
            if (playerItemList[i].item_id == id)
            {
                playerItemList[i].value++;
                playerItemList[i].text.text = $"{playerItemList[i].value}";
                return;
            }
        }

        //같은거 없으면 새로 생성
        for (int i = 0; i < playerItemList.Count; i++)
        {
            if (playerItemList[i].item_id == Item_ID_TG.None)
            {
                playerItemList[i].GetItem(id, num);
                break;
            }
        }
    }

    private void UIslot_select()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (UIslot_index >= 8)
            {
                return;
            }
            UIslot_index++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (UIslot_index <= 0)
            {
                return;
            }
            UIslot_index--;
        }

        for (int i = 0; i < UIItemList.Count; i++)
        {
            if (i == UIslot_index)
            {
                UIItemList[i].is_active = true;
                UIItemList[i].Active();
            }
            else
            {
                UIItemList[i].is_active = false;
                UIItemList[i].NotActive();
            }
        }
    }
}