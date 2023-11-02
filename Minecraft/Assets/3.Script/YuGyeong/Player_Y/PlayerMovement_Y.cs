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
    [SerializeField]private Transform cam;

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

        //������ ����
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
     
        //ī�޶�
        CamSet();
    }
    private void PositionInput()
    {
        //�ӵ� ����
        if (Input.GetKeyDown(KeyCode.LeftControl))
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
        transform.Rotate(Vector3.up, mouseX * r_speed);
    }
    private void CamSet()
    {
        // ���� ī�޶��� ȸ�� ���� ������
        Vector3 camRotation = cam.transform.localEulerAngles;

        // ȸ������ ���ϰ� ����
        camRotation.x -= mouseY * r_speed;

        // ī�޶��� ȸ������ ����
        camRotation.x = Mathf.Clamp(camRotation.x, 0f, 180f); // -90������ 90�������� ����

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
}