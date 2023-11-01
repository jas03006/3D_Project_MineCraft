using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_TG
{
    public int chunk_size;
    public Block_Node_TG[,,] block_data;
    // Start is called before the first frame update
    public Chunk_TG(int chunk_size_) {
        chunk_size = chunk_size_;
        block_data = new Block_Node_TG[chunk_size, chunk_size, chunk_size];
    }
    public void generate_blocks(Vector3Int chunk_pos)
    {
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j <  chunk_size; j++)
            {
                for (int k =0; k < chunk_size; k++)
                {
                    GameObject go = GameObject.Instantiate(Biom_Manager.instance.block_prefab_list[0], new Vector3(origin_pos.x+i, origin_pos.y+j, origin_pos.z+ k), Quaternion.identity);
                    block_data[i, j , k] = go.GetComponent<Block_Node_TG>();
                    block_data[i, j, k].set_local_pos(i,j,k);
                }
            }
        }
        check_open_all();
    }

    private void check_open_all() {
        for (int i = 0; i < chunk_size; i++)
        {
            for (int j = 0; j < chunk_size; j++)
            {
                for (int k = 0; k < chunk_size; k++)
                {
                    if (is_open(i, j, k))
                    {
                        block_data[i, j, k].show_block();
                    }/*
                    else {
                        block_data[i, j, k].hide_block();
                    }       */           
                }
            }
        }
    }

    public bool is_open(int x, int y, int z) {
        int[] dir = {-1, 0, 1 };
        for (int i = 0; i < dir.Length; i++)
        {
            for (int j = 0; j < dir.Length; j++)
            {
                for (int k = 0; k < dir.Length; k++)
                {
                    if (i == 0 && j ==0 && k ==0) {
                        continue;
                    }
                    if(!is_in_range(x+i, y+j, z+k)){
                        return true;
                    }
                    if (!block_data[x+i,y+j,z+k].is_blocking) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool is_in_range(int x, int y, int z) {
        if (x < 0 || x >= chunk_size || y < 0 || y >= chunk_size || z < 0 || z >= chunk_size)
        {
            return false;
        }
        return true;
    }

}
