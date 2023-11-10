using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Creat_Recipe : MonoBehaviour
{
    protected Item_ID_TG id; // 배출구

    protected int ItemCount;


    [Header("1  세로로")]
    [SerializeField] protected Item_ID_TG tG1;
    [SerializeField] protected Item_ID_TG tG4;
    [SerializeField] protected Item_ID_TG tG7;
    [Header("2")]
    [SerializeField] protected Item_ID_TG tG2;
    [SerializeField] protected Item_ID_TG tG5;
    [SerializeField] protected Item_ID_TG tG8;
    [Header("3")]
    [SerializeField] protected Item_ID_TG tG3;
    [SerializeField] protected Item_ID_TG tG6;
    [SerializeField] protected Item_ID_TG tG9;



    //----------------------------------------------------------------------------------------


    protected void Recipe()
    {
        //---------------------------------------------1.Board
        if (tG1 == Item_ID_TG.tree &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.None &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.board;
            ItemCount = 4;
        }
        //---------------------------------------------2.Box
        else if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.board &&
        tG3 == Item_ID_TG.board &&
        tG4 == Item_ID_TG.board &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.board &&
        tG7 == Item_ID_TG.board &&
        tG8 == Item_ID_TG.board &&
        tG9 == Item_ID_TG.board)
        {
            id = Item_ID_TG.box;
            ItemCount = 1;
        }
        //---------------------------------------------3.stick
        else if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.board &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.stick;
            ItemCount = 4;
        }
        //---------------------------------------------4.craft_box
        else if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.board &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.board &&
        tG5 == Item_ID_TG.board &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.craft_box;
            ItemCount = 1;
        }
        //---------------------------------------------5.Furnace
        else if (tG1 == Item_ID_TG.stone &&
        tG2 == Item_ID_TG.stone &&
        tG3 == Item_ID_TG.stone &&
        tG4 == Item_ID_TG.stone &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.stone &&
        tG7 == Item_ID_TG.stone &&
        tG8 == Item_ID_TG.stone &&
        tG9 == Item_ID_TG.stone)
        {
            id = Item_ID_TG.furnace;
            ItemCount = 1;
        }
        //---------------------------------------------6.door
        else if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.board &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.board &&
        tG5 == Item_ID_TG.board &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.board &&
        tG8 == Item_ID_TG.board &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.door;
            ItemCount = 3;
        }
        //---------------------------------------------7.bed
        else if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.board &&
        tG3 == Item_ID_TG.board &&
        tG4 == Item_ID_TG.None &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.bed;
            ItemCount = 1;
        }
        //---------------------------------------------8.Wood_Sword
        else if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.board &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.stick &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.wood_sword;
            ItemCount = 1;
        }
        //---------------------------------------------9.Stone_sword
        else if (tG1 == Item_ID_TG.stone &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.stone &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.stick &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.stone_sword;
            ItemCount = 1;
        }
        //---------------------------------------------10.iron_sword
        else if (tG1 == Item_ID_TG.iron &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.iron &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.stick &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.iron_sword;
            ItemCount = 1;
        }
        //---------------------------------------------11.diamond_sword
        else if (tG1 == Item_ID_TG.diamond &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.diamond &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.stick &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.diamond_sword;
            ItemCount = 1;
        }
        //---------------------------------------------12.wood_pickaxe
        else if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.board &&
        tG3 == Item_ID_TG.board &&
        tG4 == Item_ID_TG.None &&
        tG5 == Item_ID_TG.stick &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.stick &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.wood_pickaxe;
            ItemCount = 1;

        }
        //---------------------------------------------13.stone_pickaxe
        else if (tG1 == Item_ID_TG.stone &&
        tG2 == Item_ID_TG.stone &&
        tG3 == Item_ID_TG.stone &&
        tG4 == Item_ID_TG.None &&
        tG5 == Item_ID_TG.stick &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.stick &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.stone_pickaxe;
            ItemCount = 1;
        }
        //---------------------------------------------14.iron_pickaxe
        else if (tG1 == Item_ID_TG.iron &&
        tG2 == Item_ID_TG.iron &&
        tG3 == Item_ID_TG.iron &&
        tG4 == Item_ID_TG.None &&
        tG5 == Item_ID_TG.stick &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.stick &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.iron_pickaxe;
            ItemCount = 1;

        }
        //---------------------------------------------15.diamond_pickaxe
        else if (tG1 == Item_ID_TG.diamond &&
        tG2 == Item_ID_TG.diamond &&
        tG3 == Item_ID_TG.diamond &&
        tG4 == Item_ID_TG.None &&
        tG5 == Item_ID_TG.stick &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.stick &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.diamond_pickaxe;
            ItemCount = 1;
        }
        //---------------------------------------------16.wood_axe
        else if (tG1 == Item_ID_TG.board &&
        tG2 == Item_ID_TG.board &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.board &&
        tG5 == Item_ID_TG.stick &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.stick &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.wood_axe;
            ItemCount = 1;
        }
        //---------------------------------------------17.stone_axe
        else if (tG1 == Item_ID_TG.stone &&
        tG2 == Item_ID_TG.stone &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.stone &&
        tG5 == Item_ID_TG.stick &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.stick &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.stone_axe;
            ItemCount = 1;
        }
        //---------------------------------------------18.iron_axe
        else if (tG1 == Item_ID_TG.iron &&
        tG2 == Item_ID_TG.iron &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.iron &&
        tG5 == Item_ID_TG.stick &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.stick &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.iron_axe;
            ItemCount = 1;
        }
        //---------------------------------------------19.diamond_axe
        else if (tG1 == Item_ID_TG.diamond &&
        tG2 == Item_ID_TG.diamond &&
        tG3 == Item_ID_TG.None &&
        tG4 == Item_ID_TG.iron &&
        tG5 == Item_ID_TG.stick &&
        tG6 == Item_ID_TG.None &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.stick &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.diamond_axe;
            ItemCount = 1;
        }
        //---------------------------------------------20.iron_helmet
        else if (tG1 == Item_ID_TG.iron &&
        tG2 == Item_ID_TG.iron &&
        tG3 == Item_ID_TG.iron &&
        tG4 == Item_ID_TG.iron &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.iron &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.iron_helmet;
            ItemCount = 1;
        }
        //---------------------------------------------21.diamond_helmet
        else if (tG1 == Item_ID_TG.diamond &&
        tG2 == Item_ID_TG.diamond &&
        tG3 == Item_ID_TG.diamond &&
        tG4 == Item_ID_TG.diamond &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.diamond &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.diamond_helmet;
            ItemCount = 1;
        }
        //---------------------------------------------22.iron_armor
        else if (tG1 == Item_ID_TG.iron &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.iron &&
        tG4 == Item_ID_TG.iron &&
        tG5 == Item_ID_TG.iron &&
        tG6 == Item_ID_TG.iron &&
        tG7 == Item_ID_TG.iron &&
        tG8 == Item_ID_TG.iron &&
        tG9 == Item_ID_TG.iron)
        {
            id = Item_ID_TG.iron_armor;
            ItemCount = 1;
        }
        //---------------------------------------------23.diamond_armor
        else if (tG1 == Item_ID_TG.diamond &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.diamond &&
        tG4 == Item_ID_TG.diamond &&
        tG5 == Item_ID_TG.diamond &&
        tG6 == Item_ID_TG.diamond &&
        tG7 == Item_ID_TG.diamond &&
        tG8 == Item_ID_TG.diamond &&
        tG9 == Item_ID_TG.diamond)
        {
            id = Item_ID_TG.diamond_armor;
            ItemCount = 1;
        }
        //---------------------------------------------24.iron_leggings
        else if (tG1 == Item_ID_TG.iron &&
        tG2 == Item_ID_TG.iron &&
        tG3 == Item_ID_TG.iron &&
        tG4 == Item_ID_TG.iron &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.iron &&
        tG7 == Item_ID_TG.iron &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.iron)
        {
            id = Item_ID_TG.iron_leggings;
            ItemCount = 1;
        }
        //---------------------------------------------25.diamond_leggings
        else if (tG1 == Item_ID_TG.diamond &&
        tG2 == Item_ID_TG.diamond &&
        tG3 == Item_ID_TG.diamond &&
        tG4 == Item_ID_TG.diamond &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.diamond &&
        tG7 == Item_ID_TG.diamond &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.diamond)
        {
            id = Item_ID_TG.diamond_leggings;
            ItemCount = 1;
        }
        //---------------------------------------------26.iron_boots
        else if (tG1 == Item_ID_TG.iron &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.iron &&
        tG4 == Item_ID_TG.iron &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.iron &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.iron_boots;
            ItemCount = 1;
        }
        //---------------------------------------------27.diamond_boots
        else if (tG1 == Item_ID_TG.diamond &&
        tG2 == Item_ID_TG.None &&
        tG3 == Item_ID_TG.diamond &&
        tG4 == Item_ID_TG.diamond &&
        tG5 == Item_ID_TG.None &&
        tG6 == Item_ID_TG.diamond &&
        tG7 == Item_ID_TG.None &&
        tG8 == Item_ID_TG.None &&
        tG9 == Item_ID_TG.None)
        {
            id = Item_ID_TG.diamond_boots;
            ItemCount = 1;
        }
        else {
            id = Item_ID_TG.None;
            ItemCount = 0;
        }        
    }
}
