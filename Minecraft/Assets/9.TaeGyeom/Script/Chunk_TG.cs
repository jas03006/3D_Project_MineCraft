using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_TG: MonoBehaviour
{
    public int chunk_size;
    public Block_Node_TG[,,] block_data;
    public Vector3Int chunk_pos;
    public bool is_open_checked = false;
    public bool is_active = true;
    // Start is called before the first frame update
    
    public void init(int chunk_size_, Vector3Int chunk_pos_)
    {
        chunk_pos = chunk_pos_;
        chunk_size = chunk_size_;
        block_data = new Block_Node_TG[chunk_size, chunk_size, chunk_size];
        is_open_checked = false;
    }
    public void generate_blocks()
    {
        is_active = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j <  chunk_size; j++)
            {
                for (int k =0; k < chunk_size; k++)
                {
                    Item_ID_TG temp_index = get_prefabs_index(i,j,k);

                    block_data[i, j, k] = Biom_Manager.instance.get_block_node();//new Block_Node_TG();
                    block_data[i, j, k].id = temp_index;
                    block_data[i, j, k].set_local_pos(i, j, k);
                    if (temp_index == Item_ID_TG.None) {
                        continue;
                    }
                    GameObject go = Biom_Manager.instance.pool_get(temp_index, new Vector3(origin_pos.x + i, origin_pos.y + j, origin_pos.z + k), Quaternion.identity);

                    //go.transform.SetParent(transform);
                     //go.GetComponent<Block_Node_TG>();
                    block_data[i, j, k].set_gameobject(go);
                    

                    /*GameObject go = GameObject.Instantiate(Biom_Manager.instance.block_prefabs_SO.get_prefab(temp_index), new Vector3(origin_pos.x+i, origin_pos.y+j, origin_pos.z+ k), Quaternion.identity);
                    go.transform.SetParent(transform);
                    block_data[i, j , k] = go.GetComponent<Block_Node_TG>();
                    block_data[i, j, k].set_local_pos(i,j,k);*/
                }
                
            }
        }
       // check_open_and_show_all();
    }

    public void generate_block_nodes()
    {
        is_active = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        Vector3 new_pos = new Vector3();
        //float start_time = Time.time;
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j < chunk_size; j++)
            {
                for (int k = 0; k < chunk_size; k++)
                {

                    Item_ID_TG temp_index = get_prefabs_index(i, j, k);
                    block_data[i, j, k] = Biom_Manager.instance.get_block_node();//new Block_Node_TG();
                    block_data[i, j, k].id = temp_index;
                    block_data[i, j, k].set_local_pos(i, j, k);

                }
            }

        }
    }
    public IEnumerator generate_blocks_co()
    {
        is_active = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        Vector3 new_pos = new Vector3();
        //float start_time = Time.time;
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j < chunk_size; j++)
            {
                for (int k = 0; k < chunk_size; k++)
                {
                    
                    Item_ID_TG temp_index = get_prefabs_index(i, j, k);
                    block_data[i, j, k] = Biom_Manager.instance.get_block_node();//new Block_Node_TG();
                    block_data[i, j, k].id = temp_index;
                    block_data[i, j, k].set_local_pos(i, j, k);
                    /*if (temp_index == Item_ID_TG.None)
                    {
                        continue;
                    }
                    new_pos.x = origin_pos.x + i;
                    new_pos.y = origin_pos.y + j;
                    new_pos.z = origin_pos.z + k;
                    // GameObject go = GameObject.Instantiate(Biom_Manager.instance.block_prefabs_SO.get_prefab(temp_index), new Vector3(origin_pos.x + i, origin_pos.y + j, origin_pos.z + k), Quaternion.identity);
                    GameObject go = Biom_Manager.instance.pool_get(temp_index, new_pos, Quaternion.identity);
 
                    go.transform.SetParent(transform);
                    //go.GetComponent<Block_Node_TG>();                    
                    block_data[i, j, k].set_gameobject(go); */                   
                                    
                }
                
            }
            
        }
        yield return null;
        //check_open_and_show_all();
    }
  
    public IEnumerator request_pool_blocks_co()
    {
        is_active = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        Vector3 new_pos = new Vector3();
        //float start_time = Time.time;
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j < chunk_size; j++)
            {
                for (int k = 0; k < chunk_size; k++)
                {

                    /*Item_ID_TG temp_index = block_data[i, j, k].id;
                    if (temp_index == Item_ID_TG.None)
                    {
                        continue;
                    }
                    new_pos.x = origin_pos.x + i;
                    new_pos.y = origin_pos.y + j;
                    new_pos.z = origin_pos.z + k;
                    // GameObject go = GameObject.Instantiate(Biom_Manager.instance.block_prefabs_SO.get_prefab(temp_index), new Vector3(origin_pos.x + i, origin_pos.y + j, origin_pos.z + k), Quaternion.identity);
                    GameObject go = Biom_Manager.instance.pool_get(temp_index, new_pos, Quaternion.identity);
                    go.transform.SetParent(transform);
                    block_data[i, j, k].set_gameobject(go);*/
                }
            }
            
        }

        yield return null;
        //check_open_and_show_all();        
    }
    public void check_open_and_show_all() {
        gameObject.SetActive(true);
        is_active = true;
        is_open_checked = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        Vector3 new_pos = new Vector3();
        for (int i = 0; i < block_data.GetLength(0); i++)
        {
            for (int j = 0; j < block_data.GetLength(1); j++)
            {
                for (int k = 0; k < block_data.GetLength(2); k++)
                {
                    if (block_data[i,j,k] != null && block_data[i, j, k].id != Item_ID_TG.None )
                    {
                        if (is_open(i, j, k)) {
                            new_pos.x = origin_pos.x + i;
                            new_pos.y = origin_pos.y + j;
                            new_pos.z = origin_pos.z + k;
                            GameObject go = Biom_Manager.instance.pool_get(block_data[i, j, k].id, new_pos, Quaternion.identity);
                            //go.transform.SetParent(transform);
                            block_data[i, j, k].set_gameobject(go);
                            block_data[i, j, k].show();
                        }
                        else
                        {
                            block_data[i, j, k].hide();
                        }
                    }
                                
                }
            }
        }
        
        
    }
    public IEnumerator check_open_and_show_all_co()
    {
        gameObject.SetActive(true);
        is_active = true;
        is_open_checked = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        Vector3 new_pos = new Vector3();
        for (int i = 0; i < block_data.GetLength(0); i++)
        {
            for (int j = 0; j < block_data.GetLength(1); j++)
            {
                for (int k = 0; k < block_data.GetLength(2); k++)
                {
                    if (block_data[i, j, k] != null && block_data[i, j, k].id != Item_ID_TG.None)
                    {
                        if (is_open(i, j, k))
                        {
                            new_pos.x = origin_pos.x + i;
                            new_pos.y = origin_pos.y + j;
                            new_pos.z = origin_pos.z + k;
                            GameObject go = Biom_Manager.instance.pool_get(block_data[i, j, k].id, new_pos, Quaternion.identity);
                            //go.transform.SetParent(transform);
                            block_data[i, j, k].set_gameobject(go);
                            block_data[i, j, k].show();
                        }
                        else
                        {
                            block_data[i, j, k].hide();
                        }
                    }

                }
            }            
        }
        yield return null;
    }
    public void destory_and_show_adjacents(int x, int y, int z) {
        int[] dir = { -1, 1 };
        block_data[x, y, z] = null;
        Block_Node_TG bn;
        for (int i_ = 0; i_ < dir.Length; i_++)
        {
            int i = dir[i_];
            if (is_in_range(x + i, y, z))
            {
                if (block_data[x + i, y, z] != null)
                {
                    block_data[x + i, y, z].show();
                }
            }
            else {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x + i, y, z));
                if (bn != null) {
                    bn.show();
                }
            }
            if (is_in_range(x , y + i, z) )
            {
                if (block_data[x, y + i, z] != null) {
                    block_data[x, y + i, z].show();
                }
                
            }
            else
            {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x, y + i, z));
                if (bn != null)
                {
                    bn.show();
                }
            }
            if (is_in_range(x , y, z + i) )
            {
                if (block_data[x, y, z + i] != null) {
                    block_data[x, y, z + i].show();
                }                
            }
            else
            {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x, y, z + i));
                if (bn != null)
                {
                    bn.show();
                }
            }
        }
    }

    public void set_and_hide_adjacents(int x, int y, int z, Block_Node_TG bn)
    {
        int[] dir = { -1, 1 };
        block_data[x, y, z] = bn;
        for (int i_ = 0; i_ < dir.Length; i_++)
        {
            int i = dir[i_];
            if (is_in_range(x + i, y , z) && block_data[x + i, y, z ] != null && is_open(x + i, y , z))
            {
                block_data[x + i, y , z].hide();
            }
            if (is_in_range(x , y + i, z) && block_data[x , y + i, z] != null && is_open(x, y + i, z))
            {
                block_data[x , y + i, z].hide();
            }
            if (is_in_range(x , y, z + i) && block_data[x , y, z + i] != null && is_open(x , y, z + i))
            {
                block_data[x, y, z + i].hide();
            }
        }
    }

    public bool is_open(int x, int y, int z) {
        int[] dir = {-1,  1 };
        if (block_data[x,y,z].open_flag != 0) {
            return block_data[x, y, z].open_flag>0;
        }
        for (int i_ = 0; i_ < dir.Length; i_++)
        {
            int i = dir[i_];
            Block_Node_TG bn;
            if (!is_in_range(x + i, y, z)) {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x + i, y, z));
                if (bn == null || bn.id == Item_ID_TG.None || !bn.is_blocking) {
                    return true;
                }
            }
            else if (block_data[x + i, y, z].id == Item_ID_TG.None || !block_data[x + i, y, z].is_blocking)
            {
                return true;
            }

            if (!is_in_range(x, y + i, z))
            {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x, y + i, z));
                if (bn == null || bn.id == Item_ID_TG.None || !bn.is_blocking)
                {
                    return true;
                }
            }
            else if (block_data[x, y + i, z].id == Item_ID_TG.None || !block_data[x, y + i, z].is_blocking)
            {
                return true;
            }
            if (!is_in_range(x, y, z + i))
            {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x, y, z + i));
                if (bn == null || bn.id == Item_ID_TG.None || !bn.is_blocking)
                {
                    return true;
                }
            }
            else if( block_data[x , y, z + i].id == Item_ID_TG.None || !block_data[x, y, z + i].is_blocking)
            {
                return true;
            }

        }
        return false;
    }

    private bool is_in_range(int x, int y, int z) {
        if (x < 0 || x >= block_data.GetLength(0) || y < 0 || y >= block_data.GetLength(1) || z < 0 || z >= block_data.GetLength(2))
        {
            return false;
        }
        return true;
    }

    private Item_ID_TG get_prefabs_index(int x, int y, int z) {
        int world_y = Biom_Manager.instance.chunk2world_pos_int(chunk_pos).y + y;
        if (world_y == -1)
        {
            return Item_ID_TG.dirt;
        }
        else if (world_y < -1)
        {
            Item_ID_TG temp_id = Item_ID_TG.dirt;
            /*if (Random.Range(0, 3) > 1 )
            {
                return Item_ID_TG.None;
            }*/
            return temp_id;
        }
        else if (world_y < -5)
        {
            Item_ID_TG temp_id = Item_ID_TG.stone;
            if (Random.Range(0, 7) > 1 )
            {
                return Item_ID_TG.coal;
            }
            return temp_id;
        }

        int h = Biom_Manager.instance.get_mountain_height(chunk_pos, new Vector3Int(x,y,z));
        if (h > world_y) {
            return Item_ID_TG.dirt;
        } else if (h == world_y) {
            return Item_ID_TG.grass;
        }

        return Item_ID_TG.None;
    }

    public void pool_back_all()
    {
        
        is_active = false;
        is_open_checked = false;
        for (int i = 0; i < block_data.GetLength(0); i++)
        {
            for (int j = 0; j < block_data.GetLength(1); j++)
            {
                for (int k = 0; k < block_data.GetLength(2); k++)
                {
                    if (block_data[i, j, k] != null && block_data[i, j, k].gameObject != null)
                    {
                        Biom_Manager.instance.pool_return(block_data[i, j, k].id, block_data[i, j, k].gameObject);
                        block_data[i, j, k].remove_gameobject();
                    }
                }
            }
        }
        gameObject.SetActive(false);
    }
   
}
