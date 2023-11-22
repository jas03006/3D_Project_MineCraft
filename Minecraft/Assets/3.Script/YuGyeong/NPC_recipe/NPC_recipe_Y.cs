using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC_recipe", menuName = "Scriptable Object/NPC_recipe", order = int.MaxValue)]
public class NPC_recipe_Y : ScriptableObject
{
    [SerializeField] public ID2Datalist_YG id2data;

    [SerializeField] public Item_ID_TG[] item_id_list; //0:item_id_1,1:item_id_2,2:item_result
    [SerializeField] public int[] num_list; //0:num_1,1:num_2,2:num_result

}
