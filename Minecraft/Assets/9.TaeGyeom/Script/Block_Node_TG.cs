using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Node_TG : MonoBehaviour
{
    public int id { get; private set;}
    public Block_Node_TG parent;
    public Block_Node_TG child;
    public Vector3Int local_pos_in_chunk = Vector3Int.one * -1;
    public bool is_blocking = true;


    private void OnDestroy()
    {
        //Debug.Log("destroy");
        transform.parent.GetComponent<Chunk_TG>().destory_and_show_adjacents(local_pos_in_chunk.x, local_pos_in_chunk.y, local_pos_in_chunk.z);

    }

    public void destroy_block()
    {
        if (parent != null) {
            parent.child = null;
            parent.destroy_block();
        }
        if (child != null)
        {
            child.parent = null;
            child.destroy_block();
        }
        transform.parent.GetComponent<Chunk_TG>().destory_and_show_adjacents(local_pos_in_chunk.x, local_pos_in_chunk.y, local_pos_in_chunk.z);
    }    

    public void show()
    {
        gameObject.layer = 0;
        for (int ch = 0; ch < transform.childCount; ch++)
        {
            transform.GetChild(ch).gameObject.layer = 0;
        }
    }
    public void hide()
    {
        gameObject.layer = LayerMask.NameToLayer("Hidden_Block");
        for (int ch = 0; ch < transform.childCount; ch++)
        {
            transform.GetChild(ch).gameObject.layer = LayerMask.NameToLayer("Hidden_Block");
        }
    }

    public void set_local_pos(int x, int y, int z)
    {
        local_pos_in_chunk = new Vector3Int(x,y,z);
    }
}
