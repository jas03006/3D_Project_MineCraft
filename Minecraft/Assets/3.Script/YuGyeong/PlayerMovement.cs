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
        //레이캐스트

    //추가로 해야할 것
        //애니메이션 : 웅크리기,뛰기,걷기
        //카메라 모션 : 뛸때 카메라 살짝 멀어지기, 웅크릴때 카메라 살짝 가까워지기
     */
    #endregion
    [Header("속도")]
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

        //speed설정
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
    private bool DrawRay()
    {
        RaycastHit ray = new RaycastHit();
        //Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask
        if (Physics.Raycast(transform.position + Vector3.down, Vector3.down,out ray, 1f))
        { Debug.Log("ray 있음"); return true;  }
        else
        { Debug.Log("ray 없음"); return false; }
    }
    private void OnDrawGizmos()
    {
        //Debug.DrawRay(transform.position + Vector3.down,Vector3.down,Color.red,1f);
    }
}
