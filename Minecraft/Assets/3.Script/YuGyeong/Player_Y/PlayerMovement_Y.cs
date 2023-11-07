using UnityEngine;

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

    private Rigidbody rigid;

    [Header("Position")]
    [SerializeField] private float crouchspeed;
    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;
    private float currentspeed;
    public bool canrun;

    [SerializeField] private float jumpforce;
    [SerializeField] private bool isjump;

    private float horizontal;
    private float vertical;
    private Vector3 moveVec;

    [Header("Rotation")]
    private float mouseX;
    private float mouseY;
    [SerializeField] private float r_speed;
    float cameraVerticalRotation = 0f;
    private float tmp;

    [Header("Cam")]
    [SerializeField] private Transform cam;
    private Camera cam1;
    private Camera cam3;


    private void Start()
    {
        TryGetComponent(out rigid);
        cam = FindObjectOfType<Camera>().transform;

        isjump = false;
        jumpforce = 15f;
        crouchspeed = 1f;
        walkspeed = 5f;
        runspeed = 10f;
        r_speed = 1f;
        currentspeed = walkspeed;
    }
    void Update()
    {
        //Transform
        //x,z��
        //�Է�
        PositionInput();


        ////������ ����

        Vector3 moveDirection = cam.transform.forward * vertical + cam.transform.right * horizontal;
        moveDirection.y = 0;
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
    private void PositionInput()
    {
        //�ӵ� ����
        if (Input.GetKeyDown(KeyCode.LeftControl) && canrun)
        {
            currentspeed = runspeed;
            Debug.Log("�ٱ� ����");
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            currentspeed = walkspeed;
            Debug.Log("�ٱ� ����");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentspeed = crouchspeed;
            Debug.Log("��ũ���� ����");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentspeed = walkspeed;
            Debug.Log("��ũ���� ����");
        }

        //x,z�� ������
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    private void RotationInput()
    {
        transform.rotation = Quaternion.Euler(0, cam.rotation.y * 100, 0);
        //cameraVer -= mouseY;
        //cameraHor += mouseX;
        //cameraVer = Mathf.Clamp(cameraVer, -90f, 90f);
        //transform.localEulerAngles = new Vector3(cameraVer,0,0);

        // transform.Rotate(Vector3.up, mouseX * r_speed);
    }

    private void CamChange()
    {


        tmp -= mouseY * r_speed;
        tmp = Mathf.Clamp(tmp, -90, 90);
        cam.rotation = Quaternion.Euler(tmp, 0, 0);

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Stepable_Block"))
        {
            isjump = false;
        }
    }
}