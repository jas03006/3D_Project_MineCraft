using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
enum ItemCode
{
    CubbleStone = 1, Grass = 2, Dirt = 3, Plank = 5, BedRock = 7, Iron = 15, Coal = 16, Oak = 17, Leaf = 18,
    Chest = 54, Diamond = 56, CraftingTable = 58, Furnance = 61, Apple = 260, Door = 324, Bed = 355,
    Meat = 363, GrilledMeat = 364
}


public class Inventory : MonoBehaviour
{
    private Dictionary<ItemCode, InventoryData> Dict;

    [SerializeField] private List<int> playerHandList = new List<int>(9); // �÷��̾� �տ� ����ִ� ������ ����Ʈ

    [SerializeField] private List<int> playerInventoryList = new List<int>(27); // �÷��̾� �κ��丮 ������ ����Ʈ

    public Image inventoryImage, menuImage;

    public List<Sprite> spriteImage;

    Transform[] children;

    private bool isOptionOpen = false;
    private bool isInventoryOpen;
    private int maxStackCount = 64;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();

        for (int i = 1; i < children.Length; i++) // 0��° �ε��� = Canvas
        {
            children[i].gameObject.SetActive(false);
        }
        //inventoryImage.enabled = true;
        isInventoryOpen = false;
    }

    private void Update()
    {
        InventoryInteraction();
    }

    /*-------------------- �÷��̾� Ű�Է� ---------------------*/
    private void InventoryInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInventoryOpen == false)
            {
                for (int i = 1; i < children.Length; i++) // 0��° �ε��� = Canvas
                {
                    children[i].gameObject.SetActive(true);
                }
                //inventoryImage.enabled = true;
                isInventoryOpen = true;
            }
            else if (isInventoryOpen == true)
            {
                for (int i = 1; i < children.Length; i++)
                {
                    children[i].gameObject.SetActive(false);
                }
                //inventoryImage.enabled = false;
                isInventoryOpen = false;
            }
        }
    }
    private void OptionInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInventoryOpen)
            {
                menuImage.enabled = true;
                isInventoryOpen = true;
            }
            else if (isInventoryOpen)
            {
                menuImage.enabled = false;
                isInventoryOpen = false;
            }
        }
    }

    /*-------------------- ������ �̹��� ������ ---------------------*/
}





