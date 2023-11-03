using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Objectpooling : MonoBehaviour
{
    public static Block_Objectpooling instance = null;

    public List<GameObject>[] Blockpool;

    public GameObject[] blockitem;
    [SerializeField] private int itemCount;
    private void Awake()
    {
        MakePool();
        blockinstance();
    }

    public void MakePool()
    {
        Blockpool = new List<GameObject>[blockitem.Length];
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

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in Blockpool[index])
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
            select = Instantiate(blockitem[index],transform);
            Blockpool[index].Add(select);
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