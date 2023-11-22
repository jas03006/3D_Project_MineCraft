using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_TG : MonoBehaviour
{
    public int chunk_size;
    public Block_Node_TG[,,] block_data;
    public Vector3Int chunk_pos;
    public bool is_open_checked = false;
    public bool is_active = true;
    public List<Cave_Point> valid_cave_point_list;
    public List<Village_Point> valid_village_point_list;

    // Start is called before the first frame update

    public void init(int chunk_size_, Vector3Int chunk_pos_)
    {
        chunk_pos = chunk_pos_;
        chunk_size = chunk_size_;
        block_data = new Block_Node_TG[chunk_size, chunk_size, chunk_size];
        is_open_checked = false;
        Biom_Manager.instance.get_valid_cave_points(chunk_pos, ref valid_cave_point_list);
        Biom_Manager.instance.get_valid_village_points(chunk_pos, ref valid_village_point_list);
    }
    public void generate_blocks()
    {
        is_active = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j < chunk_size; j++)
            {
                for (int k = 0; k < chunk_size; k++)
                {
                    Item_ID_TG temp_index = get_prefabs_index(i, j, k);
                    if (block_data[i, j, k] == null) {
                        block_data[i, j, k] = Biom_Manager.instance.get_block_node();//new Block_Node_TG();
                        block_data[i, j, k].set_local_pos(i, j, k);
                        block_data[i, j, k].chunk = this;
                        block_data[i, j, k].id = temp_index;
                    }
                        
                }

            }
        }
        decide_mineral_blocks();
        // check_open_and_show_all();
    }

    public void generate_block_nodes()
    {
        is_active = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        //Vector3 new_pos = new Vector3();
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
                    block_data[i, j, k].chunk = this;
                }
            }

        }
        decide_mineral_blocks();
    }
    public IEnumerator generate_blocks_co()
    {
        is_active = true;
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
       // Vector3 new_pos = new Vector3();
        //float start_time = Time.time;
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j < chunk_size; j++)
            {
                for (int k = 0; k < chunk_size; k++)
                {

                    Item_ID_TG temp_index = get_prefabs_index(i, j, k);
                    if (block_data[i, j, k] == null) {
                        block_data[i, j, k] = Biom_Manager.instance.get_block_node();//new Block_Node_TG();
                        block_data[i, j, k].set_local_pos(i, j, k);
                        block_data[i, j, k].chunk = this;
                        block_data[i, j, k].id = temp_index;
                    }
                    

                }

            }

        }
        decide_mineral_blocks();
        
        yield return null;
        //check_open_and_show_all();
    }


    public void check_open_and_show_all()
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
                    Block_Node_TG bn = block_data[i, j, k];
                    if (bn != null && bn.id != Item_ID_TG.None)
                    {
                        if (is_open(i, j, k))
                        {
                            if (block_data[i, j, k].gameObject == null)
                            {
                                new_pos.x = origin_pos.x + i;
                                new_pos.y = origin_pos.y + j;
                                new_pos.z = origin_pos.z + k;
                                GameObject go = Biom_Manager.instance.pool_get(block_data[i, j, k].id, new_pos, block_data[i, j, k].rotation);//Quaternion.identity);
                                //go.transform.SetParent(transform);
                                bn.set_gameobject(go);
                            }
                            bn.show();
                        }
                        else
                        {
                            bn.hide();
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
                            if (block_data[i, j, k].gameObject == null)
                            {
                                new_pos.x = origin_pos.x + i;
                                new_pos.y = origin_pos.y + j;
                                new_pos.z = origin_pos.z + k;
                                GameObject go = Biom_Manager.instance.pool_get(block_data[i, j, k].id, new_pos, Quaternion.identity);
                                //go.transform.SetParent(transform);
                                block_data[i, j, k].set_gameobject(go);
                            }
                            block_data[i, j, k].show();
                        }
                        /*else
                        {
                            block_data[i, j, k].hide();
                        }*/
                    }

                }
            }
        }        
        yield return null;
    }
    public void destory_and_show_adjacents(int x, int y, int z)
    {

        Block_Node_TG bn = block_data[x, y, z];
        if (bn.id == Item_ID_TG.None)
        {
            return;

        }

        if (bn.gameObject != null)
        {
            bn.hide();
            Biom_Manager.instance.pool_return(bn.id, bn.gameObject);

        }
        bn.id = Item_ID_TG.None;
        bn.remove_gameobject();


        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        Vector3 new_pos = new Vector3();
        int[] dir = { -1, 1 };
        //block_data[x, y, z] = null;

        for (int i_ = 0; i_ < dir.Length; i_++)
        {
            int i = dir[i_];

            new_pos.x = origin_pos.x + x + i;
            new_pos.y = origin_pos.y + y;
            new_pos.z = origin_pos.z + z;
            bn = null;
            if (is_in_range(x + i, y, z))
            {
                bn = block_data[x + i, y, z];
            }
            else
            {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x + i, y, z));
            }
            if (bn != null && bn.id != Item_ID_TG.None)
            {
                if (bn.gameObject == null)
                {
                    GameObject go = Biom_Manager.instance.pool_get(bn.id, new_pos, bn.rotation);
                    bn.set_gameobject(go);
                }
                bn.show();
            }

            new_pos.x = origin_pos.x + x;
            new_pos.y = origin_pos.y + y + i;
            new_pos.z = origin_pos.z + z;
            bn = null;
            if (is_in_range(x, y + i, z))
            {
                bn = block_data[x, y + i, z];
            }
            else
            {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x, y + i, z));
            }
            if (bn != null && bn.id != Item_ID_TG.None)
            {
                if (bn.gameObject == null)
                {
                    GameObject go = Biom_Manager.instance.pool_get(bn.id, new_pos, bn.rotation);
                    bn.set_gameobject(go);
                }
                bn.show();
            }

            new_pos.x = origin_pos.x + x;
            new_pos.y = origin_pos.y + y;
            new_pos.z = origin_pos.z + z + i;
            bn = null;
            if (is_in_range(x, y, z + i))
            {
                bn = block_data[x, y, z + i];
            }
            else
            {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x, y, z + i));
            }
            if (bn != null && bn.id != Item_ID_TG.None)
            {
                if (bn.gameObject == null)
                {
                    GameObject go = Biom_Manager.instance.pool_get(bn.id, new_pos, bn.rotation);
                    bn.set_gameobject(go);
                }
                bn.show();
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
            if (is_in_range(x + i, y, z) && block_data[x + i, y, z] != null && !is_open(x + i, y, z))
            {
                block_data[x + i, y, z].hide();
            }
            if (is_in_range(x, y + i, z) && block_data[x, y + i, z] != null && !is_open(x, y + i, z))
            {
                block_data[x, y + i, z].hide();
            }
            if (is_in_range(x, y, z + i) && block_data[x, y, z + i] != null && !is_open(x, y, z + i))
            {
                block_data[x, y, z + i].hide();
            }
        }
    }
    public void set_and_hide_adjacents(Block_Node_TG bn)
    {
        if (!bn.is_blocking) {
            return;
        }
        int[] dir = { -1, 1 };
        int x = bn.local_pos_in_chunk.x;
        int y = bn.local_pos_in_chunk.y;
        int z = bn.local_pos_in_chunk.z;
        for (int i_ = 0; i_ < dir.Length; i_++)
        {
            int i = dir[i_];
            if (is_in_range(x + i, y, z) && block_data[x + i, y, z] != null && !is_open(x + i, y, z))
            {
                block_data[x + i, y, z].hide();
            }
            if (is_in_range(x, y + i, z) && block_data[x, y + i, z] != null && !is_open(x, y + i, z))
            {
                block_data[x, y + i, z].hide();
            }
            if (is_in_range(x, y, z + i) && block_data[x, y, z + i] != null && !is_open(x, y, z + i))
            {
                block_data[x, y, z + i].hide();
            }
        }
    }
    public void set_block(Item_ID_TG id, Vector3Int block_pos, Vector3 world_pos, Quaternion rotate, List<Vector3Int> space = null) {
        Block_Node_TG bn = block_data[block_pos.x, block_pos.y, block_pos.z];
        if (bn.id == Item_ID_TG.None) {
            bn.set_block(id, world_pos, rotate, space);
            set_and_hide_adjacents(bn);
        }        
    }
    public void change_block(Item_ID_TG id, Vector3Int block_pos, Vector3 world_pos, Quaternion rotate, List<Vector3Int> space = null)
    {
        Block_Node_TG bn = block_data[block_pos.x, block_pos.y, block_pos.z];
        bn.set_block(id, world_pos, rotate, space);
        set_and_hide_adjacents(bn);
    }

    public bool is_open(int x, int y, int z)
    {
        int[] dir = { -1, 1 };
        if (block_data[x, y, z].open_flag != 0)
        {
            return block_data[x, y, z].open_flag > 0;
        }
        for (int i_ = 0; i_ < dir.Length; i_++)
        {
            int i = dir[i_];
            Block_Node_TG bn;
            if (!is_in_range(x + i, y, z))
            {
                bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x + i, y, z));
                if (bn == null || bn.id == Item_ID_TG.None || !bn.is_blocking)
                {
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
            else if (block_data[x, y, z + i].id == Item_ID_TG.None || !block_data[x, y, z + i].is_blocking)
            {
                return true;
            }

        }
        return false;
    }

    private bool is_in_range(int x, int y, int z)
    {
        if (x < 0 || x >= block_data.GetLength(0) || y < 0 || y >= block_data.GetLength(1) || z < 0 || z >= block_data.GetLength(2))
        {
            return false;
        }
        return true;
    }

    private void decide_mineral_one_kind(Item_ID_TG id, int min_cnt, int max_cnt) {
        int[] dir = { -1, 1 };
        int x, y, z;
        int mineral_num = Random.Range(min_cnt, max_cnt);
        for (int i = 0; i < mineral_num; i++)
        {
            x = Random.Range(1, chunk_size - 1);
            y = Random.Range(1, chunk_size - 1);
            z = Random.Range(1, chunk_size - 1);
            Vector3Int block_world_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos) + new Vector3Int(x, y, z);
            if (block_data[x, y, z].id != Item_ID_TG.None && Biom_Manager.instance.get_mountain_height(block_world_pos) -5 > block_world_pos.y)
            {
                block_data[x, y, z].id = id;
                for (int dir_ind = 0; dir_ind < dir.Length; dir_ind++)
                {
                    if (Random.Range(0, 4) < 3)
                    {
                        block_data[x + dir[dir_ind], y, z].id = id;
                    }
                    if (Random.Range(0, 4) < 3)
                    {
                        block_data[x, y + dir[dir_ind], z].id = id;
                    }
                    if (Random.Range(0, 4) < 3)
                    {
                        block_data[x, y, z + dir[dir_ind]].id = id;
                    }
                }
            }
        }
    }
    private void decide_mineral_blocks() {
        int[] dir = { -1, 1 };
        int x, y, z;
        if (Biom_Manager.instance.get_mountain_height(chunk_pos, Vector3Int.zero)+5 <= Biom_Manager.instance.chunk2world_pos(chunk_pos).y) {
            return;
        }
        decide_mineral_one_kind(Item_ID_TG.coal, 8, 12);
        decide_mineral_one_kind(Item_ID_TG.iron, 4, 8);
        
        if (chunk_pos.y < 0)
        {
            decide_mineral_one_kind(Item_ID_TG.diamond, 1, 2);
        }
    }

