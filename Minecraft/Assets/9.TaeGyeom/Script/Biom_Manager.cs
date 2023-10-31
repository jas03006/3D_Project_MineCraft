using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biom_Manager : MonoBehaviour
{
    public static Biom_Manager instance = null;
    [SerializeField] private Vector3Int start_pos = Vector3Int.zero;
    [SerializeField] public int chunk_size;
    [SerializeField] public List<GameObject> block_prefab_list;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        update_start_pos();
    }

    private void Start()
    {
        generate_start_map();
    }
    private void update_start_pos() {
        //Vector3Int temp_pos = new Vector3Int(start_pos.x, start_pos.y-1,start_pos.z);
        //start_pos = temp_pos;
    }

    private void generate_start_map() {
        Chunk_TG new_chunk = new Chunk_TG(chunk_size);
        new_chunk.generate_blocks(start_pos, block_prefab_list);
    }
    

    public Vector3 chunk2world_pos(Vector3Int chunk_pos) {
        return chunk_pos * chunk_size;
    }
    public Vector3Int chunk2world_pos_int(Vector3Int chunk_pos)
    {
        return chunk_pos * chunk_size;
    }

    public Vector3Int world2chunk_pos(Vector3 world_pos)
    {
        Vector3Int pos = new Vector3Int((int)world_pos.x / chunk_size, (int)world_pos.y / chunk_size, (int)world_pos.z / chunk_size);
        return pos;
    }

}
