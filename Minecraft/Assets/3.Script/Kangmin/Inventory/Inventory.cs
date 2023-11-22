using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
아이템 얻음
(오브젝트 생성(데이터), 아이템 들어갈 자리찾기, 아이템 띄우기)

네번째줄, 다섯번째 줄 동기화    
다섯번째 줄 선택 시 id 뱉어내기
 */

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;
    [SerializeField] public List<Slot_Y> playerItemList = new List<Slot_Y>(36);
    [SerializeField] private List<UISlot_Y> UIItemList = new List<UISlot_Y>(9);
    [SerializeField] private List<Slot_Y> CraftList = new List<Slot_Y>(10); //짱규동 데이터
    [SerializeField] private List<Slot_Y> CraftList_Small = new List<Slot_Y>(5); //짱규동 데이터
    [SerializeField] private List<Slot_Y> CraftList_very_Small = new List<Slot_Y>(2); //짱규동 데이터
    [SerializeField] public Weapon_position_J weapon_position;
    public int UIslot_index = 0;

    public Image inventoryImage;
    public List<Sprite> spriteImage;
    [SerializeField] private Transform[] children;
    public bool isInventoryOpen = false;

    [SerializeField] public CreatSystem creat_system;
    [SerializeField] public GameObject craft_UI;
    [SerializeField] public GameObject box_UI;
    [SerializeField] public GameObject furnace_UI;
    [SerializeField] public Slot_Y cursor_slot;
    private Action<List<Slot_Y>> hide_callback = null;

    [SerializeField] public CursorController cursorController;

    [SerializeField] public GameObject inven_camera;
    [SerializeField] public GameObject NPC_UI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
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

        // 로딩 중 UI 끄기
        UIManager.instance.loading_page.SetActive(false);
        UIManager.instance.position_UI.gameObject.SetActive(true);
    }
    private void Update()
    {
        InventoryInteraction();
        UIslot_select();
        Test(); //디버그용
    }
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            GetItem(Item_ID_TG.apple, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            GetItem(Item_ID_TG.diamond, 10);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            GetItem(Item_ID_TG.tree, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GetItem(Item_ID_TG.stone, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            NPC_UI.SetActive(!NPC_UI.activeSelf);
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

    public void show(Action<List<Slot_Y>> callback = null) {
        for (int i = 1; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(true);
        }
        isInventoryOpen = true;
        Cursor.visible = true;
        hide_callback = callback;
        inven_camera.SetActive(true);
    }

    public void show_craft(Action<List<Slot_Y>> callback = null) {
        show(callback);
        craft_UI.SetActive(true);
    }
    public void show_box(List<KeyValuePair<Item_ID_TG, int>> data, Action<List<Slot_Y>> callback = null)
    {
        show(callback);
        box_UI.SetActive(true);
        box_UI.GetComponent<Box_Y>().Get_data(data);
    }
    public void show_furnace(Furnace_TG furnace_tg, List<KeyValuePair<Item_ID_TG, int>> data, List<float> time_data, Action<List<Slot_Y>> callback = null)
    {
        show(callback);
        furnace_UI.SetActive(true);
        furnace_UI.TryGetComponent<Furnace_Y>(out Furnace_Y furnace_y);
        Furnace_Y.furnace_tg = furnace_tg;
        furnace_y.Get_data(data, time_data);
        
    }

    public void show_NPC(Action<List<Slot_Y>> callback = null)
    {
        show(callback);
        NPC_UI.SetActive(true);
    }

    public void hide() {
        cursorController.Reset_info();//cursor_slot.hide_info();
        List<Slot_Y> callback_param = null;
        if (craft_UI.activeSelf == true)
        {
            hide_craft();
        }
        else if (box_UI.activeSelf == true)
        {
            callback_param = box_UI.GetComponent<Box_Y>().Get_slots();
        }
        else if (furnace_UI.activeSelf == true)
        {
            callback_param = furnace_UI.GetComponent<Furnace_Y>().Get_slots();
        }
        else if (NPC_UI.activeSelf == true)
        {
            hide_craft_very_small();
        }
        else
        {
            hide_craft_small();
        }
        
        for (int i = 1; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
        }

        isInventoryOpen = false;
        Cursor.visible = false;
        if (hide_callback != null) {
            hide_callback(callback_param);
            hide_callback = null;
        }
        if (box_UI.activeSelf == true)
        {
            hide_box();
        } else if (furnace_UI.activeSelf == true) {
            hide_furnace();
        }
        else if (NPC_UI.activeSelf == true)
        {
            hide_NPC();
        }
        inven_camera.SetActive(false);

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

    public void hide_craft_small()
    {
        for (int i = 0; i < CraftList_Small.Count - 1; i++)
        {
            Slot_Y slot_ = CraftList_Small[i];
            GetItem(slot_.item_id, slot_.number);
            slot_.ResetItem();
        }
        CraftList_Small[CraftList_Small.Count - 1].ResetItem();
    }

    public void hide_craft_very_small()
    {
        //for (int i = 0; i < CraftList_very_Small.Count - 1; i++)
        //{
        //    Slot_Y slot_ = CraftList_very_Small[i];
        //    GetItem(slot_.item_id, slot_.number);
        //    slot_.ResetItem();
        //}
        //CraftList_very_Small[CraftList_very_Small.Count - 1].ResetItem();
    }

    public void hide_box() {
        box_UI.GetComponent<Box_Y>().reset_data();
        box_UI.SetActive(false);
    }
    public void hide_furnace()
    {
        furnace_UI.TryGetComponent<Furnace_Y>(out Furnace_Y furnace_y);
        Furnace_Y.furnace_tg = null;
        furnace_y.reset_data();
        furnace_UI.SetActive(false);
    }
    public void hide_NPC()
    {
        NPC_UI.SetActive(false);
    }
    public void GetItem(Item_ID_TG id, int num)
    {
        //같은거 있으면 갯수++
        for (int i_ = 27; i_ < playerItemList.Count+ 27; i_++)
        {
            int i = i_ % playerItemList.Count;
            if (playerItemList[i].item_id == id && playerItemList[i].number < playerItemList[i].id2data.Get_data(playerItemList[i].item_id).MaxValue)
            {
                num =playerItemList[i].GetItem(id, playerItemList[i].number+num);                
                if (i >= playerItemList.Count- UIItemList.Count) {
                    UIItemList[i - playerItemList.Count + UIItemList.Count].GetItem(id, playerItemList[i].number, Color.white);
                    //UIItemList[i - playerItemList.Count + UIItemList.Count]._value = playerItemList[i].number;
                    //UIItemList[i - playerItemList.Count + UIItemList.Count].text.text = $"{playerItemList[i].number}";
                }
                if (num == 0) {
                    return;
                }               
            }
        }

        //같은거 없으면 새로 생성
        for (int i_ = 27; i_ < playerItemList.Count + 27; i_++)
        {
            int i = i_ % playerItemList.Count;
            if (playerItemList[i].item_id == Item_ID_TG.None)
            {
                playerItemList[i].GetItem(id, num);
                return;
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
    public void UIslot_minus()
    {
        UIItemList[UIslot_index].value_minus();
        if (UIItemList[UIslot_index]._value <= 0)
        {
            //손 장비 초기화 메소드 넣을 자리
            weapon_position.Equip_Weapon(Item_ID_TG.None);
        }
    }
    public Item_ID_TG UIslot_Item()
    {
        return UIItemList[UIslot_index]._item_id;
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
        //Debug.Log($"Craft: {result_.Key} / {result_.Value} ");
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