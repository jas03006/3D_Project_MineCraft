
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Player_Raycast_Points {
    public List<Vector3> front;
    public List<Vector3> right;
    public List<Vector3> left;
    public List<Vector3> back;
    public List<Vector3> top;
    public List<Vector3> bottom;

    public Collider collider;

    public int x_count = 4;
    public int y_count = 4;
    public int z_count = 4;
    public Vector3 forward;

    public Player_Raycast_Points() {
        front = new List<Vector3>(); //x,y
        right = new List<Vector3>();  // z,y
        left = new List<Vector3>();  // z,y
        back = new List<Vector3>(); // x,y
        top = new List<Vector3>();   // 
        bottom = new List<Vector3>();
    }

    public void set_points(BoxCollider col) {
        collider = col;
        forward = col.transform.forward;
        forward.y = 0;
        Vector3 back_left_bottom = col.bounds.min- col.bounds.center + Vector3.up *0.1f;
        Vector3 front_right_top = col.bounds.max- col.bounds.center;
        float x_len = (front_right_top.x - back_left_bottom.x) / (x_count-1);
        float y_len = (front_right_top.y - back_left_bottom.y) / (y_count-1);
        float z_len = (front_right_top.z - back_left_bottom.z) / (z_count-1);
        Vector3 temp;
        front.Clear();
        right.Clear();
        left.Clear();
        back.Clear();
        top.Clear();
        bottom.Clear();
        for (int x=0; x < x_count; x++) {
            for (int y = 0; y < y_count; y++)
            {
                temp = new Vector3(x_len * x, y_len * y, 0);
                front.Add(front_right_top - temp);
                back.Add(back_left_bottom + temp);
            }
            for (int z = 0; z < z_count; z++)
            {
                temp = new Vector3(x_len * x, 0, z*z_len);
                top.Add(front_right_top - temp);
                bottom.Add(back_left_bottom + temp);
            }
        }
        for (int y = 0; y < y_count; y++)
        {
            for (int z = 0; z < z_count; z++)
            {
                temp = new Vector3( 0, y*y_len, z * z_len);
                right.Add(front_right_top - temp);
                left.Add(back_left_bottom + temp);
            }
        }       
    }

    public void set_points(CapsuleCollider col)
    {
        collider = col;
        forward = col.transform.forward;
        forward.y = 0;
        Vector3 back_left_bottom =  Vector3.down*(col.height/2f -  0.1f) + Quaternion.Euler(0f, 240f, 0f) * forward * col.radius;
        Vector3 front_right_top = Vector3.up * (col.height/2f) + Quaternion.Euler(0f,60f,0f)* forward * col.radius;
        float x_angle = 120f / (x_count - 1);
        float y_len = (front_right_top.y - back_left_bottom.y) / (y_count - 1);
        float z_angle = 60f / (z_count - 1);
        float x_len = (col.radius * 1.732f)/ (x_count - 1);
        float z_len = (col.radius) / (z_count - 1);
        Vector3 temp;
        front.Clear();
        right.Clear();
        left.Clear();
        back.Clear();
        top.Clear();
        bottom.Clear();
        for (int x = 0; x < x_count; x++)
        {
            for (int y = 0; y < y_count; y++)
            {
                temp = new Vector3(0, y_len * y, 0);
                front.Add(Quaternion.Euler(0f, -x_angle*x, 0f)*(front_right_top - temp));
                back.Add(Quaternion.Euler(0f, -x_angle*x, 0f) * (back_left_bottom + temp));
            }
            for (int z = 0; z < z_count; z++)
            {
                temp = forward * z * z_len + col.transform.right * x_len * x + Vector3.down*0.08f;
                //temp = new Vector3(x_len * x, 0, z * z_len);
                //top.Add(front_right_top - temp);
                bottom.Add(back_left_bottom + temp);
            }
        }
        for (int y = 0; y < y_count; y++)
        {
            for (int z = 0; z < z_count; z++)
            {
                temp = new Vector3(0, y * y_len, 0);
                right.Add(Quaternion.Euler(0f, z_angle*z, 0f) * (front_right_top - temp));
                left.Add(Quaternion.Euler(0f, z_angle*z, 0f) * (back_left_bottom + temp));
            }
        }
    }
}

