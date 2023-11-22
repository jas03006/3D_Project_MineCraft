using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster_controll
{

    Ray ray = new Ray();
    Ray ray1 = new Ray();
    Ray ray3 = new Ray();
    Ray ray4 = new Ray();
    Zombie piggy;
    Animator animation;
    Collider sensor;
    [SerializeField] Renderer[] renders;
    Color zomcolor = new Color(1f, 0.2f, 0.2f, 1f);
    List<Color>[] monsterco;
    //-------------------------좀비 컨트롤러
    [SerializeField] private float ZomJumppow;
   // protected bool move = true;
    private int ZomHp;
    private int ItemCount = 1;
    private int JumpCount = 1;
    private float zombietimer = 0f;
    protected bool Zomattack = true;
    [Header("몬스터 인식거리")]
    [SerializeField] private float Scandistance;
    [Header("몬스터 드롭 아이템")]
    [SerializeField] private Item_ID_TG id;
    [Header("몬스터 레이 포인트")]
    [SerializeField] private GameObject Ray1;
    [SerializeField] private GameObject Ray2;
    [SerializeField] private GameObject Ray3;
    [SerializeField] private GameObject Ray4;
    [SerializeField] private GameObject FloorRay;
    [Header("몬스터 사운드")]
    [SerializeField] private AudioClip[] ZomHit;
    [SerializeField] private AudioClip ZomDead;
    private AudioSource audioSource;
    private float Monster_Speedcontroll = 1;
    private Coroutine follow_co = null;
    private Coroutine stand_co = null;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigi = GetComponent<Rigidbody>();
        renders = GetComponentsInChildren<Renderer>();
        animation = GetComponent<Animator>();
        sensor = GetComponent<Collider>();
        Rengering();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        ItemCount = 1;
        ZomHp = starthealth;        
        stand_co = StartCoroutine(MonsterStand());
    }
    private void Update()
    {
        if (follow_co == null) {
            MonsterScanner();
        }
    }
    private void MonsterScanner()          //좀비 Player인식
    {
        pos = player.transform.position;
        float sqr_distance = (pos - transform.position).sqrMagnitude;

        if (sqr_distance <= Scandistance)
        {
            if (stand_co != null)
            {
                StopCoroutine(stand_co);
                stand_co = null;
            }
            if (follow_co != null)
            {
                StopCoroutine(follow_co);
            }
            follow_co = StartCoroutine(MonsterFollow());
        }
    }
    private IEnumerator Stop1sec()
    {
        yield return new WaitForSeconds(1f);
        yield break;
    }
    public override void MonsterHurt(int PlayerDamage)   //좀비가 맞을때
    {
        ZomHp -= PlayerDamage;
        // move = false;
        if (ZomHp > 0)
        {
            ZomHitSound();
            StartCoroutine(MonsterFracture());
            OnDamage(PlayerDamage);
        }
        else if (ZomHp <=0 && ItemCount ==1)
        {
            Zom_Dead();
            StopAllCoroutines();
            StartCoroutine(MonsterFracture());
            animation.SetTrigger("ZombieDead");
            Block_Objectpooling.instance.Get(id, transform.position);
            ItemCount--;
            MonsterDead();
            Invoke("MonsterHide", 2f);
        }
        
    }

    private void Rengering()       //배열안에 배열
    {
        monsterco = new List<Color>[renders.Length];
        for (int i = 0; i < renders.Length; i++)
        {
            monsterco[i] = new List<Color>();
            for (int j = 0; j < renders[i].materials.Length; j++)
            {               
                monsterco[i].Add(renders[i].materials[j].color);
            }
        }
    }

    protected override void MonsterMove()
    {

    }
    protected override IEnumerator MonsterFracture()   //좀비 맞았을때 넉백
    {
        Monster_Speedcontroll = 0;
        Vector3 dir = this.transform.position - player.transform.position;
        rigi.AddForce(transform.up * 125f);
        dir.y = 0;
        rigi.AddForce(dir * 100f);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < renders.Length; i++)
        {
            for (int j = 0; j < renders[i].materials.Length; j++)
            {
                renders[i].materials[j].color = zomcolor;
            }
        }

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i <renders.Length; i++)
        {
            for (int j = 0; j < renders[i].materials.Length; j++)
            {
                renders[i].materials[j].color = monsterco[i][j];
            }
        }
        yield return new WaitForSeconds(0.3f);
        // move = true;
        Monster_Speedcontroll = 1;
        yield break;

    }
    protected override IEnumerator MonsterStand()  //좀비가 가만히 있을때
    {
        var moveTime = CurveWeighedRandom(ani);
        Vector3 dir = new Vector3();
        dir.x = Random.Range(-10f, 10f);
        dir.y = 0;
        dir.z = Random.Range(-10f, 10f);
        pos = dir + this.transform.position;
        transform.forward = dir.normalized;
        while (true)
        {
            /*if (move)
            {*/

            Zom_FrontScan();
            ZomRay();
            zombietimer += Time.deltaTime;
            this.transform.position += transform.forward * Monster_Speed * Time.deltaTime;
            animation.SetBool("ZombieWalk", true);
            float distance = Vector3.Distance(transform.position, pos);

            if (distance <= 0.1f || zombietimer > 3f)
            {
                zombietimer = 0f;
                animation.SetBool("ZombieWalk", false);
                yield return new WaitForSeconds(Random.Range(1f, moveTime));

                dir.x = Random.Range(-3f, 3f);
                dir.z = Random.Range(-3f, 3f);
                pos = dir  + this.transform.position;
                transform.forward = dir.normalized;
            }
            //}
                yield return null;
        }
    }

    protected IEnumerator MonsterFollow()    //좀비 어그로s
    {
        //        pos = this.transform.position;
        while (true)
        {
            
            Zom_FrontScan();
            ZomRay();
            Vector3 dir = player.transform.position - this.transform.position;
            dir.y = 0;

            this.transform.forward = dir.normalized;
            this.transform.position += transform.forward * Monster_Speed* Monster_Speedcontroll * Time.deltaTime;
            
            pos = player.transform.position;
            float distance = Vector3.Distance(transform.position, pos);

            if (distance <= 0.7f)
            {
                Zomattack = false;
                this.Monster_Speed = 0f;
                animation.SetBool("ZombieWalk", false);
            }
            else
            {
                animation.SetBool("ZombieWalk", true);
                Zomattack = true;
                this.Monster_Speed = 1f;
            }

             yield return null;
        }
        follow_co = null;
    }
    private void ZomRay()    //정면 레이
    {
        ray.origin = new Vector3(Ray1.transform.position.x, Ray1.transform.position.y, Ray1.transform.position.z);

        ray1.origin = new Vector3(Ray2.transform.position.x, Ray2.transform.position.y, Ray2.transform.position.z);

        ray3.origin = new Vector3(Ray3.transform.position.x, Ray3.transform.position.y, Ray3.transform.position.z);

        ray4.origin = new Vector3(Ray4.transform.position.x, Ray4.transform.position.y, Ray4.transform.position.z);

        ray.direction = Ray1.transform.forward;
        ray1.direction = Ray2.transform.forward;
        ray3.direction = Ray3.transform.forward;
        ray4.direction = Ray4.transform.forward;

        Debug.DrawRay(ray.origin, ray.direction*1f, Color.red);
        Debug.DrawRay(ray1.origin, ray1.direction * 1f, Color.blue);
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

        hit = Physics.RaycastAll(ray, 0.5f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block"))
            {
                rigi.velocity = Vector3.up* ZomJumppow;
                return;
            }
        }
        hit = Physics.RaycastAll(ray1, 0.8f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block"))
            {
                rigi.velocity = Vector3.up*ZomJumppow;
                return;
            }
        }
    }
    private void Zom_FrontScan()     //낭떠러지 스캔
    {
        ray.origin = FloorRay.transform.position;
        ray.direction = transform.up * -1;
        Debug.DrawRay(ray.origin, ray.direction * 1.3f, Color.black);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.3f, LayerMask.GetMask("Default"))&& Zomattack == true)
        {
           // move = true;
            this.Monster_Speed = 1f;
        }
        else
        {
            animation.SetBool("ZombieWalk", false);
            this.Monster_Speed = 0f;
        }
    }

    private void ZomHitSound()  //좀비가 맞을때
    {
        audioSource.clip = ZomHit[Random.Range(0, 2)];
        audioSource.Play();
    }

    private void Zom_Dead()     //좀비가 죽을때
    {
        audioSource.clip = ZomDead;
        audioSource.Play();
    }

}