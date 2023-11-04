using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Dictionary<Item_ID_TG, InventoryData> Dict;

    Vector2 mousePos;

    [SerializeField] private List<int> playerHandList = new List<int>(36); // 아이템 리스트

    public Image inventoryImage, menuImage;
    public List<Sprite> spriteImage;

    [SerializeField]private Transform[] children;
    [SerializeField] private Button[] button;

    private bool isMouseOver;
    private bool isInventoryOpen = false;
    private int currentIndex; // 클릭했을때 저장되는 인덱스 숫자
    private int maxStackCount = 64;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
        button = FindObjectsOfType<Button>();

        for (int i = 1; i < children.Length; i++) // 0번째 인덱스 = Canvas
        {
            children[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < button.Length; i++)
        {
            button[i].onClick.AddListener(OnClickItem);
            Debug.Log(button[i].name);
        }
    }
    private void Update()
    {
        mousePos = Input.mousePosition;

        InventoryInteraction();
    }
    /*-------------------- 플레이어 키입력 ---------------------*/
    private void InventoryInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInventoryOpen == false)
            {
                for (int i = 1; i < children.Length; i++) // 0번째 인덱스 = gameObject.name("E")
                {
                    children[i].gameObject.SetActive(true);
                }
                //inventoryImage.enabled = true;
                isInventoryOpen = true;
                Cursor.visible = true;
            }
            else if (isInventoryOpen == true)
            {
                for (int i = 1; i < children.Length; i++)
                {
                    children[i].gameObject.SetActive(false);
                }
                //inventoryImage.enabled = false;
                isInventoryOpen = false;
                Cursor.visible = false;
            }
        }
    }
    /*-------------------- 아이템 상호작용 ---------------------*/
    private void OnClickItem()
    {
        Debug.Log("마우스 클릭 위치 = " + mousePos);
    }
    private void SwapItems(int beforeIndex, int afterIndex)
    {
        int tmp = playerHandList[beforeIndex];
        playerHandList[beforeIndex] = playerHandList[afterIndex];
        playerHandList[afterIndex] = tmp;
    }
}

