using UnityEngine;
using System;

public enum Cam_State
{ 
    cam1 = 0,
    cam2 =1,
    cam3 =2
}

public class PlayerMovement_Y : MonoBehaviour
{
    #region ����ġ
    /*
     <ĳ���� ������ ����>
    //���� - �����ǰ���
        //���ǵ�(��ũ����) 
        //���ǵ�(�ȱ�) - �⺻
        //���ǵ�(�ٱ�)
        //���罺�ǵ�
        
        //jumpforce
        //isjump
        //horizontal
        //vertical
        //rigidbody
    //���� - �����̼� ����
        //mouseX
        //mouseY
        //�ӵ�

    //�޼���
        //Start
            // ������Ʈ �ҷ�����
        //Update
            //������
                //x,z�� �̵� : �Է�, �����ӱ���
                //y�� �̵�
            //�����̼�
                //ĳ����
                //ī�޶�
        
    //�߰��� �ؾ��� ��
        //�ִϸ��̼� : ��ũ����,�ٱ�,�ȱ� ����, �޼� �ִϸ��̼� �����
        //ī�޶� : 3��Ī ī�޶� �����
     */
    #endregion

    protected Rigidbody rigid;

    [Header("Position")]
    [SerializeField] protected float crouchspeed;
    [SerializeField] protected float walkspeed;
    [SerializeField] protected float runspeed;
    protected float currentspeed;
    public bool canrun;

    [SerializeField] protected float jumpforce;
    [SerializeField] protected bool isjump;

    protected float horizontal;
    protected float vertical;
    protected Vector3 moveVec;

    [Header("Rotation")]
    protected float mouseX;
    protected float mouseY;
    [SerializeField] protected float r_speed;
    float cameraVerticalRotation = 0f;
    protected float tmp;

    [Header("Cam")]
    [SerializeField] protected Transform cam;
    [SerializeField] protected Transform[] cam_pos_arr;
    protected Cam_State cam_state = Cam_State.cam1;

    //animator
    [SerializeField] protected Animator head_animator;
    [SerializeField] protected Animator arm_animator;
    [SerializeField] protected Animator body_animator;

    public int camera_mask_basic=0;
    protected virtual void Start()
    {
        TryGetComponent(out rigid);
        cam = FindObjectOfType<Camera>().transform;
        cam.transform.position = cam_pos_arr[(int)cam_state].position;
        cam.transform.rotation = cam_pos_arr[(int)cam_state].rotation;


        isjump = false;
        if (jumpforce == 0) {
            jumpforce = 11f;
        }
        if (crouchspeed == 0) { 
            crouchspeed = 1f;
        }
        if (walkspeed == 0)
        {
            walkspeed = 3f;
        }
        if (runspeed == 0)
        {
            runspeed = 4.5f;
        }
        if (r_speed == 0)
        {
            r_speed = 1f;
        }
        currentspeed = walkspeed;
    }
    protected virtual void FixedUpdate()
    {
        //Transform
        //x,z��
        //�Է�
        PositionInput();


        ////������ ����

        Vector3 moveDirection = cam.transform.forward * vertical + cam.transform.right * horizontal;
        moveDirection.y = 0;
        if (Vector3.zero != moveDirection) {
            transform.forward = moveDirection.normalized;
            cam.transform.localEulerAngles = Vector3.zero;
        }
        moveVec = moveDirection * currentspeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        //y��
        //����
        if (!isjump && Input.GetButtonDown("Jump"))
        {
            isjump = true;
            rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }

        //Rotation
        //�Է�
        RotationInput();

        if (Input.GetKeyDown(KeyCode.F5))
        {
            CamChange();
        }

    }
    protected void PositionInput()
    {
        //x,z�� ������
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (horizontal != 0 || vertical !=0)
        {
            
            if (!body_animator.GetBool("is_walk"))
            {
                body_animator.SetBool("is_crouch", false);
                arm_animator.SetBool("is_crouch", false);
                head_animator.SetBool("is_crouch", false);
                body_animator.SetBool("is_walk", true);
                if (!arm_animator.GetCurrentAnimatorStateInfo(0).IsName("righthand_active_Y")) {
                    arm_animator.SetBool("is_walk", true);
                }                   
            }else if (!arm_animator.GetBool("is_walk") && !arm_animator.GetBool("is_crouch"))
            {
                if (!arm_animator.GetCurrentAnimatorStateInfo(0).IsName("righthand_active_Y") && !arm_animator.GetNextAnimatorStateInfo(0).IsName("righthand_active_Y"))
                {
                    body_animator.SetBool("is_walk", false);
                }               
            }
        }
        else
        {
            body_animator.SetBool("is_walk", false);
            arm_animator.SetBool("is_walk", false);
        }

        //�ӵ� ����
        if (Input.GetKeyDown(KeyCode.LeftControl) && canrun)
        {
            currentspeed = runspeed;            
            //Debug.Log("�ٱ� ����");
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            currentspeed = walkspeed;
            //Debug.Log("�ٱ� ����");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentspeed = crouchspeed;
            if (!body_animator.GetBool("is_crouch")) {
                body_animator.SetBool("is_walk", false);
                arm_animator.SetBool("is_walk", false);
                body_animator.SetBool("is_crouch", true);
                arm_animator.SetBool("is_crouch", true);
                head_animator.SetBool("is_crouch", true);
            }
            
            //Debug.Log("��ũ���� ����");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentspeed = walkspeed;
            body_animator.SetBool("is_crouch", false);
            arm_animator.SetBool("is_crouch", false);
            head_animator.SetBool("is_crouch", false);
            //Debug.Log("��ũ���� ����");
        }

        

    }

    protected void RotationInput()
    {
        //transform.rotation = Quaternion.Euler(0, cam.rotation.y * 100, 0);
        //cameraVer -= mouseY;
        //cameraHor += mouseX;
        //cameraVer = Mathf.Clamp(cameraVer, -90f, 90f);
        //transform.localEulerAngles = new Vector3(cameraVer,0,0);

        //transform.Rotate(Vector3.up, mouseX * r_speed *Time.deltaTime);
    }

    protected void CamChange()
    {
        if (camera_mask_basic == 0) {
            camera_mask_basic = cam.GetComponent<Camera>().cullingMask | LayerMask.GetMask("Player_Right_Arm");
        }
        if (cam_state == Cam_State.cam1)
        {
            cam.GetComponent<Camera>().cullingMask = camera_mask_basic;
        }
        cam_state = (Cam_State)(((int)cam_state + 1)%Enum.GetValues(typeof(Cam_State)).Length);
        cam.transform.position = cam_pos_arr[(int)cam_state].position;
        cam.transform.rotation = cam_pos_arr[(int)cam_state].rotation;
        if (cam_state == Cam_State.cam1)
        {
            cam.GetComponent<Camera>().cullingMask = camera_mask_basic & ((-1>>32) ^ LayerMask.GetMask("Player_J"));
        }
        /*tmp -= mouseY * r_speed;
        tmp = Mathf.Clamp(tmp, -90, 90);
        cam.rotation = Quaternion.Euler(tmp, 0, 0);*/

    }
    protected virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Stepable_Block"))
        {
            isjump = false;
        }
    }
}