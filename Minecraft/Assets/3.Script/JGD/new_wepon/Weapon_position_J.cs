using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_position_J : MonoBehaviour
{
    public Item_ID_TG id;    //나중에 받아올것 수정
    [SerializeField] Transform start_pos;
    [SerializeField] Transform end_pos;

    private void Update()
    {
        Equip_Weapon();
    }
    public void Equip_Weapon()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Hand_Item_Pooling.instance.GetWeapon(id, transform.position,transform, end_pos.position - start_pos.position);
        }
    }
}