private Item_ID_TG get_prefabs_index(int x, int y, int z) {
        
        if (block_data[x, y, z] != null && block_data[x,y,z].id != Item_ID_TG.None && block_data[x, y, z].id != Item_ID_TG.leaf) {
            return block_data[x, y, z].id;
        }

        Vector3Int block_pos = new Vector3Int(x, y, z);
        Vector3Int block_world_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos) + block_pos;
        int world_y = block_world_pos.y;
        
        if (is_in_cave(block_world_pos)) {
            return Item_ID_TG.None;
        }

        //int h = Biom_Manager.instance.get_mountain_height(chunk_pos, block_pos);
        int h = Biom_Manager.instance.get_mountain_height(block_world_pos);
        if (h > world_y)
        {
            if (world_y == h - 5)
            {
                if (Random.Range(0, 3) < 1)
                {
                    return Item_ID_TG.dirt;
                }
                return Item_ID_TG.stone;
            }
            if (world_y < h - 5)
            {
                return Item_ID_TG.stone;
            }

            if (world_y <= h - 1)
            {
                return Item_ID_TG.dirt;
            }
            return Item_ID_TG.dirt;
        }
        else if (h == world_y)
        {
            return Item_ID_TG.grass;
        }
        else {
            if (h + 1 == world_y)
            {
                if (x > 3 && x < 13 && z > 3 && z < 13 && y < chunk_size - 8) {
                    Block_Node_TG bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x, y - 1, z));
                    if (bn != null && (bn.id == Item_ID_TG.dirt || bn.id == Item_ID_TG.grass) )
                    {
                        float decide_flag = Random.Range(0f, 1f);
                        Village_Point vp = find_village_point(block_world_pos);
                        if (vp !=null && decide_flag < 0.1f && build_house(x, y, z, vp.position - block_world_pos)) {
                            return Item_ID_TG.board;
                        }else if (decide_flag < 0.133f) {
                            // 唱公 关 扁嫡 积己
                            return Item_ID_TG.tree;
                        }                       
                    }
                }                
            }
            else if (h + 5 >= world_y)
            {
                Block_Node_TG bn;
                if (!is_in_range(x, y - 1, z))
                {
                    bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x, y - 1, z));
                }
                else {
                    bn = block_data[x, y - 1, z];
                }
                if (bn!= null && bn.id == Item_ID_TG.tree)
                {
                    if (h + 5 != world_y && Random.Range(0, 10) < 8)
                    {
                        return Item_ID_TG.tree;
                    }
                    else {
                        int leaf_width = (world_y-h)/3 +1;
                        for (int i =0; i<4; i++) {
                            for (int j = -leaf_width + i; j < leaf_width +1- i; j++) {
                                for (int k= -leaf_width + i; k < leaf_width+1 - i; k++)
                                {
                                    if (is_in_range(x + j, y + i, z + k))
                                    {
                                        if (block_data[x + j, y + i, z + k] == null)
                                        {
                                            block_data[x + j, y + i, z + k] = Biom_Manager.instance.get_block_node();
                                            block_data[x + j, y + i, z + k].chunk = this;
                                            block_data[x + j, y + i, z + k].set_local_pos(x + j, y + i, z + k);
                                            block_data[x + j, y + i, z + k].id = Item_ID_TG.leaf;
                                        }
                                        else if(block_data[x + j, y + i, z + k].id == Item_ID_TG.None)
                                        {
                                            block_data[x + j, y + i, z + k].id = Item_ID_TG.leaf;
                                        }
                                    }
                                    else {
                                        bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x + j, y + i, z + k));
                                        if (bn != null)
                                        {
                                            bn.id = Item_ID_TG.leaf;
                                        }                      
                                    }                                    
                                }
                            }                            
                        }
                        return Item_ID_TG.leaf;
                    }
                }
            }
        }

        return Item_ID_TG.None;
    }
    private bool build_house(int x, int y, int z, Vector3 forward_dir) {
        Block_Node_TG bn;
        int size_ = 5;
        int height_ = 5;
        for (int j = 0; j < height_; j++)
        {
            for (int i = 0; i < size_+1 ; i++)
            {
                for (int k = 0; k < size_+1; k++)
                {
                    if (is_in_range(x + i, y + j, z + k))
                    {
                        bn = block_data[x + i, y + j, z + k];
                        //bn = Biom_Manager.instance.get_block(chunk_pos, new Vector3Int(x + i, y + j, z + k));
                        
                        if (bn != null && (bn.id == Item_ID_TG.board || bn.id == Item_ID_TG.tree))
                        {
                            return false;
                        }
                    }
                    else {
                        return false;
                    }
                        
                                    
                }
            }
        }        

        for (int j = 0; j < height_; j++)
        {
            for (int i = 0; i < size_ ; i++)
            {
                for (int k = 0; k < size_; k++)
                {
                    bn = block_data[x + i, y + j, z + k];
                    if (bn == null)
                    {
                        bn = Biom_Manager.instance.get_block_node();
                        bn.chunk = this;
                        bn.set_local_pos(x + i, y + j, z + k);
                        block_data[x + i, y + j, z + k] = bn;
                    }
                    /*if (j == size_ || i < 0 || i >= size_  || (j == size_-1 && (i==0 || i == size_-1)))
                    {
                        bn.id = Item_ID_TG.board;
                    }
                    else {*/
                        bn.id = Item_ID_TG.board;
                    //}                    
                }
            }
        }

        
        for (int j = 1; j < height_-1; j++)
        {
            for (int i = 1; i < size_ - 1; i++)
            {
                for (int k = 1; k < size_-1; k++)
                {
                    block_data[x + i, y + j, z + k].id = Item_ID_TG.None;
                }
            }
        }

        Quaternion rot_;
        Vector3 furniture_pos = new Vector3(1, 0, 1);
        Vector3 pivot_pos;
        if (Mathf.Abs(forward_dir.x) > Mathf.Abs(forward_dir.z))
        {
            
            if (forward_dir.x < 0)
            {
                rot_ = Quaternion.Euler(0, -90f, 0); //Quaternion.FromToRotation(Vector3.forward, Vector3.left); //Quaternion.LookRotation(Vector3.left, Vector3.up);
                pivot_pos = new Vector3(x, y, z+ size_ - 1);
            }
            else
            {
                rot_ = Quaternion.Euler(0, 90f, 0); //Quaternion.LookRotation(Vector3.right, Vector3.up);
                pivot_pos = new Vector3(x + size_ - 1, y, z);
            }
        }
        else
        {
            if (forward_dir.z < 0)
            {
                rot_ = Quaternion.Euler(0,180f,0); //Quaternion.LookRotation(Vector3.back, Vector3.up);
                pivot_pos = new Vector3(x, y, z);                
            }
            else
            {
                rot_ = Quaternion.identity;//Quaternion.FromToRotation(Vector3.forward, Vector3.forward); //Quaternion.LookRotation(Vector3.forward, Vector3.up);                
                pivot_pos = new Vector3(x+size_ - 1, y, z+size_ - 1);               
            }
        }

        furniture_pos.y = 1;
        Vector3Int temp_pos = Vector3Int.zero;
        Vector3 space_;

        furniture_pos.x = -(size_ - 1)/2;
        furniture_pos.z = 0;
        furniture_pos = rot_ * furniture_pos;        
        temp_pos.x = Mathf.RoundToInt(pivot_pos.x + furniture_pos.x);
        temp_pos.y = Mathf.RoundToInt(pivot_pos.y + furniture_pos.y);
        temp_pos.z = Mathf.RoundToInt(pivot_pos.z + furniture_pos.z);
        block_data[temp_pos.x, temp_pos.y + 1, temp_pos.z].id = Item_ID_TG.None;
        bn = block_data[temp_pos.x, temp_pos.y, temp_pos.z];
        bn.set_block(Item_ID_TG.door, Biom_Manager.instance.chunk2world_pos_int(chunk_pos) + temp_pos, rot_, new List<Vector3Int> { new Vector3Int(0, 1, 0) });

        furniture_pos.x = -(size_ - 2);
        furniture_pos.z = -3;
        furniture_pos = rot_ * furniture_pos;
        temp_pos.x = Mathf.RoundToInt(pivot_pos.x + furniture_pos.x);
        temp_pos.y = Mathf.RoundToInt(pivot_pos.y + furniture_pos.y);
        temp_pos.z = Mathf.RoundToInt(pivot_pos.z + furniture_pos.z);
        bn = block_data[temp_pos.x, temp_pos.y, temp_pos.z];
        space_ = rot_ * new Vector3Int(0, 0, 1);
        bn.set_block(Item_ID_TG.bed, Biom_Manager.instance.chunk2world_pos_int(chunk_pos) + temp_pos, rot_, new List<Vector3Int> { new Vector3Int((int)space_.x, 0, (int)space_.y) });


        furniture_pos.x = -1;
        furniture_pos.z = -2;
        furniture_pos = rot_ * furniture_pos;
        temp_pos.x = Mathf.RoundToInt(pivot_pos.x + furniture_pos.x);
        temp_pos.y = Mathf.RoundToInt(pivot_pos.y + furniture_pos.y);
        temp_pos.z = Mathf.RoundToInt(pivot_pos.z + furniture_pos.z);
        bn = block_data[temp_pos.x, temp_pos.y, temp_pos.z];
        bn.set_block(Item_ID_TG.box, Biom_Manager.instance.chunk2world_pos_int(chunk_pos) + temp_pos, rot_*Quaternion.Euler(0, -90f, 0));

        furniture_pos.x = -1;
        furniture_pos.z = -3;
        furniture_pos = rot_ * furniture_pos;
        temp_pos.x = Mathf.RoundToInt(pivot_pos.x + furniture_pos.x);
        temp_pos.y = Mathf.RoundToInt(pivot_pos.y + furniture_pos.y);
        temp_pos.z = Mathf.RoundToInt(pivot_pos.z + furniture_pos.z);
        bn = block_data[temp_pos.x, temp_pos.y, temp_pos.z];
        bn.set_block(Item_ID_TG.furnace, Biom_Manager.instance.chunk2world_pos_int(chunk_pos) + temp_pos, rot_*Quaternion.Euler(0, -90f, 0));


        /*block_data[x+(size_-1)/2, y+2, z + size_ - 1].id = Item_ID_TG.None;
        bn = block_data[x + (size_ - 1) / 2, y + 1, z + size_ - 1];
        bn.set_block(Item_ID_TG.door, Biom_Manager.instance.chunk2world_pos_int(chunk_pos)+ new Vector3Int(x + (size_ - 1) / 2, y + 1, z + size_ - 1),Quaternion.identity,new List<Vector3Int> { new Vector3Int(0,1,0)});
        
        bn = block_data[x + (size_ -2), y + 1, z + 1];
        bn.set_block(Item_ID_TG.bed, Biom_Manager.instance.chunk2world_pos_int(chunk_pos)+ new Vector3Int(x + (size_ - 2), y + 1, z + 1),Quaternion.identity,new List<Vector3Int> { new Vector3Int(0,0,1)});
        
        bn = block_data[x + 1, y + 1, z + 1];
        bn.set_block(Item_ID_TG.box, Biom_Manager.instance.chunk2world_pos_int(chunk_pos) + new Vector3Int(x + 1, y + 1, z + 1), Quaternion.Euler(0,90f,0));
        
        bn = block_data[x + 1, y + 1, z + 2];
        bn.set_block(Item_ID_TG.furnace, Biom_Manager.instance.chunk2world_pos_int(chunk_pos) + new Vector3Int(x + 1, y + 1, z + 2), Quaternion.Euler(0, 90f, 0));
        */

        return true;

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

    private bool is_in_cave(Vector3 block_world_pos) {        
        if (valid_cave_point_list != null  ) {
            for (int i = 0; i < valid_cave_point_list.Count; i++) {
                if ((block_world_pos - valid_cave_point_list[i].position).sqrMagnitude <= (valid_cave_point_list[i].radius+1) * (valid_cave_point_list[i].radius+1)) {
                    return true;
                }
            }            
        }
        return false;
    }
    private bool is_in_village(Vector3 block_world_pos)
    {
        if (valid_village_point_list != null)
        {
            for (int i = 0; i < valid_village_point_list.Count; i++)
            {
                if (valid_village_point_list[i].is_inner(block_world_pos))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private Village_Point find_village_point(Vector3 block_world_pos)
    {
        if (valid_village_point_list != null)
        {
            for (int i = 0; i < valid_village_point_list.Count; i++)
            {
                if (valid_village_point_list[i].is_inner(block_world_pos))
                {
                    return valid_village_point_list[i];
                }
            }
        }
        return null;
    }
}
