using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_position_J : MonoBehaviour
{
    public Item_ID_TG id;    //���߿� �޾ƿð� ����
    [SerializeField] Transform start_pos;
    [SerializeField] Transform end_pos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Equip_Weapon( id);
        }
    }
    public void Equip_Weapon(Item_ID_TG id_)
    {
        id = id_;
        Hand_Item_Pooling.instance.GetWeapon(id, transform.position,transform, end_pos.position - start_pos.position);
    }
}