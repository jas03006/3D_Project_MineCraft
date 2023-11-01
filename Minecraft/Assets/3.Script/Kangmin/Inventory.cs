using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum ItemCode { CubbleStone = 1, Grass = 2, Dirt = 3, Plank = 5, BedRock = 7 , Iron = 15, Coal = 16, Oak = 17, Leaf = 18, 
                 Chest = 54, Diamond = 56, CraftingTable = 58, Furnance = 61, Door = 324, Bed = 355 }

public class Inventory : MonoBehaviour
{

    [SerializeField] private List<int> playerHandList = new List<int>(9); // 플레이어 손에 들려있는 아이템 리스트
    [SerializeField] private List<int> playerInventoryList = new List<int>(27); // 플레이어 인벤토리 아이템 리스트

    public Image image;
    
    private bool isOptionOpen = false;
    private bool isInventoryOpen = false;
    private int maxStackCount = 64;

    private void Update()
    {
        InventoryInteraction();
    }

    private void InventoryInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!isOptionOpen)
            {
                image.enabled = true;
                isOptionOpen = true;
            }
            else if(isOptionOpen)
            {
                image.enabled = false;
                isOptionOpen = false;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInventoryOpen)
            {
                image.enabled = true;
                isInventoryOpen = true;
            }
            else if (isInventoryOpen)
            {
                image.enabled = false;
                isInventoryOpen = false;
            }
        }
    }

    private void 

}
