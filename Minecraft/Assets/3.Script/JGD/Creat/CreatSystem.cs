using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSystem : Creat_Recipe
{
    /*
    테스트 제작대 만들기?

    칸 비교해서 왼쪽칸이 비었으면 왼쪽으로         (이렇게 했을때 곂치는게 있는지 띵킹
    윗칸이 비었으면 위쪽으로                     -완-

    레시피 만들고

    만들어서 빼내면 배열에 있는거 하나씩 감소 0이 된다면 
     */
    [Header("화긴")]
    public Item_ID_TG num;

    public Item_ID_TG[,] mad = new Item_ID_TG[3, 3];

    private void Update()
    {
        MakerBoxSystem();
    }
    private void MakerBoxSystem()
    {
        if (tG1 == Item_ID_TG.None && tG4 == Item_ID_TG.None && tG7 == Item_ID_TG.None)       //왼쪽으로 미는거
        {
            if (tG2 == Item_ID_TG.None && tG5 == Item_ID_TG.None && tG8 == Item_ID_TG.None)
            {
                tG1 = tG3;
                tG4 = tG6;
                tG7 = tG9;

                tG3 = Item_ID_TG.None;
                tG6 = Item_ID_TG.None;
                tG9 = Item_ID_TG.None;
            }
            else
            {
                tG1 = tG2;
                tG4 = tG5;
                tG7 = tG8;
                tG2 = tG3;
                tG5 = tG6;
                tG8 = tG9;

                tG3 = Item_ID_TG.None;
                tG6 = Item_ID_TG.None;
                tG9 = Item_ID_TG.None;
            }
        
        }
        if (tG1 == Item_ID_TG.None && tG2 == Item_ID_TG.None && tG3 == Item_ID_TG.None)   //위쪽으로 미는거
        {
            if (tG4 == Item_ID_TG.None && tG5 == Item_ID_TG.None && tG6 == Item_ID_TG.None)
            {
                tG1 = tG7;
                tG2 = tG8;
                tG3 = tG9;
        
                tG7 = Item_ID_TG.None;
                tG8 = Item_ID_TG.None;
                tG9 = Item_ID_TG.None;
            }
            else
            {
                tG1 = tG4;
                tG2 = tG5;
                tG3 = tG6;
                tG4 = tG7;
                tG5 = tG8;
                tG6 = tG9;
        
                tG7 = Item_ID_TG.None;
                tG8 = Item_ID_TG.None;
                tG9 = Item_ID_TG.None;
            }
        }
        Recipe();   // 레시피


        //---------확인용
        mad[0, 0] = tG1;
        mad[0, 1] = tG2;
        mad[0, 2] = tG3;
        mad[1, 0] = tG4;
        mad[1, 1] = tG5;
        mad[1, 2] = tG6;
        mad[2, 0] = tG7;
        mad[2, 1] = tG8;
        mad[2, 2] = tG9;
        num = id;

    }



    
}