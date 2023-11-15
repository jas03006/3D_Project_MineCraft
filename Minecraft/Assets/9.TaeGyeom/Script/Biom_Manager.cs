using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface Interactive_TG{
    public void react();
}
public class Cave_Point {
    public Vector3 position;
    public float radius;
    public Cave_Point() { 
    
    }
    public Cave_Point(Vector3 position_, float radius_)
    {
        position = position_;
        radius = radius_;
        if (radius < 2) {
            radius = 2;
        }
    }
}
public class Mountain_Point
{
    public Vector3 position;
    public float slope; //tan ceta
    public Mountain_Point()
    {

    }
    public Mountain_Point(Vector3Int position_, float slope_)
    {
        position = position_;
        if (position.y <= 0)
        {
            position.y = 1;
        }
        slope = slope_;
        if (slope < 0)
        {
            slope = 0.1f;
        }
    }
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
    private List<Mountain_Point> mountain_point_list;
    private Dictionary<Vector2Int, int> mountain_height_dict;
    private List<Cave_Point> cave_point_list;

    private List<GameObject> monster_controll_list;

    private Dictionary<Vector3Int, Chunk_TG> chunk_data;
    private Queue<GameObject>[] pool_list;
    private Vector3 pool_position = new Vector3(1000f, 1000f, 1000f);
    private Queue<Block_Node_TG> block_ready_queue;
    private int block_ready_cnt = 150000;
    private int ready_block_generate_cnt = 3000;
        

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
            block_ready_cnt = 1_000_000;
        }
        chunk_data = new Dictionary<Vector3Int, Chunk_TG>();
        mountain_point_list = new List<Mountain_Point>();
        init_pool_list();
        init_block_ready_queue();

        decide_mountain_point();
        decide_cave_point(); // 산 생성 후에 실행되어야함
        update_start_pos();

        init_monster_controll_list();
        
        player.GetComponent<Player_Test_TG>().deactivate_gravity();
        generate_start_map();
        player.GetComponent<Player_Test_TG>().activate_gravity();

    }

    private void Update()
    {
        
        chunk_update_timer += Time.deltaTime;
        if (now_update_co == null)
        {
            if (chunk_update_timer > chunk_update_time)
            {
                chunk_update_timer = 0f;
                current_chunk_pos = get_player_chunk_pos();
                // Debug.Log("start co");
                now_update_co = StartCoroutine(generate_map_update_co());

            }
        }

        update_monsters_visiblity(only_check_out: true);

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
            player.transform.position += (Vector3.up * (h + 2));

            PlayerState_Y psy = player.GetComponent<PlayerState_Y>();
            psy.original_spawn_position = player.transform.position;
        }
        else {
            Debug.Log("Player is null");
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
        //pool_list = new Queue<GameObject>[ Enum.GetValues(typeof(Item_ID_TG)).Length ];
        pool_list = new Queue<GameObject>[block_prefabs_SO.block_item_id_list.Count ];
        int pool_num = 1000;
        foreach (Item_ID_TG e in block_prefabs_SO.block_item_id_list)//Enum.GetValues(typeof(Item_ID_TG)))
        {
            if (e == Item_ID_TG.None || block_prefabs_SO.get_prefab(e) == null) {
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
            pool_num = 1000;
            pool_list[block_prefabs_SO.ID2index(e)] = qu;
        }
    }

    public void pool_return(Item_ID_TG id, GameObject go) {
        
        if (id == Item_ID_TG.None || id == Item_ID_TG.Fill)
        {
            return;
        }
        go.transform.position = pool_position;
        pool_list[block_prefabs_SO.ID2index(id)].Enqueue(go);
       // go.transform.SetParent(pool_transform);
        go.SetActive(false);
    }
   
    public GameObject pool_get(Item_ID_TG id, Vector3 position, Quaternion rotation)
    {
        if (id == Item_ID_TG.None || id == Item_ID_TG.Fill)
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
        for (int i = start_chunk_pos.x - render_chunk_num; i <= start_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = start_chunk_pos.y - 3; j < start_chunk_pos.y + y_render_range; j++)
            {
                for (int k = start_chunk_pos.z - render_chunk_num; k <= start_chunk_pos.z + render_chunk_num; k++)
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

        for (int i = start_chunk_pos.x - render_chunk_num; i <= start_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = start_chunk_pos.y - 3; j < start_chunk_pos.y + y_render_range; j++)
            {
                for (int k = start_chunk_pos.z - render_chunk_num; k <= start_chunk_pos.z + render_chunk_num; k++)
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
        for (int i = current_chunk_pos.x - render_chunk_num- pooling_distance+1; i < current_chunk_pos.x + render_chunk_num+pooling_distance; i++)
        {
            for (int j = current_chunk_pos.y - 2 - 1; j < current_chunk_pos.y + y_render_range + y_pool_range; j++)
            {
                for (int k = current_chunk_pos.z - render_chunk_num -pooling_distance+1; k < current_chunk_pos.z + render_chunk_num + pooling_distance; k++)
                {
                    now_chunk_pos.x = i;
                    now_chunk_pos.y = j;
                    now_chunk_pos.z = k;
                    new_chunk = get_chunk(now_chunk_pos);
                    if (i >= current_chunk_pos.x - render_chunk_num && i < current_chunk_pos.x + render_chunk_num
                        && j >= current_chunk_pos.y - 2 && j < current_chunk_pos.y + y_render_range
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
                        /*else if (!new_chunk.is_active)
                        {
                           // yield return new_chunk.request_pool_blocks_co();
                        }*/
                    }
                    else {                        
                        if (new_chunk != null && new_chunk.is_active)
                        {
                            new_chunk.pool_back_all();
                        }
                    }                    
                }
            }
        }

        for (int i = current_chunk_pos.x - render_chunk_num+1; i < current_chunk_pos.x + render_chunk_num; i++)
        {
            for (int j = current_chunk_pos.y - 2; j < current_chunk_pos.y + y_render_range; j++)
            {
                for (int k = current_chunk_pos.z - render_chunk_num+1; k < current_chunk_pos.z + render_chunk_num; k++)
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
            //if (i%2 == 0) {
                yield return null;
           // } 
            
        }
        
        now_update_co = null;
        update_monsters_visiblity();
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
        //Vector3Int pos = new Vector3Int((int)world_pos.x / chunk_size + (world_pos.x<0? -1:0), (int)world_pos.y / chunk_size + (world_pos.y < 0 ? -1 : 0), (int)world_pos.z / chunk_size + (world_pos.z < 0 ? -1 : 0));
        Vector3Int pos = new Vector3Int((int)(world_pos.x < 0 ? world_pos.x - chunk_size + 1 : world_pos.x) / chunk_size ,
            (int)(world_pos.y < 0 ? world_pos.y - chunk_size + 1 : world_pos.y) / chunk_size,
            (int)(world_pos.z < 0 ? world_pos.z - chunk_size + 1 : world_pos.z) / chunk_size );
        return pos;
    }
    public Vector3Int world2block_pos(Vector3 world_pos)
    {
        Vector3Int pos = new Vector3Int(((int)world_pos.x % chunk_size + chunk_size)% chunk_size, ((int)world_pos.y % chunk_size + chunk_size) % chunk_size, ((int)world_pos.z % chunk_size + chunk_size) % chunk_size);
        return pos;
    }

    public Chunk_TG get_chunk(Vector3Int chunk_pos) {
        if (chunk_data.ContainsKey(chunk_pos)) {
            return chunk_data[chunk_pos];
        }
        return null;
    }

    public Block_Node_TG get_block(Vector3Int chunk_pos, Vector3Int block_pos) {
        chunk_pos.x += (block_pos.x < 0 ? block_pos.x - chunk_size + 1: block_pos.x) / chunk_size ; 
        chunk_pos.y += (block_pos.y < 0 ? block_pos.y - chunk_size + 1 : block_pos.y) / chunk_size ; 
        chunk_pos.z += (block_pos.z < 0 ? block_pos.z - chunk_size + 1 : block_pos.z) / chunk_size ;
        Chunk_TG ch = get_chunk(chunk_pos);
        if (ch == null) {
            return null;
        }
        return ch.block_data[(block_pos.x+chunk_size)%chunk_size, (block_pos.y + chunk_size) % chunk_size, (block_pos.z + chunk_size) % chunk_size];
    }

    public Block_Node_TG get_block(Vector3 world_pos)
    {
        Vector3Int chunk_pos = world2chunk_pos(world_pos);
        Vector3Int block_pos = world2block_pos(world_pos);
        Chunk_TG ch = get_chunk(chunk_pos);
        if (ch == null)
        {
            return null;
        }
        return ch.block_data[block_pos.x, block_pos.y,block_pos.z];
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
        /*temp_pos.y = 0;
        int h = 0;
        int temp_h=0;
        for (int i = 0; i < mountain_point_list.Count; i++) {
            temp_pos = mountain_point_list[i].position - temp_pos;
            int d = temp_pos.x * temp_pos.x + temp_pos.z * temp_pos.z;
            if (temp_pos.y * temp_pos.y / mountain_point_list[i].slope / mountain_point_list[i].slope < d)
            {
                temp_h = 0;
            }
            else {
                temp_h = temp_pos.y - (int)(Mathf.Sqrt(d) * mountain_point_list[i].slope);
                //Debug.Log(temp_h);
            }
            if (temp_h > h) {
                h = temp_h;
            }
        }
        
        return h;*/
        return get_mountain_height(temp_pos);
    }
    /*public int get_mountain_height(Vector3Int pos_) {
        return get_mountain_height(pos_);
    }*/
    public int get_mountain_height(Vector3 pos_)
    {
        Vector2Int key = new Vector2Int((int)pos_.x, (int)pos_.z);
        if (mountain_height_dict.ContainsKey(key)) {
            return mountain_height_dict[key];
        }
        Vector3 temp_pos = pos_;
        temp_pos.y = 0;
        float h = 0;
        float temp_h = 0;
        float mountain_radius;
        float d_sqr;
        for (int i = 0; i < mountain_point_list.Count; i++)
        {
            temp_pos = mountain_point_list[i].position - temp_pos;
            d_sqr = temp_pos.x * temp_pos.x + temp_pos.z * temp_pos.z;
            mountain_radius = temp_pos.y / mountain_point_list[i].slope;
            if (mountain_radius * mountain_radius < d_sqr)
            {
                temp_h = 0;
            }
            else
            {
                temp_h = temp_pos.y - (int)(Mathf.Sqrt(d_sqr) * mountain_point_list[i].slope);
                //Debug.Log(temp_h);
            }
            if (temp_h > h)
            {
                h = temp_h;
            }
        }
        int h_ = (int)Mathf.Round(h);
        mountain_height_dict[key] = h_;
        return h_;
    }

    private void decide_mountain_point() {
        int mountain_generate_range = 150;
        int mountain_num = 1200;
        int mountain_max_height = 32;
        int mountain_min_height = 8;
        int y_pos;
        mountain_height_dict = new Dictionary<Vector2Int, int>();
        for (int i =0; i < mountain_num; i++) {
            y_pos = UnityEngine.Random.Range(mountain_min_height, mountain_max_height);
            mountain_point_list.Add(new Mountain_Point(new Vector3Int(UnityEngine.Random.Range(-mountain_generate_range, mountain_generate_range), y_pos, UnityEngine.Random.Range(-mountain_generate_range, mountain_generate_range)),
                                                            Mathf.Tan(UnityEngine.Random.Range(Mathf.PI*(1f+3f*(y_pos- mountain_min_height) /(mountain_max_height- mountain_min_height)) / 18f, Mathf.PI/3f))));
        }
    }

    private void decide_cave_point() {
        cave_point_list = new List<Cave_Point>();
        //cave_point_list.Add(new Cave_Point());
        int cave_generate_range = 200;
        int cave_cnt = 100;
        int cave_depth = 10;
        Cave_Point now_cp;// = new Cave_Point(Vector3.right * 40, 4);
        Cave_Point next_cp;
        Vector3 gen_dir = Vector3.zero;
        //cave_point_list.Add(now_cp);
        for (int ci = 0; ci < cave_cnt; ci++) {
            if (ci == 0)
            {
                now_cp = new Cave_Point(Vector3.right*20, 4);
                cave_depth = 10;
            }
            else {
                now_cp = new Cave_Point(
                    new Vector3(
                        UnityEngine.Random.Range(-cave_generate_range, cave_generate_range),
                    0,//UnityEngine.Random.Range(-5, 0),
                    UnityEngine.Random.Range(-cave_generate_range, cave_generate_range)),
                    4);                
                cave_depth = UnityEngine.Random.Range(3, 13);
            }
            now_cp.position.y = get_mountain_height(now_cp.position);// + UnityEngine.Random.Range(-5, 0);
            next_cp = null;
            cave_point_list.Add(now_cp);
            for (int i = 1; i < cave_depth; i++)
            {
                //temp_pos = 
                get_next_gen_dir(ref gen_dir);
                next_cp = new Cave_Point(now_cp.position + now_cp.radius * gen_dir * 8 / 10, get_next_radius(now_cp.radius));
                next_cp.position += gen_dir * next_cp.radius * 8 / 10;
                float h = get_mountain_height(next_cp.position);
                if (next_cp.position.y > h) {
                    next_cp.position.y = h;
                }
                cave_point_list.Add(next_cp);
                gen_dir = next_cp.position - now_cp.position;
                now_cp = next_cp;
            }
        }
        
    }

    private void get_next_gen_dir(ref Vector3 gen_dir) {
        if (gen_dir == Vector3.zero) {
            gen_dir = new Vector3(1,-1,0).normalized;
            return;
        }
        gen_dir = Quaternion.Euler(0,UnityEngine.Random.Range(-180f,180f), 0)* gen_dir;
        gen_dir.y = 0;
        gen_dir = gen_dir.normalized;
        gen_dir.y = UnityEngine.Random.Range(-0.5f,0.1f);
        gen_dir = gen_dir.normalized;
    }
    private float get_next_radius(float radius) {
        float min_rate = 0.75f;
        float max_rate = 1.25f;
        if (radius * min_rate < 4) {
            min_rate = 4f / radius;
        }
        if (radius * max_rate < 8)
        {
            max_rate = 8f / radius;
        }
        return radius *UnityEngine.Random.Range(min_rate,max_rate);
    }

    public void get_valid_cave_points(Vector3Int chunk_pos, ref List<Cave_Point> cp_list) {
        Vector3 world_center_pos = chunk2world_pos(chunk_pos);
        world_center_pos.x += chunk_size / 2 -(chunk_size+1)%2;
        world_center_pos.y += chunk_size / 2 - (chunk_size + 1) % 2;
        world_center_pos.z += chunk_size / 2 - (chunk_size + 1) % 2;
        cp_list = new List<Cave_Point>();
        for (int i =0; i < cave_point_list.Count; i++) {
            Cave_Point cp = cave_point_list[i];
            if ((chunk_size + cp.radius+1 ) *(chunk_size  + cp.radius+1) >= (cp.position- world_center_pos).sqrMagnitude) {
                cp_list.Add(cp);
            }
        }
        //return cp_list;
    }

    public void set_block(Item_ID_TG id, Vector3 world_pos, Quaternion rotate, List<Vector3Int> space = null) {
        get_chunk(world2chunk_pos(world_pos)).set_block(id, world2block_pos(world_pos), world_pos, rotate,space);
    }

    public bool is_in_render_space(Vector3 position_) {
        position_ = world2chunk_pos(position_);
        int y_render_range = (current_chunk_pos.y >= 0 ? render_chunk_num : 3);
        if (position_.x >= current_chunk_pos.x - render_chunk_num + 1 && position_.x < current_chunk_pos.x + render_chunk_num
                        && position_.y >= current_chunk_pos.y - 2 && position_.y < current_chunk_pos.y + y_render_range
                        && position_.z >= current_chunk_pos.z - render_chunk_num + 1 && position_.z < current_chunk_pos.z + render_chunk_num) {
            return true;
        }
        return false;
    }

    private void init_monster_controll_list() {
        monster_controll_list = new List<GameObject>();
        int monster_cnt = 400;
        int monster_spawn_range = 300;
        Vector3Int pos = Vector3Int.zero;
        for (int i =0; i < monster_cnt; i++) {
            pos.x = UnityEngine.Random.Range(-monster_spawn_range, monster_spawn_range+1);
            pos.z = UnityEngine.Random.Range(-monster_spawn_range, monster_spawn_range+1);
            monster_controll_list.Add(Monster_Pool_Manager.instance.get(Monster_ID_J.Pig, new Vector3(pos.x, get_mountain_height(pos)+1, pos.z), Quaternion.identity, false));
        }
    }

    private void update_monsters_visiblity(bool only_check_out = false) {
        for (int i =0; i < monster_controll_list.Count; i++) {
            if (is_in_render_space(monster_controll_list[i].transform.position))
            {
                if (!only_check_out) {
                    monster_controll_list[i].gameObject.SetActive(true);
                }                
            }
            else { 
                monster_controll_list[i].gameObject.SetActive(false);
            }
        }
    }
}
