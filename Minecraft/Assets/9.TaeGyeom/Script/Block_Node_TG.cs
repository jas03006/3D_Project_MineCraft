using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Node_TG 
{
    public Item_ID_TG id;
    public Block_Node_TG parent;
    public Block_Node_TG child;
    public Vector3Int local_pos_in_chunk = Vector3Int.one * -1;
    public bool is_blocking = true;
    public GameObject gameObject = null;
    public Transform transform = null;
    public int open_flag=0;

    /*private void OnDestroy()
    {
        //Debug.Log("destroy");
        transform.parent.GetComponent<Chunk_TG>().destory_and_show_adjacents(local_pos_in_chunk.x, local_pos_in_chunk.y, local_pos_in_chunk.z);

    }*/

    public Block_Node_TG() {
        local_pos_in_chunk = new Vector3Int();
    }
    
    public void set_gameobject(GameObject go) {
        gameObject = go;
        transform = go.transform;
    }
    public void remove_gameobject() {
        gameObject = null;
        transform = null;
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
        if (gameObject == null || gameObject.layer == 0) {
            return;
        }
        gameObject.layer = 0;
        for (int ch = 0; ch < transform.childCount; ch++)
        {
            transform.GetChild(ch).gameObject.layer = 0;
        }
        open_flag = 1;
    }
    public void hide()
    {
        if (gameObject == null || gameObject.layer == LayerMask.NameToLayer("Hidden_Block"))
        {
            return;
        }
        gameObject.layer = LayerMask.NameToLayer("Hidden_Block");
        for (int ch = 0; ch < transform.childCount; ch++)
        {
            transform.GetChild(ch).gameObject.layer = LayerMask.NameToLayer("Hidden_Block");
        }
        open_flag = -1;
    }

    public void set_local_pos(int x, int y, int z)
    {
        local_pos_in_chunk.x = x;
        local_pos_in_chunk.y = y;
        local_pos_in_chunk.z = z;
    }
}
