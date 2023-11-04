using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block_Objectpooling : MonoBehaviour
{
    public static Block_Objectpooling instance = null;

    public List<GameObject>[] Blockpool;

    public ID2Item iD2Item;
    [SerializeField] private int itemCount;
    private void Awake()
    {
        MakePool();
        blockinstance();
    }

    public void MakePool()
    {
        Blockpool = new List<GameObject>[Enum.GetValues(typeof(Item_ID_TG)).Length];
        for (int i = 0; i < Blockpool.Length; i++)
        {
            Blockpool[i] = new List<GameObject>();
        }

        //for (int i = 0; i < itemCount; i++)
        //{
        //    GameObject gameObject = Instantiate(blockitem);
        //    gameObject.SetActive(false);
        //    Blockpool.Enqueue(blockitem);
        //}
    }

    public GameObject Get(Item_ID_TG id)
    {
        GameObject select = null;

        foreach (GameObject item in Blockpool[iD2Item.ID2index(id)])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(iD2Item.get_prefab(id),transform);
            Blockpool[iD2Item.ID2index(id)].Add(select);
        }




        return select;
    }


    private void blockinstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



}