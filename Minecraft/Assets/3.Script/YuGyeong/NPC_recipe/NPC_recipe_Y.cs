using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC_recipe", menuName = "Scriptable Object/NPC_recipe", order = int.MaxValue)]
public class NPC_recipe_Y : ScriptableObject
{
    [SerializeField] public ID2Datalist_YG id2data; //= Inventory.instance.cursor_slot.id2data;

    [SerializeField] public Item_ID_TG item_id_1;
    [SerializeField] public int num_1;

    [SerializeField] public Item_ID_TG item_id_2;
    [SerializeField] public int num_2;

    [SerializeField] public Item_ID_TG item_result;
    [SerializeField] public int num_result;


}
