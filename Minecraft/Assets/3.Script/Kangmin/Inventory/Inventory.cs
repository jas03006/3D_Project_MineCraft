using System.Collections;
using System.Collections.Generic;
using System;
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
    Vector2 mousePos;

    [SerializeField] private List<int> playerItemList = new List<int>(36); // ������ ����Ʈ
    private ID2Datalist_YG Datalist_YG;
    public Image inventoryImage;
    public List<Sprite> spriteImage;
    [SerializeField] private Transform[] children;
    [SerializeField] private Button[] button;
    [SerializeField] private GameObject[] buttons;
    private GameObject selectedItem; // ���콺�� ������ �������� ��Ÿ���� UI ���

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
        //buttons = transform.GetChild(0).gameObject.transform.GetComponentInChildren<>;
        button = FindObjectsOfType<Button>();
        Datalist_YG = FindObjectOfType<ID2Datalist_YG>();

        for (int i = 1; i < children.Length; i++) 
        {
            children[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < button.Length; i++)
        {
            button[i].onClick.AddListener(OnClickItem);
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

    private void Getitem(Item_ID_TG id)
    {
        for (int i = 0; i < playerItemList.Count; i++)
        {
            if(playerItemList == null)
            {
                var obj = Instantiate(Datalist_YG.get_prefab(id),children[i].position,Quaternion.identity);
            }
        }

    }
    private void OnClickItem()
    {
        Debug.Log("아이템 클릭" + mousePos);
    }

    private void SwapItems(int beforeIndex, int afterIndex)
    {
        int tmp = playerItemList[beforeIndex];
        playerItemList[beforeIndex] = playerItemList[afterIndex];
        playerItemList[afterIndex] = tmp;
    }
}