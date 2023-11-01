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
        //����ĳ��Ʈ

    //�߰��� �ؾ��� ��
        //�ִϸ��̼� : ��ũ����,�ٱ�,�ȱ�
        //ī�޶� ��� : �۶� ī�޶� ��¦ �־�����, ��ũ���� ī�޶� ��¦ ���������
     */
    #endregion
    [Header("�ӵ�")]
    [SerializeField] private float crouchspeed;
    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;
    private float currentspeed;

    private float horizontal;
    private float vertical;
    private float jumpforce;
    private Vector3 moveVec;

    private Rigidbody rigid;

    private void Awake()
    {
        TryGetComponent(out rigid);

        //speed����
        crouchspeed = 1f;
        walkspeed = 5f;
        runspeed = 10f;
        currentspeed = walkspeed;
    }
    void Update()
    {
        if (DrawRay())
        {
            PlayerInput();
        }
    }
    private void PlayerInput()
    {
        SetSpeed();
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveVec = new Vector3(horizontal, 0, vertical) * currentspeed * Time.deltaTime;
        transform.position += moveVec;
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
    private bool DrawRay()
    {
        RaycastHit ray = new RaycastHit();
        //Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask
        if (Physics.Raycast(transform.position + Vector3.down, Vector3.down,out ray, 1f))
        { Debug.Log("ray ����"); return true;  }
        else
        { Debug.Log("ray ����"); return false; }
    }
    private void OnDrawGizmos()
    {
        //Debug.DrawRay(transform.position + Vector3.down,Vector3.down,Color.red,1f);
    }
}
