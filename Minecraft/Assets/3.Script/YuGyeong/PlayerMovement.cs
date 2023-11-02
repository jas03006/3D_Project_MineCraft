using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        //움직임 속도 조절
        SetSpeed();
        
        //x,z축 움직임
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        moveVec = new Vector3(horizontal, 0, vertical) * currentspeed * Time.deltaTime;
        transform.position += moveVec;

        //y축 움직임
        if (!isjump && Input.GetButtonDown("Jump"))
        {
            Debug.Log("점프");
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
            Debug.Log("뛰기 시작");
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            currentspeed = walkspeed;
            Debug.Log("뛰기 종료");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentspeed = crouchspeed;
            Debug.Log("웅크리기 시작");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentspeed = walkspeed;
            Debug.Log("웅크리기 종료");
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
