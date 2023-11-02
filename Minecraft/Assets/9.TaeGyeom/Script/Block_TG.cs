using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_TG : MonoBehaviour
{
    
    private void OnDestroy()
    {
        Vector3Int local_pos_in_chunk = Biom_Manager.instance.world2block_pos(transform.position);
        Chunk_TG ch = Biom_Manager.instance.get_chunk(Biom_Manager.instance.world2chunk_pos(transform.position));
        if (ch != null) {
            ch.destory_and_show_adjacents(local_pos_in_chunk.x, local_pos_in_chunk.y, local_pos_in_chunk.z);
        }
       
    }
}
