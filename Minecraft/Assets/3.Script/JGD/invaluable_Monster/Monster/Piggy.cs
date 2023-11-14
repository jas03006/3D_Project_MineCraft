using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggy : Monster_controll
{
    Ray ray;
    [SerializeField] private float piggyJumppower;
    Piggy piggy;
    Animator animation;
    [Header("몬스터 드롭 아이템")]
    [SerializeField]private Item_ID_TG id;
    protected bool move = true;
    private int PigHp;
    private int ItemCount = 1;

    private void Awake()
    {
        move = true;
        ray = new Ray();
        PigHp = curhealth;
        animation = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        render = GetComponentInChildren<Renderer>();
        monstercolor = render.material.color;
    }
    private void Start()
    {
        ItemCount = 1;
        StartCoroutine(MonsterStand());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //StartCoroutine(MonsterRunout());
            MonsterHurt(20);
            Invoke("MonsterHurt",3f);
        }
    }

    private void PigRay()    //정면 레이
    {
        ray.origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        ray.origin += transform.forward * 0.6f;
        ray.direction = transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red);
        RaycastHit[] hit;
        hit = Physics.RaycastAll(ray,0.2f);
        for (int i =0; i < hit.Length;i++) {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block")) {
                rigi.AddForce(transform.up * piggyJumppower);  
                break;
            }
        }
            
        
    }
    private void PigRay_Bellybutton()   //돼지 아래쪽 레이
    {
        ray.origin = new Vector3(this.transform.position.x, this.transform.position.y+0.1f, this.transform.position.z);
        ray.direction = transform.up * -1;
        Debug.DrawRay(ray.origin, ray.direction*0.1f, Color.black);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,0.1f))
        {
            PigRay();
        }

    }


    public override void MonsterHurt(int PlayerDamage)    // 몬스터가 맞는거 
    {
        if (PigHp >0)
        {
            PigHp = PigHp - PlayerDamage;
            StartCoroutine(MonsterRunout());
            StopCoroutine(MonsterStand());
            move = true;
            OnDamage(PlayerDamage);
        }
        else if (PigHp <= 0)
        {
            StopAllCoroutines();
            StopCoroutine(MonsterRunout());
            if (ItemCount ==1)
            {
                animation.SetTrigger("isDead");
                Block_Objectpooling.instance.Get(id, transform.position);
                ItemCount--;
            }
            Invoke("MonsterDead", 2f);
        }

    }
    protected override void MonsterDead() //죽었을때
    {
        Destroy(gameObject);
    }



    private void Look_otherside()     // 플레이어 방대방향 보기
    {
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;   
    }
    protected override void MonsterMove()   
    {
    
    }

    public IEnumerator MonsterRunout()    //맞았을 때 잠시 우왕좌왕
    {
        yield return StartCoroutine(MonsterFracture());
        yield return new WaitForSeconds(1f);
        if (!isDead)
        {

            move = false;
            var dir = new Vector3();
            dir.x = Random.Range(-3f, 3f);
            dir.y = 0;
            dir.z = Random.Range(-3f, 3f);
            pos = dir + this.transform.position;
            transform.forward = dir.normalized;
            float Standtimer = 0f;
            float Maxtimer = 0f;
            while (true)
            {
                Maxtimer += Time.deltaTime;
                Standtimer += Time.deltaTime;
                this.transform.position += transform.forward * Monster_Speed*5 * Time.deltaTime;
                animation.SetBool("isPigWalk", true);
                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.1f || Maxtimer > 3f)
                {
                    Maxtimer = 0f;
                    animation.SetBool("isPigWalk", false);
                    yield return new WaitForSeconds(0.1f);

                    dir.x = Random.Range(-3f, 3f);
                    dir.z = Random.Range(-3f, 3f);
                    pos = dir + this.transform.position;
                    transform.forward = dir.normalized;

                }
                yield return null;
                if (Standtimer > 5f)
                {
                    move = true;
                    yield break;
                }
            }
        }

    }

    protected override IEnumerator MonsterFracture()       //맞았을때 피격판전 넉백
    {
        Vector3 dir = transform.position - player.transform.position;
        yield return new WaitForSeconds(0.1f);
        render.material.color = Hitcolor;
        yield return new WaitForSeconds(0.1f);
        render.material.color = monstercolor;
        rigi.AddForce(transform.up * 150f);
        rigi.AddForce(dir * 10f);

        yield break;

    }

    protected override IEnumerator MonsterStand()    //평상시
    {
        var moveTime = CurveWeighedRandom(ani);
        var dir = new Vector3();
        dir.x = Random.Range(-3f, 3f);
        dir.y = 0;
        dir.z = Random.Range(-3f, 3f);
        pos = dir + this.transform.position;
        transform.forward = dir.normalized;
        if (move)
        {
            while (true)
            {

                float Maxtimer = 0f;
                Maxtimer += Time.deltaTime;
                this.transform.position += transform.forward * Monster_Speed * Time.deltaTime;
                animation.SetBool("isPigWalk", true);
                PigRay_Bellybutton();
                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.1f || Maxtimer >3f)
                {
                    Maxtimer = 0f;
                    animation.SetBool("isPigWalk", false);
                    yield return new WaitForSeconds(Random.Range(1f, moveTime));

                    dir.x = Random.Range(-3f, 3f);
                    dir.z = Random.Range(-3f, 3f);
                    pos = dir + this.transform.position;
                    transform.forward = dir.normalized;
                }
                yield return null;
            }
        }
    }

}

