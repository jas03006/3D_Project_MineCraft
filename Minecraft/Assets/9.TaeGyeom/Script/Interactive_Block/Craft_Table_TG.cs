using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft_Table_TG : Block_TG, Interactive_TG
{
    private Inventory inventory = null;
    private void Start()
    {
        //inventory = GameObject.FindObjectOfType<Inventory>();
    }
    public void react()
    {
        if (inventory == null) {
            inventory = GameObject.FindObjectOfType<Inventory>();
        }
        inventory.show_craft();
    }
}