public class Player_Test_TG : PlayerMovement_Y
{
    #region 스케치
    /*
     <캐릭터 움직임 구현>
    //변수
        //스피드(웅크리기) 
        //스피드(걷기) - 기본
        //스피드(뛰기)

        //horizontal
        //vertical
        //jumpforce
        //rigidbody
    //메서드
        //Awake
            // 컴포넌트 불러오기
        //Update
            //if(ray 충돌체), 플레이어 입력받기
        //플레이어 입력(Input.GetAxis, jump(addforce))
        //속도 설정

    //추가로 해야할 것
        //애니메이션 : 웅크리기,뛰기,걷기
        //카메라 모션 : 뛸때 카메라 살짝 멀어지기, 웅크릴때 카메라 살짝 가까워지기
        //낙사 데미지 구현
     */
    #endregion

    //TG
    [SerializeField] private float interaction_range = 4f;//TG
    private float attck_timer = 1f;//TG
    private float invincible_timer = 0f;
    private float invincible_time = 1.5f;
    [SerializeField]private Slider attack_charge_slider;//TG
    public int basic_att_speed = 4;

    [SerializeField] public GameObject block_in_hand;//TG
    public Item_ID_TG block_in_hand_id;//TG
    public InventoryData block_in_hand_data;//TG

    public bool is_sleeping = false;//TG
    private Block_Break now_breaking_block = null;

    [SerializeField] public Inventory inventory;

    protected Camera camera;
    [SerializeField] Transform head_tr;
    [SerializeField] Transform body_tr;    
    
    private Vector3 temp_vec3;
    private float temp_float;
    private Vector3 front_right = new Vector3(-90f, 45f, 0);
    private Vector3 front_left = new Vector3(-90f, -45f, 0);
    private Vector3 current_move_vec = Vector3.zero;
    private float collider_radius = 1f;
    private bool is_trigger_checked = false;
    private Vector3 movement_adjustment = Vector3.zero;
    private Player_Raycast_Points raycast_points;
    //private BoxCollider box_collider;
    private CapsuleCollider capsule_collider;

    public PlayerState_Y player_state;
    private bool is_grounded;

    private float cam3Tocam1 =0f;

