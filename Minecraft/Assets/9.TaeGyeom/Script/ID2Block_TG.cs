using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ID2Block_TG", menuName = "Scriptable Object/ID2Block_TG")]
public class ID2Block_TG : ScriptableObject
{
    [SerializeField] public List<GameObject> block_prefab_list;
    private Dictionary<Item_ID_TG, int> ID2index_dict;
    [SerializeField]
    public List<Item_ID_TG> block_item_id_list =
        new List<Item_ID_TG>() {
            Item_ID_TG.Fill,
            Item_ID_TG.None,
            Item_ID_TG.stone,
            Item_ID_TG.grass,
            Item_ID_TG.dirt,
            Item_ID_TG.board,
            Item_ID_TG.bedrock,
            Item_ID_TG.coal,
            Item_ID_TG.iron,            
            Item_ID_TG.tree,
            Item_ID_TG.leaf,
            Item_ID_TG.diamond,
            Item_ID_TG.box,
            Item_ID_TG.craft_box,
            Item_ID_TG.furnace,
            Item_ID_TG.door,
            Item_ID_TG.bed
        };
    public ID2Block_TG() {

        ID2index_dict = new Dictionary<Item_ID_TG, int>();
        int i = 0;
        foreach(Item_ID_TG e in block_item_id_list){//Enum.GetValues(typeof(Item_ID_TG)) ) {
            ID2index_dict[e] = i;
            //Debug.Log($"{e}, {i}");
            i++;            
        }
        
    }
    public GameObject get_prefab(Item_ID_TG id) {
        int ind = ID2index(id);
        if (block_prefab_list.Count <= ind) {
            return null;
        }
        return block_prefab_list[ind];
    }

    public int ID2index(Item_ID_TG id) {
        return ID2index_dict[id];
    }
}
