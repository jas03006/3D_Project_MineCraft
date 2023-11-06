using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hand_Item_Pooling : MonoBehaviour
{
    public static Hand_Item_Pooling instance = null;

    public List<GameObject>[] WeaponPool;

    public ID2Item iD2Item;
    [SerializeField] private int itemCount;
    private void Awake()
    {
        SingleTon();
        Make_Weapon_pool();
    }



    public void Make_Weapon_pool()
    {
        WeaponPool = new List<GameObject>[Enum.GetValues(typeof(Item_ID_TG)).Length];
        for (int i = 0; i < WeaponPool.Length; i++)
        {
            WeaponPool[i] = new List<GameObject>();
        }
    }
    public GameObject GetWeapon(Item_ID_TG id, Vector3 position, Transform parent_tr, Vector3 direction)
    {
        GameObject select = null;

        foreach (GameObject item in WeaponPool[iD2Item.ID2index(id)])
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
            GameObject prefab = iD2Item.get_prefab(id);
            if (prefab != null)
            {
                select = Instantiate(prefab, position, Quaternion.identity);
                select.transform.up = direction;
                select.transform.SetParent(parent_tr);
                
                WeaponPool[iD2Item.ID2index(id)].Add(select);
            }
        }
        return select;
    }






    public void SingleTon()
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