    protected override void Start()
    {
        base.Start();

        
        camera = FindObjectOfType<Camera>();
        collider_radius = GetComponent<CapsuleCollider>().radius;
        raycast_points = new Player_Raycast_Points();
        //gameObject.TryGetComponent<BoxCollider>(out box_collider);
        gameObject.TryGetComponent<CapsuleCollider>(out capsule_collider);
        raycast_points.set_points(capsule_collider);
        //TG
        if (interaction_range <=0) {
            interaction_range = 8;
        }
        deactivate_gravity(); 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;//CursorLockMode.Locked;
        player_state = GetComponent<PlayerState_Y>();
        player_state.att_speed = basic_att_speed;
        //player_state.attack_power = 20;
        attack_charge_slider.gameObject.SetActive(false);

        cam3Tocam1 = (cam_pos_arr[0].transform.position - cam_pos_arr[2].transform.position).magnitude;
    }
    private float angle_clamp_around0(float value, float min, float max) {
        float center = (min+max)/ 2f + 180f;
        if (value > max) {
            if (value < center) {
                return max;
            }
            value -= 360f;
            if (value < min)
            {
                return min;
            }
             return value;
        }
        if (value < min)
        {
            value += 360f;
            if (value > max) {
                if (value < center) {
                    return max;
                }
                return value - 360f;
            }
            return value;
        }
        return value;
    }
    protected void Update()
    {
        invincible_timer += Time.deltaTime;
        attck_timer += Time.deltaTime;
        if (attack_charge_slider.value < attack_charge_slider.maxValue)
        {
            attack_charge_slider.value += player_state.att_speed / 2f * Time.deltaTime;
        }
        else {
            attack_charge_slider.gameObject.SetActive(false);
        }

/*        if (Input.GetMouseButtonUp(0))
        {
            arm_animator.SetBool("is_crouch", false);
            arm_animator.SetBool("is_walk", false);
            //arm_animator.SetBool("is_active", false);
        }*/

        if (inventory.isInventoryOpen)
        {
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            stop_breaking();
        }

        if (attck_timer >= 0.1f)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                attck_timer = 0f;
                throw_item();
            }
        }
        
        if (attck_timer >= 0.2f)
        {
            if (Input.GetMouseButton(0))
            {
                attck_timer = 0f;
                arm_animator.SetBool("is_crouch", false);
                arm_animator.SetBool("is_walk", false);
                arm_animator.SetTrigger("is_active");
                left_click();
            }
            if (Input.GetMouseButtonDown(1))
            {
                attck_timer = 0f;
                right_click();
            }
            else if (Input.GetMouseButton(1))
            {
                attck_timer = 0f;
                right_click(true);
            }
        }
        
    }
    protected override void FixedUpdate()
    {
        check_and_grap();
        if (inventory.isInventoryOpen)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            CamChange();
        }
        if (is_sleeping)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                is_sleeping = false;
                transform.rotation = Quaternion.identity;
                transform.Translate(Vector3.up * 0.5f);
                return;
            }
            return;
        }

        PositionInput();
        // Head Rotate
        temp_vec3.x = angle_clamp_around0(head_tr.localEulerAngles.x - mouseY, -90f, 90f);        
        temp_vec3.y = angle_clamp_around0(head_tr.localEulerAngles.y + mouseX, -90f, 90f);
        temp_vec3.z = 0;
        head_tr.localEulerAngles = temp_vec3;
        //whole rotate
        if (vertical != 0) {
            temp_vec3.x = head_tr.forward.x;
            temp_vec3.z = head_tr.forward.z;
            temp_vec3.y = 0;
            transform.forward = temp_vec3;
            temp_vec3.y = 0;
            temp_vec3.z = 0;
            temp_vec3.x = head_tr.localEulerAngles.x;
            head_tr.localEulerAngles = temp_vec3;
        }
        //body rotate
        temp_float = vertical * horizontal;
        if (temp_float > 0 || (vertical == 0 &&horizontal > 0))
        {
            body_tr.localEulerAngles = front_right;
        }
        else if (temp_float < 0 || (vertical == 0 && horizontal < 0))
        {
            body_tr.localEulerAngles = front_left;
        }
        else {
            body_tr.localEulerAngles = Vector3.right* -90f;
        }

        // Player Move
        current_move_vec = (transform.forward * vertical + transform.right * horizontal) * currentspeed * Time.deltaTime;
        raycast_forward(current_move_vec,ref current_move_vec);
        rigid.MovePosition(transform.position + current_move_vec);

        //낙하 체크
        is_grounded = raycast_all_points(raycast_points.bottom, Vector3.down, 0.1f - rigid.velocity.y* Time.fixedDeltaTime);
        if ((is_grounded == true) == isjump)
        {
            if (rigid.velocity.y < -6f)
            {

                int dmg = Mathf.FloorToInt(rigid.velocity.y * rigid.velocity.y / -Physics.gravity.y / 2f) - 3;
                //Debug.Log($"{rigid.velocity.y}, {dmg}");
                if (dmg > 0)
                {
                    player_state.OnDamage(dmg);
                    invincible_timer = 0f;
                    rigid.velocity = new Vector3(rigid.velocity.x,0, rigid.velocity.z);
                }
            }
        }
        isjump = !is_grounded;

        if (!isjump)
        {
            if (Input.GetButton("Jump"))
            {
                isjump = true;
                rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            }
        }

    }

    private void left_click() { //TG

        attack_charge_slider.gameObject.SetActive(true);
        RaycastHit hit;
        
        Ray ray = camera.ScreenPointToRay( new Vector3( camera.pixelWidth/2f, camera.pixelHeight / 2f));
        ray.origin = cam_pos_arr[0].transform.position;
        if (cam_state == Cam_State.cam2) {
            ray.direction = ray.direction * -1f;
        }
        if (Physics.Raycast(ray, out hit, interaction_range, LayerMask.GetMask("Default") | LayerMask.GetMask("Monster_J")))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("Stepable_Block"))
            {
                //Debug.Log(hit.point - transform.position);                
                if (now_breaking_block == null)
                {
                    objectHit.TryGetComponent<Block_Break>(out now_breaking_block);
                }
                else
                {
                    Block_Break temp_breaking_block = objectHit.GetComponent<Block_Break>();
                    if (temp_breaking_block == null || !now_breaking_block.Equals(temp_breaking_block))
                    {
                        now_breaking_block.block_recover_hp();
                        now_breaking_block = temp_breaking_block;
                    }
                }
                //objectHit.GetComponent<Block_TG>()
                int dmg = 10 * Combat_system.instance.cal_combat_block(now_breaking_block.id);
                now_breaking_block.Destroy_Block(dmg);//die();
                //Debug.Log("Damage: "+dmg);
            }
            else {//monster
                Monster_controll monster = objectHit.GetComponent<Monster_controll>();
                monster.MonsterHurt(10 * Combat_system.instance.cal_combat_monster(monster.monsterID));
                //Debug.Log();
            }
        }
        //Debug.Log((int)(player_state.attack_power * attack_charge_slider.value));
        attack_charge_slider.value = 0;
    }

    private void stop_breaking() {
        if (now_breaking_block != null) {
            now_breaking_block.block_recover_hp();
            now_breaking_block = null;
        }
    }
    private void right_click(bool is_button_stay = false) { //TG
        if (block_in_hand_data != null &&  block_in_hand_data.Iseatable) {
            (block_in_hand_data as Food).R_Eat();
            Inventory.instance.UIslot_minus();
            return;
        }
        
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay( new Vector3( camera.pixelWidth/2f, camera.pixelHeight / 2f));
        ray.origin = cam_pos_arr[0].transform.position;
        if (cam_state == Cam_State.cam2)
        {
            ray.direction = ray.direction * -1f;
        }
        if (Physics.Raycast(ray, out hit, interaction_range, LayerMask.GetMask("Default")))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("Stepable_Block") ) {
               // Debug.Log(hit.point - transform.position);
                Vector3 dir = hit.point - transform.position;
                if (!is_button_stay) {
                    Interactive_TG interactive_block = hit.transform.gameObject.GetComponentInChildren<Interactive_TG>();
                    if (interactive_block != null)
                    {
                        interactive_block.react();
                        return;
                    }                        
                }
                    
                if (block_in_hand == null)
                {
                    return;
                }
                Block_TG block_ = block_in_hand.GetComponentInChildren<Block_TG>();
                if(block_ ==null) {
                    return;
                }
                Item_ID_TG id_ = block_.id;
                dir = hit.point - hit.collider.transform.position;
                Vector3 set_dir = six_dir_normalization_cube(dir, 0.49f);
                
                set_dir = hit.collider.transform.position + set_dir;
                if (Physics.OverlapBox(set_dir, Vector3.one / 2.1f).Length == 0)
                {
                    //List<Vector3Int> space_ = new List<Vector3Int>();
                    //space_.Add(Vector3Int.up);
                    
                    Biom_Manager.instance.set_block(id_, set_dir,
                        Quaternion.LookRotation(six_dir_normalization_cube(new Vector3(-transform.forward.x, 0f, -transform.forward.z), 0.70711f))
                        ,block_.space);
                    Inventory.instance.UIslot_minus();
                }
                    //objectHit.GetComponent<Block_TG>().die();
            }
        }
    }

    public void throw_item() {
        Block_Objectpooling.instance.throw_item(block_in_hand_id, cam_pos_arr[0].position + head_tr.forward, head_tr.forward);
        Inventory.instance.UIslot_minus();
    }
    public Vector3 six_dir_normalization_cube(Vector3 dir,  float threshold = 0.49f) {
        Vector3 result_dir = Vector3.zero;
        if (dir.x >= threshold)
        {
            result_dir = Vector3.right;
        }
        else if (dir.x <= -threshold)
        {
            result_dir = Vector3.left;
        }
        else if (dir.y >= threshold)
        {
            result_dir = Vector3.up;
        }
        else if (dir.y <= -threshold)
        {
            result_dir = -Vector3.down;
        }
        else if (dir.z >= threshold)
        {
            result_dir = Vector3.forward;
        }
        else if (dir.z <= -threshold)
        {
            result_dir = Vector3.back;
        }
        return result_dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!get_item(other) && invincible_timer > invincible_time) {
            get_attacked(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!get_item(other) && invincible_timer > invincible_time)
        {
            get_attacked(other);
        }
    }

    /* protected void OnCollisionStay(Collision col)
     {
         get_item(col);
     }*/
    public void get_attacked(Collider collider) {
        if (collider.gameObject.layer.Equals(LayerMask.NameToLayer("Monster_Attack"))) {
            Monster_controll mc = collider.gameObject.GetComponentInParent<Monster_controll>();
            Debug.Log("Monster_Attack!");
            if (mc != null)
            {

                int dmg = (int)mc.Monster_Damage - player_state.defense_power;
                Debug.Log(dmg);
                player_state.OnDamage((dmg < 0 ? 0 : dmg));
                invincible_timer = 0f;
            }
        }        
    }
    private bool get_item(Collider col) {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Floating_Item")))
        {
            //아이템 습득
            inventory.GetItem(col.gameObject.GetComponent<Break_Block_Item>().id, 1);
            col.gameObject.SetActive(false);
            return true;
        } else if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Exp"))) {
            Exp_pooling.instance.return_pool(col.gameObject);
            player_state.GetExp(1);
            return true;
        }
        return false;
    }

    
    private void raycast_forward(Vector3 movement, ref Vector3 original_movement) {
        Vector3 temp_forward = transform.forward;
        temp_forward.y = 0f;
        movement.y = 0f;
        float cos_temp_angle = Vector3.Dot(temp_forward.normalized, movement.normalized);

        Vector3 force_dir = movement;
        if (cos_temp_angle == 1)
        {
            //front
            raycast_all_points(raycast_points.front, movement, ref force_dir);            
        }
        else if (cos_temp_angle < 1 && cos_temp_angle > 0)
        {
            //front, right
            if (horizontal > 0 ) {
                raycast_all_points(raycast_points.front, movement, ref force_dir);
                raycast_all_points(raycast_points.right, movement, ref force_dir);
            }

            // front, left
            else 
            {
                raycast_all_points(raycast_points.front, movement, ref force_dir);
                raycast_all_points(raycast_points.left, movement, ref force_dir);
            }
        }
        else if (cos_temp_angle == 0) {
            if (horizontal > 0)
            {
                //right
                raycast_all_points(raycast_points.right, movement, ref force_dir);
            }
            else {
                // left
                raycast_all_points(raycast_points.left, movement, ref force_dir);
            }                                  
        }
        else if (cos_temp_angle < 0 && cos_temp_angle > -1)
        {
            //back, right
            if (horizontal > 0)
            {
                raycast_all_points(raycast_points.back, movement, ref force_dir);
                raycast_all_points(raycast_points.right, movement, ref force_dir);
            }

            //back, left
            else 
            {
                raycast_all_points(raycast_points.back, movement, ref force_dir);
                raycast_all_points(raycast_points.left, movement, ref force_dir);
            }
        }
        else if (cos_temp_angle == -1 )
        {
            //back
            raycast_all_points(raycast_points.back, movement, ref force_dir);
        }

        original_movement = force_dir;
        Debug.DrawRay(transform.position, force_dir.normalized, Color.yellow);
        Debug.DrawRay(transform.position, original_movement.normalized, Color.blue);
    }

    public Vector3 get_dir_from_vector3(Vector3 origin, Vector3 dir) {
        float cos_a = Vector3.Dot(origin.normalized, dir.normalized);
        return dir.normalized * (origin * cos_a).magnitude * (cos_a >= 0 ? 1 : -1);
    }

    private bool raycast_all_points(List<Vector3> points,Vector3 dir, ref Vector3 force_dir) {
        Ray ray;
        RaycastHit hit;
        float dis = 0.1f;
        Vector3 now_forward = transform.forward.normalized;
        now_forward.y = 0;
        foreach (Vector3 p in points) {
            ray = new Ray(raycast_points.collider.bounds.center +(Quaternion.FromToRotation(raycast_points.forward.normalized, now_forward) * p), dir.normalized * dis);    
            //Debug.Log(box_collider.bounds.center + p);
            Debug.DrawRay(ray.origin, dir.normalized * dis, Color.red);
            if (Physics.Raycast(ray,out hit, dis, LayerMask.GetMask("Default"))) {
                Vector3 temp_dir = hit.point - hit.collider.transform.position;
                temp_dir.y = 0;
                if (Mathf.Abs(temp_dir.x) > Mathf.Abs(temp_dir.z))
                {
                    temp_dir.x = 0;
                }
                else {
                    temp_dir.z = 0;
                }
                //force_dir = temp_dir;
                temp_dir = get_dir_from_vector3(force_dir, temp_dir);
                if (temp_dir.normalized != force_dir.normalized) {
                    force_dir = temp_dir;                    
                }                
            }
        }
        return false;
    }
    private bool raycast_all_points(List<Vector3> points, Vector3 dir, float dis_ = 0.1f)
    {
        Ray ray;
        RaycastHit hit;
        float dis = dis_;
        Vector3 now_forward = transform.forward.normalized;
        now_forward.y = 0;
        foreach (Vector3 p in points)
        {
            ray = new Ray(raycast_points.collider.bounds.center + (Quaternion.FromToRotation(raycast_points.forward.normalized, now_forward) * p), dir.normalized * dis);
            //Debug.Log(box_collider.bounds.center + p);
            Debug.DrawRay(ray.origin, dir.normalized * dis, Color.red);
            if (Physics.Raycast(ray, out hit, dis, LayerMask.GetMask("Default")))
            {
                return true;   
            }
        }
        return false;
    }


    public void deactivate_gravity() //TG
    {
        rigid.useGravity = false;
    }
    public void activate_gravity() //TG
    {
        rigid.useGravity = true;
    }

    public void check_and_grap() {
        Vector3 target_pos = transform.position + Vector3.up*1.5f;
        float dis;
        Rigidbody rigid;
        Collider[] cols = Physics.OverlapBox(target_pos, Vector3.one * 2f, Quaternion.identity, LayerMask.GetMask("Floating_Item"));
        foreach (Collider col in cols) {
            //Debug.Log("check drop");
            dis = (target_pos - col.transform.position).magnitude;
            col.TryGetComponent<Rigidbody>(out rigid);
            if (rigid != null) {
                if (rigid.velocity.x != 0 || rigid.velocity.z != 0) {
                    continue;
                } 
            }
            col.transform.position = Vector3.Lerp(col.transform.position, target_pos, Mathf.Min(0.1f / dis, 1f));
        }

        cols = Physics.OverlapBox(target_pos, Vector3.one * (interaction_range+1), Quaternion.identity,  LayerMask.GetMask("Exp"));
        foreach (Collider col in cols)
        {
            dis = (target_pos - col.transform.position).magnitude;
            col.transform.position = Vector3.Lerp(col.transform.position, target_pos, Mathf.Min(0.1f / dis, 1f));
        }
    }

    public void stop() {
        rigid.velocity = Vector3.zero;
    }
}

