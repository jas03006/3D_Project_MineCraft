using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ID2Datalist_YG", menuName = "Scriptable Object/ID2Datalist_YG")]
public class ID2Datalist_YG : ScriptableObject
{
    [SerializeField] public List<InventoryData> itemdata;
    private Dictionary<Item_ID_TG, int> ID2index_dict;
    public List<Item_ID_TG> block_item_id_list =
        new List<Item_ID_TG>() {
            Item_ID_TG.None,
            Item_ID_TG.stone,
            Item_ID_TG.dirt,
            Item_ID_TG.board,
            Item_ID_TG.bedrock,
            Item_ID_TG.coal,
            Item_ID_TG.iron,
            Item_ID_TG.tree,
            Item_ID_TG.box,//상자
            Item_ID_TG.diamond,
            Item_ID_TG.craft_box,
            Item_ID_TG.furnace,//화로
            Item_ID_TG.apple,
            Item_ID_TG.stick,
            Item_ID_TG.door,
            Item_ID_TG.bed,
            Item_ID_TG.meat,
            Item_ID_TG.steak,
            Item_ID_TG.wood_sword,
            Item_ID_TG.stone_sword,
            Item_ID_TG.iron_sword,
            Item_ID_TG.diamond_sword,
            Item_ID_TG.wood_pickaxe,
            Item_ID_TG.stone_pickaxe,
            Item_ID_TG.iron_pickaxe,
            Item_ID_TG.diamond_pickaxe,
            Item_ID_TG.wood_axe,
            Item_ID_TG.stone_axe,
            Item_ID_TG.iron_axe,
            Item_ID_TG.diamond_axe,
            Item_ID_TG.iron_helmet,
            Item_ID_TG.diamond_helmet,
            Item_ID_TG.iron_armor,
            Item_ID_TG.diamond_armor,
            Item_ID_TG.iron_leggings,
            Item_ID_TG.diamond_leggings,
            Item_ID_TG.iron_boots,
            Item_ID_TG.diamond_boots,
        };
    public ID2Datalist_YG()
    {
        ID2index_dict = new Dictionary<Item_ID_TG, int>();
        int i = 0;
        foreach (Item_ID_TG e in block_item_id_list)
        {
            ID2index_dict[e] = i;
            i++;
        }
    }

    public InventoryData Get_data(Item_ID_TG id)
    {
        int ind = ID2index(id);
        if (itemdata.Count <= ind)
        {
            return null;
        }
        return itemdata[ind];
    }
    public int ID2index(Item_ID_TG id)
    {
        return ID2index_dict[id];
    }
}
