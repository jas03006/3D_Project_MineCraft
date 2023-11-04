using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ID2Block_TG", menuName = "Scriptable Object/ID2Block_TG")]
public class ID2Block_TG : ScriptableObject
{
    [SerializeField] public List<GameObject> block_prefab_list;
    Dictionary<Item_ID_TG, int> ID2index_dict;
    public ID2Block_TG() {

        ID2index_dict = new Dictionary<Item_ID_TG, int>();
        int i = 0;
        foreach(Item_ID_TG e in Enum.GetValues(typeof(Item_ID_TG)) ) {
            ID2index_dict[e] = i;
            //Debug.Log($"{e}, {i}");
            i++;            
        }
        
    }
    public GameObject get_prefab(Item_ID_TG id) {
        
        return block_prefab_list[ID2index(id)];
    }

    public int ID2index(Item_ID_TG id) {        
        return ID2index_dict[id];
    }
}
