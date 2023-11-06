using UnityEngine;

public class PlayerMovement_Y : MonoBehaviour
{
    #region 스케치
    /*
     <캐릭터 움직임 구현>
    //변수 - 포지션관련
        //스피드(웅크리기) 
        //스피드(걷기) - 기본
        //스피드(뛰기)
        //현재스피드
        
        //jumpforce
        //isjump
        //horizontal
        //vertical
        //rigidbody
    //변수 - 로테이션 관련
        //mouseX
        //mouseY
        //속도

    //메서드
        //Start
            // 컴포넌트 불러오기
        //Update
            //포지션
                //x,z축 이동 : 입력, 움직임구현
                //y축 이동
            //로테이션
                //캐릭터
                //카메라
        
    //추가로 해야할 것
        //애니메이션 : 웅크리기,뛰기,걷기 연동, 왼손 애니메이션 만들기
        //카메라 : 3인칭 카메라 만들기
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
        //x,z축
        //입력
        PositionInput();

        ////움직임 구현
        Vector3 moveDirection = cam.transform.forward * vertical + cam.transform.right * horizontal;
        moveDirection.y = 0;
        moveVec = moveDirection * currentspeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        //y축
        //점프
        if (!isjump && Input.GetButtonDown("Jump"))
        {
            isjump = true;
            rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }

        //Rotation
        //입력
        RotationInput();

        if (Input.GetKeyDown(KeyCode.F5))
        {
            CamChange();
        }
    }
    private void PositionInput()
    {
        //속도 세팅
        if (Input.GetKeyDown(KeyCode.LeftControl) && canrun)
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

        //x,z축 움직임
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
        if (cam1)
        {
            
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Stepable_Block"))
        {
            isjump = false;
        }
    }
}