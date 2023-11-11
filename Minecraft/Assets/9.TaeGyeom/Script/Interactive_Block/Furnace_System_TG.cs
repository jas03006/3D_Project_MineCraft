using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace_System_TG : MonoBehaviour
{
    public static Furnace_System_TG instance = null;
    public float unit_time = 7.5f;
    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }
    public Item_ID_TG get_furnace_result(Item_ID_TG id_)
    {
        if (id_ == Item_ID_TG.meat)
        {
            return Item_ID_TG.steak;
        }
        if (id_ == Item_ID_TG.raw_iron)
        {
            return Item_ID_TG.bar_iron;
        }
        if (id_ == Item_ID_TG.stone) { //test
            return Item_ID_TG.tree;
        }
        return Item_ID_TG.None;
    }
    public int get_fuel_energy(Item_ID_TG id_) {
        if (is_fuel(id_)) {
            if (id_ != Item_ID_TG.coal) {
                return 2;
            }
            return 8;
        }
        return 0;
    }

    public bool is_ingredient(Item_ID_TG id_) {
        if (id_ == Item_ID_TG.meat || id_ == Item_ID_TG.raw_iron || id_ == Item_ID_TG.stone)
        {
            return true;
        }
        return false;
    }

    public bool is_fuel(Item_ID_TG id_)
    {
        if (id_ == Item_ID_TG.coal || id_ == Item_ID_TG.tree || id_ == Item_ID_TG.stick || id_ == Item_ID_TG.board 
            || id_ == Item_ID_TG.wood_axe || id_ == Item_ID_TG.wood_pickaxe || id_ == Item_ID_TG.wood_sword) { 
            return true;
        }
        return false;
    }
}
