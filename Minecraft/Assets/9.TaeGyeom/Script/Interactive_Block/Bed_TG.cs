using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed_TG : Block_TG, Interactive_TG
{
    public void react()
    {
        Player_Test_TG player_go = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Test_TG>();

        //player_go.transform.position = transform.GetChild(0).position;// + Vector3.up * 0.5f + Vector3.forward;
        //player_go.transform.rotation = transform.GetChild(0).rotation;
        
        player_go.sleep(transform.GetChild(0).position, transform.GetChild(0).rotation, Biom_Manager.instance.get_block(get_bed_position()));     
        //player_go.gameObject.GetComponent<PlayerState_Y>().respawn_bed = this;
        // player_go.transform.up = Vector3.forward;
    }
    public override void die_early()
    {
        PlayerState_Y psy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState_Y>();
        if (psy.respawn_bed == Biom_Manager.instance.get_block(get_bed_position())) {
            psy.respawn_bed = null;
        }
    }

    private Vector3 get_bed_position() {
        return new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
    }
    /*public Vector3 get_respawn_position() {
        return  transform.GetChild(0).position + Vector3.up * 0.5f;
    }*/
}
