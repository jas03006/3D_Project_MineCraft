using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSystem : MonoBehaviour
{
    /*
    테스트 제작대 만들기?

    칸 비교해서 왼쪽칸이 비었으면 왼쪽으로         (이렇게 했을때 곂치는게 있는지 띵킹
    윗칸이 비었으면 위쪽으로                     -완-

    레시피 만들고

    만들어서 빼내면 배열에 있는거 하나씩 감소 0이 된다면 
     */
    public Item_ID_TG id;   //배출구

    [SerializeField] private ScriptableObject[] objects;

    Item_ID_TG[,] Maker = new Item_ID_TG[3,3];

    [Header("1  세로로")]
    [SerializeField] public Item_ID_TG tG1;
    [SerializeField] public Item_ID_TG tG4;
    [SerializeField] public Item_ID_TG tG7;
    [Header("2")]
    [SerializeField] private Item_ID_TG tG2; 
    [SerializeField] private Item_ID_TG tG5;
    [SerializeField] private Item_ID_TG tG8;
    [Header("3")]
    [SerializeField] private Item_ID_TG tG3;
    [SerializeField] private Item_ID_TG tG6;
    [SerializeField] private Item_ID_TG tG9;

    public long RecipeNumber;

    private void Start()
    {
        Maker[0, 0] = tG1;
        Maker[0, 1] = tG2;
        Maker[0, 2] = tG3;
        Maker[1, 0] = tG4;
        Maker[1, 1] = tG5;
        Maker[1, 2] = tG6;
        Maker[2, 0] = tG7;
        Maker[2, 1] = tG8;
        Maker[2, 2] = tG9;

    }

    private void Update()
    {
        MakerBoxSystem();
        Check();
    }
    private void MakerBoxSystem()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {       
                if (j != 0)
                {
                    if (Maker[i, j] != Item_ID_TG.None && Maker[i, j - 1] == Item_ID_TG.None)
                    {
                        Maker[i, j - 1] = Maker[i, j];
                        Maker[i, j] = Item_ID_TG.None;
                    }
                }
                if (i != 0)
                {
                    if (Maker[i, j] != Item_ID_TG.None && Maker[i - 1, j] == Item_ID_TG.None)
                    {
                        Maker[i - 1, j] = Maker[i, j];
                        Maker[i, j] = Item_ID_TG.None;
                    }
                }
            }
        }
        //---------확인용
        tG1 = Maker[0, 0];
        tG2 = Maker[0, 1];
        tG3 = Maker[0, 2];
        tG4 = Maker[1, 0];
        tG5 = Maker[1, 1];
        tG6 = Maker[1, 2];
        tG7 = Maker[2, 0];
        tG8 = Maker[2, 1];
        tG9 = Maker[2, 2];
    }
    private void Check()   //구시대적 제작
    {
        if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.board &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None&&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.steak;
        }
    }


    public void MakeRecipe()
    {


        switch (id)
        {
            case Item_ID_TG.box:
                tG1 = Item_ID_TG.board;
                tG2 = Item_ID_TG.board;
                tG3 = Item_ID_TG.board;
                tG4 = Item_ID_TG.board;
                tG5 = Item_ID_TG.None;
                tG6 = Item_ID_TG.board;
                tG7 = Item_ID_TG.board;
                tG8 = Item_ID_TG.board;
                tG9 = Item_ID_TG.board;
                break;
            case Item_ID_TG.craft_box:
                tG1 = Item_ID_TG.board;
                tG2 = Item_ID_TG.board;
                tG3 = Item_ID_TG.board;
                tG4 = Item_ID_TG.board;
                tG5 = Item_ID_TG.None;
                tG6 = Item_ID_TG.board;
                tG7 = Item_ID_TG.board;
                tG8 = Item_ID_TG.board;
                tG9 = Item_ID_TG.board;
                break;
            case Item_ID_TG.furnace:
                break;
            case Item_ID_TG.stick:
                break;
            case Item_ID_TG.door:
                break;
            case Item_ID_TG.bed:
                break;
            case Item_ID_TG.steak:
                break;
            case Item_ID_TG.wood_sword:
                break;
            case Item_ID_TG.stone_sword:
                break;
            case Item_ID_TG.iron_sword:
                break;
            case Item_ID_TG.diamond_sword:
                break;
            case Item_ID_TG.wood_pickaxe:
                break;
            case Item_ID_TG.stone_pickaxe:
                break;
            case Item_ID_TG.iron_pickaxe:
                break;
            case Item_ID_TG.diamond_pickaxe:
                break;
            case Item_ID_TG.wood_axe:
                break;
            case Item_ID_TG.stone_axe:
                break;
            case Item_ID_TG.iron_axe:
                break;
            case Item_ID_TG.diamond_axe:
                break;
            case Item_ID_TG.iron_helmet:
                break;
            case Item_ID_TG.diamond_helmet:
                break;
            case Item_ID_TG.iron_armor:
                break;
            case Item_ID_TG.diamond_armor:
                break;
            case Item_ID_TG.iron_leggings:
                break;
            case Item_ID_TG.diamond_leggings:
                break;
            case Item_ID_TG.iron_boots:
                break;
            case Item_ID_TG.diamond_boots:
                break;
            case Item_ID_TG.raw_iron:
                break;
            default:
                break;
        }
    }
}