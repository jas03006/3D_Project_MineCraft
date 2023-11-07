
using UnityEngine;
using System.Collections.Generic;
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

    [SerializeField] public GameObject block_in_hand;//TG
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
    protected override void Start()
    {
        base.Start();

        camera = FindObjectOfType<Camera>();
        //TG
        if (interaction_range <=0) {
            interaction_range = 8;
        }
        deactivate_gravity(); 
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

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
    protected override void Update()
    {
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
            }
            return;
        }
        //base.Update();
        //CamChange();

        PositionInput();
        temp_vec3.x = angle_clamp_around0(head_tr.localEulerAngles.x - mouseY, -90f, 90f);        
        temp_vec3.y = angle_clamp_around0(head_tr.localEulerAngles.y + mouseX, -90f, 90f);
        temp_vec3.z = 0;
        head_tr.localEulerAngles = temp_vec3;
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
        transform.position += (transform.forward*vertical+ transform.right*horizontal) * currentspeed* Time.deltaTime;

        if (!isjump && Input.GetButtonDown("Jump"))
        {
            isjump = true;
            rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
        //TG

        
        attck_timer += Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            stop_breaking();
        }
        if (attck_timer >= 0.2f) {            
            if (Input.GetMouseButton(0))
            {
                attck_timer = 0f;
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
    private void FixedUpdate()
    {
        //TG
        check_and_grap();
    }

    private void left_click() { //TG
        RaycastHit hit;
        
        Ray ray = camera.ScreenPointToRay( new Vector3( camera.pixelWidth/2f, camera.pixelHeight / 2f));

        if (Physics.Raycast(ray, out hit, interaction_range, LayerMask.GetMask("Default")))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("Stepable_Block") ) {
                //Debug.Log(hit.point - transform.position);                
                if (now_breaking_block == null)
                {
                    objectHit.TryGetComponent<Block_Break>(out now_breaking_block);
                }
                else {
                    Block_Break temp_breaking_block = objectHit.GetComponent<Block_Break>();
                    if (temp_breaking_block == null || !now_breaking_block.Equals(temp_breaking_block)) {
                        now_breaking_block.block_recover_hp();
                        now_breaking_block = temp_breaking_block;
                    }                        
                }                    
                //objectHit.GetComponent<Block_TG>()
                now_breaking_block.Destroy_Block(20f);//die();                           
            }
        }
    }
    private void stop_breaking() {
        if (now_breaking_block != null) {
            now_breaking_block.block_recover_hp();
            now_breaking_block = null;
        }
    }
    private void right_click(bool is_button_stay = false) { //TG
        RaycastHit hit;
        
        Ray ray = camera.ScreenPointToRay( new Vector3( camera.pixelWidth/2f, camera.pixelHeight / 2f));

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
                }
                                      
                    
                    //objectHit.GetComponent<Block_TG>().die();
                             
            }
        }
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
    
    
    /*private void CamSet()
    {
        // 현재 카메라의 회전 값을 가져옴
        Vector3 camRotation = cam.transform.localEulerAngles;

        // 회전값을 더하고 제한

        camRotation.x -= mouseY * r_speed;

        // 카메라의 회전값을 제한
        camRotation.x = Mathf.Clamp(camRotation.x, -50f, 360f);
        *//*camRotation.x = Mathf.Clamp(camRotation.x, -90f, 90f);
        camRotation.x = (camRotation.x + 360f) % 360f;*//* // -90도부터 90도까지로 제한
        //Debug.Log(camRotation.x);

        // 실제 카메라에 회전 적용
        cam.localEulerAngles = camRotation;
    }*/
    protected override void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Stepable_Block"))
        {
            if ((transform.position - col.transform.position).normalized.y >= 0.49f) {
                isjump = false;
            }            
        }
        //TG
        else if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Floating_Item")))
        {
            Debug.Log("Take");
            
            inventory.GetItem(col.gameObject.GetComponent<Break_Block_Item>().id, 1);
            col.gameObject.SetActive(false);
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Stepable_Block"))
        {
            isjump = false;
        }
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
        Vector3 target_pos = transform.position + Vector3.up;
        Collider[] cols = Physics.OverlapBox(target_pos, Vector3.one * 2f, Quaternion.identity, LayerMask.GetMask("Floating_Item"));
        float dis;
        foreach (Collider col in cols) {
            Debug.Log("check drop");
            dis = (target_pos - col.transform.position).magnitude;
            col.transform.position = Vector3.Lerp(col.transform.position, target_pos, 0.1f / dis);
        }
    }
}

