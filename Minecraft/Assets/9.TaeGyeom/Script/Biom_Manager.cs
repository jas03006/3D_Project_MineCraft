using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biom_Manager : MonoBehaviour
{
    public static Biom_Manager instance = null;
    [SerializeField] private Vector3Int start_chunk_pos = Vector3Int.zero;
    [SerializeField] public int chunk_size;
    [SerializeField] public int render_distance;
    [SerializeField] public List<GameObject> block_prefab_list;
    [SerializeField] private GameObject player;
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
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            start_chunk_pos = world2chunk_pos(player.transform.position);
        }
        
    }

    private void generate_start_map() {
        int render_chunk_num = render_distance / chunk_size;
        Vector3Int now_chunk_pos = Vector3Int.zero;
        for (int i = start_chunk_pos.x - render_chunk_num; i < start_chunk_pos.x + render_chunk_num; i++) {
            for (int j = start_chunk_pos.y - 1; j < start_chunk_pos.y; j++)
            {
                for (int k = start_chunk_pos.z- render_chunk_num; k < start_chunk_pos.z + render_chunk_num; k++)
                {
                    Chunk_TG new_chunk = new Chunk_TG(chunk_size);
                    now_chunk_pos.x = i;
                    now_chunk_pos.y = j;
                    now_chunk_pos.z = k;
                    new_chunk.generate_blocks(now_chunk_pos);
                }
            }
        }        
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
