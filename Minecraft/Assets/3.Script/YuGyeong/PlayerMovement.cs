using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    private Transform cam;

    [Header("Position")]
    [SerializeField] private float crouchspeed;
    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;
    private float currentspeed;
    private Vector3 velocity;

    [SerializeField] private float jumpforce;
    [SerializeField] private bool isjump;

    private float horizontal;
    private float vertical;
    private Vector3 moveVec;

    [Header("Rotation")]
    private float mouseX;
    private float mouseY;
    [SerializeField]private float r_speed;

    private void Start()
    {
        TryGetComponent(out rigid);
        cam = GameObject.Find("Main Camera").transform;

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
        PositionInput();
        RotationInput();
        velocity = (transform.forward * vertical);
    }
    private void PositionInput()
    {
        //������ �ӵ� ����
        SetSpeed();
        
        //x,z�� ������
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        moveVec = new Vector3(horizontal, 0, vertical) * currentspeed * Time.deltaTime;
        transform.position += moveVec;

        //y�� ������
        if (!isjump && Input.GetButtonDown("Jump"))
        {
            Debug.Log("����");
            isjump = true;
            rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    private void RotationInput()
    {
        //mouseX = Input.GetAxis("Mouse X");
        //mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log(mouseX + "/" +mouseY);
        transform.Rotate(Vector3.up,mouseX * r_speed);
    }
    private void CamSet()
    {
        cam.Rotate(Vector3.right * -mouseY);
    }
    private void SetSpeed()
    {
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

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "BG")
        {
            isjump = false;
        }
    }
}
