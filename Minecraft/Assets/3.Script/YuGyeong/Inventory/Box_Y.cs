using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Y : MonoBehaviour
{
    [SerializeField] List<Slot_Y> slots;
    void Start()
    {
        Slot_Y[] _slots = GetComponentsInChildren<Slot_Y>();
        slots = new List<Slot_Y>(_slots);
    }

    
    void Update()
    {
        
    }

    void Get_data(List<Item_ID_TG> IDList,List<int> numList)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetItem(IDList[i], numList[i]);
        }
    }
}
