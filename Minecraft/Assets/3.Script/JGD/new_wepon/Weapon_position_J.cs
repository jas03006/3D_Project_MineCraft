using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_position_J : MonoBehaviour
{
    public Item_ID_TG id;    //나중에 받아올것 수정
    public GameObject current_hand = null;
    [SerializeField] Transform start_pos;
    [SerializeField] Transform end_pos;
    [SerializeField] Transform forward_pos;
    private Player_Test_TG player_tg;
    [SerializeField] private ID2Datalist_YG datalist;
    private void Awake()
    {
         GameObject.FindGameObjectWithTag("Player").TryGetComponent<Player_Test_TG>(out player_tg);
    }
    /*  private void Update()
      {
          if (Input.GetKeyDown(KeyCode.I))
          {
              Equip_Weapon( id);
          }
      }*/
    public void Equip_Weapon(Item_ID_TG id_)
    {
        id = id_;
        if (player_tg != null) {
            Useful useful = datalist.Get_data(id_) as Useful;
            if (useful != null)
            {
                player_tg.player_state.att_speed = useful.att_speed;
            }
            else {
                player_tg.player_state.att_speed = player_tg.basic_att_speed;
            }
            
            player_tg.block_in_hand_data = datalist.Get_data(id_);
            player_tg.block_in_hand_id = id_;
            if (id_ == Item_ID_TG.coal || id_ == Item_ID_TG.diamond)
            {
                player_tg.block_in_hand = null;
            }
            else {
                //player_tg.block_in_hand = Biom_Manager.instance.block_prefabs_SO.get_prefab(id_);
            }            
        }

        if (current_hand != null) {
            // 나중에 pool manager에게 리턴하는 것으로 바꿔도 됨.
            current_hand.transform.SetParent(Hand_Item_Pooling.instance.transform);
            current_hand.SetActive(false);
            current_hand = null;
        }
        //Debug.Log("equip weapon");
        current_hand = Hand_Item_Pooling.instance.GetWeapon(id, transform.position,transform, end_pos.position - start_pos.position, forward_pos.position);
    }
}
