using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggy : Monster_controll
{
    Ray ray = new Ray();
    Ray ray1 = new Ray();
    Ray ray3 = new Ray();
    Ray ray4 = new Ray();
    [SerializeField] private float piggyJumppower;
    Piggy piggy;
    Animator animation;
    [Header("몬스터 드롭 아이템")]
    [SerializeField] private Item_ID_TG id;
    protected bool move = true;
    private int PigHp;
    private int ItemCount = 1;
    private int JumpCount = 1;
    float Maxtimer = 0;
    [Header("몬스터 레이 포인트")]
    [SerializeField] private GameObject Ray1;
    [SerializeField] private GameObject Ray2;
    [SerializeField] private GameObject Ray3;
    [SerializeField] private GameObject Ray4;
    [SerializeField] private GameObject FloorRay;
    [Header("몬스터 사운드")]
    [SerializeField] private AudioClip[] PigHit;
    [SerializeField] private AudioClip PigDead;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        move = true;
        animation = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        render = GetComponentInChildren<Renderer>();
        monstercolor = render.material.color;
    }
    private void Start()
    {
        PigHp = starthealth;
        ItemCount = 1;
        StartCoroutine(MonsterStand());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //StartCoroutine(MonsterRunout());
            MonsterHurt(20);
        }

    }
    
    private void PigRay()    //정면 레이
    {
        ray.origin = new Vector3(Ray1.transform.position.x, Ray1.transform.position.y, Ray1.transform.position.z);

        ray1.origin = new Vector3(Ray2.transform.position.x, Ray2.transform.position.y, Ray2.transform.position.z);

        ray3.origin = new Vector3(Ray3.transform.position.x, Ray3.transform.position.y, Ray3.transform.position.z);
        
        ray4.origin = new Vector3(Ray4.transform.position.x, Ray4.transform.position.y, Ray4.transform.position.z);

        ray.direction = Ray1.transform.forward;
        ray1.direction = Ray2.transform.forward;
        ray3.direction = Ray3.transform.forward;
        ray4.direction = Ray4.transform.forward;

        Debug.DrawRay(ray.origin, ray.direction*0.8f, Color.red);
        Debug.DrawRay(ray1.origin, ray1.direction*0.8f, Color.blue);
        Debug.DrawRay(ray3.origin, ray3.direction*1.5f, Color.blue);
        Debug.DrawRay(ray4.origin, ray4.direction*1.5f, Color.blue);
        RaycastHit[] hit;

        hit = Physics.RaycastAll(ray3, 1.5f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block"))
            {
                return;
            }
        }
        hit = Physics.RaycastAll(ray4, 1.5f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block"))
            {
                return;
            }
        }

        hit = Physics.RaycastAll(ray, 0.8f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block") && JumpCount == 1)
            {
                JumpCount--;
                Monster_Speed = 0.01f;
                rigi.AddForce(transform.up * piggyJumppower* 0.5f, ForceMode.Impulse);
                Invoke("Pigjump", 0.2f);
                //rigi.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
                return;
            }
        }
        hit = Physics.RaycastAll(ray1, 0.8f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block") && JumpCount == 1)
            {
                JumpCount--;
                Monster_Speed = 0.01f;
                rigi.AddForce(transform.up * piggyJumppower*0.5f, ForceMode.Impulse);
                Invoke("Pigjump", 0.2f);
                //rigi.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
                return;
            }
        }


    }
    private void Pigjump() //돼지의 정교한 점프실력
    {
        rigi.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
    }
    private void PigRay_Bellybutton()   //돼지 아래쪽 레이
    {
        ray.origin = new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z);
        ray.direction = transform.up * -1;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.black);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.1f))
        {
            JumpCount = 1;
            PigRay();
        }

    }

    private void Pig_FrontScan()     //낭떠러지 스캔
    {
        ray.origin = FloorRay.transform.position;
        ray.direction = transform.up * -1;
        Debug.DrawRay(ray.origin, ray.direction * 1.1f, Color.black);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.1f,LayerMask.GetMask("Default")))
        {
            move = true;
            this.Monster_Speed = 1f;
        }
        else
        {
            animation.SetBool("isPigWalk", false);
            this.Monster_Speed = 0f;
        }
    }


    public override void MonsterHurt(int PlayerDamage)    // 몬스터가 맞는거 
    {
        PigHp = PigHp - PlayerDamage;
        if (PigHp > 0)
        {
            PigHitSound();
            StartCoroutine(MonsterRunout());
            StopCoroutine(MonsterStand());
            move = true;
            OnDamage(PlayerDamage);
        }
        else if (PigHp <= 0)
        {
            StopAllCoroutines();
            //StopCoroutine(MonsterRunout());
            if (ItemCount == 1)
            {
                Pig_Dead();
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
                this.transform.position += transform.forward * Monster_Speed * 2f * Time.deltaTime;
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
        while (true)
        {
            if (move)
            {

                Pig_FrontScan();
                PigRay_Bellybutton();
                Maxtimer += Time.deltaTime;
                this.transform.position += transform.forward * Monster_Speed * Time.deltaTime;
                animation.SetBool("isPigWalk", true);
                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.1f || Maxtimer > 3f)
                {
                    Maxtimer = 0f;
                    animation.SetBool("isPigWalk", false);
                    yield return new WaitForSeconds(Random.Range(1f, moveTime));

                    dir.x = Random.Range(-3f, 3f);
                    dir.z = Random.Range(-3f, 3f);
                    pos = dir + this.transform.position;
                    transform.forward = dir.normalized;
                }
            }
                yield return null;
        }
    }


    private void PigHitSound()  //돼지가 맞을때
    {
        audioSource.clip = PigHit[Random.Range(1, 3)];
        audioSource.Play();
    }

    private void Pig_Dead()     //돼지가 죽을때
    {
        audioSource.clip = PigDead;
        audioSource.Play();
    }
}


