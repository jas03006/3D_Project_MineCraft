using System.Collections;
using System;
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
    [SerializeField] public int pooling_distance;
    private int render_chunk_num = 1;
    private Vector3Int current_chunk_pos = Vector3Int.zero;
    [SerializeField] private float chunk_update_time = 0.3f;
    private float chunk_update_timer = 0f;
    private Coroutine now_update_co = null;
    //[SerializeField] public Dictionary<Item_ID_TG, GameObject> block_prefab_dict;
    [SerializeField] public ID2Block_TG block_prefabs_SO;
    [SerializeField] public GameObject chunk_prefab;
    [SerializeField] private GameObject player;
    [SerializeField] Transform pool_transform;
    private List<Vector3Int> mountain_point_list;

    private Dictionary<Vector3Int, Chunk_TG> chunk_data;
    private Queue<GameObject>[] pool_list;
    private Queue<Block_Node_TG> block_ready_queue;
     private int block_ready_cnt = 150000;
     private int ready_block_generate_cnt = 10000;
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
        if (pooling_distance <=0) {
            pooling_distance = render_chunk_num;
        }
        block_ready_cnt = render_distance * render_distance * render_distance * 16;
        Debug.Log(block_ready_cnt);
        if (block_ready_cnt > 1000000) {
            block_ready_cnt = 1000000;
        }
        chunk_data = new Dictionary<Vector3Int, Chunk_TG>();
        mountain_point_list = new List<Vector3Int>();
        init_pool_list();
        init_block_ready_queue();

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
            Debug.Log("start co");
            now_update_co = StartCoroutine(generate_map_update_co());
        }

    }

    private void FixedUpdate()
    {
        generate_ready_block_nodes();
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

    private void init_block_ready_queue() {
        block_ready_queue = new Queue<Block_Node_TG>();
        for (int i =0; i < block_ready_cnt; i++) {
            block_ready_queue.Enqueue(new Block_Node_TG());
        }
    }

    public Block_Node_TG get_block_node() {
        if (block_ready_queue.Count == 0)
        {
            return new Block_Node_TG();
        }
        else {
            return block_ready_queue.Dequeue();
           
        }
    }
    private void generate_ready_block_nodes() {
        
        if (block_ready_queue.Count < block_ready_cnt - ready_block_generate_cnt) {
            //Debug.Log(block_ready_queue.Count);
            for (int i =0; i < ready_block_generate_cnt; i++) {
                block_ready_queue.Enqueue(new Block_Node_TG());
            }
        }
    }

    private void init_pool_list() {
        pool_list = new Queue<GameObject>[ Enum.GetValues(typeof(Item_ID_TG)).Length ];
        Vector3 pool_position = new Vector3(1000f, 1000f, 1000f);
        int pool_num = 5000;
        foreach (Item_ID_TG e in Enum.GetValues(typeof(Item_ID_TG)))
        {
            if (e == Item_ID_TG.None) {
                continue;
            }
            Queue<GameObject> qu = new Queue<GameObject>();
            if (e == Item_ID_TG.dirt || e == Item_ID_TG.stone)
            {
                pool_num = 200000;
            }
            for (int n =0; n < pool_num; n++) {
                GameObject go = Instantiate(block_prefabs_SO.get_prefab(e), pool_position, Quaternion.identity);
                go.SetActive(false);
                qu.Enqueue(go);
                go.transform.SetParent(pool_transform);
            }
            pool_num = 5000;
            pool_list[block_prefabs_SO.ID2index(e)] = qu;
        }
    }

    public void pool_return(Item_ID_TG id, GameObject go) {
        if (id == Item_ID_TG.None)
        {
            return;
        }
        go.SetActive(false);
        //Debug.Log("pool back");
        pool_list[block_prefabs_SO.ID2index(id)].Enqueue(go);
        go.transform.SetParent(pool_transform);
    }
    public GameObject pool_get(Item_ID_TG id) {
        if (id == Item_ID_TG.None)
        {
            return null;
        }
        int ind = block_prefabs_SO.ID2index(id);
        GameObject go;
        if (pool_list[ind].Count == 0) {
            
            go = Instantiate(block_prefabs_SO.get_prefab(id), new Vector3(1000f, 1000f, 1000f), Quaternion.identity);
        }
        else {
            go = pool_list[ind].Dequeue();
        }
        go.SetActive(true);
        return go;
    }
    public GameObject pool_get(Item_ID_TG id, Vector3 position, Quaternion rotation)
    {
        if (id == Item_ID_TG.None)
        {
            return null;
        }
        int ind = block_prefabs_SO.ID2index(id);
        GameObject go;
        if (pool_list[ind].Count == 0)
        {
            //Debug.Log(id);
            go = Instantiate(block_prefabs_SO.get_prefab(id), position, rotation);
        }
        else
        {
            go = pool_list[ind].Dequeue();
            go.transform.position = position;
            go.transform.rotation = rotation;
        }
        go.SetActive(true);
        return go;
    }

    private void generate_start_map()
    {
        Chunk_TG new_chunk;
        Vector3Int now_chunk_pos = Vector3Int.zero;
        int y_render_range = (current_chunk_pos.y >= 0 ? render_chunk_num : 3);
        for (int i = start_chunk_pos.x - render_chunk_num; i < start_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = start_chunk_pos.y - 2; j < start_chunk_pos.y + y_render_range; j++)
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

        for (int i = start_chunk_pos.x - render_chunk_num; i < start_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = start_chunk_pos.y - 2; j < start_chunk_pos.y + y_render_range; j++)
            {
                for (int k = start_chunk_pos.z - render_chunk_num; k < start_chunk_pos.z + render_chunk_num; k++)
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
        int y_render_range = (current_chunk_pos.y >=0 ? render_chunk_num: 3);
        int y_pool_range = (current_chunk_pos.y >=0 ? pooling_distance : 3);
        for (int i = current_chunk_pos.x - render_chunk_num- pooling_distance; i < current_chunk_pos.x + render_chunk_num+pooling_distance; i++)
        {
            for (int j = current_chunk_pos.y - 1 - 1; j < current_chunk_pos.y + y_render_range + y_pool_range; j++)
            {
                for (int k = current_chunk_pos.z - render_chunk_num -pooling_distance; k < current_chunk_pos.z + render_chunk_num + pooling_distance; k++)
                {
                    now_chunk_pos.x = i;
                    now_chunk_pos.y = j;
                    now_chunk_pos.z = k;
                    new_chunk = get_chunk(now_chunk_pos);
                    if (i >= current_chunk_pos.x - render_chunk_num && i < current_chunk_pos.x + render_chunk_num
                        && j >= current_chunk_pos.y - 1 && j < current_chunk_pos.y + y_render_range
                        && k >= current_chunk_pos.z - render_chunk_num && k < current_chunk_pos.z + render_chunk_num)
                    {                        
                        if (new_chunk == null)
                        {
                            new_chunk = Instantiate(chunk_prefab, chunk2world_pos(now_chunk_pos), Quaternion.identity).GetComponent<Chunk_TG>();
                            new_chunk.transform.SetParent(transform);
                            new_chunk.init(chunk_size, now_chunk_pos);
                            chunk_data[now_chunk_pos] = new_chunk;

                            //new_chunk.generate_block_nodes();
                            yield return new_chunk.generate_blocks_co();
                        }
                        else if (!new_chunk.is_active)
                        {
                           // yield return new_chunk.request_pool_blocks_co();
                        }
                    }
                    else {                        
                        if (new_chunk != null)
                        {
                            new_chunk.pool_back_all();
                        }
                    }                    
                }
            }
        }

        for (int i = current_chunk_pos.x - render_chunk_num; i < current_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = current_chunk_pos.y - 1; j < current_chunk_pos.y + y_render_range; j++)
            {
                for (int k = current_chunk_pos.z - render_chunk_num; k < current_chunk_pos.z + render_chunk_num; k++)
                {
                    now_chunk_pos.x = i;
                    now_chunk_pos.y = j;
                    now_chunk_pos.z = k;
                    new_chunk = get_chunk(now_chunk_pos);
                    if (new_chunk != null && !new_chunk.is_open_checked) {
                        new_chunk.check_open_and_show_all();
                        //yield return new_chunk.check_open_and_show_all_co();                        
                    }                       
                }
            }
            
        }
        yield return null;
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
    public Vector3Int world2block_pos(Vector3 world_pos)
    {
        Vector3Int pos = new Vector3Int((int)world_pos.x % chunk_size,  (int)world_pos.y % chunk_size,  (int)world_pos.z % chunk_size);
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
        int mountain_generate_range = 200;
        int mountain_num = 100;
        int mountain_max_height = 32;
        int mountain_min_height = 48;
        for (int i =0; i < mountain_num; i++) {
            mountain_point_list.Add(new Vector3Int(UnityEngine.Random.Range(-mountain_generate_range, mountain_generate_range), UnityEngine.Random.Range(mountain_min_height, mountain_max_height), UnityEngine.Random.Range(-mountain_generate_range, mountain_generate_range)));
        }
    }
}
