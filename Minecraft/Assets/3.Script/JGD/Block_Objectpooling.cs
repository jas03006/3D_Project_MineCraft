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

    public GameObject Get(Item_ID_TG id, Vector3 position)
    {
        GameObject select = null;
        
        foreach (GameObject item in Blockpool[iD2Item.ID2index(id)])
        {
            
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = position;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            //Debug.Log(id);
            
            GameObject prefab_ = iD2Item.get_prefab(id);
            if (prefab_ != null)
            {
                Debug.Log(iD2Item.ID2index(id));
                select = Instantiate(prefab_, position, Quaternion.identity);
                Blockpool[iD2Item.ID2index(id)].Add(select);
            }
            
            
        }
        return select;
    }

    public void throw_item(Item_ID_TG id_, Vector3 position_, Vector3 forward_) {
        GameObject go = Get(id_, position_);
        if (go == null) {
            return;
        }
        Rigidbody rigid = go.GetComponent<Rigidbody>();
        if (rigid != null) {
            rigid.AddForce((forward_ + Vector3.up * 0.5f)*3.5f,ForceMode.Impulse);
        }
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