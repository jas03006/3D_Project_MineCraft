using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft_Table_TG : Block_TG, Interactive_TG
{
    //private Inventory inventory = null;

    public void react()
    {
        /*if (inventory == null) {
            inventory = GameObject.FindObjectOfType<Inventory>();
        }*/
        Inventory.instance.show_craft();
    }
}
