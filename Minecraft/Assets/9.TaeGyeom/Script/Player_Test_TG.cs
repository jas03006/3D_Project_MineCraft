
using UnityEngine;
using System.Collections.Generic;
public class Player_Test_TG : MonoBehaviour
{
    #region ����ġ
    /*
     <ĳ���� ������ ����>
    //����
        //���ǵ�(��ũ����) 
        //���ǵ�(�ȱ�) - �⺻
        //���ǵ�(�ٱ�)

        //horizontal
        //vertical
        //jumpforce
        //rigidbody
    //�޼���
        //Awake
            // ������Ʈ �ҷ�����
        //Update
            //if(ray �浹ü), �÷��̾� �Է¹ޱ�
        //�÷��̾� �Է�(Input.GetAxis, jump(addforce))
        //�ӵ� ����

    //�߰��� �ؾ��� ��
        //�ִϸ��̼� : ��ũ����,�ٱ�,�ȱ�
        //ī�޶� ��� : �۶� ī�޶� ��¦ �־�����, ��ũ���� ī�޶� ��¦ ���������
        //���� ������ ����
     */
    #endregion

    private Rigidbody rigid;
    [SerializeField] private Transform cam;
    public Camera camera;
    [Header("Position")]
    [SerializeField] private float crouchspeed;
    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;
    private float currentspeed;

    [SerializeField] private float jumpforce;
    [SerializeField] private bool isjump;

    private float horizontal;
    private float vertical;
    private Vector3 moveVec;

    [Header("Rotation")]
    private float mouseX;
    private float mouseY;
    [SerializeField] private float r_speed;

    //TG
    [SerializeField] private float interaction_range = 4f;//TG
    private float attck_timer = 1f;//TG

    [SerializeField] public GameObject block_in_hand;//TG
    public bool is_sleeping = false;//TG
    private void Start()
    {
        TryGetComponent(out rigid);
        cam = FindObjectOfType<Camera>().transform;

        isjump = false;
        jumpforce = 10f;
        crouchspeed = 1f;
        walkspeed = 5f;
        runspeed = 10f;
        r_speed = 1f;
        currentspeed = walkspeed;
        if (interaction_range <=0) {
            interaction_range = 8;
        }
        deactivate_gravity(); //TG
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        //Transform
        //x,z��
        //�Է�
        PositionInput();

        //������ ����
        Vector3 moveDirection = cam.transform.forward * vertical + cam.transform.right * horizontal;
        moveDirection.y = 0;
        moveVec = moveDirection * currentspeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        //����
        if (!isjump && Input.GetButton("Jump"))
        {
            isjump = true;
            rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }

        //Rotation
        //�Է�
        RotationInput();

        //ī�޶�
        CamSet();


        //TG
        if (is_sleeping) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                is_sleeping = false;
                transform.rotation = Quaternion.identity;
                transform.Translate(Vector3.up * 0.5f);
            }
            return;
        }
        attck_timer += Time.deltaTime;
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

    private void left_click() { //TG
        RaycastHit hit;
        
        Ray ray = camera.ScreenPointToRay( new Vector3( camera.pixelWidth/2f, camera.pixelHeight / 2f));

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("Stepable_Block") ) {
                //Debug.Log(hit.point - transform.position);
                if (((hit.point - transform.position).sqrMagnitude <= interaction_range * interaction_range)) {
                    objectHit.GetComponent<Block_TG>().die();
                }              
            }
        }
    } 
    private void right_click(bool is_button_stay = false) { //TG
        RaycastHit hit;
        
        Ray ray = camera.ScreenPointToRay( new Vector3( camera.pixelWidth/2f, camera.pixelHeight / 2f));

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("Stepable_Block") ) {
               // Debug.Log(hit.point - transform.position);
                Vector3 dir = hit.point - transform.position;
                if ((dir.sqrMagnitude <= interaction_range * interaction_range)) {
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
                    Vector3 set_dir = Vector3.up;
                    if (dir.x >= 0.49f)
                    {
                        set_dir = Vector3.right;
                    }
                    else if (dir.x <= -0.49f)
                    {
                        set_dir = Vector3.left;
                    }
                    else if (dir.y >= 0.49f)
                    {
                        set_dir = Vector3.up;
                    }
                    else if (dir.y <= -0.49f)
                    {
                        set_dir = -Vector3.down;
                    }
                    else if (dir.z >= 0.49f)
                    {
                        set_dir = Vector3.forward;
                    }
                    else if (dir.z <= -0.49f)
                    {
                        set_dir = Vector3.back;
                    }
                    set_dir = hit.collider.transform.position + set_dir;
                    if (Physics.OverlapBox(set_dir, Vector3.one / 2.1f).Length == 0)
                    {
                        //List<Vector3Int> space_ = new List<Vector3Int>();
                        //space_.Add(Vector3Int.up);
                        
                        Biom_Manager.instance.set_block(id_, set_dir, Quaternion.identity, block_.space);
                    }
                                      
                    
                    //objectHit.GetComponent<Block_TG>().die();
                }              
            }
        }
    }
    private void PositionInput()
    {
        //�ӵ� ����
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            currentspeed = runspeed;
            //Debug.Log("�ٱ� ����");
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            currentspeed = walkspeed;
           // Debug.Log("�ٱ� ����");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentspeed = crouchspeed;
            //Debug.Log("��ũ���� ����");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentspeed = walkspeed;
            //Debug.Log("��ũ���� ����");
        }

        //x,z�� ������
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
    private void RotationInput()
    {
        transform.Rotate(Vector3.up, mouseX * r_speed);
    }
    private void CamSet()
    {
        // ���� ī�޶��� ȸ�� ���� ������
        Vector3 camRotation = cam.transform.localEulerAngles;

        // ȸ������ ���ϰ� ����

        camRotation.x -= mouseY * r_speed;

        // ī�޶��� ȸ������ ����
        camRotation.x = Mathf.Clamp(camRotation.x, -50f, 360f);
        /*camRotation.x = Mathf.Clamp(camRotation.x, -90f, 90f);
        camRotation.x = (camRotation.x + 360f) % 360f;*/ // -90������ 90�������� ����
        //Debug.Log(camRotation.x);

        // ���� ī�޶� ȸ�� ����
        cam.localEulerAngles = camRotation;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Stepable_Block"))
        {
            isjump = false;
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
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test_TG : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jump_force = 100f;
    [SerializeField] private Rigidbody rigid_body;
    // Start is called before the first frame update
    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
        deactivate_gravity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            float tilt = 0f;
            if (Input.GetKey(KeyCode.E))
            {
                tilt = 1f;
            } else if (Input.GetKey(KeyCode.Q)) {
                tilt = -1f;
            }
            transform.Translate((hor* transform.right + ver*transform.forward + tilt*transform.up) *Time.deltaTime* speed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("jump");
                rigid_body.AddForce(jump_force * Vector3.up);
            }
        }
    }

    public void deactivate_gravity()
    {
        rigid_body.useGravity = false;
    }
    public void activate_gravity() {
        rigid_body.useGravity = true;
    }
    
}
*/