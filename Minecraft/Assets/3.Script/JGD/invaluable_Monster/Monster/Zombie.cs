using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster_controll
{

    [SerializeField] Renderer[] renders;
    Color zomcolor = new Color(1f, 0.2f, 0.2f, 1f);
    List<Color>[] monsterco;
    protected bool move = true;
    private int ZomHp;
    Animator animation;
    Collider sensor;
    [Header("몬스터 드롭 아이템")]
    [SerializeField] private Item_ID_TG id;
    [Header("몬스터 레이 포인트")]
    [SerializeField] private GameObject Ray1;
    [SerializeField] private GameObject Ray2;
    [SerializeField] private GameObject Ray3;
    [SerializeField] private GameObject Ray4;
    private void Awake()
    {
        ZomHp = curhealth;
        player = GameObject.FindGameObjectWithTag("Player");
        rigi = GetComponent<Rigidbody>();
        renders = GetComponentsInChildren<Renderer>();
        animation = GetComponent<Animator>();
        sensor = GetComponent<Collider>();
    }
    private void Start()
    {
        Rengering();
        StartCoroutine(MonsterStand());
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(MonsterFollow());
            StopCoroutine(MonsterStand());
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            MonsterHurt(55);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(MonsterStand());
            StartCoroutine(MonsterFollow());
        }
    }
    public override void MonsterHurt(int PlayerDamage)
    {
        ZomHp -= PlayerDamage;
        move = false;
        StartCoroutine(MonsterFracture());
    }

    private void Rengering()               //배열안에 배열
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
    protected override IEnumerator MonsterFracture()
    {
        Vector3 dir = this.transform.position - player.transform.position;

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

        rigi.AddForce(transform.up * 150f);
        rigi.AddForce(dir * 15f);

        move = true;
        yield break;

    }

    protected override IEnumerator MonsterStand()  //좀비가 가만히 있을때
    {
        var moveTime = CurveWeighedRandom(ani);
        var dir = new Vector3();
        dir.x = Random.Range(-10f, 10f);
        dir.y = 0;
        dir.z = Random.Range(-10f, 10f);
        pos = dir + this.transform.position;
        transform.forward = dir.normalized;
        if (move)
        {
            while (true)
            {

                float zombietimer = 0f;
                zombietimer += Time.deltaTime;
                this.transform.position += transform.forward * Monster_Speed * Time.deltaTime;
                animation.SetBool("ZombieWalk", true);
                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.1f || zombietimer > 2f)
                {
                    zombietimer = 0f;
                    animation.SetBool("ZombieWalk", false);
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

    private IEnumerator MonsterFollow()
    {
        move = false;
        if (!move)
        {
            while (true)
            {
                animation.SetBool("ZombieWalk", true);
                Vector3 dir = player.transform.position - this.transform.position;
                dir.y = 0;
                this.transform.position += transform.forward * Monster_Speed * Time.deltaTime;
                transform.forward = dir.normalized;
                pos = player.transform.position;
                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.7f)
                {
                    this.Monster_Speed = 0f;
                }
                else
                {
                    this.Monster_Speed = 1f;
                }
                yield return null;
            }
        }
    }    
    protected override void MonsterDead()
    {
        Destroy(gameObject);
    }
}