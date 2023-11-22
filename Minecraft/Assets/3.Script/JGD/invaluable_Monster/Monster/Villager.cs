using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Monster_controll
{
    Ray ray = new Ray();
    Ray ray1 = new Ray();
    Ray ray3 = new Ray();
    Ray ray4 = new Ray();
    [SerializeField] private float piggyJumppower;
    Villager villager;
    Animator animation;
    protected bool move = true;
    private int VillagerHp;
    private int ItemCount = 1;
    private int JumpCount = 1;
    float Maxtimer = 0;
    [Header("마을사람 레이 포인트")]
    [SerializeField] private GameObject Ray1;
    [SerializeField] private GameObject Ray2;
    [SerializeField] private GameObject Ray3;
    [SerializeField] private GameObject Ray4;
    [SerializeField] private GameObject FloorRay;
    [Header("마을사람 사운드")]
    [SerializeField] private AudioClip[] VillgerCall;
    [SerializeField] private AudioClip[] VillgerHit;
    [SerializeField] private AudioClip VillgerDead;
    private AudioSource audioSource;
    [Header("마을사람 맞을때")]
    [SerializeField] private GameObject Eyes;

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

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemCount = 1;
        VillagerHp = starthealth;
        StartCoroutine(MonsterStand());
        Eyes.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            MonsterHurt(20);
        }
    }
    protected override IEnumerator MonsterStand()
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

                Villager_FrontScan();
                VillagerRay();
                Maxtimer += Time.deltaTime;
                this.transform.position += transform.forward * Monster_Speed * Time.deltaTime;
                //animation.SetBool("isPigWalk", true);
                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.1f || Maxtimer > 3f)
                {
                    Maxtimer = 0f;
                    //animation.SetBool("isPigWalk", false);
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

    protected override IEnumerator MonsterFracture()
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

    protected override void MonsterMove()
    {
        throw new System.NotImplementedException();
    }

    public override void MonsterHurt(int PlayerDamage)
    {
        VillagerHp = VillagerHp - PlayerDamage;
        Eyes.SetActive(true);
        if (VillagerHp > 0)
        {
            VillagerHitSound();
            StartCoroutine(MonsterRunout());
            StopCoroutine(MonsterStand());
            move = true;
            OnDamage(PlayerDamage);
            Invoke("Villagerfrighten",2f);
        }
        else if (VillagerHp <= 0)
        {
            StopAllCoroutines();
            if (ItemCount == 1)
            {
                Villager_Dead();
                //animation.SetTrigger("isDead");
                ItemCount--;
            }
            MonsterDead();
            Invoke("MonsterHide", 2f);
        }
    }

    private void VillagerRay()    //정면 레이
    {
        ray.origin = new Vector3(Ray1.transform.position.x, Ray1.transform.position.y, Ray1.transform.position.z);

        ray1.origin = new Vector3(Ray2.transform.position.x, Ray2.transform.position.y, Ray2.transform.position.z);

        ray3.origin = new Vector3(Ray3.transform.position.x, Ray3.transform.position.y, Ray3.transform.position.z);

        ray4.origin = new Vector3(Ray4.transform.position.x, Ray4.transform.position.y, Ray4.transform.position.z);

        ray.direction = Ray1.transform.forward;
        ray1.direction = Ray2.transform.forward;
        ray3.direction = Ray3.transform.forward;
        ray4.direction = Ray4.transform.forward;

        Debug.DrawRay(ray.origin, ray.direction * 0.45f, Color.red);
        Debug.DrawRay(ray1.origin, ray1.direction * 0.45f, Color.blue);
        Debug.DrawRay(ray3.origin, ray3.direction * 1f, Color.blue);
        Debug.DrawRay(ray4.origin, ray4.direction * 1f, Color.blue);
        RaycastHit[] hit;

        hit = Physics.RaycastAll(ray3, 1f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block"))
            {
                return;
            }
        }
        hit = Physics.RaycastAll(ray4, 1f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block"))
            {
                return;
            }
        }

        hit = Physics.RaycastAll(ray, 0.45f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block"))
            {
                rigi.velocity = Vector3.up * piggyJumppower;
                return;
            }
        }
        hit = Physics.RaycastAll(ray1, 0.45f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.CompareTag("Stepable_Block"))
            {
                rigi.velocity = Vector3.up * piggyJumppower;
                return;
            }
        }
    }
    private void Villager_FrontScan()     //낭떠러지 스캔
    {
        ray.origin = FloorRay.transform.position;
        ray.direction = transform.up * -1;
        Debug.DrawRay(ray.origin, ray.direction * 1.1f, Color.black);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.1f, LayerMask.GetMask("Default")))
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
    private void Look_otherside()     // 플레이어 방대방향 보기
    {
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
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
                //animation.SetBool("isPigWalk", true);
                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.1f || Maxtimer > 3f)
                {
                    Maxtimer = 0f;
                    //animation.SetBool("isPigWalk", false);
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
    private void Villagerfrighten()
    {
        Eyes.SetActive(false);
    }
    private void VillagerHitSound()  //마을사람 맞을때
    {
        audioSource.clip = VillgerHit[Random.Range(0,2)]; //이거 픽스
        audioSource.Play();
    }

    private void Villager_Dead()     //마을사람 죽을때
    {
        audioSource.clip = VillgerDead;
        audioSource.Play();
    }
}