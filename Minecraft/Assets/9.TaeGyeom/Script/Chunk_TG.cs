using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_TG: MonoBehaviour
{
    public int chunk_size;
    public Block_Node_TG[,,] block_data;
    public Vector3Int chunk_pos;
    // Start is called before the first frame update
    
    public void init(int chunk_size_, Vector3Int chunk_pos_)
    {
        chunk_pos = chunk_pos_;
        chunk_size = chunk_size_;
        block_data = new Block_Node_TG[chunk_size, chunk_size, chunk_size];
    }
    public void generate_blocks()
    {
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j <  chunk_size; j++)
            {
                for (int k =0; k < chunk_size; k++)
                {
                    int temp_index = get_prefabs_index(i,j,k);
                    if (temp_index == -1) {
                        continue;
                    }
                    GameObject go = GameObject.Instantiate(Biom_Manager.instance.block_prefab_list[temp_index], new Vector3(origin_pos.x+i, origin_pos.y+j, origin_pos.z+ k), Quaternion.identity);
                    go.transform.SetParent(transform);
                    block_data[i, j , k] = go.GetComponent<Block_Node_TG>();
                    block_data[i, j, k].set_local_pos(i,j,k);
                }
            }
        }
        check_open_and_show_all();
    }

    private void check_open_and_show_all() {
        for (int i = 0; i < block_data.GetLength(0); i++)
        {
            for (int j = 0; j < block_data.GetLength(1); j++)
            {
                for (int k = 0; k < block_data.GetLength(2); k++)
                {
                    if (block_data[i,j,k] != null && is_open(i, j, k))
                    {
                        block_data[i, j, k].show();
                    }/*
                    else {
                        block_data[i, j, k].hide_block();
                    }       */           
                }
            }
        }
    }

    public void destory_and_show_adjacents(int x, int y, int z) {
        int[] dir = { -1, 1 };
        block_data[x, y, z] = null;
        for (int i = 0; i < dir.Length; i++)
        {            
            if (is_in_range(x + i, y , z ) && block_data[x + i, y , z] != null)
            {
                block_data[x + i, y, z ].show();
            }
            if (is_in_range(x , y + i, z) && block_data[x , y + i, z] != null)
            {
                block_data[x , y + i, z].show();
            }
            if (is_in_range(x , y, z + i) && block_data[x , y, z + i] != null)
            {
                block_data[x, y, z + i].show();
            }
        }
    }

    public void set_and_hide_adjacents(int x, int y, int z, Block_Node_TG bn)
    {
        int[] dir = { -1, 1 };
        block_data[x, y, z] = bn;
        for (int i = 0; i < dir.Length; i++)
        {
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
        for (int i = 0; i < dir.Length; i++)
        {
            if (!is_in_range(x + i, y, z) || block_data[x + i, y, z] == null || !block_data[x + i, y, z].is_blocking)
            {
                return true;
            }
            if (!is_in_range(x, y + i, z) || block_data[x, y+i, z] == null || !block_data[x, y+i, z].is_blocking)
            {
                return true;
            }
            if (!is_in_range(x, y, z + i) || block_data[x , y, z + i] == null || !block_data[x, y, z + i].is_blocking)
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

    private int get_prefabs_index(int x, int y, int z) {
        if (Biom_Manager.instance.chunk2world_pos_int(chunk_pos).y  + y >= 0) {
            return -1;
        }
        return 0;
    }

}
