using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSystem : Creat_Recipe
{
    /*
    �׽�Ʈ ���۴� �����?

    ĭ ���ؼ� ����ĭ�� ������� ��������         (�̷��� ������ ��ġ�°� �ִ��� ��ŷ
    ��ĭ�� ������� ��������                     -��-

    ������ �����

    ���� ������ �迭�� �ִ°� �ϳ��� ���� 0�� �ȴٸ� 
     */
    [Header("ȭ��")]
    public Item_ID_TG num;

    public Item_ID_TG[,] mad = new Item_ID_TG[3, 3];

    private void Update()
    {
        MakerBoxSystem();
    }
    private void MakerBoxSystem()
    {
        if (tG1 == Item_ID_TG.None && tG4 == Item_ID_TG.None && tG7 == Item_ID_TG.None)       //�������� �̴°�
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
        if (tG1 == Item_ID_TG.None && tG2 == Item_ID_TG.None && tG3 == Item_ID_TG.None)   //�������� �̴°�
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
        Recipe();   // ������


        //---------Ȯ�ο�
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