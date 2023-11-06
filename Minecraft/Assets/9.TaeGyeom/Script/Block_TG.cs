using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_TG : MonoBehaviour
{

    [SerializeField] public Item_ID_TG id;
    [SerializeField] public List<Vector3Int> space;
    public void die() {
        Debug.Log("die");
        Block_Node_TG bn = Biom_Manager.instance.get_block(Biom_Manager.instance.world2chunk_pos(transform.parent.position), Biom_Manager.instance.world2block_pos(transform.parent.position));
        if (bn != null) { 
            bn.destroy_chain();
        }
    }

}
