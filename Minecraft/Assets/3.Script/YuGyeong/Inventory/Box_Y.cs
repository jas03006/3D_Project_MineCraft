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

    public void Get_data(List<KeyValuePair<Item_ID_TG, int>> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            slots[i].ResetItem();
            slots[i].GetItem(data[i].Key, data[i].Value);
        }
    }
    public void reset_data()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].ResetItem();
        }
    }

    public List<Slot_Y> Get_slots() {
        return slots;
    }
}
