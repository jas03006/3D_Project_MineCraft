using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Pool_Manager : MonoBehaviour
{
    public static Monster_Pool_Manager instance = null;
    private Queue<GameObject>[] pool_list;
    private Vector3 pool_position = new Vector3(1000f, 1000f, 1000f);
    [SerializeField] List<GameObject> monster_prefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
            return;
        }
        init();
    }

    private void init()
    {
        pool_list = new Queue<GameObject>[Enum.GetValues(typeof(Monster_ID_J)).Length];
        int pool_num = 10;
        foreach (Monster_ID_J e in Enum.GetValues(typeof(Monster_ID_J)))//Enum.GetValues(typeof(Item_ID_TG)))
        {
            if (e == Monster_ID_J.None) {
                continue;
            }
            Queue<GameObject> qu = new Queue<GameObject>();
            for (int n = 0; n < pool_num; n++)
            {
                GameObject go = Instantiate(monster_prefabs[(int) e], pool_position, Quaternion.identity);
                go.SetActive(false);
                qu.Enqueue(go);
                go.transform.SetParent(this.transform);
            }
            pool_list[(int)e] = qu;
        }
    }

    public GameObject get(Monster_ID_J id_, Vector3 position_ = new Vector3(), Quaternion rotation_ = new Quaternion(), bool is_active = true) {
        if (id_ == Monster_ID_J.None)
        {
            return null;
        }
        GameObject go;
        if ( pool_list[(int)id_].Count == 0)
        {
            go = Instantiate(monster_prefabs[(int)id_], position_, rotation_);
        }
        else
        {
            go = pool_list[(int)id_].Dequeue();
            go.transform.position = position_;
            go.transform.rotation = rotation_;
            
        }
        go.SetActive(is_active);
        go.transform.SetParent(this.transform);
        return go;
    }

    public void back(Monster_ID_J id_, GameObject go_) {
        if (id_ == Monster_ID_J.None)
        {
            return;
        }
        go_.SetActive(false);
        pool_list[(int)id_].Enqueue(go_);
    }
}
