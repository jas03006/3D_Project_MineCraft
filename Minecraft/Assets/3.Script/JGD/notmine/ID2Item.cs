using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "ID2Item", menuName = "Scriptable Object/ID2Item")]
public class ID2Item : ScriptableObject
{
    
        [SerializeField] public List<GameObject> Item_prefab_list;
        Dictionary<Item_ID_TG, int> ID2index_dict;
        public ID2Item()
        {

            ID2index_dict = new Dictionary<Item_ID_TG, int>();
            int i = 0;
            foreach (Item_ID_TG e in Enum.GetValues(typeof(Item_ID_TG)))
            {
                ID2index_dict[e] = i;
                i++;
            }

        }
        public GameObject get_prefab(Item_ID_TG id)
        {
        int ind = ID2index(id);
        if (Item_prefab_list == null || ind >= Item_prefab_list.Count) {
            return null;
        }

            return Item_prefab_list[ind];
        }

        public int ID2index(Item_ID_TG id)
        {
            return ID2index_dict[id];
        }

}
