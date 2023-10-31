using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_TG : MonoBehaviour
{
    public int chunk_size;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Chunk_TG(int chunk_size_) {
        chunk_size = chunk_size_;
    }
    public void generate_blocks(Vector3Int chunk_pos, List<GameObject> block_prefab_list)
    {
        Vector3Int origin_pos = Biom_Manager.instance.chunk2world_pos_int(chunk_pos);
        for (int i = origin_pos.x; i < origin_pos.x + chunk_size; i++)
        {
            for (int j = origin_pos.y; j < origin_pos.y + chunk_size; j++)
            {
                for (int k = origin_pos.z; k < origin_pos.z + chunk_size; k++)
                {
                    Instantiate(block_prefab_list[0], new Vector3(i,j,k), Quaternion.identity);
                }
            }
        }
    }
}
