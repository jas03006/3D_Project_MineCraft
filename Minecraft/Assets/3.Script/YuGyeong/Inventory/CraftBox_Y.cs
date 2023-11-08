using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftBox_Y : MonoBehaviour
{
    [SerializeField] List<Slot_Y> slots = new List<Slot_Y>();
    [SerializeField] Inventory inven;
    void Start()
    {
        inven = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item_id != 0)
            {
                return;
            }
            else
            {
                inven.GetItem(slots[i].item_id, slots[i].number);
            }
        }
    }
}
