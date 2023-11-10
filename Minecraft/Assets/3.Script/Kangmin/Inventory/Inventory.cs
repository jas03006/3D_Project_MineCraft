using System.Collections.Generic;
using System.Collections;
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
    public static Inventory instance = null;
    [SerializeField] private List<Slot_Y> playerItemList = new List<Slot_Y>(36);
    [SerializeField] private List<UISlot_Y> UIItemList = new List<UISlot_Y>(9);
    [SerializeField] private List<Slot_Y> CraftList = new List<Slot_Y>(10); //짱규동 데이터
    [SerializeField] private List<Slot_Y> CraftList_Small = new List<Slot_Y>(5); //짱규동 데이터
    [SerializeField] private Weapon_position_J weapon_position;
    private int UIslot_index = 0;

    public Image inventoryImage;
    public List<Sprite> spriteImage;
    [SerializeField] private Transform[] children;
    public bool isInventoryOpen = false;


    [SerializeField] public GameObject craft_UI;
    [SerializeField] public CreatSystem creat_system;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
            return;
        }
        Cursor.visible = false;
    }

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
        StartCoroutine(start_co());
        /*for (int i = 1; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
        }*/
    }

    public IEnumerator start_co() {
        yield return null;
        for (int i = 1; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            GetItem(Item_ID_TG.bed, 1);
        }
    }
    private void InventoryInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInventoryOpen == false)
            {
                show();
            }
            else if (isInventoryOpen == true)
            {
                hide();
            }
        }
    }

    public void show() {
        for (int i = 1; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(true);
        }
        isInventoryOpen = true;
        Cursor.visible = true;
    }

    public void show_craft() {
        show();
        craft_UI.SetActive(true);
    }

    public void hide() {
        if (craft_UI.activeSelf == true) {
            hide_craft();
        }
        
        for (int i = 1; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
        }
        isInventoryOpen = false;
        Cursor.visible = false;        
    }
    public void hide_craft()
    {
        for (int i =0; i < CraftList.Count-1; i++) {
            Slot_Y slot_ = CraftList[i];
            GetItem(slot_.item_id, slot_.number);
            slot_.ResetItem();
        }
        CraftList[CraftList.Count - 1].ResetItem();
        craft_UI.SetActive(false);
    }
    public void GetItem(Item_ID_TG id, int num)
    {
        //같은거 있으면 갯수++
        for (int i = 0; i < playerItemList.Count; i++)
        {
            if (playerItemList[i].item_id == id)
            {
                playerItemList[i].number++;
                if (i >= playerItemList.Count- UIItemList.Count) {
                    UIItemList[i - playerItemList.Count + UIItemList.Count].text.text = $"{playerItemList[i].number}";
                }                
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
            weapon_position.Equip_Weapon(UIItemList[UIslot_index]._item_id);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (UIslot_index <= 0)
            {
                return;
            }
            UIslot_index--;
            weapon_position.Equip_Weapon(UIItemList[UIslot_index]._item_id);
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

    public void check_recipe(Slot_Y slot_) {
        List<Slot_Y> slot_list_;
        if (CraftList.Contains(slot_))
        {
            slot_list_ = CraftList;
        }
        else if (CraftList_Small.Contains(slot_))
        {
            slot_list_ = CraftList_Small;
        }
        else {
            return;
        }
        KeyValuePair<Item_ID_TG, int> result_ = creat_system.get_result(slot_list_);
        slot_list_[slot_list_.Count - 1].ResetItem();
        if (result_.Key != Item_ID_TG.None)
        {
            slot_list_[slot_list_.Count - 1].GetItem(result_.Key, result_.Value);
        }
        Debug.Log($"Craft: {result_.Key} / {result_.Value} ");
    }

    public void use_recipe(Slot_Y slot_) {
        List<Slot_Y> slot_list_;
        if (CraftList.Contains(slot_))
        {
            slot_list_ = CraftList;
        }
        else if (CraftList_Small.Contains(slot_))
        {
            slot_list_ = CraftList_Small;
        }
        else
        {
            return;
        }
        for (int i =0; i < slot_list_.Count-1; i++) {
            if (slot_list_[i].number > 0) {
                slot_list_[i].number--;
                if (slot_list_[i].number == 0)
                {
                    slot_list_[i].ResetItem();
                }
            }                
        }
        
    }
}