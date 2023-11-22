using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Node_TG 
{
    public bool is_blocking = true;
    private Item_ID_TG _id;
    public Item_ID_TG id { 
        get { return _id; }  
        set {
            _id = value;
            if (_id == Item_ID_TG.door || _id == Item_ID_TG.bed || _id == Item_ID_TG.Fill || _id == Item_ID_TG.box)
            {
                is_blocking = false;
            }
            else
            {
                is_blocking = true;
            }
        } 
    }
    public Chunk_TG chunk;
    public Block_Node_TG parent;
    public List<Block_Node_TG> children;
    public Vector3Int local_pos_in_chunk = Vector3Int.one * -1;
    
    public GameObject gameObject = null;
    public Transform transform = null;
    public int open_flag=0;
    public Quaternion rotation = Quaternion.identity;

    [Header("Block Data")]
    public bool is_open = false; // check for door or box is opened
    public List<KeyValuePair<Item_ID_TG, int>> contain_data = null;
    public List<float> time_data = null;
    /*private void OnDestroy()
    {
        //Debug.Log("destroy");
        transform.parent.GetComponent<Chunk_TG>().destory_and_show_adjacents(local_pos_in_chunk.x, local_pos_in_chunk.y, local_pos_in_chunk.z);

    }*/

    public Block_Node_TG() {
        local_pos_in_chunk = new Vector3Int();
        children = new List<Block_Node_TG>();
    }

    public void set_block(Item_ID_TG id_, Vector3 world_pos, Quaternion rotate, List<Vector3Int> space = null) {
        
        if (space != null)
        {
            //Debug.Log(space);
            Block_Node_TG bn;
            for (int i = 0; i < space.Count; i++)
            {
                bn = Biom_Manager.instance.get_block(chunk.chunk_pos, local_pos_in_chunk + space[i]);
                if (bn == null || bn.id != Item_ID_TG.None) {
                    return;
                }
            }
            for (int i = 0; i < space.Count; i++)
            {
                bn = Biom_Manager.instance.get_block(chunk.chunk_pos, local_pos_in_chunk + space[i]);
                bn.id = Item_ID_TG.Fill;
                bn.parent = this;
                children.Add(bn);
            }
        }
        id = id_;
        
        if (gameObject != null)
        {
            Biom_Manager.instance.pool_return(id, gameObject);
        }
        
        set_gameobject(Biom_Manager.instance.pool_get(id, world_pos, rotate));

        show();
    }

    public bool is_data_defendent(Item_ID_TG id_) {
        if (id == Item_ID_TG.door || id == Item_ID_TG.box || id == Item_ID_TG.furnace) {
            return true;
        }
        return false;
    }
    public void set_gameobject(GameObject go) {
        if (go == null) {
            return;
        }
        gameObject = go;
        transform = go.transform;
        rotation = transform.rotation;
        if (is_data_defendent(id)){//id == Item_ID_TG.door || id == Item_ID_TG.box || id == Item_ID_TG.furnace) {
            transform.GetChild(0).TryGetComponent<Block_TG>(out Block_TG bt);
            if (time_data != null) {
                bt.init(is_open, contain_data, time_data);
            }
            else if (contain_data != null)
            {
                bt.init(is_open, contain_data);
            }
            else {
                bt.init(is_open);
            }            
        }
        //open_flag = 1;
    }
    public void remove_gameobject() {
        if (transform != null) {
            rotation = transform.rotation;
            if (is_data_defendent(id))// id == Item_ID_TG.door || id == Item_ID_TG.box || id == Item_ID_TG.furnace)
            {                
                transform.GetChild(0).TryGetComponent<Block_TG>(out Block_TG bt);
                is_open = bt.is_open;
                contain_data = bt.contain_data;
                time_data = bt.time_data;
                //Debug.Log($"Save : {bt.time_data}");
            }
        }      
                
        gameObject = null;
        transform = null;
        //open_flag = -1;
    }
    public void destroy_chain()
    {
        //Debug.Log("Destroy");
        if (id != Item_ID_TG.Fill) {
            transform.GetChild(0).TryGetComponent<Block_TG>(out Block_TG bt);
            if (bt != null)
            {
                bt.drop_items();
            }
        }        

        chunk.destory_and_show_adjacents(local_pos_in_chunk.x, local_pos_in_chunk.y, local_pos_in_chunk.z);
        if (parent != null) {
            parent.children.Remove(this);
            parent.destroy_chain();
            parent = null;
        }
        if (children != null)
        {
            foreach (Block_Node_TG child_bn in children) {
                child_bn.parent = null;
                child_bn.destroy_chain();
            }
            children.Clear();
        }
        is_open = false;
        contain_data = null;
        time_data = null;
        // transform.parent.GetComponent<Chunk_TG>().destory_and_show_adjacents(local_pos_in_chunk.x, local_pos_in_chunk.y, local_pos_in_chunk.z);
    }    

    public void show()
    {
        if (gameObject == null ) {
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
        if (gameObject == null )
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

    public Vector3 get_world_position() {
        return Biom_Manager.instance.chunk2world_pos(chunk.chunk_pos) + local_pos_in_chunk;
    }
}
