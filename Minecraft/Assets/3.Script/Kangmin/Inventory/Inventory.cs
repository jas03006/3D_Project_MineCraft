using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Dictionary<Item_ID_TG, InventoryData> Dict;

    Vector2 mousePos;

    [SerializeField] private List<int> playerHandList = new List<int>(36); // ������ ����Ʈ

    public Image inventoryImage, menuImage;
    public List<Sprite> spriteImage;

    [SerializeField]private Transform[] children;
    [SerializeField] private Button[] button;

    private bool isMouseOver;
    private bool isInventoryOpen = false;
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

        for (int i = 1; i < children.Length; i++) // 0��° �ε��� = Canvas
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
    /*-------------------- �÷��̾� Ű�Է� ---------------------*/
    private void InventoryInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInventoryOpen == false)
            {
                for (int i = 1; i < children.Length; i++) // 0��° �ε��� = gameObject.name("E")
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
    /*-------------------- ������ ��ȣ�ۿ� ---------------------*/
    private void OnClickItem()
    {
        Debug.Log("���콺 Ŭ�� ��ġ = " + mousePos);
    }
    private void SwapItems(int beforeIndex, int afterIndex)
    {
        int tmp = playerHandList[beforeIndex];
        playerHandList[beforeIndex] = playerHandList[afterIndex];
        playerHandList[afterIndex] = tmp;
    }
}

