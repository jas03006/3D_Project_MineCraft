using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_ID_TG { 
    None = 0,
    stone= 1,
    grass,
    dirt,
    coal = 15
}

public class Biom_Manager : MonoBehaviour
{
    public static Biom_Manager instance = null;
    [SerializeField] private Vector3Int start_chunk_pos = Vector3Int.zero;
    [SerializeField] public int chunk_size;
    [SerializeField] public int render_distance;
    private int render_chunk_num = 1;
    private Vector3Int current_chunk_pos = Vector3Int.zero;
    [SerializeField] private float chunk_update_time = 1f;
    private float chunk_update_timer = 0f;
    private Coroutine now_update_co = null;
    //[SerializeField] public Dictionary<Item_ID_TG, GameObject> block_prefab_dict;
    [SerializeField] public ID2Block_TG block_prefabs_SO;
    [SerializeField] public GameObject chunk_prefab;
    [SerializeField] private GameObject player;
    private List<Vector3Int> mountain_point_list;

    private Dictionary<Vector3Int, Chunk_TG> chunk_data;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        render_chunk_num = render_distance / chunk_size;
        
        
    }

    private void Start()
    {
        
        chunk_data = new Dictionary<Vector3Int, Chunk_TG>();
        mountain_point_list = new List<Vector3Int>();       
        decide_mountain_point();
        update_start_pos();
        player.GetComponent<Player_Test_TG>().deactivate_gravity();
        generate_start_map();
        player.GetComponent<Player_Test_TG>().activate_gravity();

    }

    private void Update()
    {
        chunk_update_timer += Time.deltaTime;
        if (now_update_co == null && chunk_update_timer > chunk_update_time)
        {
            chunk_update_timer = 0f;
            current_chunk_pos = get_player_chunk_pos();            
            now_update_co = StartCoroutine(generate_map_update_co());
        }

    }
    private void update_start_pos()
    {
        //Vector3Int temp_pos = new Vector3Int(start_pos.x, start_pos.y-1,start_pos.z);
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            current_chunk_pos = get_player_chunk_pos();
            int h = get_mountain_height(current_chunk_pos, get_player_block_pos());
            current_chunk_pos.y = h / chunk_size;
            start_chunk_pos = current_chunk_pos;
            player.transform.Translate(Vector3.up * h);
        }

    }

    private void generate_start_map()
    {
        Chunk_TG new_chunk;
        Vector3Int now_chunk_pos = Vector3Int.zero;
        for (int i = start_chunk_pos.x - render_chunk_num; i < start_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = start_chunk_pos.y - 1; j < start_chunk_pos.y + 1; j++)
            {
                for (int k = start_chunk_pos.z - render_chunk_num; k < start_chunk_pos.z + render_chunk_num; k++)
                {
                    now_chunk_pos.x = i;
                    now_chunk_pos.y = j;
                    now_chunk_pos.z = k;
                    new_chunk = Instantiate(chunk_prefab, chunk2world_pos(now_chunk_pos), Quaternion.identity).GetComponent<Chunk_TG>();
                    new_chunk.transform.SetParent(transform);
                    new_chunk.init(chunk_size, now_chunk_pos);
                    chunk_data[now_chunk_pos] = new_chunk;
                    new_chunk.generate_blocks();                    
                }
            }
        }

        for (int i = current_chunk_pos.x - render_chunk_num; i < current_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = current_chunk_pos.y - 1; j < current_chunk_pos.y + 1; j++)
            {
                for (int k = current_chunk_pos.z - render_chunk_num; k < current_chunk_pos.z + render_chunk_num; k++)
                {
                    now_chunk_pos.x = i;
                    now_chunk_pos.y = j;
                    now_chunk_pos.z = k;
                    new_chunk = get_chunk(now_chunk_pos);
                    if (!new_chunk.is_open_checked)
                    {
                        new_chunk.check_open_and_show_all();
             
                    }
                }
            }
        }
    }

    private IEnumerator generate_map_update_co() {
        Vector3Int now_chunk_pos = Vector3Int.zero;
        Chunk_TG new_chunk;
        int y_render_range = (current_chunk_pos.y >=0 ? render_chunk_num: 2);
        for (int i = current_chunk_pos.x - render_chunk_num; i < current_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = current_chunk_pos.y - 1; j < current_chunk_pos.y + y_render_range; j++)
            {
                for (int k = current_chunk_pos.z - render_chunk_num; k < current_chunk_pos.z + render_chunk_num; k++)
                {
                    now_chunk_pos.x = i;
                    now_chunk_pos.y = j;
                    now_chunk_pos.z = k;
                    if (get_chunk(now_chunk_pos) == null) {
                        new_chunk = Instantiate(chunk_prefab, chunk2world_pos(now_chunk_pos), Quaternion.identity).GetComponent<Chunk_TG>();
                        new_chunk.transform.SetParent(transform);
                        new_chunk.init(chunk_size, now_chunk_pos);
                        chunk_data[now_chunk_pos] = new_chunk;
                        
                        yield return new_chunk.generate_blocks_co();
                    }                    
                }
            }
        }

        for (int i = current_chunk_pos.x - render_chunk_num; i < current_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = current_chunk_pos.y - 1; j < current_chunk_pos.y + 1; j++)
            {
                for (int k = current_chunk_pos.z - render_chunk_num; k < current_chunk_pos.z + render_chunk_num; k++)
                {
                    now_chunk_pos.x = i;
                    now_chunk_pos.y = j;
                    now_chunk_pos.z = k;
                    new_chunk = get_chunk(now_chunk_pos);
                    if (!new_chunk.is_open_checked) {
                        new_chunk.check_open_and_show_all();
                        yield return null;
                    }                       
                }
            }
        }

        now_update_co = null;
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

    public Chunk_TG get_chunk(Vector3Int chunk_pos) {
        if (chunk_data.ContainsKey(chunk_pos)) {
            return chunk_data[chunk_pos];
        }
        return null;
    }

    public Block_Node_TG get_block(Vector3Int chunk_pos, Vector3Int block_pos) {
        chunk_pos.x += (block_pos.x) / chunk_size; 
        chunk_pos.y += (block_pos.y) / chunk_size; 
        chunk_pos.z += (block_pos.z) / chunk_size;
        Chunk_TG ch = get_chunk(chunk_pos);
        if (ch == null) {
            return null;
        }
        return ch.block_data[(block_pos.x+chunk_size)%chunk_size, (block_pos.y + chunk_size) % chunk_size, (block_pos.z + chunk_size) % chunk_size];
    }

    private Vector3Int get_player_chunk_pos() {

        return world2chunk_pos(player.transform.position);
    }
    private Vector3Int get_player_block_pos()
    {

        return new Vector3Int((int)player.transform.position.x%chunk_size, (int)player.transform.position.y % chunk_size, (int)player.transform.position.z % chunk_size);
    }

    public int get_mountain_height(Vector3Int chunk_pos, Vector3Int block_pos) {
        Vector3Int temp_pos = chunk2world_pos_int(chunk_pos) + block_pos;
        temp_pos.y = 0;
        int h = 0;
        int temp_h=0;
        for (int i = 0; i < mountain_point_list.Count; i++) {
            temp_pos = mountain_point_list[i] - temp_pos;
            int d = temp_pos.x * temp_pos.x + temp_pos.z * temp_pos.z;
            if (temp_pos.y * temp_pos.y < d)
            {
                temp_h = 0;
            }
            else {
                temp_h = temp_pos.y - (int)Mathf.Sqrt(d);
                //Debug.Log(temp_h);
            }
            if (temp_h > h) {
                h = temp_h;
            }
        }
        
        return h;
    }

    private void decide_mountain_point() {
        int mountain_generate_range = 50;
        int mountain_num = 40;
        int mountain_max_height = 32;
        int mountain_min_height = 48;
        for (int i =0; i < mountain_num; i++) {
            mountain_point_list.Add(new Vector3Int(Random.Range(-mountain_generate_range, mountain_generate_range), Random.Range(mountain_min_height, mountain_max_height), Random.Range(-mountain_generate_range, mountain_generate_range)));
        }
    }
}
