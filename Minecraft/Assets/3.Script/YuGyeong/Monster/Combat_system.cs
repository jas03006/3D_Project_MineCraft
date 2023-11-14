using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_system : MonoBehaviour
{
    public static Combat_system instance = null;
    [SerializeField] List<Item_ID_TG> axe_damage_list;
    [SerializeField] List<Item_ID_TG> sword_damage_list;
    [SerializeField] List<Item_ID_TG> ax_damage_list;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void cal_combat()
    {
        
    }
}
